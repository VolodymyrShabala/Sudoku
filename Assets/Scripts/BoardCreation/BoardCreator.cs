using Data;
using Tile;
using UnityEngine;

namespace Board{
    public class BoardCreator{
        private readonly TileView prefab;
        private readonly RectTransform parent;
        private readonly LevelData levelData;
        private readonly float tileWidth;
        private readonly float tileHeight;
        private readonly GapController gapController;

        public BoardCreator(TileView tilePrefab, RectTransform boardParent, LevelData data) {
            prefab = tilePrefab;
            parent = boardParent;
            levelData = data;
            
            Vector2Int boardSize = levelData.GetBoardSize();
            Rect rect = boardParent.rect;
            tileWidth = (rect.width / boardSize.x);
            tileHeight = (rect.height / boardSize.y);
            
            gapController = new GapController(levelData.GetBoardSize(), levelData.GetSquareLayout(), tileWidth);
            tileWidth -= gapController.GetTotalWidthDeduction();
            tileHeight -= gapController.GetTotalHeightDeduction();
        }

        public TileController[,] CreateBoard(int[,] boardNumbers, bool[,] hiddenNumbers) {
            Vector2Int boardSize = levelData.GetBoardSize();
            Vector2Int squareLayout = levelData.GetSquareLayout();
            TileController[,] board = new TileController[boardSize.y, boardSize.x];
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    TileView tileView = Object.Instantiate(prefab, parent);
                    tileView.name = $"Tile: {x}|{y}";
                    RectTransform tileViewRect = tileView.GetComponent<RectTransform>();
                    tileViewRect.sizeDelta = new Vector2(tileWidth, tileHeight);
                    tileViewRect.anchoredPosition = GetPosition(x, y, boardSize.y, squareLayout);
        
                    int numberIndex = boardNumbers[y, x] - 1;
                    bool isHidden = hiddenNumbers[y, x];
                    string visualNumber = levelData.GetVisualNumber(numberIndex);
                    TileModel model = new(new Vector2Int(x, y), numberIndex, visualNumber, isHidden);
                    TileController tileController = new(tileView, model);
                    board[y, x] = tileController;
                }
            }

            return board;
        }

        private Vector2 GetPosition(int x, int y, int numberOfRows, Vector2Int squareLayout) {
            // V: Have to add half of the width because on the anchors. Otherwise tile will be spawned not in the corner, but on the corner
            float halfWidth = tileWidth * 0.5f;
            float halfHeight = tileHeight * 0.5f;
            Vector2 result = new(halfWidth + tileWidth * x, tileHeight * -(numberOfRows - y - 1) - halfHeight);
            result += gapController.GetPositionAdjustment(x, y, squareLayout);
            return result;
        }
    }
}