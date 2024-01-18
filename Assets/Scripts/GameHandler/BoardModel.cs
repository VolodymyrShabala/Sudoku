using System.Collections.Generic;
using Data;
using Tile;
using UnityEngine;

namespace GameHandler{
    public class BoardModel{
        private readonly TileController[,] board;
        private readonly Vector2Int boardSize;
        private readonly Vector2Int squareLayout;
        private readonly Vector2 bottomLeft;
        private readonly Vector2 topRight;
        private readonly float tileWidth;
        private readonly float tileHeight;

        public BoardModel(TileController[,] gameBoard, LevelData levelData) {
            board = gameBoard;
            boardSize = levelData.GetBoardSize();
            squareLayout = levelData.GetSquareLayout();
            
            TileController bottomLeftTile = GetTile(0, 0);
            TileController topRightTile = GetTile(boardSize.x - 1, boardSize.y - 1);

            RectTransform bottomLeftRectTransform = bottomLeftTile.GetRectTransform();
            RectTransform topRightRectTransform = topRightTile.GetRectTransform();

            Vector3 bottomLeftPosition = bottomLeftRectTransform.position;
            Vector3 topRightPosition = topRightRectTransform.position;

            TileController tileToTheRight = GetTile(1, 0);
            RectTransform tileToTheRightRectTransform = tileToTheRight.GetRectTransform();
            Vector3 tileToTheRightPosition = tileToTheRightRectTransform.position;
            tileWidth = tileToTheRightPosition.x - bottomLeftPosition.x;

            TileController tileToTheUp = GetTile(0, 1);
            RectTransform tileToTheUpRectTransform = tileToTheUp.GetRectTransform();
            Vector3 tileToTheUpPosition = tileToTheUpRectTransform.position;
            tileHeight = tileToTheUpPosition.y - bottomLeftPosition.y;

            bottomLeft = new Vector2(bottomLeftPosition.x - (tileWidth * 0.5f),
                                     bottomLeftPosition.y - (tileHeight * 0.5f));

            topRight = new Vector2(topRightPosition.x + (tileWidth * 0.5f),
                                   topRightPosition.y + (tileHeight * 0.5f));
        }

        public bool IsInsideOfBoard(Vector3 position) {
            bool isYCorrect = position.y <= topRight.y && position.y >= bottomLeft.y;
            bool isXCorrect = position.x >= bottomLeft.x && position.x <= topRight.x;
            bool result = isXCorrect && isYCorrect;
            return result;
        }

        public TileController GetTile(Vector3 position) {
            float xDifference = position.x - bottomLeft.x;
            float xTileJumps = xDifference / tileWidth;
            int x = Mathf.FloorToInt(xTileJumps);

            float yDifference = position.y - bottomLeft.y;
            float yTileJumps = yDifference / tileHeight;
            int y = Mathf.FloorToInt(yTileJumps);

            if (IsInsideOfBoardArray(x, y) == false) {
                return null;
            }

            TileController result = GetTile(x, y);
            return result;
        }

        public void SelectTile(TileController tile, bool simpleHighlight = false) {
            Vector2Int boardPosition = tile.GetBoardPosition();
            HighlightBlock(boardPosition);
            HighlightRowAndCol(boardPosition);
            if (tile.IsNumberShown()) {
                int number = tile.GetShowingNumberIndex();
                HighlightAllOfNumber(number, boardPosition, simpleHighlight);
                HighlightAllOfPencilNumbers(number, boardPosition);
            }
            
            tile.SelectTile(simpleHighlight);
        }

        private void HighlightBlock(Vector2Int position) {
            int xBlock = position.x / squareLayout.x;
            int yBlock = position.y / squareLayout.y;

            for (int y = yBlock * squareLayout.y; y < yBlock * squareLayout.y + squareLayout.y; y++) {
                for (int x = xBlock * squareLayout.x; x < xBlock * squareLayout.x + squareLayout.x; x++) {
                    TileController tile = GetTile(x, y);
                    tile.HighlightTile();
                }
            }
        }

        private void HighlightRowAndCol(Vector2Int position) {
            for (int x = 0; x < boardSize.x; x++) {
                TileController tile = GetTile(x, position.y);
                tile.HighlightTile();
            }

            for (int y = 0; y < boardSize.y; y++) {
                TileController tile = GetTile(position.x, y);
                tile.HighlightTile();
            }
        }

