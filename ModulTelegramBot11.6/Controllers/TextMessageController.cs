using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ModulTelegramBot11._6.Controllers
{
    internal class TextMessageController
    {
        private ITelegramBotClient _tegramBotClient;
        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _tegramBotClient = telegramBotClient;
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
                
            }
        }
    }
}
