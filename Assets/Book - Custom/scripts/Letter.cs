using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshPro))]
public class Letter : MonoBehaviour {
    public int PageCount;
    public TextMeshProUGUI TextMesh;

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
}
