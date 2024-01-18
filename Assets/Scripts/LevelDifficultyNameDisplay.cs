using TMPro;
using UnityEngine;

public class LevelDifficultyNameDisplay : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI difficultyText;

    public void Init(string levelDifficultyName) {
        difficultyText.text = levelDifficultyName;
    }
}