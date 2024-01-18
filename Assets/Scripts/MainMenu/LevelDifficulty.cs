using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu{
    public class LevelDifficulty : MonoBehaviour{
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI difficultyText;
        private DifficultySelection difficultySelection;
        private int index;
        
        public void Init(DifficultySelection parent, int i, string difficultyName) {
            difficultySelection = parent;
            index = i;
            difficultyText.text = difficultyName;
            button.onClick.AddListener(DifficultySelected);
        }

        private void DifficultySelected() {
            difficultySelection.DifficultySelected(index);
        }

        public void Clear() {
            button.onClick.RemoveListener(DifficultySelected);
        }
    }
}