using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown rows;
    [SerializeField] private TMP_Dropdown columns;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_InputField minInputField;
    [SerializeField] private TMP_InputField maxInputField;
    [SerializeField] private int defaultMin;
    [SerializeField] private int defaultMax;

    private ProvidedInputData _providedInputData;
    
    private void Start()
    {
        _providedInputData = new ProvidedInputData();
        
        minInputField.text = defaultMin.ToString();
        maxInputField.text = defaultMax.ToString();
        
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        inputField.text = String.Empty;
        int rowAmount = rows.value + 1;
        int columnAmount = columns.value + 1;

        inputField.pointSize = columnAmount > 43 ? GetFontSize(columnAmount) : 15;
        int[,] grid = new int[rowAmount, columnAmount];
        int minNum = Int32.Parse(minInputField.text);
        int maxNum = Int32.Parse(maxInputField.text);
        for (int i = 0; i < rowAmount; i++)
        {
            for (int j = 0; j < columnAmount; j++)
            {
                int num = Random.Range(minNum,maxNum + 1);
                if (Mathf.Abs(num) == num && num < 10)
                    inputField.text += " ";
                inputField.text += num + ",";
            }

            inputField.text += "\n";
        }
    }

    public void GenerateGridOne() => GenerateGrid(_providedInputData.GridOne);
    public void GenerateGridTwo() => GenerateGrid(_providedInputData.GridTwo);
    public void GenerateGridThree() => GenerateGrid(_providedInputData.GridThree);
    
    private void GenerateGrid(int[,] providedInput)
    {
        inputField.text = String.Empty;
        int rowAmount = providedInput.GetLength(0);
        int columnAmount = providedInput.GetLength(1);
        inputField.pointSize = 15;

        for (int i = 0; i < rowAmount; i++)
        {
            for (int j = 0; j < columnAmount; j++)
            {
                if (providedInput[i, j] < 10)
                    inputField.text += " ";
                inputField.text += providedInput[i, j] + ",";
            }
            inputField.text += "\n";
        }
    }

    private float GetFontSize(int columns)
    {
        float fontSize = 17.75f - (columns / 8f);
        return fontSize;
    }
}
