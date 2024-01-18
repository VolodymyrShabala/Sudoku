using UnityEngine;

namespace Events{
    public class PlayerInputEvent : BaseEvent{
        public Vector3 inputPosition;

        public PlayerInputEvent(Vector3 position) : base(EventKey.PlayerInput) {
            inputPosition = position;
        }
    }
}