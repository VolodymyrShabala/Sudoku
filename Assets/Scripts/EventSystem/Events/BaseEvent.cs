namespace Events{
    public class BaseEvent{
        public readonly EventKey key;

        protected BaseEvent(EventKey eventKey) {
            key = eventKey;
        }
    }
}