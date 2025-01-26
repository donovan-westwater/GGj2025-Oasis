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

    [HideInInspector]
    public int lettersUnlocked { get; private set; }

    [HideInInspector]
    public bool IsRunningCutscene { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        lettersUnlocked = 0;
        UnlockLetterAndLoadCustscene();
    }

    public void HidePanel()
    {
        hideButton.SetActive(false);
        expandButton.SetActive(true);
        pieceMenu.transform.position -= new Vector3(100,0,0);
    }
    public void ExpandPanel()
    {
        hideButton.SetActive(true);
        expandButton.SetActive(false);
        pieceMenu.transform.position += new Vector3(100, 0, 0);
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

    public void UnlockLetterAndLoadCustscene()
    {
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
