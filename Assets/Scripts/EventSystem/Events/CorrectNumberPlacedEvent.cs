namespace Events{
    public class CorrectNumberPlacedEvent : BaseEvent{
        public readonly int numberIndex;

        public CorrectNumberPlacedEvent(int correctNumberIndex) : base(EventKey.CorrectNumberPlaced) {
            numberIndex = correctNumberIndex;
        }
    }
}