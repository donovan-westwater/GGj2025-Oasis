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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        date = System.DateTime.Now;
        counterDisplay = counterObj.GetComponent<TextMeshProUGUI>();
        dateDisplay = dateObj.GetComponent<TextMeshProUGUI>();
        rawObjectArray = Resources.LoadAll<GridStateObject>("GridStates/");
        foreach(GridStateObject g in rawObjectArray)
        {
            g.completed = false;
        }
        counterDisplay.text = submissionCount.ToString();
        dateDisplay.text = date.ToString("MM/dd/yyy");
        menuManagerRef = this.gameObject.GetComponent<MenuManager>();
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

        ReadBoardState();
        //Empty board state is always 0 index
        if (CompareGridStates(rawObjectArray[0], gridState))
        {
            Debug.Log("Empty!");
            isSubmitting = false;
            return;
        }
        CheckAchievements();
        submissionCount++;

        if (menuManagerRef.lettersUnlocked < unlocks.Length)
        {
            unlocks[menuManagerRef.lettersUnlocked].SetActive(true);
        }
        menuManagerRef.lettersUnlocked++;
        date = date.AddDays(7.0);
        counterDisplay.text = submissionCount.ToString();
        dateDisplay.text = date.ToString("MM/dd/yyy");

        if (submit != null)
        {
            audioSource.PlayOneShot(submit);
        }

        ClearBoard();
        isSubmitting = false;
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
    }
}
