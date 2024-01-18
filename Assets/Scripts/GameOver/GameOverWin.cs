using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOver{
    public class GameOverWin : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI difficultyName;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI timerBestText;
        [SerializeField] private Button newGameButton;
        
        public void Init(string levelDifficultyName, string score, string timer, string bestTime) {
            newGameButton.onClick.AddListener(NewGameButtonPressed);

            scoreText.text = score;
            difficultyName.text = levelDifficultyName;
            timerText.text = timer;
            timerBestText.text = bestTime;
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        private void NewGameButtonPressed() {
            EventSystem.Trigger(new LeveQuitEvent());
        }

        public void Clear() {
            newGameButton.onClick.RemoveListener(NewGameButtonPressed);
            gameObject.SetActive(false);
        }
    }
}