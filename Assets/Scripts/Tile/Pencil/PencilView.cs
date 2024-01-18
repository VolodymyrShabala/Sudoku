using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tile.Pencil{
    public class PencilView : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI pencilText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color highlightColor;
        private Color defaultColor;
        
        public void Init(string value) {
            pencilText.text = value;
            pencilText.enabled = false;
            defaultColor = backgroundImage.color;
        }

        public void Show() {
            pencilText.enabled = true;
        }

        public void Hide() {
            pencilText.enabled = false;
        }
        
        public bool IsShowing() {
            bool result = pencilText.enabled;
            return result;
        }

        public void Highlight(bool highlight) {
            backgroundImage.color = highlight ? highlightColor : defaultColor;
        }
    }
}