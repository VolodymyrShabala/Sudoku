namespace Events{
    public class LevelFailedEvent : BaseEvent{
        public LevelFailedEvent() : base(EventKey.LevelFailed) {
        }
    }
}