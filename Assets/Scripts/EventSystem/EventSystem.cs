using System.Collections.Generic;

namespace Events{
    public class EventSystem{
        public static EventSystem Instance => instance ??= new EventSystem();
        private static EventSystem instance;
        private readonly Dictionary<EventKey, List<HandleEventDelegate>> eventListeners;

        private EventSystem() {
            eventListeners = new Dictionary<EventKey, List<HandleEventDelegate>>();
        }

        public static void Trigger(BaseEvent baseEvent) {
            Instance.TriggerInternal(baseEvent);
        }

        public static void Subscribe(EventKey key, HandleEventDelegate listener) {
            Instance.SubscribeInternal(key, listener);
        }

        public static void Unsubscribe(EventKey key, HandleEventDelegate listener) {
            Instance.UnsubscribeInternal(key, listener);
        }

        public static void Dispose() {
            Instance.DisposeInternal();
        }

        private void TriggerInternal(BaseEvent baseEvent) {
            if (eventListeners.ContainsKey(baseEvent.key) == false) {
                return;
            }
            
            List<HandleEventDelegate> eventsList = eventListeners[baseEvent.key];
            int length = eventsList.Count;
            if (length == 0) {
                return;
            }
            
            for (int i = length - 1; i >= 0; i--) {
                eventsList[i]?.Invoke(baseEvent);
            }
        }

        private void SubscribeInternal(EventKey key, HandleEventDelegate listener) {
            if (eventListeners.ContainsKey(key) == false) {
                eventListeners.Add(key, new List<HandleEventDelegate>());
            }

            if (eventListeners[key].Contains(listener) == false) {
                eventListeners[key].Add(listener);
            }
        }

        private void UnsubscribeInternal(EventKey key, HandleEventDelegate listener) {
            if (eventListeners.ContainsKey(key) && eventListeners[key].Contains(listener)) {
                eventListeners[key].Remove(listener);
            }
        }

        private void DisposeInternal() {
            foreach (KeyValuePair<EventKey, List<HandleEventDelegate>> pair in eventListeners) {
                pair.Value.Clear();
            }

            eventListeners.Clear();
            instance = null;
        }
    }
}