        private void HighlightAllOfNumber(int numberIndex, Vector2Int originalPosition, bool isSimpleHighlight) {
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    if (x == originalPosition.x && y == originalPosition.y) {
                        continue;
                    }

                    TileController tile = GetTile(x, y);
                    int showingNumberIndex = tile.GetShowingNumberIndex();
                    if (tile.IsNumberShown() && showingNumberIndex == numberIndex) {
                        tile.HighlightSameNumberTile(isSimpleHighlight);
                    }
                }
            }
        }

        public void HighlightAllOfPencilNumbers(int numberIndex, Vector2Int originalPosition) {
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    if (x == originalPosition.x && y == originalPosition.y) {
                        continue;
                    }

                    TileController tile = GetTile(x, y);
                    if (tile.pencilController.IsShowing(numberIndex)) {
                        tile.pencilController.Highlight(numberIndex);
                    }
                }
            }
        }

        public void HighlightAllOfNumberSimple(int numberIndex, Vector2Int originalPosition) {
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    if (x == originalPosition.x && y == originalPosition.y) {
                        continue;
                    }

                    TileController tile = GetTile(x, y);
                    int showingNumberIndex = tile.GetShowingNumberIndex();
                    if (tile.IsNumberShown() && showingNumberIndex == numberIndex) {
                        tile.HighlightTile();
                    }
                }
            }
        }

        public List<TileController> GetTilesWithPencilIndexShowing(int numberIndex, Vector2Int position) {
            List<TileController> result = new();
            IEnumerable<TileController> tileInSameBlock = GetTilesWithPencilNumberInSameBlock(numberIndex, position);
            IEnumerable<TileController> tilesInSameRowCol = GetTilesPencilNumbersInRowAnCol(numberIndex, position);

            result.AddRange(tileInSameBlock);
            result.AddRange(tilesInSameRowCol);
            return result;
        }

        private IEnumerable<TileController> GetTilesWithPencilNumberInSameBlock(int numberIndex, Vector2Int position) {
            List<TileController> result = new();
            int xBlock = position.x / squareLayout.x;
            int yBlock = position.y / squareLayout.y;

            for (int y = yBlock * squareLayout.y; y < yBlock * squareLayout.y + squareLayout.y; y++) {
                for (int x = xBlock * squareLayout.x; x < xBlock * squareLayout.x + squareLayout.x; x++) {
                    if (x == position.x && y == position.y) {
                        continue;
                    }

                    TileController tile = GetTile(x, y);
                    if (tile.pencilController.IsShowing(numberIndex)) {
                        result.Add(tile);
                    }
                }
            }

            return result;
        }

        private IEnumerable<TileController> GetTilesPencilNumbersInRowAnCol(int numberIndex, Vector2Int position) {
            List<TileController> result = new();
            for (int x = 0; x < boardSize.x; x++) {
                if (x == position.x) {
                    continue;
                }

                TileController tile = GetTile(x, position.y);
                if (tile.pencilController.IsShowing(numberIndex)) {
                    result.Add(tile);
                }
            }

            for (int y = 0; y < boardSize.y; y++) {
                if (y == position.y) {
                    continue;
                }

                TileController tile = GetTile(position.x, y);
                if (tile.pencilController.IsShowing(numberIndex)) {
                    result.Add(tile);
                }
            }

            return result;
        }

        public void HighlightConflictingNumbers(int numberIndex, Vector2Int position, bool pencil) {
            HighlightConflictingNumberInSameBlock(numberIndex, position, pencil);
            HighlightConflictingNumbersRowAndCol(numberIndex, position, pencil);
        }

        private void HighlightConflictingNumberInSameBlock(int numberIndex, Vector2Int position, bool pencil) {
            int xBlock = position.x / squareLayout.x;
            int yBlock = position.y / squareLayout.y;

            for (int y = yBlock * squareLayout.y; y < yBlock * squareLayout.y + squareLayout.y; y++) {
                for (int x = xBlock * squareLayout.x; x < xBlock * squareLayout.x + squareLayout.x; x++) {
                    if (x == position.x && y == position.y) {
                        continue;
                    }

                    TileController tile = GetTile(x, y);
                    if (tile.IsNumberShown() && tile.GetShowingNumberIndex() == numberIndex) {
                        if (pencil == false) {
                            tile.HighlightAdjacentWrongTile();
                        }
                        else {
                            tile.HighlightBorderSimple();
                        }
                    }
                }
            }
        }

        private void HighlightConflictingNumbersRowAndCol(int numberIndex, Vector2Int position, bool pencil) {
            for (int x = 0; x < boardSize.x; x++) {
                if (x == position.x) {
                    continue;
                }

                TileController tile = GetTile(x, position.y);
                if (tile.IsNumberShown() && tile.GetShowingNumberIndex() == numberIndex) {
                    if (pencil == false) {
                        tile.HighlightAdjacentWrongTile();
                    }
                    else {
                        tile.HighlightBorderSimple();
                    }
                }
            }

            for (int y = 0; y < boardSize.y; y++) {
                if (y == position.y) {
                    continue;
                }

                TileController tile = GetTile(position.x, y);
                if (tile.IsNumberShown() && tile.GetShowingNumberIndex() == numberIndex) {
                    if (pencil == false) {
                        tile.HighlightAdjacentWrongTile();
                    }
                    else {
                        tile.HighlightBorderSimple();
                    }
                }
            }
        }

        public void RemoveAllHighlight() {
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    TileController tile = GetTile(x, y);
                    tile.RemoveHighlight();
                    tile.pencilController.RemoveAllHighlight();
                }
            }
        }

        private bool IsInsideOfBoardArray(int x, int y) {
            bool result = x >= 0 && x < boardSize.x && y >= 0 && y < boardSize.y;
            return result;
        }

        // V: Y is the first, X is the second
        private TileController GetTile(int x, int y) {
            TileController result = board[y, x];
            return result;
        }

        public bool IsBoardFilled() {
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    TileController tile = GetTile(x, y);
                    if (tile.IsCorrectNumberShown() == false) {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Clear() {
        }
    }
}