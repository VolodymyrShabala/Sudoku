namespace Events{
    public class PauseGameEvent : BaseEvent{
        public readonly bool state;
        
        public PauseGameEvent(bool pauseState) : base(EventKey.PauseGame) {
            state = pauseState;
        }
    }
}