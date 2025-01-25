using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class Letter : MonoBehaviour {
    public GameObject[] Pages;
    public int CurrentPageIndex = 0;
    public bool IsLocked = true;

    void Start()
    {
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        SetCurrentPage(0);
    }

    public void Deactivate()
    {
        SetCurrentPage(0);
        this.gameObject.SetActive(false);
    }

    public bool CanDecrementPage()
    {
        return CurrentPageIndex > 0;
    }

    public bool CanIncrementPage()
    {
        return CurrentPageIndex < Pages.Length - 1;
    }

    public void DecrementPage()
    {
        Assert.IsTrue(CanDecrementPage());
        SetCurrentPage(CurrentPageIndex - 1);
    }

    public void IncrementPage()
    {
        Assert.IsTrue(CanIncrementPage());
        SetCurrentPage(CurrentPageIndex + 1);
    }

    private void SetCurrentPage(int iPage)
    {
        if (CurrentPageIndex == iPage) return;

        Pages[CurrentPageIndex].gameObject.SetActive(false);

        CurrentPageIndex = iPage;

        Pages[CurrentPageIndex].gameObject.SetActive(true);
    }
}
