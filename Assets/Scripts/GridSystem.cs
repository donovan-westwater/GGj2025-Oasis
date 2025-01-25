using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour
{
    public System.DateTime date;
    public GameObject counterObj;
    public GameObject dateObj;
    TextMeshProUGUI counterDisplay;
    TextMeshProUGUI dateDisplay;
    int[] gridState = new int[9];
    [HideInInspector]
    public int submissionCount = 0;
    [HideInInspector]
    public static GameObject currentSelection = null;
    [HideInInspector]
    public static GridCell hoveredCell = null;
    // Start is called before the first frame update
    void Start()
    {
        date = System.DateTime.Now;
        counterDisplay = counterObj.GetComponent<TextMeshProUGUI>();
        dateDisplay = dateObj.GetComponent<TextMeshProUGUI>();
    }
    //Go over each cell and write down what pieace in the cell [PLACEHOLDER]
    void ReadBoardState()
    {
        Debug.Log("Reading the board state happens here!");
    }
    public void Sumbit()
    {
        submissionCount++;
        date = date.AddDays(7.0);
        counterDisplay.text = submissionCount.ToString();
        dateDisplay.text = date.ToString("MM/dd/yyy");
    }
    //Clears the board. Assumes columns can only have 1 child at a time
    void ClearBoard()
    {
        gridState = new int[9];
        foreach(Transform row in this.transform)
        {
            foreach(Transform col in row)
            {
                if (col.transform.childCount > 0)
                {
                    GridCell cell = col.GetComponent<GridCell>();
                    cell.isEmpty = true;
                    cell.isSelected = false;
                    cell.ResetColor();
                    Destroy(col.transform.GetChild(0).gameObject);
                }
            
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearBoard();
        }
    }
}
