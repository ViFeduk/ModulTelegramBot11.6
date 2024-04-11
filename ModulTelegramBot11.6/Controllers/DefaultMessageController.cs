using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace ModulTelegramBot11._6.Controllers
{
    internal class DefaultMessageController
    {
        private ITelegramBotClient _tegramBotClient;
        public DefaultMessageController (ITelegramBotClient telegramBotClient)
        {
            _tegramBotClient = telegramBotClient;
        }
        public async Task HandleText(Message message, CancellationToken cancellationToken)
        {
            await _tegramBotClient.SendTextMessageAsync(message.Chat.Id, "Ты направил мне что-то непонятное! Мне нужен либо текст либо цифры а не остальное!!!", cancellationToken: cancellationToken);
        }
    }
}
