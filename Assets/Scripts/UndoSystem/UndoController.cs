using System.Collections.Generic;
using System.Linq;
using Events;

namespace UndoSystem{
    public class UndoController{
        private Stack<IUndoCommand> playerActions;

        public UndoController() {
            playerActions = new Stack<IUndoCommand>();

            EventSystem.Subscribe(EventKey.PlayerAction, PlayerPerformedAction);
            EventSystem.Subscribe(EventKey.UndoButtonClicked, UndoPressed);
        }

        private void PlayerPerformedAction(BaseEvent baseEvent) {
            UndoRecordEvent undoRecordEvent = (UndoRecordEvent)baseEvent;
            playerActions.Push(undoRecordEvent.undoCommand);
        }

        private void UndoPressed(BaseEvent baseEvent) {
            while (playerActions.Count > 0) {
                IUndoCommand undoCommand = playerActions.Pop();
                undoCommand.Undo();
                if (undoCommand.IsPlayerAction) {
                    break;
                }
            }
        }

        public void Clear() {
            playerActions.Clear();
            playerActions = null;

            EventSystem.Unsubscribe(EventKey.PlayerAction, PlayerPerformedAction);
            EventSystem.Unsubscribe(EventKey.UndoNumber, UndoPressed);
        }
    }
}