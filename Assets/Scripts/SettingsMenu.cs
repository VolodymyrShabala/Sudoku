using Events;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    public void Init() {
        restartButton.onClick.AddListener(RestartButtonClicked);
        continueButton.onClick.AddListener(ContinueButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }
    
    private void RestartButtonClicked() {
        EventSystem.Trigger(new LevelRestartEvent());
    }

    private void ContinueButtonClicked() {
        gameObject.SetActive(false);
        EventSystem.Trigger(new PauseGameEvent(true));
    }

    private void QuitButtonClicked() {
        EventSystem.Trigger(new LeveQuitEvent());
    }

    public void Open() {
        gameObject.SetActive(true);
        EventSystem.Trigger(new PauseGameEvent(false));
    }

    public void Clear() {
        restartButton.onClick.RemoveListener(RestartButtonClicked);
        continueButton.onClick.RemoveListener(ContinueButtonClicked);
        quitButton.onClick.RemoveListener(QuitButtonClicked);
    }
}