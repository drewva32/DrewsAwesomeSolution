using System;
using System.Collections.Generic;
using NUnit.Framework;

public class SolutionUtilitiesTests
{
    [Test]
    public void _Get_Path_End_Row_Index_Returns_Row_Index_Of_Min_Cost_Node_In_End_Column()
    {
        int[,] grid = new int[,]
                {
                    {1, 2, 3, 4},
                    {5, 6, 7, 8},
                    {9,10,11,-1},
                    {8, 8, 8,20}
                };
                var nodeTableGenerator = new NodeTableGenerator();
                Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int pathEndColumnIndex);
                var solutionUtilities = new SolutionUtilities();
                int rowIndex = solutionUtilities.GetPathEndRowIndex(pathEndColumnIndex, nodeTable);
                
                //-1 is at row index 2
                Assert.AreEqual(2,rowIndex);
    }

    [Test]
    public void _Last_Index_In_List_Is_Row_Index_Of_Last_Node_In_Path()
    {
        int[,] grid =
        {
            { 1, 1, 1,60},
            {60,60,60, 1},
            {60,60,60,60},
            {60,60,60,60}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int pathEndColumnIndex);
        var solutionUtilities = new SolutionUtilities();
        int pathEndRowIndex = solutionUtilities.GetPathEndRowIndex(pathEndColumnIndex, nodeTable);
        LinkedList<int> path = solutionUtilities.GetPath(pathEndRowIndex, pathEndColumnIndex, nodeTable);
        
        //path ends at row index 2 (it is now 1 based to match the desired final output)
        Assert.AreEqual(2,path.Last.Value);
    }

    [Test]
    public void _Path_Linked_List_Follows_Node_Neighbor_Pointers_Along_Best_Path_From_End_Node()
    {
        int[,] grid =
        {
            { 4,60,60,60},
            {60, 3,60,60},
            {60,60, 2,60},
            {60,60,60, 1}
        };
        var nodeTableGenerator = new NodeTableGenerator();
        Node[,] nodeTable = nodeTableGenerator.GenerateNodeTable(grid, grid.GetLength(0),grid.GetLength(1), out int pathEndColumnIndex);
        var solutionUtilities = new SolutionUtilities();
        int pathEndRowIndex = solutionUtilities.GetPathEndRowIndex(pathEndColumnIndex, nodeTable);
        LinkedList<int> path = solutionUtilities.GetPath(pathEndRowIndex, pathEndColumnIndex, nodeTable);

        LinkedListNode<int> currentListNode = path.Last.Previous;
        //nodeTable[i,i].neighbor starts at 2 and moves diagonally up.
        for (int i = 3; i > 0; i--)
        {
            Assert.AreEqual(nodeTable[i, i].NeighborRowIndex, currentListNode.Value - 1);//-1 because this list is 1 based.
            currentListNode = currentListNode.Previous;
        }
        
    }
}
