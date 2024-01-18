using Data;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu{
    public class DifficultySelection : MonoBehaviour{
        [SerializeField] private LevelDatasHolder levelDatasHolder;
        [SerializeField] private LevelDifficulty levelDifficultyPrefab;
        private LevelDifficulty[] levelDifficulties;
        
        private void Start() {
            int length = levelDatasHolder.GetDatasLength();
            levelDifficulties = new LevelDifficulty[length];
            for (int i = 0; i < length; i++) { 
                levelDifficulties[i] = Instantiate(levelDifficultyPrefab, transform);
                levelDifficulties[i].Init(this, i, levelDatasHolder.GetDifficultyName(i));
            }
        }

        public void DifficultySelected(int index) {
            LevelSave.SaveLevelSelected(index);
            Clear();
            SceneManager.LoadScene("GameScene");
        }

        private void Clear() {
            foreach (LevelDifficulty levelDifficulty in levelDifficulties) {
                levelDifficulty.Clear();
            }
        }
    }
}