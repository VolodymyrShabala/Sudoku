using Events;
using UnityEngine;

public class InputController : MonoBehaviour{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera sceneCamera;
    private bool isInputEnabled = true;
    
    public void Init() {
        isInputEnabled = true;
        EventSystem.Subscribe(EventKey.PauseGame, PauseInput);
    }

    private void PauseInput(BaseEvent baseEvent) {
        PauseGameEvent pauseGameEvent = (PauseGameEvent)baseEvent;
        isInputEnabled = pauseGameEvent.state;
    }

    private void Update() {
        if (isInputEnabled == false) {
            return;
        }
        
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Input.mousePosition;
            Camera myCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : sceneCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, myCamera,
                                                                    out Vector2 position2D);
            Vector3 position = canvas.transform.TransformPoint(position2D);
            EventSystem.Trigger(new PlayerInputEvent(position));
        }
    }

    public void Clear() {
        EventSystem.Unsubscribe(EventKey.PauseGame, PauseInput);
    }
}