using System;
using System.Collections;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class TextLoader : MonoBehaviour
{
    public TextAsset textAsset; // Drag your TextAsset here in the Inspector
    private TextMeshProUGUI textMeshPro; // Reference to your TextMeshPro component

    public void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textAsset != null && textMeshPro != null)
        {
            textMeshPro.text = textAsset.text;
        }
        else
        {
            Debug.LogWarning("Please assign a TextAsset and TextMeshPro component.");
        }
    }
}