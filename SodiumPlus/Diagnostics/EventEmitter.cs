using System.Collections.Generic;

namespace SodiumPlus.Diagnostics
{
    public static class EventEmitter
    {
        private static readonly List<IEventSubscriber> Subscribers = new List<IEventSubscriber>();

        public static void Log(string eventType, string context, object data)
        {
            Subscribers.Enumerate(s => s.Log(eventType, context, data));
        }

        public static void Log<T>(string eventType, string context, IEnumerable<T> data)
        {
            Subscribers.Enumerate(s => s.Log(eventType, context, data));
        }

        public static void Subscribe(IEventSubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public static void Unsubscribe(IEventSubscriber subscriber)
        {
            Subscribers.Remove(subscriber);
        }
    }
}
