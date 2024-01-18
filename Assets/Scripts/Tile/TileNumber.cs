using TMPro;
using UnityEngine;

namespace Tile{
    public class TileNumber : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Color wrongNumberColor;
        [SerializeField] private Color correctNumberColor;
        
        public void ShowNumber(string visualNumber) {
            numberText.text = visualNumber;
        }

        public void ShowWrongNumber(string visualNumber) {
            ShowNumber(visualNumber);
            numberText.color = wrongNumberColor;
        }

        public void ShowCorrectNumber(string visualNumber) {
            ShowNumber(visualNumber);
            numberText.color = correctNumberColor;
        }
    }
}