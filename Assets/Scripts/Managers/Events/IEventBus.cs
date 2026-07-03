using System;

namespace Game.Scripts.Shared.Events
{
    public interface IEventBus
    { 
        void Subscribe<TEvent>(Action<TEvent> action);
        void Unsubscribe<TEvent>(Action<TEvent> action);
        void Publish<TEvent>(TEvent eventData);
        void ClearAll();
    }
}
