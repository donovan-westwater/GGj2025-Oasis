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
    GridStateObject[] rawObjectArray;
    MenuManager menuManagerRef;
    public GameObject[] unlocks;
    public AudioClip submit;
    public AudioClip clear;
    public AudioClip achievement;
    private AudioSource audioSource;
    private bool isSubmitting = false;

    // Start is called before the first frame update
    void Start()
    {
        AssignDate();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        counterDisplay = counterObj.GetComponent<TextMeshProUGUI>();
        dateDisplay = dateObj.GetComponent<TextMeshProUGUI>();
        rawObjectArray = Resources.LoadAll<GridStateObject>("GridStates/");
        foreach(GridStateObject g in rawObjectArray)
        {
            g.completed = false;
        }
        counterDisplay.text = submissionCount.ToString();
        dateDisplay.text = date.ToString("MMMM d, yyy");
        menuManagerRef = this.gameObject.GetComponent<MenuManager>();
    }
    void AssignDate()
    {
        int f = submissionCount;
        switch (f) { 
            case 0:
                date = new System.DateTime(2075, 1, 24);
                break;
            case 1:
                date = new System.DateTime(2075, 4, 9);
                break;
            case 2:
                date = new System.DateTime(2075, 7, 13);
                break;
            case 3:
                date = new System.DateTime(2075, 10, 31);
                break;
            case 4:
                date = new System.DateTime(2075, 12, 18);
                break;
        }
    }
    //Go over each cell and write down what pieace in the cell [PLACEHOLDER]
    void ReadBoardState()
    {
        Debug.Log("Reading the board state happens here!");
        int i = 0;
        foreach (Transform row in this.transform)
        {
            foreach (Transform col in row)
            {
                if (col.transform.childCount > 0)
                {
                    PieceID pID = col.transform.GetChild(0).GetComponent<PieceID>();
                    gridState[i] = pID.typeID;
                }
                else
                {
                    gridState[i] = -1;
                }
                i++;
            }
        }
    }
    bool CompareGridStates(GridStateObject a, int[] b)
    {
        if (a.gridState.Length < 1 || a.gridState.Length > 9) return false;
        if (b.Length < 1 || b.Length > 9) return false;
        for(int i = 0;i < 9; i++)
        {
            if (a.gridState[i] != b[i]) return false;
        }
        return true;
    }
    public bool IsGridEmpty()
    {
        ReadBoardState();
        //Empty board state is always 0 index
        return CompareGridStates(rawObjectArray[0], gridState);
    }
    void CheckAchievements()
    {
        bool achievementUnlocked = false;

        for (int i = 1; i < rawObjectArray.Length; i++)
        {
            bool comp = CompareGridStates(rawObjectArray[i], gridState);
            if (comp && !rawObjectArray[i].completed)
            {
                rawObjectArray[i].completed = true;
                achievementUnlocked = true;
            }
        }

        if (achievementUnlocked && achievement != null)
        {
            audioSource.PlayOneShot(achievement);
        }
    }
    public void Sumbit()
    {
        if (isSubmitting) return;
        isSubmitting = true;

        if (IsGridEmpty())
        {
            Debug.Log("Empty!");
            isSubmitting = false;
            return;
        }
        CheckAchievements();
        submissionCount++;
        if(submissionCount-1 < unlocks.Length)
        {
            unlocks[submissionCount-1].SetActive(true);
        }

        AssignDate();
        counterDisplay.text = submissionCount.ToString();

        if (submit != null)
        {
            audioSource.PlayOneShot(submit);
        }

        dateDisplay.text = date.ToString("MMMM d, yyy");
        ClearBoard();
        isSubmitting = false;

        menuManagerRef.UnlockLetterAndLoadCustscene();
    }
    public void Quit()
    {
        Application.Quit();
    }
    //Clears the board. Assumes columns can only have 1 child at a time
    public void ClearBoard()
    {
        gridState = new int[9];
        int i = 0;
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
                gridState[i] = -1;
                i++;
            }
        }

        if (!isSubmitting && clear != null)
        {
            audioSource.PlayOneShot(clear);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearBoard();
        }
        if(currentSelection != null)
        {
            if(hoveredCell != null) { 
                Vector3 curPos = currentSelection.transform.position;
                currentSelection.transform.position = new Vector3(curPos.x, 1.5f, curPos.z);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentSelection.transform.Rotate(new Vector3(0, 90f, 0),Space.Self);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentSelection.transform.Rotate(new Vector3(0, 90f, 0), Space.Self);
            }
        }
    }
}
