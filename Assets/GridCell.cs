using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected = false;
    [HideInInspector]
    public bool isHovered = false;
    Material mat;
    MeshRenderer renderer;
    Color defaultColor;
    private void Start()
    {
        renderer = this.transform.GetComponent<MeshRenderer>();
        mat = renderer.material;
        defaultColor = mat.color;
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        GridSystem.hoveredCell = this;
        Debug.Log(this.transform.parent.name + " " + this.name + " " + isHovered);
        mat.color = Color.red;
        renderer.material = mat;
        //Debug.Log(this.transform.parent.name+" "+this.name+" "+isSelected);
    }
    private void OnMouseExit()
    {
        isHovered = false;
        GridSystem.hoveredCell = null;
        Debug.Log(this.transform.parent.name + " " + this.name + " " + isHovered);
        if (isSelected)
        {
            mat.color = Color.green;
            renderer.material = mat;
            return;
        }
        mat.color = this.defaultColor;
        renderer.material = mat;
        //Debug.Log("FREEDOM!");
    }
    private void OnMouseDown()
    {
        isSelected = true;
        if (this.transform.childCount > 0)
            GridSystem.currentSelection = this.transform.GetChild(0).gameObject;
        mat.color = Color.green;
        renderer.material = mat;
    }
    private void OnMouseUp()
    {
        isSelected = false;
        Debug.Log(this.transform.parent.name + " " + this.name + " MouseUP");
        if (GridSystem.currentSelection != null && GridSystem.hoveredCell != null)
        {
            GridSystem.currentSelection.transform.SetParent(GridSystem.hoveredCell.transform);
            GridSystem.currentSelection.transform.localPosition = new Vector3(0, 1f, 0);
            GridSystem.currentSelection = null;
        }
        mat.color = this.defaultColor;
        renderer.material = mat;
    }
}
