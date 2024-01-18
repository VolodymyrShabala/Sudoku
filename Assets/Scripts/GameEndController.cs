using Events;
using GameOver;
using SaveSystem;
using UnityEngine.SceneManagement;

public class GameEndController{
    private readonly PopupController popupController;
    private readonly TimerController timerController;

    public GameEndController(PopupController popup, TimerController timer) {
        popupController = popup;
        timerController = timer;
        
        EventSystem.Subscribe(EventKey.LevelFailed, LevelFailed);
        EventSystem.Subscribe(EventKey.LevelRestart, LevelRestart);
        EventSystem.Subscribe(EventKey.LevelQuit, LevelQuit);
        EventSystem.Subscribe(EventKey.LevelWon, LevelWon);
    }
    
    private void LevelWon(BaseEvent baseEvent) {
        int levelIndex = LevelSave.GetSaveLevelIndex();
        float time = timerController.GetElapsedTime();
        popupController.ShowGameWonPopup();
        LevelSave.SaveBestTimerForLevel(levelIndex, time);
    }
    
    private void LevelRestart(BaseEvent baseEvent) {
        EventSystem.Trigger(new ClearLevelEvent());
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    
    private void LevelFailed(BaseEvent baseEvent) {
        popupController.ShowLevelFailed();
    }

    private void LevelQuit(BaseEvent baseEvent) {
        EventSystem.Trigger(new ClearLevelEvent());
        SceneManager.LoadScene("MainMenu");
    }

    public void Clear() {
        EventSystem.Subscribe(EventKey.LevelFailed, LevelFailed);
        EventSystem.Subscribe(EventKey.LevelRestart, LevelRestart);
        EventSystem.Subscribe(EventKey.LevelQuit, LevelQuit);
        EventSystem.Subscribe(EventKey.LevelWon, LevelWon);
    }
}