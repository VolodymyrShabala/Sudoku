namespace Events{
    public class LevelRestartEvent : BaseEvent{
        public LevelRestartEvent() : base(EventKey.LevelRestart) {
        }
    }
}