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
    [HideInInspector]
    public int lettersUnlocked = 0;
    public GameObject hideButton;
    public GameObject expandButton;
    public AudioClip click;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void PlayClickSound()
    {
        if (click != null)
        {
            audioSource.PlayOneShot(click);
        }
    }
    public void HidePanel()
    {
        PlayClickSound();
        hideButton.SetActive(false);
        expandButton.SetActive(true);
        pieceMenu.transform.position -= new Vector3(100,0,0);
    }
    public void ExpandPanel()
    {
        PlayClickSound();
        hideButton.SetActive(true);
        expandButton.SetActive(false);
        pieceMenu.transform.position += new Vector3(100, 0, 0);
    }
    public void ReturnToGameFromBook()
    {
        PlayClickSound();
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
        PlayClickSound();
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
        PlayClickSound();
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
        PlayClickSound();
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
