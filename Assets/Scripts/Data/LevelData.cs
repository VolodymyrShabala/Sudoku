using System.Collections.Generic;
using UnityEngine;

namespace Data{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData/LevelData", order = 1)]
    public class LevelData : ScriptableObject{
        [Tooltip("Length is set automatically"), SerializeField] private List<string> numberStrings;
        [SerializeField] private int numberOfHiddenTiles;
        [Tooltip("Must be a square. Can't be bigger than 16x16"), SerializeField] private Vector2Int boardSize;
        [SerializeField] private Vector2Int squareLayout = new(3, 3);
        [SerializeField] private int scoreIncreaseAmount;
        [SerializeField] private int mistakesAmountAllowed;
        [SerializeField] private string difficultyName;
        // V: Can be future expanded to write code that looks for good seeds
        // V: Can also add function to rotate the puzzle, so that even if player encounters the same puzzle, it will not look the same
        [SerializeField] private List<int> seeds;
        
        private void OnValidate() {
            int length = numberStrings.Count;
            int desiredLength = squareLayout.x * squareLayout.y;
            if (desiredLength > length) {
                int diff = desiredLength - length;
                for (int i = 0; i < diff; i++) {
                    numberStrings.Add(string.Empty);
                }
            } else if (length > desiredLength) {
                int diff = length - desiredLength;
                numberStrings.RemoveRange(desiredLength, diff);
            }
        }

        public string GetVisualNumber(int index) {
            if (index >= numberStrings.Count || index < 0) {
                Debug.LogError($"Tried to get the number by index: {index}");
                return string.Empty;
            }
            
            string result = numberStrings[index];
            return result;
        }
        
        public List<string> GetAllVisualNumbers() {
            return numberStrings;
        }

        public int GetNumberOfHiddenValues() {
            return numberOfHiddenTiles;
        }

        public Vector2Int GetBoardSize() {
            return boardSize;
        }

        public Vector2Int GetSquareLayout() {
            return squareLayout;
        }

        public int GetScoreIncreaseAmount() {
            return scoreIncreaseAmount;
        }

        public int GetAllowedMistakesAmount() {
            return mistakesAmountAllowed;
        }

        public string GetDifficultyName() {
            return difficultyName;
        }

        public int GetRandomSeed() {
            int length = seeds.Count;
            int index = Random.Range(0, length);
            int result = seeds[index];
            return result;
        }
    }
}