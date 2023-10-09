using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image joystickBackground;
    [SerializeField] private Image joystick;
    [SerializeField] private Image joystickArea;

    private Vector2 joystickBackgroundStartPosition;
    private Vector2 inputVector;
    private void Start() {
        joystickBackgroundStartPosition = joystickBackground.rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 joystickPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, eventData.position, null, out joystickPosition)) {
            joystickPosition.x = (joystickPosition.x * 2 / joystickBackground.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y * 2 / joystickBackground.rectTransform.sizeDelta.y);

            inputVector = new Vector2(joystickPosition.x, joystickPosition.y);
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;
            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.rectTransform.sizeDelta.x /2), inputVector.y * (joystickBackground.rectTransform.sizeDelta.y /2));
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        Vector2 joystickBackgroundPosition;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickArea.rectTransform, eventData.position, null, out joystickBackgroundPosition)) {
            joystickBackground.rectTransform.position = new Vector2(joystickBackgroundPosition.x + joystickArea.rectTransform.position.x, joystickBackgroundPosition.y + joystickArea.rectTransform.position.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        joystickBackground.rectTransform.position = joystickBackgroundStartPosition;

        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public Vector2 GetMovementVector() {
        return inputVector;
    }
}
