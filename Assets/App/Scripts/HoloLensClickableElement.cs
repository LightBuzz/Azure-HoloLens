using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoloLensClickableElement : MonoBehaviour, IInputClickHandler, IFocusable
{
    private bool clicked;
    private TextMesh text;
    private Vector3 originalSize;

    public event EventHandler OnClick;

    public TodoItem Item { get; set; }

    public void Setup(TodoItem item)
    {
        Item = item;

        text = GetComponentInChildren<TextMesh>();
        text.text = item.Text;

        originalSize = transform.localScale;
    }

    public void OnFocusEnter()
    {
        Debug.Log("Focus enter");

        if (!clicked)
        {
            StopAllCoroutines();
            StartCoroutine(Animate_GotFocus());
        }
    }

    public void OnFocusExit()
    {
        Debug.Log("Focus exit");

        if (!clicked)
        {
            StopAllCoroutines();
            StartCoroutine(Animate_LostFocus());
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Click");

        clicked = true;

        StopAllCoroutines();
        StartCoroutine(Animate_Delete());

        OnClick?.Invoke(this, new EventArgs());
    }

    private IEnumerator Animate_GotFocus()
    {
        while (transform.localScale.x < originalSize.x + 0.4f)
        {
            transform.localScale *= 1.01f;
            yield return null;
        }
    }

    private IEnumerator Animate_LostFocus()
    {
        while (transform.localScale.x > originalSize.x)
        {
            transform.localScale /= 1.01f;
            yield return null;
        }
    }

    private IEnumerator Animate_Delete()
    {
        while (transform.localScale.x > 0f)
        {
            transform.localScale /= 1.1f;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
