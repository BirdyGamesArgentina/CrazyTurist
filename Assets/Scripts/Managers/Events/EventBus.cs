using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.Shared.Events
{
    public class GameEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> events = new();

        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            var eventType = typeof(TEvent);
            if (!events.ContainsKey(eventType))
                events[eventType] = new List<Delegate>();

            if (!events[eventType].Contains(action))
                events[eventType].Add(action);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> action)
        {
            var eventType = typeof(TEvent);
            if (!events.ContainsKey(eventType)) return;

            events[eventType].Remove(action);

            if (events[eventType].Count == 0)
                events.Remove(eventType);
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            var eventType = typeof(TEvent);
            if (!events.TryGetValue(eventType, out var @event)) return;

            foreach (Action<TEvent> action in @event.ToList())
                action?.Invoke(eventData);
        }

        public void ClearAll()
        {
            events.Clear();
        }
    }
}