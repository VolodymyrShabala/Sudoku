namespace Events{
    public class UndoButtonClickedEvent : BaseEvent{
        public UndoButtonClickedEvent() : base(EventKey.UndoButtonClicked) {

        }
    }
}