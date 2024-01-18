using Enums;
using UnityEngine;

namespace Tile{
    public class TileView : MonoBehaviour{
        [SerializeField] private TileNumber tileNumber;
        [SerializeField] private TileBackground tileBackground;
        [SerializeField] private TileBorders tileBorders;
        [SerializeField] private RectTransform pencilParent;

        public void Init(string numberToShow) {
            ShowNumber(numberToShow);
            tileBackground.Init();
        }
        
        public void ShowNumber(string visualNumber) {
            tileNumber.ShowNumber(visualNumber);
        }

        public void ShowWrongNumber(string visualNumber) {
            tileNumber.ShowWrongNumber(visualNumber);
        }
        
        public void ShowCorrectNumber(string visualNumber) {
            tileNumber.ShowCorrectNumber(visualNumber);
        }

        public void SetColor(TileBackgroundColorEnum colorEnum) {
            tileBackground.SetColor(colorEnum);
        }

        public void PlayBorderAnimation(TileBackgroundColorEnum colorEnum) {
            tileBorders.PlayAnimation(colorEnum);
        }

        public RectTransform GetPencilHolder() {
            return pencilParent;
        }
    }
}