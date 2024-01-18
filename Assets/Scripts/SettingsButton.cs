using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour{
    [SerializeField] private Button settingsButton;
    [SerializeField] private SettingsMenu settingsMenu;
    
    public void Init() {
        settingsButton.onClick.AddListener(OpenSettings);
        settingsMenu.Init();
    }

    private void OpenSettings() {
        settingsMenu.Open();
    }

    public void Clear() {
        settingsButton.onClick.RemoveListener(OpenSettings);
        settingsMenu.Clear();
    }
}