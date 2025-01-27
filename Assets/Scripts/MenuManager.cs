using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pieceMenu;
    public GameObject memoryMenu;
    public GameObject mainGameUI;
    public GameObject dome;
    public Camera mainCam;
    public GameObject hideButton;
    public GameObject expandButton;
    public AudioClip click;
    private AudioSource audioSource;

    [HideInInspector]
    public int lettersUnlocked { get; private set; }

    [HideInInspector]
    public bool IsRunningCutscene { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        lettersUnlocked = 0;
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = click;
        audioSource.playOnAwake = false;
        UnlockLetterAndLoadCustscene();
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
        RectTransform rectTransform = pieceMenu.GetComponent<RectTransform>();  
        rectTransform.anchoredPosition -= new Vector2(175, 0);
    }
    public void ExpandPanel()
    {
        PlayClickSound();
        hideButton.SetActive(true);
        expandButton.SetActive(false);
        RectTransform rectTransform = pieceMenu.GetComponent<RectTransform>();  
        rectTransform.anchoredPosition += new Vector2(175, 0);
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

    public void UnlockLetterAndLoadCustscene()
    {
        PlayClickSound();
        lettersUnlocked++;
        IsRunningCutscene = true;
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

    public void ReturnToGameFromBook()
    {
        PlayClickSound();
        IsRunningCutscene = false;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            pieceMenu.SetActive(!pieceMenu.activeSelf);
        }
    }
}
