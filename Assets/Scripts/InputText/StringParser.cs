using System;
using System.Collections.Generic;
using UnityEngine;

public class StringParser : MonoBehaviour, IParseInput
{
    [SerializeField] private GameObject errorPanelNotSymmetrical;
    [SerializeField] private GameObject errorPanelInvalidInput;
    
    public int[,] ParseInput(string input)
    {
        string gridInput = input;
        string[] splitString = gridInput.Split('\n',',');
        string[] rows = gridInput.Split('\n');
        
        List<string> allGridPoints = RemoveEmptyStrings(splitString);
        List<string> stringRows = RemoveEmptyStrings(rows);

        float columns = (float)allGridPoints.Count / stringRows.Count;
        if (columns % 1 > 0.0005)
        {
            errorPanelNotSymmetrical.SetActive(true);
            return null;
        }

        int[,] grid = new int[stringRows.Count,(int)columns];
        int counter = 0;
        for (int i = 0; i < stringRows.Count; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int parse;
                if (!int.TryParse(allGridPoints[counter], out parse))
                {
                    errorPanelInvalidInput.SetActive(true);
                    return null;
                }
                grid[i, j] = Int32.Parse(allGridPoints[counter]);
                counter++;
            }
        }
        return grid;
    }
    
    private List<string> RemoveEmptyStrings(string[] stringArray)
    {
        List<string> stringList = new List<string>();
        for (int i = 0; i < stringArray.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(stringArray[i]))
            {
                stringList.Add(stringArray[i]);
            }
        }

        return stringList;
    }
}
