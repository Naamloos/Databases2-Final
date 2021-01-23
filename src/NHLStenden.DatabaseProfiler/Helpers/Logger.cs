using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.Helpers
{
    /// <summary>
    /// Logger class for logging to console.
    /// Could have used Microsoft's logger abstractions,
    /// but for this appointment it doesn't seem necessary.
    /// </summary>
    public class Logger
    {
        private string application;

        public Logger(string application)
        {
            this.application = application;
        }

        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="important">
        ///     Whether this message is important. 
        ///     If so, will appear in read instead of white.
        /// </param>
        public void LogMessage(string message, bool important = false)
        {
            var splitmessage = message.Split('\n');

            foreach (var part in splitmessage)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(this.application);
                Console.ResetColor();
                Console.Write("][");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(DateTime.Now.ToString("HH:mm"));
                Console.ResetColor();
                Console.Write($"] ");
                if (important)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{part}");
                Console.ResetColor();
            }
        }
    }
}
