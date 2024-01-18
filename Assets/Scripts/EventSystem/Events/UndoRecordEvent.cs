using UndoSystem;

namespace Events{
    public class UndoRecordEvent : BaseEvent{
        public readonly IUndoCommand undoCommand;

        public UndoRecordEvent(IUndoCommand command) : base(EventKey.PlayerAction) {
            undoCommand = command;
        }
    }
}