using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Tile.Pencil{
    public class PencilSpawner{
        private readonly PencilView prefab;
        private readonly LevelData levelData;
        private readonly Vector2Int squareLayout;
        private readonly int sqrtBorderSide; // V: Used to calculate where to spawn a pencil object

        public PencilSpawner(PencilView pencilPrefab, LevelData data) {
            prefab = pencilPrefab;
            levelData = data;
            squareLayout = levelData.GetSquareLayout();
            sqrtBorderSide = Mathf.CeilToInt(Mathf.Sqrt(levelData.GetBoardSize().x));
        }

        public PencilController Create(RectTransform parent, IReadOnlyList<int> possibleNumberIndices) {
            int length = possibleNumberIndices.Count;
            PencilView[] pencilViews = new PencilView[length];
            Dictionary<int, int> indexToNumberDictionary = new();

            if (length > 0) {
                Rect rect = parent.rect;
                float width = rect.width / squareLayout.x;
                float height = rect.height / squareLayout.y;

                for (int i = 0; i < length; i++) {
                    int index = possibleNumberIndices[i];
                    pencilViews[i] = CreateView(width, height, parent, index);
                    indexToNumberDictionary.Add(index, i);
                }
            }

            PencilModel pencilModel = new(indexToNumberDictionary);
            PencilController pencilController = new(pencilModel, pencilViews);
            return pencilController;
        }

        private PencilView CreateView(float width, float height, Transform parent, int index) {
            PencilView pencilView = Object.Instantiate(prefab, parent);
            AdjustViewRectTransform(pencilView, index, width, height);
            InitView(pencilView, index);
            return pencilView;
        }

        private void AdjustViewRectTransform(Component pencilView, int index, float width, float height) {
            int row = index / sqrtBorderSide;
            int column = index % sqrtBorderSide;
            RectTransform pencilRect = pencilView.GetComponent<RectTransform>();
            pencilRect.sizeDelta = new Vector2(width, height);
            pencilRect.anchoredPosition = new Vector2(width * column, height * -row);
        }

        private void InitView(PencilView pencilView, int index) {
            string pencilNumber = levelData.GetVisualNumber(index);
            pencilView.Init(pencilNumber);
        }
    }
}