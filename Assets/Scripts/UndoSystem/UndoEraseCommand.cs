using System.Collections.Generic;
using Events;
using Tile;

namespace UndoSystem{
    public class UndoEraseCommand : IUndoCommand{
        private readonly TileController tile;
        private readonly List<int> pencilNumberIndices;
        public bool IsPlayerAction => true;
        
        public UndoEraseCommand(TileController selectedTile, List<int> shownNumberIndices) {
            tile = selectedTile;
            pencilNumberIndices = shownNumberIndices;
        }

        public void Undo() {
            EventSystem.Trigger(new SelectTileEvent(tile));
            tile.pencilController.Show(pencilNumberIndices);
        }
    }
}