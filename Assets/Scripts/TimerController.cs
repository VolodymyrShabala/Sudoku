using Events;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isTimerStarted;
    
    public void Init() {
        elapsedTime = 0;
        isTimerStarted = true;
        EventSystem.Subscribe(EventKey.PauseGame, UpdateState);
    }

    private void UpdateState(BaseEvent baseEvent) {
        PauseGameEvent pauseGameEvent = (PauseGameEvent)baseEvent;
        isTimerStarted = pauseGameEvent.state;
    }

    private void Update() {
        if (isTimerStarted == false) {
            return;
        }

        elapsedTime += Time.deltaTime;
        timerText.text = GetFormattedTime(elapsedTime);
    }

    public float GetElapsedTime() {
        return elapsedTime;
    }
    
    public string GetFormattedTime(float time) {
        string result = time.ToString("00:00");
        return result;
    }

    public void Clear() {
        isTimerStarted = false;
    }
}