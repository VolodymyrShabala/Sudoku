using System.Collections.Generic;
using Events;
using Tile;

namespace UndoSystem{
    public class UndoNumberPlacedCommand : IUndoCommand{
        private readonly TileController tile;
        private readonly int previousIndex;
        private readonly List<int> previousPencilIndices;
        public bool IsPlayerAction => true;
        
        public UndoNumberPlacedCommand(TileController tileController, int previouslySelectedIndex, List<int> pencilIndices) {
            tile = tileController;
            previousIndex = previouslySelectedIndex;
            previousPencilIndices = pencilIndices;
        }
        
        public void Undo() {
            int numberIndex = tile.GetShowingNumberIndex();
            bool isCorrect = tile.IsCorrectNumberShown();
            EventSystem.Trigger(new UndoNumberPlacedEvent(numberIndex, isCorrect));
            
            tile.RemoveNumber();
            if (previousIndex != -1) {
                EventSystem.Trigger(new SelectTileEvent(tile));
                EventSystem.Trigger(new NumberSelectedEvent(previousIndex, true));
            }
            else if(previousPencilIndices.Count > 0){
                EventSystem.Trigger(new SelectTileEvent(tile));
                tile.pencilController.Show(previousPencilIndices);
            }
        }
    }
}