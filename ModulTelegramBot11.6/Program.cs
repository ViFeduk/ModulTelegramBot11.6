using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModulTelegramBot11._6.Configuration;
using ModulTelegramBot11._6.Controllers;
using ModulTelegramBot11._6.Services;
using Telegram.Bot;

namespace ModulTelegramBot11._6
{
    internal class Program
    {
        static async Task Main()
        {
            var host = new HostBuilder().ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем
            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");

            static void ConfigureServices(IServiceCollection services)
            {
                AppSettings appSettings = BuildAppSetting();
                services.AddSingleton(BuildAppSetting());
                services.AddTransient<DefaultMessageController>();
                services.AddTransient<InlineKeyboardController>();
                services.AddTransient<IntMessageController>();
                services.AddTransient<TextMessageController>();
                services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6379370800:AAGKQ4-jyHS76ZSJvIyK2x2zkOIC0ZLCv64"));
                services.AddHostedService<Bot>();
                services.AddSingleton<IStorage, MemoryStorage>();
            }
            static AppSettings BuildAppSetting()
            {
                return new AppSettings()
                {
                    BotToken = "6379370800:AAGKQ4-jyHS76ZSJvIyK2x2zkOIC0ZLCv64"
                };
            }
        }
    }
}
