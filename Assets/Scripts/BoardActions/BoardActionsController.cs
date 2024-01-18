using UnityEngine;

namespace BoardActions{
    public class BoardActionsController : MonoBehaviour{
        [SerializeField] private BoardAction[] boardActions;

        public void Init() {
            foreach (BoardAction boardAction in boardActions) {
                boardAction.Init();
            }
        }

        public void Clear() {
            foreach (BoardAction boardAction in boardActions) {
                boardAction.Clear();
            }
        }
    }
}