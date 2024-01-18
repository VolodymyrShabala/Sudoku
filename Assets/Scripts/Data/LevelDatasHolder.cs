using UnityEngine;

namespace Data{
    [CreateAssetMenu(fileName = "LevelDatasHodler", menuName = "LevelData/Holder", order = 1)]
    public class LevelDatasHolder : ScriptableObject{
        [SerializeField] private LevelData[] levelDatas;

        public int GetDatasLength() {
            return levelDatas.Length;
        }

        public LevelData GetLevel(int index) {
            return levelDatas[index];
        }

        public string GetDifficultyName(int index) {
            return levelDatas[index].GetDifficultyName();
        }
    }
}