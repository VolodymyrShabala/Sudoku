namespace Events{
    public class LevelWonEvent : BaseEvent{
        public LevelWonEvent() : base(EventKey.LevelWon) {
        }
    }
}