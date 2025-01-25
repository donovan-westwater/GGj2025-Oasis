using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pieceMenu;
    public GameObject mainGameUI;
    public GameObject dome;
    public Camera mainCam;
    [HideInInspector]
    public int lettersUnlocked = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ReturnToGameFromBook()
    {
        mainGameUI.SetActive(true);
        dome.SetActive(true);
        foreach (Transform c in this.transform)
        {
            c.gameObject.SetActive(true);
        }
        mainCam.transform.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(1);
    }
    public void LoadBookMenu()
    {
        mainGameUI.SetActive(false);
        dome.SetActive(false);
        foreach(Transform c in this.transform)
        {
            c.gameObject.SetActive(false);
        }
        mainCam.transform.gameObject.SetActive(false);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            pieceMenu.SetActive(!pieceMenu.activeSelf);
        }
    }
}
