using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOver{
    public class GameOverMistakes : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button restartButton;
        private int mistakesAmount;
        
        public void Init(int numberOfMistakes) {
            mistakesAmount = numberOfMistakes;
            restartButton.onClick.AddListener(RestartButtonClicked);
            newGameButton.onClick.AddListener(NewGameButtonClicked);
        }

        private void RestartButtonClicked() {
            EventSystem.Trigger(new LevelRestartEvent());
        }
        
        private void NewGameButtonClicked() {
            EventSystem.Trigger(new LeveQuitEvent());
        }

        public void Show() {
            gameObject.SetActive(true);
            string text = descriptionText.text;
            string formattedText = string.Format(text, mistakesAmount);
            descriptionText.text = formattedText;
        }
        
        public void Clear() {
            restartButton.onClick.RemoveListener(RestartButtonClicked);
            newGameButton.onClick.RemoveListener(NewGameButtonClicked);
            gameObject.SetActive(false);
        }
    }
}