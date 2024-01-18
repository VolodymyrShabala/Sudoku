using Tile;

namespace Events{
    public class SelectTileEvent : BaseEvent{
        public readonly TileController tile;

        public SelectTileEvent(TileController tileController) : base(EventKey.SelectTile) {
            tile = tileController;
        }
    }
}