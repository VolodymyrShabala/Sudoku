using Data;
using Events;
using Tile;
using UnityEngine;

namespace SelectableNumbers{
    public class SelectableNumberController : MonoBehaviour{
        [SerializeField] private SelectableNumber prefab;
        [SerializeField] private Transform holder;
        private SelectableNumber[] selectableNumbers;

        public void Init(TileController[,] board, LevelData levelData) {
            int[] numbersIndicesHidden = GetAllHiddenNumberIndices(board, levelData);
            SelectableNumbersCreator creator = new();
            selectableNumbers = creator.Create(prefab, holder, levelData, numbersIndicesHidden);

            EventSystem.Subscribe(EventKey.CorrectNumberPlaced, CorrectNumberPlaced);
            EventSystem.Subscribe(EventKey.UndoNumber, NumberUndo);
            EventSystem.Subscribe(EventKey.PencilSelected, PencilSelected);
        }
        
        private void CorrectNumberPlaced(BaseEvent baseEvent) {
            CorrectNumberPlacedEvent correctNumberPlacedEvent = (CorrectNumberPlacedEvent)baseEvent;
            int index = correctNumberPlacedEvent.numberIndex;
            selectableNumbers[index].DecrementAmountLeft();
        }

        private void NumberUndo(BaseEvent baseEvent) {
            UndoNumberPlacedEvent undoButtonClicked = (UndoNumberPlacedEvent)baseEvent;
            if (undoButtonClicked.isNumberCorrect == false) {
                return;
            }
            
            int index = undoButtonClicked.numberIndex;
            selectableNumbers[index].IncrementAmountLeft();
        }
        
        private void PencilSelected(BaseEvent baseEvent) {
            PencilSelectedEvent pencilSelectedEvent = (PencilSelectedEvent)baseEvent;
            foreach (SelectableNumber selectableNumber in selectableNumbers) {
                selectableNumber.ChangeVisualState(pencilSelectedEvent.isSelected);
            }
        }
        
        private int[] GetAllHiddenNumberIndices(TileController[,] board, LevelData levelData) {
            int length = levelData.GetAllVisualNumbers().Count;
            int[] result = new int[length];
            Vector2Int boardSize = levelData.GetBoardSize();
            for (int y = 0; y < boardSize.y; y++) {
                for (int x = 0; x < boardSize.x; x++) {
                    TileController tile = board[y, x];
                    if (tile.IsCorrectNumberShown()) {
                        continue;
                    }

                    int index = tile.GetCorrectNumberIndex();
                    result[index]++;
                }
            }

            return result;
        }

        public void Clear() {
            foreach (SelectableNumber selectableNumber in selectableNumbers) {
                if (selectableNumber == null) {
                    continue;
                }
                
                selectableNumber.Clear();
            }

            selectableNumbers = null;
            EventSystem.Unsubscribe(EventKey.CorrectNumberPlaced, CorrectNumberPlaced);
            EventSystem.Unsubscribe(EventKey.UndoNumber, NumberUndo);
            EventSystem.Unsubscribe(EventKey.PencilSelected, PencilSelected);
        }
    }
}