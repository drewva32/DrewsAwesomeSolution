using UnityEngine;

public class ProvidedInputData
{
    public int[,] GridOne { get; private set; } = new int[,]
    {
        {3, 4, 1, 2, 8, 6},
        {6, 1, 8, 2, 7, 4},
        {5, 9, 3, 9, 9, 5},
        {8, 4, 1, 3, 2, 6},
        {3, 7, 2, 8, 6, 4}
    };
    public int[,] GridTwo { get; private set; } = new int[,]
    {
        {3, 4, 1, 2, 8, 6},
        {6, 1, 8, 2, 7, 4},
        {5, 9, 3, 9, 9, 5},
        {8, 4, 1, 3, 2, 6},
        {3, 7, 2, 1, 2, 3}
    };
    public int[,] GridThree { get; private set; } = new int[,]
    {
        {19, 10, 19, 10, 19,},
        {21, 23, 20, 19, 12},
        {20, 12, 20, 11, 10},
    };
}
