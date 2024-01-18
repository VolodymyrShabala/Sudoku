using System.Globalization;
using Data;
using Events;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    private int scoreIncreaseAmount;
    
    public void Init(LevelData levelData) {
        scoreText.text = score.ToString(CultureInfo.InvariantCulture);
        scoreIncreaseAmount = levelData.GetScoreIncreaseAmount();
        EventSystem.Subscribe(EventKey.CorrectNumberPlaced, IncreaseScore);
        EventSystem.Subscribe(EventKey.UndoNumber, DecreaseScore);
    }

    private void IncreaseScore(BaseEvent baseEvent) {
        score += scoreIncreaseAmount;
        scoreText.text = score.ToString(CultureInfo.InvariantCulture);
    }

    private void DecreaseScore(BaseEvent baseEvent) {
        UndoNumberPlacedEvent undoNumberPlacedEvent = (UndoNumberPlacedEvent)baseEvent;
        if (undoNumberPlacedEvent.isNumberCorrect == false) {
            return;
        }
        
        score -= scoreIncreaseAmount;
        scoreText.text = score.ToString(CultureInfo.InvariantCulture);
    }

    public string GetScoreText() {
        return score.ToString(CultureInfo.InvariantCulture);
    }

    public void Clear() {
        score = 0;
        scoreText.text = score.ToString(CultureInfo.InvariantCulture);
        EventSystem.Unsubscribe(EventKey.CorrectNumberPlaced, IncreaseScore);
        EventSystem.Unsubscribe(EventKey.UndoNumber, DecreaseScore);
    }
}