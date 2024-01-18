using Events;
using UnityEngine;
using UnityEngine.UI;

namespace BoardActions{
    public class BoardActionPencil : BoardAction{
        [SerializeField] private Button button;
        private bool isPencilSelected;
        
        public override void Init() {
            button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked() {
            isPencilSelected = !isPencilSelected;
            EventSystem.Trigger(new PencilSelectedEvent(isPencilSelected));
        }

        public override void Clear() {
            button.onClick.RemoveListener(ButtonClicked);
        }
    }
}