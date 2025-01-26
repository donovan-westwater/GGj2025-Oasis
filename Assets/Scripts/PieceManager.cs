using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    GameObject[] piecePrefabs;
    [HideInInspector]
    public static bool isReady = false;
    // Start is called before the first frame update
    void Start()
    {
        piecePrefabs = Resources.LoadAll<GameObject>("Prefab/Test Pieces/");
    }
    public void GeneratePiece(int index)
    {
        Debug.Log("PIECES: " + piecePrefabs.Length+" "+index);
        if (index >= piecePrefabs.Length 
            || GridSystem.hoveredCell == null
            || !GridSystem.hoveredCell.isEmpty) return;
        GridSystem.hoveredCell.isEmpty = false;
        GridSystem.currentSelection = GameObject.Instantiate(piecePrefabs[index], new Vector3(0, 1, 0)
            , Quaternion.identity);
        GridSystem.currentSelection.transform.SetParent(GridSystem.hoveredCell.transform);
        GridSystem.currentSelection.transform.localPosition = new Vector3(0, 1, 0);
        GridSystem.currentSelection = null;
        isReady = false;
    }
}
