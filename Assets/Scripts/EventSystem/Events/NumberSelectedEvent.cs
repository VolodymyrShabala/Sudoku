namespace Events{
    public class NumberSelectedEvent : BaseEvent{
        public readonly int numberIndex;
        public readonly bool isUndo;

        public NumberSelectedEvent(int index, bool undo = false) : base(EventKey.NumberSelected) {
            numberIndex = index;
            isUndo = undo;
        }
    }
}