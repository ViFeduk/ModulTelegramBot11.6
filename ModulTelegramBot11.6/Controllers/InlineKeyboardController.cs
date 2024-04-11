using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using ModulTelegramBot11._6.Services;

namespace ModulTelegramBot11._6.Controllers
{
    internal class InlineKeyboardController
    {
        private ITelegramBotClient _tegramBotClient;
        private readonly IStorage _memoryStorage;
        public InlineKeyboardController (ITelegramBotClient telegramBotClient, IStorage storage)
        {
            _tegramBotClient = telegramBotClient;
            _memoryStorage = storage;
        }

        public async Task HandleText(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;
            _memoryStorage.GetSession(callbackQuery.From.Id).ChooseUser = callbackQuery.Data;

            string chooseUser = callbackQuery.Data switch
            {
                "length" => "рассчитать длину",
                "calc" => "посчитать сумму",
                _ => String.Empty
            };
            await _tegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, $"Вы выбрали {chooseUser}", cancellationToken: ct);

        }
    }
}
