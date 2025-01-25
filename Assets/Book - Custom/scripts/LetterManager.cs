//The implementation is based on this article:http://rbarraza.com/html5-canvas-pageflip/
//As the rbarraza.com website is not live anymore you can get an archived version from web archive 
//or check an archived version that I uploaded on my website: https://dandarawy.com/html5-canvas-pageflip/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class LetterManager : MonoBehaviour {
    public Letter[] Letters;
    
    public int CurrentLetterIndex = 0;

    public Button ButtonLetterNext;
    public Button ButtonLetterPrev;

    public Button ButtonPageNext;
    public Button ButtonPagePrev;

    void Start()
    {
        RefreshControls();
    }

    public void Exit()
    {
        var menuManager = GameObject.FindObjectOfType<MenuManager>();
        menuManager.ReturnToGameFromBook();
    }

    public void DecrementLetter()
    {
        Assert.IsTrue(CanDecrementLetter());
        SetCurrentLetter(CurrentLetterIndex - 1);
    }

    public void IncrementLetter ()
    {
        Assert.IsTrue(CanIncrementLetter());
        SetCurrentLetter(CurrentLetterIndex + 1);
    }

    public void IncrementPage ()
    {
        Letters[CurrentLetterIndex].IncrementPage();
        RefreshControls();
    }

    public void DecrementPage ()
    {
        Letters[CurrentLetterIndex].DecrementPage();
        RefreshControls();
    }
    private bool CanDecrementLetter()
    {
        return CurrentLetterIndex > 0;
    }

    private bool CanIncrementLetter()
    {
        return CurrentLetterIndex < Letters.Length - 1;
    }

    private void SetCurrentLetter(int iLetter)
    {
        if (CurrentLetterIndex == iLetter) return;

        Letters[CurrentLetterIndex].Deactivate();

        CurrentLetterIndex = iLetter;

        Letters[CurrentLetterIndex].Activate();

        RefreshControls();
    }

    private void RefreshControls()
    {
        ButtonLetterNext.gameObject.SetActive(CanIncrementLetter());
        ButtonLetterPrev.gameObject.SetActive(CanDecrementLetter());
        ButtonPageNext.gameObject.SetActive(Letters[CurrentLetterIndex].CanIncrementPage());
        ButtonPagePrev.gameObject.SetActive(Letters[CurrentLetterIndex].CanDecrementPage());
    }
}
