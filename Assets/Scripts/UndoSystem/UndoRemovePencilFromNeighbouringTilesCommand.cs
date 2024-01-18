using System.Collections.Generic;
using Tile;

namespace UndoSystem{
    public class UndoRemovePencilFromNeighbouringTilesCommand : IUndoCommand{
        private readonly List<TileController> tiles;
        private readonly int numberIndex;
        public bool IsPlayerAction => false;

        public UndoRemovePencilFromNeighbouringTilesCommand(List<TileController> tileControllers, int index) {
            tiles = tileControllers;
            numberIndex = index;
        }

        public void Undo() {
            foreach (TileController tile in tiles) {
                tile.pencilController.Show(numberIndex);
            }
        }
    }
}