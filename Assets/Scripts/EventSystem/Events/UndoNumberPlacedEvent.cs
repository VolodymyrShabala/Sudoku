namespace Events{
    public class UndoNumberPlacedEvent : BaseEvent{
        public readonly int numberIndex;
        public readonly bool isNumberCorrect;

        public UndoNumberPlacedEvent(int undoneNumberIndex, bool isCorrect) : base(EventKey.UndoNumber) {
            numberIndex = undoneNumberIndex;
            isNumberCorrect = isCorrect;
        }
    }
}