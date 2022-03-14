using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberMaze : MonoBehaviour
{
    [SerializeField] public List<GameObject> cells = new List<GameObject>();
    [SerializeField] private List<int> cellValues = new List<int>();
    [SerializeField] private int tokenIndex = 1;
    [SerializeField] private GameObject token;
    [SerializeField] private int rowNumber = 1;
    [SerializeField] private int columnNumber = 1;
    [SerializeField] private GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //int cellNumber = rowNumber * columnNumber;
        for (int i = 0; i < rowNumber*columnNumber; i++)
        {
            GameObject cellObject = Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cellObject.transform.Translate((i % columnNumber) * 2, 0, (i / rowNumber) * 2);
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
        if (Input.GetKeyDown(KeyCode.D))
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
        if (Input.GetKeyDown(KeyCode.W))
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
        if (Input.GetKeyDown(KeyCode.S))
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
}
