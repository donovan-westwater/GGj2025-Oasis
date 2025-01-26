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

    [SerializeField] private AudioClip pickUp;
    [SerializeField] private AudioClip pickDown;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PieceManager.isReady = true;
        PlaySound(pickUp);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnButtonRelease.Invoke();
        PlaySound(pickDown);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
