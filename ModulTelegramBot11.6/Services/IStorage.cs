using ModulTelegramBot11._6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulTelegramBot11._6.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}
