using System;

public class NodeTableGenerator
{
    public Node[,] GenerateNodeTable(int[,] grid, int amountOfRows, int amountOfColumns, out int pathEndColumnIndex)
    {
        Node[,] nodeTable = new Node[amountOfRows, amountOfColumns];
        
        pathEndColumnIndex = 0;
        for (int c = 0; c < amountOfColumns; c++)
        {
            bool columnHasAValidNode = false;
            for (int r = 0; r < amountOfRows; r++)
            {
                if (c == 0)
                { 
                    columnHasAValidNode = true;
                    nodeTable[r, c].TotalPathCost = grid[r, c];
                    continue;
                }
                
                //populates a node at a specific coordinate in the node table and returns that node
                Node node = PopulateNode(r,c, grid[r,c], nodeTable);
                if (node.TotalPathCost <= 50)
                {
                    pathEndColumnIndex = c;
                    columnHasAValidNode = true;
                }
            }
            //Early break out of this nested loop if an entire column has no valid moves.
            if (!columnHasAValidNode)
            {
                break;
            }
        }
        return nodeTable;
    }
    
    private Node PopulateNode(int rowIndex, int columnIndex, int gridCostAtCoordinate, Node[,] nodeTable)
    {
        int amountOfRows = nodeTable.GetLength(0);
        int lowestTotalCost = Int32.MaxValue;
        for (int i = -1; i < 2; i++)
        {
            int neighborRowIndex = (rowIndex + amountOfRows + i) % amountOfRows;
            Node neighbor = nodeTable[neighborRowIndex, columnIndex - 1];
    
            if (neighbor.TotalPathCost > 50)
                continue;
            if (neighbor.TotalPathCost < lowestTotalCost)
            {
                lowestTotalCost = neighbor.TotalPathCost;
                nodeTable[rowIndex,columnIndex].TotalPathCost = neighbor.TotalPathCost + gridCostAtCoordinate;
                nodeTable[rowIndex,columnIndex].NeighborRowIndex = neighborRowIndex;
            }
        }
        if(lowestTotalCost == Int32.MaxValue)
            nodeTable[rowIndex,columnIndex].TotalPathCost = Int32.MaxValue;

        return nodeTable[rowIndex, columnIndex];
    }
}
