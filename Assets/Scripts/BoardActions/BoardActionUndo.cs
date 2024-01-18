using Events;
using UnityEngine;
using UnityEngine.UI;

namespace BoardActions{
    public class BoardActionUndo : BoardAction{
        [SerializeField] private Button button;
        
        public override void Init() {
            button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked() {
            EventSystem.Trigger(new UndoButtonClickedEvent());
        }

        public override void Clear() {
            button.onClick.RemoveListener(ButtonClicked);
        }
    }
}