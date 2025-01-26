using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMemory : MonoBehaviour
{
    [SerializeField]
    GridStateObject achievement;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Image imageContainer;
    //[SerializeField]
    //Image iconContainer;
    Sprite tex;
    // Start is called before the first frame update
    void Start()
    {
        tex = Resources.Load<Sprite>("checkmark");
        //iconContainer.sprite = achievement.icon;
        text.text = achievement.title;

    }

    // Update is called once per frame
    void Update()
    {
        if (achievement.completed)
        {
            imageContainer.gameObject.SetActive(true);
        }
        else
        {
            imageContainer.gameObject.SetActive(false);
        }
    }
}
