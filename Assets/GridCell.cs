using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected = false;
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
        mat.color = Color.red;
        renderer.material = mat;
        //Debug.Log(this.transform.parent.name+" "+this.name+" "+isSelected);
    }
    private void OnMouseExit()
    {
        if (isSelected) return;
        mat.color = this.defaultColor;
        renderer.material = mat;
        //Debug.Log("FREEDOM!");
    }
    private void OnMouseDown()
    {
        isSelected = true;
        mat.color = Color.green;
        renderer.material = mat;
    }
    private void OnMouseUp()
    {
        isSelected = false;
        mat.color = this.defaultColor;
        renderer.material = mat;
    }
}
