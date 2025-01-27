using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected = false;
    [HideInInspector]
    public bool isHovered = false;
    [HideInInspector]
    public bool isEmpty = true;
    Material mat;
    MeshRenderer renderer;
    Color defaultColor;
    Color selectedColor;
    Color hoveredColor;
    public AudioClip pickUp;
    public AudioClip putDown;
    private AudioSource audioSource;
    private void Start()
    {
        renderer = this.transform.GetComponent<MeshRenderer>();
        mat = renderer.material;
        defaultColor = mat.color;
        selectedColor = Color.Lerp(defaultColor, Color.black, .25f);
        hoveredColor = Color.Lerp(defaultColor, Color.white, .15f);
        if (this.transform.childCount > 0) isEmpty = false;
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void ResetColor()
    {
        if (isSelected)
        {
            mat.color = this.selectedColor;
        }else if (isHovered)
        {
            mat.color = this.hoveredColor;
        }
        else
        {
            mat.color = defaultColor;
        }
        renderer.material = mat;

    }
    private void OnMouseEnter()
    {
        isHovered = true;
        GridSystem.hoveredCell = this;
        Debug.Log(this.transform.parent.name + " " + this.name + " " + isHovered);
        mat.color = this.hoveredColor;
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
            mat.color = this.selectedColor;
            renderer.material = mat;
            return;
        }
        mat.color = this.defaultColor;
        renderer.material = mat;
        //Debug.Log("FREEDOM!");
    }
    private void OnMouseDown()
    {
        audioSource.PlayOneShot(pickUp);
        isSelected = true;
        if (this.transform.childCount > 0)
            GridSystem.currentSelection = this.transform.GetChild(0).gameObject;
        mat.color = this.selectedColor;
        renderer.material = mat;
    }
    private void OnMouseUp()
    {
        audioSource.PlayOneShot(putDown);
        isSelected = false;
        Debug.Log(this.transform.parent.name + " " + this.name + " MouseUP");
        if (GridSystem.currentSelection != null && GridSystem.hoveredCell != null
            && GridSystem.hoveredCell.isEmpty)
        {
            this.isEmpty = true;
            GridSystem.currentSelection.transform.SetParent(GridSystem.hoveredCell.transform);
            GridSystem.currentSelection.transform.localPosition = new Vector3(0, 1f, 0);  
            GridSystem.hoveredCell.isEmpty = false;
        }
        if(GridSystem.currentSelection !=null) GridSystem.currentSelection.transform.localPosition = new Vector3(0, 1f, 0);
        GridSystem.currentSelection = null;
        mat.color = this.defaultColor;
        renderer.material = mat;
    }
}
