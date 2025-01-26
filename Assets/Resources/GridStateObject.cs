using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GridState/Create GridStateObject")]
public class GridStateObject : ScriptableObject
{
    [SerializeField]
    public int[] gridState = new int[9];
    [SerializeField]
    public string title = "";
    [SerializeField]
    public bool completed = false;
}
