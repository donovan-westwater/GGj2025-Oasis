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
    public void OnPointerDown(PointerEventData eventData)
    {
        PieceManager.isReady = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonRelease.Invoke();
    }
}
