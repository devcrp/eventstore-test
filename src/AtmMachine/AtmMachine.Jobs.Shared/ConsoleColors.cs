using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Jobs.Shared
{
    public static class ConsoleColors
    {
        public static void SetInitialColors(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.Clear();
        }
    }
}
