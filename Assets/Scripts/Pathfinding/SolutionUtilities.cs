using System.Collections.Generic;
using System;

public class SolutionUtilities
{
    public int GetPathEndRowIndex(int columnIndex, Node[,] nodeTable)
    {
        int gridRows = nodeTable.GetLength(0);
        int lowestCost = Int32.MaxValue;
        int bestIndex = 0;
        for (int i = 0; i < gridRows; i++)
        {
            if (nodeTable[i, columnIndex].TotalPathCost < lowestCost)
            {
                lowestCost = nodeTable[i, columnIndex].TotalPathCost;
                bestIndex = i;
            }
        }
        return bestIndex;
    }
    
    //Whenever a row index is added to the list we add 1 because the output is supposed to be 1 based not 0 based.
    public LinkedList<int> GetPath(int rowIndex,int columnIndex,Node[,] nodeTable)
    {
        LinkedList<int> path = new LinkedList<int>();
        path.AddFirst(rowIndex + 1);
        int rowPointer = nodeTable[rowIndex, columnIndex].NeighborRowIndex;
        for (int i = 0; i < columnIndex; i++)
        {
            path.AddFirst(nodeTable[rowPointer, columnIndex - i].NeighborRowIndex + 1);
            rowPointer = nodeTable[rowPointer, columnIndex - i].NeighborRowIndex;
        }
        return path;
    }
}
