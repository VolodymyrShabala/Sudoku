using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Tile{
    public class TileBackground : MonoBehaviour{
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color highlightedColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color wrongNumberColor;
        private Color defaultColor;

        public void Init() {
            defaultColor = backgroundImage.color;
        }

        public void SetColor(TileBackgroundColorEnum colorEnum) {
            Color newColor;
            switch (colorEnum) {
                case TileBackgroundColorEnum.Default:
                    newColor = defaultColor;
                    break;
                case TileBackgroundColorEnum.Highlighted:
                    newColor = highlightedColor;
                    break;
                case TileBackgroundColorEnum.Selected:
                    newColor = selectedColor;
                    break;
                case TileBackgroundColorEnum.WrongNumber:
                    newColor = wrongNumberColor;
                    break;
                default:
                    newColor = defaultColor;
                    break;
            }

            backgroundImage.color = newColor;
        }
    }
}