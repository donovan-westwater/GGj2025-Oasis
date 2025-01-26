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

    private int _iLetterActive;
    private int MaxLetterUnlockedIndex;

    void Start()
    {
        _iLetterActive = -1;
        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        MaxLetterUnlockedIndex = (menuManager != null) ?
                                    menuManager.lettersUnlocked :
                                    1;

        SetCurrentLetter(MaxLetterUnlockedIndex - 1);
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

    private void RefreshControls()
    {
        ButtonLetterNext.gameObject.SetActive(CanIncrementLetter());
        ButtonLetterPrev.gameObject.SetActive(CanDecrementLetter());
        ButtonPageNext.gameObject.SetActive(Letters[_iLetterActive].CanIncrementPage());
        ButtonPagePrev.gameObject.SetActive(Letters[_iLetterActive].CanDecrementPage());
    }
}
