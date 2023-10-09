using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image shootButtonArea;
    [SerializeField] private Image shootButtonImage;
    [SerializeField] private Color activeButtonColor;
    [SerializeField] private Color inactiveButtonColor;

    private bool isShoot = false;

    public void OnPointerDown(PointerEventData eventData) {
        ClickEffect();

        Vector2 shootAreaPosition;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(shootButtonArea.rectTransform, eventData.position, null, out shootAreaPosition)) {
            isShoot = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {

        ClickEffect();
        isShoot = false;
    }

    private void ClickEffect() {
        if(!isShoot) shootButtonImage.color = activeButtonColor;
        else shootButtonImage.color = inactiveButtonColor;
    }

    public bool IsShoot() {
        return isShoot;
    }
}
