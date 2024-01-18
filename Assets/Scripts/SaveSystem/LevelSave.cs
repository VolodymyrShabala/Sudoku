using UnityEngine;

namespace SaveSystem{
    public static class LevelSave{
        private const string levelSaveName = "SavedLeveIndex";
        
        public static void SaveLevelSelected(int index) {
            PlayerPrefs.SetInt(levelSaveName, index);
        }

        public static int GetSaveLevelIndex() {
            return PlayerPrefs.GetInt(levelSaveName);
        }

        public static void SaveBestTimerForLevel(int levelIndex, float time) {
            string key = $"{levelSaveName}{levelIndex}";
            PlayerPrefs.SetFloat(key, time);
        }

        public static float GetBestTimerForLevel(int levelIndex) {
            string key = $"{levelSaveName}{levelIndex}";
            return PlayerPrefs.GetFloat(key);
        }

        public static bool HasBestTimeForLevel(int levelIndex) {
            string key = $"{levelSaveName}{levelIndex}";
            return PlayerPrefs.HasKey(key);
        }
    }
}