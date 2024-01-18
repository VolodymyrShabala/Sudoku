using Data;
using Events;
using TMPro;
using UnityEngine;

public class MistakesController : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI mistakesText;
    private int mistakesCounter;
    private int maxMistakes;
    
    public void Init(LevelData levelData) {
        maxMistakes = levelData.GetAllowedMistakesAmount();
        EventSystem.Subscribe(EventKey.WrongNumberPlaced, WrongNumberPlaced);
    }

    private void WrongNumberPlaced(BaseEvent baseEvent) {
        mistakesCounter++;
        mistakesText.text = $"{mistakesCounter}/{maxMistakes}";

        if (mistakesCounter >= maxMistakes) {
            EventSystem.Trigger(new LevelFailedEvent());
        }
    }

    public void Clear() {
        EventSystem.Unsubscribe(EventKey.WrongNumberPlaced, WrongNumberPlaced);
    }
}