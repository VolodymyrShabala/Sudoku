using SaveSystem;
using UnityEngine;

namespace GameOver{
    public class PopupController : MonoBehaviour{
        [SerializeField] private GameOverMistakes gameOverMistakes;
        [SerializeField] private GameOverWin gameOverWin;
        [SerializeField] private TimerController timerController;
        [SerializeField] private ScoreController scoreController;
        private string levelDifficultyName;
        private int allowedMistakesCount;
        private int levelIndex;
        
        public void Init(string difficultyName, int allowedMistakes, int index) {
            levelDifficultyName = difficultyName;
            allowedMistakesCount = allowedMistakes;
            levelIndex = index;
        }

        public void ShowGameWonPopup() {
            string score = scoreController.GetScoreText();
            bool hasTime = LevelSave.HasBestTimeForLevel(levelIndex);
            float elapsedTime = timerController.GetElapsedTime();
            float savedTime = hasTime ? LevelSave.GetBestTimerForLevel(levelIndex) : elapsedTime;
            float bestTime = elapsedTime < savedTime ? elapsedTime : savedTime;
            string elapsedTimeText = timerController.GetFormattedTime(elapsedTime);
            string bestTimeText = timerController.GetFormattedTime(bestTime);
            gameOverWin.Init(levelDifficultyName, score, elapsedTimeText, bestTimeText);
            gameOverWin.Show();
        }

        public void ShowLevelFailed() {
            gameOverMistakes.Init(allowedMistakesCount);
            gameOverMistakes.Show();
        }

        public void Clear() {
            gameOverMistakes.Clear();
            gameOverWin.Clear();
        }
    }
}