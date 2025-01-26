using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceButton : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField]
    UnityEvent OnButtonRelease;
    Vector2 size;
    public void Start()
    {
        size = GetComponent<RectTransform>().sizeDelta;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PieceManager.isReady = true;
        GetComponent<RectTransform>().sizeDelta *= .8f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonRelease.Invoke();
        GetComponent<RectTransform>().sizeDelta /= .8f;
    }
}
