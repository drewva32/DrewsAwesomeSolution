using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI madeItThroughText;
    [SerializeField] private TextMeshProUGUI totalCostText;
    [SerializeField] private TextMeshProUGUI pathRowIndicesText;
    
    private Node[,] _nodeTable;
    private IParseInput _stringParser;
    private NodeTableGenerator _nodeTableGenerator;
    private SolutionUtilities _solutionUtilities;

    private void Awake()
    {
        _stringParser = GetComponent<IParseInput>();
        _nodeTableGenerator = new NodeTableGenerator();
        _solutionUtilities = new SolutionUtilities();
    }

    public void FindBestPath()
    {
        int[,] grid = _stringParser.ParseInput(inputField.text);
        if (grid == null)
            return;
        int amountOfRows = grid.GetLength(0);
        int amountOfColumns = grid.GetLength(1);
        //creates the node table and has out parameter for the column index of where the path ends.
        _nodeTable = _nodeTableGenerator.GenerateNodeTable(grid, amountOfRows, amountOfColumns, out int pathEndColumnIndex);
        
        SetPathMadeItToThroughText(pathEndColumnIndex, amountOfColumns);
        int pathEndRowIndex = _solutionUtilities.GetPathEndRowIndex(pathEndColumnIndex, _nodeTable);
        SetPathTotalCostText(pathEndRowIndex, pathEndColumnIndex);
        SetPathRowIndicesText(pathEndRowIndex, pathEndColumnIndex);
    }

    private void SetPathMadeItToThroughText(int pathEndColumnIndex, int totalAmountOfColumns)
    {
        madeItThroughText.text = pathEndColumnIndex == totalAmountOfColumns - 1 ? "Yes" : "No";
    }

    private void SetPathTotalCostText(int pathEndRowIndex, int pathEndColumnIndex)
    {
        Node pathEnd = _nodeTable[pathEndRowIndex, pathEndColumnIndex];
        totalCostText.text = pathEnd.TotalPathCost.ToString();
    }

    private void SetPathRowIndicesText(int pathEndRowIndex, int pathEndColumnIndex)
    {
        LinkedList<int> bestPath = _solutionUtilities.GetPath(pathEndRowIndex, pathEndColumnIndex, _nodeTable);
        pathRowIndicesText.text = String.Empty;
        foreach (var index in bestPath)
        {
            pathRowIndicesText.text += index + ",";
        }
    }
}
