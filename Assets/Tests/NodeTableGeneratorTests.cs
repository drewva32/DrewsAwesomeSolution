using System;
using NUnit.Framework;

public class NodeTableGeneratorTests
{

    [Test]
    public void _Node_Table_Has_Same_Amount_Of_Rows_As_Grid()
    {
        int[,] grid = new int[,]
        {
            {1, 2, 3, 4},
            {5, 6, 7, 8},
            {9,10,11,12}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);
        
        Assert.AreEqual(nodeTable.GetLength(0), grid.GetLength(0));
    }
    
    [Test]
    public void _Node_Table_Has_Same_Amount_Of_Columns_As_Grid()
    {
        int[,] grid = new int[,]
        {
            {1, 2, 3, 4},
            {5, 6, 7, 8},
            {9,10,11,12}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);
        
        Assert.AreEqual(nodeTable.GetLength(1), grid.GetLength(1));
    }

    [Test]
    public void _First_Column_Of_Node_Table_Equals_First_Column_Of_Grid_For_Each_Node_Total_Cost()
    {
        int[,] grid = new int[,]
        {
            {1, 2, 3, 4},
            {5, 6, 7, 8},
            {9,10,11,12}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);
        
        Assert.AreEqual(grid[0,0],nodeTable[0,0].TotalPathCost);
        Assert.AreEqual(grid[1,0],nodeTable[1,0].TotalPathCost);
        Assert.AreEqual(grid[2, 0], nodeTable[2, 0].TotalPathCost);
    }

    [Test]
    public void _Nodes_Neighbor_Is_The_Node_With_Min_Cost_Of_Three_Possible_Neighbors()
    {
        int[,] grid = new int[,]
        {
            {1, 2, 3, 4},
            {5, 6, 7, 8},
            {9,10,11,12}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);

        int expectedRowIndexForNodeAtTwoTwo = 0;
        //6 is at index 1,1
        Assert.AreEqual(expectedRowIndexForNodeAtTwoTwo, nodeTable[1, 1].NeighborRowIndex);
    }

    [Test]
    public void _Path_Can_Wrap_From_Top_Row_To_Bottom_Row()
    {
        int[,] grid = new int[,]
        {
            {1, 2, 3, 60},
            {5, 6,60, 60},
            {9,10,60,-10}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);
        
        //-10 is at 2,3
        //3 is at row 0
        Assert.AreEqual(0, nodeTable[2,3].NeighborRowIndex);
    }
    
    [Test]
    public void _Path_Can_Wrap_From_Bottom_Row_To_Top_Row()
    {
        int[,] grid = new int[,]
        {
            {60,60,60,-10},
            {60,60,60,60},
            { 1, 2, 3,60}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);
        
        //-10 is at index 0,3
        //3 is at row index 2
        Assert.AreEqual(2, nodeTable[0,3].NeighborRowIndex);
    }
    
    [Test]
    public void _Node_Total_Path_Cost_Equals_Int_Max_Value_If_No_Valid_Neighbors()
    {
    int[,] grid = new int[,]
    {
        {9,60,7,1},
        {1,60,5,1},
        {1,60,3,1},
        {1, 2,3,4}
    };
    var nodeTableGenerator = new NodeTableGenerator();
    Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);
        
    //5 is at 1,2
    //this node has no valid path to build off of because 3 neighbors are already above 50 total cost
    Assert.AreEqual(Int32.MaxValue, nodeTable[1,2].TotalPathCost);
    }
    
    [Test]
    public void _Path_End_Column_Index_Is_At_Furthest_Valid_Node_To_The_Right()
    {
        int[,] grid = new int[,]
        {
            {60,60,60,60},
            {60,60,60,60},
            {60,60,60,60},
            { 1, 1, 1, 2}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);

        int pathEndColumnIndex = index;
        //2 is at column index 3
        Assert.AreEqual(pathEndColumnIndex, 3);
    }
    
    [Test]
    public void _Large_Negative_Number_Does_Not_Revive_An_Invalid_Path()
    {
        int[,] grid = new int[,]
        {
            {60,60, 60,60},
            {60,60, 60,60},
            {60,60, 60,60},
            { 1,2,70,-200}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int index);

        int pathEndColumnIndex = index;
        //2 is at column index 1, this should be the last valid node
        Assert.AreEqual(pathEndColumnIndex, 1);
    }
    
}
