using UnityEngine;

namespace Tile{
    public class TileModel{
        private readonly Vector2Int gridPosition;
        private readonly int numberIndex;
        private readonly string visualNumber;
        private bool isCorrectNumberShown;
        private bool isNumberShown;
        private int showingNumber = -1;
        
        public TileModel(Vector2Int position, int tileNumberIndex, string numberVisual, bool isNumberHidden) {
            gridPosition = position;
            numberIndex = tileNumberIndex;
            visualNumber = numberVisual;
            isCorrectNumberShown = isNumberHidden == false;
            if (isCorrectNumberShown) {
                showingNumber = numberIndex;
            }
        }
        
        public int GetCorrectNumberIndex() {
            return numberIndex;
        }

        public string GetVisualNumber() {
            return visualNumber;
        }

        public bool IsCorrectNumber(int number) {
            bool result = number == numberIndex;
            return result;
        }

        public Vector2Int GetGridPosition() {
            return gridPosition;
        }

        public bool IsCorrectNumberShown() {
            return isCorrectNumberShown;
        }
        
        public bool HasNumberShown() {
            return isCorrectNumberShown || showingNumber != -1;
        }

        public void SetShowingNumber(int numberToShow) {
            showingNumber = numberToShow;
        }

        public void RemoveShowingNumber() {
            showingNumber = -1;
        }

        public int GetShowingNumber() {
            return showingNumber;
        }

        public void SetCorrectNumberPlaced(bool state) {
            isCorrectNumberShown = state;
        }
    }
}