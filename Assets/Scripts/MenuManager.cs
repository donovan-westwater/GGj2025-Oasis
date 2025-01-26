using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pieceMenu;
    public GameObject memoryMenu;
    public GameObject mainGameUI;
    public GameObject dome;
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ReturnToGameFromBook()
    {
        mainGameUI.SetActive(true);
        dome.SetActive(true);
        memoryMenu.SetActive(false);
        foreach (Transform c in this.transform)
        {
            c.gameObject.SetActive(true);
        }
        mainCam.transform.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(1);
    }
    public void OpenMemoryMenu()
    {
        foreach (Transform u in mainGameUI.transform)
        {
            u.gameObject.SetActive(false);
        }
        dome.SetActive(false);
        memoryMenu.SetActive(true);
        foreach (Transform c in this.transform)
        {
            c.gameObject.SetActive(false);
        }
    }
    public void CloseMemoryMenu()
    {
        foreach (Transform u in mainGameUI.transform)
        {
            u.gameObject.SetActive(true);
        }
        dome.SetActive(true);
        memoryMenu.SetActive(false);
        foreach (Transform c in this.transform)
        {
            c.gameObject.SetActive(true);
        }
    }
    public void LoadBookMenu()
    {
        mainGameUI.SetActive(false);
        dome.SetActive(false);
        memoryMenu.SetActive(false);
        foreach (Transform c in this.transform)
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
