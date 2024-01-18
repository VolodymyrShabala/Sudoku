namespace UndoSystem{
    public interface IUndoCommand{
        public bool IsPlayerAction { get; }
        public void Undo();
    }
}