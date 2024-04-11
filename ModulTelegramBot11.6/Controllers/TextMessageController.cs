using ModulTelegramBot11._6.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace ModulTelegramBot11._6.Controllers
{
    internal class TextMessageController
    {
        private IStorage _storage;
        private ITelegramBotClient _tegramBotClient;
        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage storage)
        {
            _tegramBotClient = telegramBotClient;
            _storage = storage;
        }
        public async Task HandleText(Message message, CancellationToken cancellationToken)
        {
            switch (message.Text)
            {
                
                case "/start": 
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[] { InlineKeyboardButton.WithCallbackData("Рассчитать длину", "length"),
                        InlineKeyboardButton.WithCallbackData("Сложить числа", "calc")});
                   
                    await _tegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Мой бот либо считает кол-во символов," +
                        $" либо сумму чисел", cancellationToken: cancellationToken, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default: if (_storage.GetSession(message.Chat.Id).ChooseUser == "length")
                    {
                        await _tegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Длина вашего сообщения равна: {message.Text.Length} символов");
                    }
                    else if(_storage.GetSession(message.Chat.Id).ChooseUser == "calc")
                    {
                        try
                        {
                            await _tegramBotClient.SendTextMessageAsync(message.Chat.Id,MessageSum(message.Text));
                        }
                        catch
                        {
                            await _tegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Во время сложения произошла ошибка. " +
                                $"Напишите нормально через пробелы числа");
                        }
                       
                    }
                    break;
                
            }
        }
        static string MessageSum(string message)
        {
            string[] numberStrings = message.Split(' ');

            // Создаем список для хранения чисел
            List<int> numbers = new List<int>();

            // Проходимся по каждой строке с числом и пытаемся преобразовать его в int
            foreach (string numberString in numberStrings)
            {
                if (int.TryParse(numberString, out int number))
                {

                    numbers.Add(number);
                }
                else return "Вы ввели строку а не числа. Сложить невозможно. Введите именно числа!!!";
            }
               return $"Сумма чисел равна: {numbers.Sum()}";
        }
    }
}
