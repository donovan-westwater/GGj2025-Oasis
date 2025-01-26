using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshPro))]
public class Letter : MonoBehaviour {
    public int PageCount;
    public TextMeshProUGUI TextMesh;

    // NOTE (bobbyz) I'm sure there's a smarter way to do this, but hardcoding these for now

    public int[] CustsceneCharacterThreshold;

    public void Update()
    {
        UpdateCutsceneAutoPageIncrement();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        SetCurrentPage(1);
    }

    public void Deactivate()
    {
        SetCurrentPage(1);
        this.gameObject.SetActive(false);
    }

    public bool CanDecrementPage()
    {
        return TextMesh.pageToDisplay > 1;
    }

    public bool CanIncrementPage()
    {
        return TextMesh.pageToDisplay < PageCount;
    }

    public void DecrementPage()
    {
        Assert.IsTrue(CanDecrementPage());
        SetCurrentPage(TextMesh.pageToDisplay - 1);
    }

    public void IncrementPage()
    {
        Assert.IsTrue(CanIncrementPage());
        SetCurrentPage(TextMesh.pageToDisplay + 1);
    }

    private void SetCurrentPage(int iPage)
    {
        if (TextMesh.pageToDisplay == iPage) return;

        TextMesh.pageToDisplay = iPage;
    }

    private void UpdateCutsceneAutoPageIncrement()
    {
        var letterManager = GameObject.FindObjectOfType<LetterManager>();
        if (!letterManager.IsIncrementPageDisabled())
            return;

        if (TextMesh.pageToDisplay >= PageCount)
            return;

        var threshold = CustsceneCharacterThreshold[TextMesh.pageToDisplay - 1];
        if (TextMesh.maxVisibleCharacters > threshold)
        {
            letterManager.EnableIncrementPage();
        }
    }
}
