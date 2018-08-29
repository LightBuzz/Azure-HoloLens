using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableElement : MonoBehaviour, IInputClickHandler, IPointerClickHandler, IDragHandler, IFocusable
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragged!");
    }

    public void OnFocusEnter()
    {
        Debug.Log("Focus enter");
    }

    public void OnFocusExit()
    {
        Debug.Log("Focus exit");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        transform.localScale /= 1.2f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale /= 1.2f;
    }
}
