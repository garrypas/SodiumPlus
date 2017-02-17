using System;
using System.Collections.Generic;

namespace SodiumPlus.Diagnostics
{
    public class ConsoleLogEventSubscriber : IEventSubscriber
    {
        public void Log(string eventType, string context, object data)
        {
            Console.WriteLine(eventType + ": " + context + "=" + data);
        }

        public void Log<T>(string eventType, string context, IEnumerable<T> data)
        {
            Console.WriteLine(eventType + ": " + context + "=" + string.Join(",", data));
        }
    }
}