//The implementation is based on this article:http://rbarraza.com/html5-canvas-pageflip/
//As the rbarraza.com website is not live anymore you can get an archived version from web archive 
//or check an archived version that I uploaded on my website: https://dandarawy.com/html5-canvas-pageflip/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class LetterManager : MonoBehaviour {
    public Letter[] Letters;

    public Button ButtonLetterNext;
    public Button ButtonLetterPrev;

    public Button ButtonPageNext;
    public Button ButtonPagePrev;

    public Button ButtonExit;

    public FadeScreen FadeScreen;

    [Header("Debug options")]
    public int StartPage;
    public bool ForceCutsceneMode;

    private int _iLetterActive;
    private int MaxLetterUnlockedIndex;

    private bool _allowIncrementPage;
    private bool _isCutsceneActive;

    void Awake()
    {
        foreach (var letter in Letters)
            letter.Deactivate();

        _iLetterActive = -1;
        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        MaxLetterUnlockedIndex = (menuManager != null) ?
                                    menuManager.lettersUnlocked :
                                    6;

        var startPage = (StartPage > 0) ? StartPage - 1 : MaxLetterUnlockedIndex - 1;
        SetCurrentLetter(startPage);

        bool isCutscene = (menuManager != null) ? menuManager.IsRunningCutscene : false;
        if (isCutscene || ForceCutsceneMode)
        {
            SetCutsceneActive(true);
            FadeScreen.SnapToOpaque();
            FadeScreen.FadeToTransparent(1.0f);
        }
        else
        {
            FadeScreen.SnapToTransparent();
        }
    }

    public bool IsIncrementPageDisabled()
    {
        return !_allowIncrementPage;
    }

    public void EnableIncrementPage()
    {
        _allowIncrementPage = true;
        RefreshControls();
    }

    public bool IsCutsceneActive()
    {
        return _isCutsceneActive;
    }

    public void CompleteCutscene()
    {
        SetCutsceneActive(false);
    }

    public void Exit()
    {
        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        menuManager.ReturnToGameFromBook();
    }

    public void DecrementLetter()
    {
        Assert.IsTrue(CanDecrementLetter());
        SetCurrentLetter(_iLetterActive - 1);
    }

    public void IncrementLetter ()
    {
        Assert.IsTrue(CanIncrementLetter());
        SetCurrentLetter(_iLetterActive + 1);
    }

    public void IncrementPage ()
    {
        if (_isCutsceneActive)
        {
            _allowIncrementPage = false;
        }

        Letters[_iLetterActive].IncrementPage();
        RefreshControls();
    }

    public void DecrementPage ()
    {
        Letters[_iLetterActive].DecrementPage();
        RefreshControls();
    }
    private bool CanDecrementLetter()
    {
        return _iLetterActive > 0;
    }

    private bool CanIncrementLetter()
    {
        return _iLetterActive < System.Math.Min(MaxLetterUnlockedIndex, Letters.Length) - 1;
    }

    private void SetCurrentLetter(int iLetter)
    {
        if (_iLetterActive == iLetter) return;

        if (_iLetterActive >= 0 &&  _iLetterActive < Letters.Length)
            Letters[_iLetterActive].Deactivate();

        _iLetterActive = iLetter;

        if (_iLetterActive >= 0 && _iLetterActive < Letters.Length)
            Letters[_iLetterActive].Activate();

        RefreshControls();
    }

    private void SetCutsceneActive(bool isCutsceneActive)
    {
        if (_isCutsceneActive == isCutsceneActive) return;

        _isCutsceneActive = isCutsceneActive;

        RefreshControls();
    }

    private void RefreshControls()
    {
        ButtonLetterNext.gameObject.SetActive(CanIncrementLetter() && !_isCutsceneActive);
        ButtonLetterPrev.gameObject.SetActive(CanDecrementLetter() && !_isCutsceneActive);
        ButtonPageNext.gameObject.SetActive(Letters[_iLetterActive].CanIncrementPage() && _allowIncrementPage);
        ButtonPagePrev.gameObject.SetActive(Letters[_iLetterActive].CanDecrementPage() && !_isCutsceneActive);
        ButtonExit.gameObject.SetActive(!_isCutsceneActive);
    }
}
