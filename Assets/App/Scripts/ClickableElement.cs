using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableElement : MonoBehaviour, IInputClickHandler, IPointerClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Clicked!");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked!");
    }
}
