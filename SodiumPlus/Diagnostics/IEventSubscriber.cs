using System.Collections.Generic;

namespace SodiumPlus.Diagnostics
{
    public interface IEventSubscriber
    {
        void Log(string eventType, string context, object data);
        void Log<T>(string eventType, string context, IEnumerable<T> data);
    }
}