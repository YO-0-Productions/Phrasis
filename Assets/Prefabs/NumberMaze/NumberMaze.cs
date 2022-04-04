using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class NumberMaze : MonoBehaviour
{
    [SerializeField] public List<GameObject> cells = new List<GameObject>();
    [SerializeField] private List<int> cellValues = new List<int>();
    [SerializeField] private int tokenIndex = 1;
    [SerializeField] private GameObject token;
    [SerializeField] private int rowNumber = 1;
    [SerializeField] private int columnNumber = 1;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float cellSize;

    private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;


    // Start is called before the first frame update
    void Start()
    {
        PlayerController tokenController = token.GetComponent<PlayerController>();
        tokenController.enabled = false;
        
        keywordActions.Add("north", GoNorth);
        keywordActions.Add("west", GoWest);
        keywordActions.Add("east", GoEast);
        keywordActions.Add("south", GoSouth);

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognised;
        keywordRecognizer.Start();
        //int cellNumber = rowNumber * columnNumber;
        for (int i = 0; i < rowNumber*columnNumber; i++)
        {
            GameObject cellObject = Instantiate(cellPrefab, transform.position, Quaternion.identity);
            cellObject.transform.Translate((i % columnNumber) * cellSize, 0, (i / rowNumber) * cellSize);
            cellObject.GetComponent<Cell>().Value = cellValues[i];
            cells.Add(cellObject);
            cellObject.transform.SetParent(this.transform);
            Debug.Log(cells[i].GetComponent<Cell>().Value);
    
        }
        token.transform.localPosition = cells[tokenIndex].transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GoWest();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GoEast();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GoNorth();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GoSouth();
        }
    }

    private void OnKeywordsRecognised(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordActions[args.text].Invoke();
    }

    private void GoNorth()
    {
        int destinationIndex = tokenIndex + cells[tokenIndex].GetComponent<Cell>().Value * columnNumber;
        if (0 <= destinationIndex && destinationIndex <= rowNumber * columnNumber)
        {
            GameObject destinationCell = cells[destinationIndex];
            token.transform.localPosition = destinationCell.transform.localPosition;
            tokenIndex = destinationIndex;
            if (cells[tokenIndex].GetComponent<Cell>().Value == 0)
            {
                Debug.Log("Puzzle is solved");
            }
        }
    }

    private void GoWest()
    {
        int destinationIndex = tokenIndex - cells[tokenIndex].GetComponent<Cell>().Value;
        if (destinationIndex / columnNumber == tokenIndex / columnNumber)
        {
            GameObject destinationCell = cells[destinationIndex];
            token.transform.localPosition = destinationCell.transform.localPosition;
            tokenIndex = destinationIndex;
            if (cells[tokenIndex].GetComponent<Cell>().Value == 0)
            {
                Debug.Log("Puzzle is solved");
            }
        }
    }

    private void GoEast()
    {
        int destinationIndex = tokenIndex + cells[tokenIndex].GetComponent<Cell>().Value;
        if (destinationIndex / columnNumber == tokenIndex / columnNumber)
        {
            GameObject destinationCell = cells[destinationIndex];
            token.transform.localPosition = destinationCell.transform.localPosition;
            tokenIndex = destinationIndex;
            if (cells[tokenIndex].GetComponent<Cell>().Value == 0)
            {
                Debug.Log("Puzzle is solved");
            }
        }
    }

    private void GoSouth()
    {
        int destinationIndex = tokenIndex - cells[tokenIndex].GetComponent<Cell>().Value * columnNumber;
        if (0 <= destinationIndex && destinationIndex <= rowNumber * columnNumber)
        {
            GameObject destinationCell = cells[destinationIndex];
            token.transform.localPosition = destinationCell.transform.localPosition;
            tokenIndex = destinationIndex;
            if (cells[tokenIndex].GetComponent<Cell>().Value == 0)
            {
                Debug.Log("Puzzle is solved");
            }
        }
    }
}
