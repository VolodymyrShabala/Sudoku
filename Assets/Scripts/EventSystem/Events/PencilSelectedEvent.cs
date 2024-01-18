namespace Events{
    public class PencilSelectedEvent : BaseEvent{
        public bool isSelected;

        public PencilSelectedEvent(bool isPencilSelected) : base(EventKey.PencilSelected) {
            isSelected = isPencilSelected;
        }
    }
}