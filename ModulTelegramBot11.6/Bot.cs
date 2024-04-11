using ModulTelegramBot11._6.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using System.Threading;
using Telegram.Bot.Polling;

namespace ModulTelegramBot11._6
{
    internal class Bot: BackgroundService
    {
        private ITelegramBotClient _telegramBot;
        private DefaultMessageController _defaultMessageController;
        private IntMessageController _intMessageController;
        private TextMessageController _textMessageController;
        private InlineKeyboardController _inlineKeyboardController;

        

        public Bot(ITelegramBotClient telegramBotClient, DefaultMessageController defaultMessageController,
            IntMessageController intMessageController, TextMessageController textMessageController, InlineKeyboardController inlineKeyboardController)
        {
            _telegramBot = telegramBotClient;
            _defaultMessageController = defaultMessageController;
            _intMessageController = intMessageController;
            _textMessageController = textMessageController;
            _inlineKeyboardController = inlineKeyboardController;
        }
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.HandleText(update.CallbackQuery, cancellationToken);
                return;
            }
            
            if (update.Message is not { } message)
                return;
            
            switch (message!.Type) 
            {
                case MessageType.Text:
                   await _textMessageController.HandleText(message, cancellationToken);
                    break;
                
                default: _defaultMessageController.HandleText(message, cancellationToken);
                    break;
            }


            
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramBot.StartReceiving(
            HandleUpdateAsync,
            HandlePollingErrorAsync,
            new ReceiverOptions() { AllowedUpdates = { } },
            cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }
    }
}
