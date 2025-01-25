using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "GridState/Create GridStateObject")]
public class GridStateObject : ScriptableObject
{
    [SerializeField]
    public int[] gridState = new int[9];
    [SerializeField]
    public string title = "";
    [SerializeField]
    public bool completed = false;
    [SerializeField]
    public Sprite icon;
}
