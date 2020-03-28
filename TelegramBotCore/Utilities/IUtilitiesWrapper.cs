using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotCore.Utilities
{
    public interface IUtilitiesWrapper
    {
        KeyboardFactory KeyboardFactory { get; }
        Utility Utility { get; }
    }
}
