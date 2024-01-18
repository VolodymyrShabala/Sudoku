using System.Collections.Generic;
using Data;
using Tile;
using Tile.Pencil;
using UnityEngine;

namespace Board{
    public class PencilCreator{
        private readonly PencilView pencilPrefab;
        private readonly TileController[,] board;

        public PencilCreator(PencilView prefab, TileController[,] tiles) {
            pencilPrefab = prefab;
            board = tiles;
        }

        public void Generate(LevelData levelData) {
            Vector2Int boardSize = levelData.GetBoardSize();
            Vector2Int squareLength = levelData.GetSquareLayout();
            int numbersLength = levelData.GetAllVisualNumbers().Count;
            PencilSpawner pencilSpawner = new(pencilPrefab, levelData);
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    TileController tile = GetTile(y, x);
                    IReadOnlyList<int> possibleNumberIndices = tile.IsCorrectNumberShown() ? new List<int>() :
                            GetPossibleNumbersIndices(x, y, numbersLength, squareLength, boardSize);

                    RectTransform parent = tile.GetPencilHolder();
                    PencilController pencilController = pencilSpawner.Create(parent, possibleNumberIndices);

                    tile.Init(pencilController);
                }
            }
        }

        private IReadOnlyList<int> GetPossibleNumbersIndices(int x, int y, int numbersLength,
                                                             Vector2Int squareLayout, Vector2Int boardSize) {
            TileController tile = GetTile(y, x);
            if (tile.IsCorrectNumberShown()) {
                return null;
            }

            int tileNumber = tile.GetCorrectNumberIndex();
            List<int> possibleNumberIndices = new();
            for (int i = 0; i < numbersLength; i++) {
                if (i == tileNumber) {
                    possibleNumberIndices.Add(i);
                    continue;
                }

                if (IsNumberUsedInBox(x, y, i, squareLayout) == false &&
                    IsNumberUsedInRowAndCol(x, y, i, boardSize) == false) {
                    possibleNumberIndices.Add(i);
                }
            }

            return possibleNumberIndices;
        }

        private bool IsNumberUsedInBox(int posX, int posY, int numberIndex, Vector2Int squareLayout) {
            int xBlock = posX / squareLayout.x;
            int yBlock = posY / squareLayout.y;

            for (int y = yBlock * squareLayout.y; y < yBlock * squareLayout.y + squareLayout.y; y++) {
                for (int x = xBlock * squareLayout.x; x < xBlock * squareLayout.x + squareLayout.x; x++) {
                    if (x == posX && y == posY) {
                        continue;
                    }

                    TileController tile = GetTile(y, x);
                    int tileNumber = tile.GetShowingNumberIndex();
                    if (numberIndex == tileNumber) {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsNumberUsedInRowAndCol(int posX, int posY, int number, Vector2Int boardSize) {
            for (int x = 0; x < boardSize.x; x++) {
                if (x == posX) {
                    continue;
                }

                TileController tile = GetTile(posY, x);
                int tileNumber = tile.GetShowingNumberIndex();
                if (number == tileNumber) {
                    return true;
                }
            }

            for (int y = 0; y < boardSize.y; y++) {
                if (y == posY) {
                    continue;
                }

                TileController tile = GetTile(y, posX);
                int tileNumber = tile.GetShowingNumberIndex();
                if (number == tileNumber) {
                    return true;
                }
            }

            return false;
        }

        private TileController GetTile(int y, int x) {
            return board[y, x];
        }
    }
}