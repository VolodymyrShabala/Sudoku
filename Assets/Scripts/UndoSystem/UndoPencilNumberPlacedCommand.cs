using Events;
using Tile;

namespace UndoSystem{
    public class UndoPencilNumberPlacedCommand : IUndoCommand{
        private readonly TileController tile;
        private readonly int numberIndex;
        public bool IsPlayerAction => true;

        public UndoPencilNumberPlacedCommand(TileController tileController, int pencilNumberIndex) {
            tile = tileController;
            numberIndex = pencilNumberIndex;
        }
        public void Undo() {
            tile.pencilController.Show(numberIndex);

            if (tile.pencilController.GetAllShowingNumberIndices().Count > 0) {
                EventSystem.Trigger(new SelectTileEvent(tile));
            }
        }
    }
}