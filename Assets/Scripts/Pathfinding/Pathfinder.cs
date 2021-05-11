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
    
    private int _gridRows;
    private int _gridColumns;
    private int[,] _grid;
    private Node[,] _nodeTable;
    private IParseInput _stringParser;

    private void Awake() => _stringParser = GetComponent<IParseInput>();

    public void FindBestPath()
    {
        _grid = _stringParser.ParseInput(inputField.text);
        if (_grid == null)
            return;
        
        _gridRows = _grid.GetLength(0);
        _gridColumns = _grid.GetLength(1);
        _nodeTable = new Node[_gridRows, _gridColumns];
        int highestColumnIndex = 0;
        
        for (int column = 0; column < _gridColumns; column++)
        {
            bool columnHasAValidNode = false;
            for (int row = 0; row < _gridRows; row++)
            {
                if (column == 0)
                {
                    if (_grid[row, column] < 51)
                        columnHasAValidNode = true;
                    _nodeTable[row, column].TotalCost = _grid[row, column];
                    continue;
                }
                //populates node table and returns column index if node total cost was under 50 and 0 if it was over.
                int columnIndex = PopulateNodeTable(column,row, _grid[row,column]);
                if (columnIndex > highestColumnIndex)
                {
                    highestColumnIndex = columnIndex;
                    columnHasAValidNode = true;
                }
            }
            if (!columnHasAValidNode)
            {
                break;
            }
        }

        madeItThroughText.text = highestColumnIndex == _gridColumns - 1 ? "Yes" : "No";
       
        Tuple<int, int> endNodeRowAndColumn = GetEndNodeCoordinates(highestColumnIndex);
        Node bestNode = _nodeTable[endNodeRowAndColumn.Item1, endNodeRowAndColumn.Item2];
        totalCostText.text = bestNode.TotalCost.ToString();
        
        LinkedList<int> bestPath = GetPath(endNodeRowAndColumn.Item1, endNodeRowAndColumn.Item2);
        pathRowIndicesText.text = String.Empty;
        foreach (var index in bestPath)
        {
            pathRowIndicesText.text += index + ",";
        }
    }
    
    private int PopulateNodeTable(int columnIndex, int rowIndex, int gridCost)
    {
        int lowestTotalCost = Int32.MaxValue;
        int highestColumnIndex = 0;
        for (int i = -1; i < 2; i++)
        {
            int neighborRowIndex = (rowIndex + _gridRows + i) % _gridRows;
            Node neighbor = _nodeTable[neighborRowIndex, columnIndex - 1];

            if (neighbor.TotalCost > 50)
                continue;
            if (neighbor.TotalCost < lowestTotalCost)
            {
                lowestTotalCost = neighbor.TotalCost;
                _nodeTable[rowIndex,columnIndex].TotalCost = neighbor.TotalCost + gridCost;
                _nodeTable[rowIndex,columnIndex].NeighborRowIndex = neighborRowIndex;
            }
            if (neighbor.TotalCost + gridCost <= 50)
                highestColumnIndex = columnIndex;
        }
        if(lowestTotalCost == Int32.MaxValue)
            _nodeTable[rowIndex,columnIndex].TotalCost = lowestTotalCost;
        
        return highestColumnIndex;
    }
    
    private Tuple<int,int> GetEndNodeCoordinates(int columnIndex)
    {
        int lowestCost = Int32.MaxValue;
        int bestIndex = 0;
        for (int i = 0; i < _gridRows; i++)
        {
            if (_nodeTable[i, columnIndex].TotalCost < lowestCost)
            {
                lowestCost = _nodeTable[i, columnIndex].TotalCost;
                bestIndex = i;
            }
        }
        return new Tuple<int, int>(bestIndex, columnIndex);
    }
    
    private  LinkedList<int> GetPath(int rowIndex,int columnIndex)
    {
        LinkedList<int> path = new LinkedList<int>();
        path.AddFirst(rowIndex + 1);
        int rowPointer = _nodeTable[rowIndex, columnIndex].NeighborRowIndex;
        for (int i = 0; i < columnIndex; i++)
        {
            path.AddFirst(_nodeTable[rowPointer, columnIndex - i].NeighborRowIndex + 1);
            rowPointer = _nodeTable[rowPointer, columnIndex - i].NeighborRowIndex;
        }
        return path;
    }
}
