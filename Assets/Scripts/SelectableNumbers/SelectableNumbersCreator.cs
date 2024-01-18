using System.Collections.Generic;
using Data;
using UnityEngine;

namespace SelectableNumbers{
    public class SelectableNumbersCreator{
        public SelectableNumber[] Create(SelectableNumber prefab, Transform parent, LevelData levelData, int[] amountLeftArray) {
            List<string> allVisualNumbers = levelData.GetAllVisualNumbers();
            int length = allVisualNumbers.Count;
            SelectableNumber[] selectableNumbers = new SelectableNumber[length];
            for (int i = 0; i < length; i++) {
                selectableNumbers[i] = Object.Instantiate(prefab, parent);
                selectableNumbers[i].Init(i, allVisualNumbers[i], amountLeftArray[i]);
            }

            return selectableNumbers;
        }
    }
}