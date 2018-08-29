using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoloLensClickableElement : MonoBehaviour, IInputClickHandler, IPointerClickHandler, IFocusable
{
    [SerializeField] private TextMesh text;

    public UnityEvent OnClick;

    public TodoItem Item { get; set; }

    public void Setup(TodoItem item)
    {
        Item = item;

        if (text == null)
        {
            text = GetComponentInChildren<TextMesh>();
        }
        text.text = item.Text;
    }

    public void OnFocusEnter()
    {
        Debug.Log("Focus enter");
        transform.localScale *= 1.2f;
    }

    public void OnFocusExit()
    {
        Debug.Log("Focus exit");
        transform.localScale /= 1.2f;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Clicked");
        OnClick?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        OnClick?.Invoke();
    }
}
