using System;
using System.Text;
using UnityEngine;
using Random = System.Random;

// V: Algorithm found at https://www.geeksforgeeks.org/program-sudoku-generator/
public class SudokuGenerator{
    private readonly int[,] sudoku;
    private readonly bool[,] hiddenNumbers;
    private readonly int rows;
    private readonly int columns;
    private readonly int sqrtRows;
    private readonly int sqrtColumns;
    private readonly int numberOfHiddenValues;
    private readonly Random rand;

    public SudokuGenerator(int boardRows, int boardColumns, int numberOfHidden, int seed) {
        rows = boardRows;
        columns = boardColumns;
        numberOfHiddenValues = numberOfHidden;

        double sqrtOfRows = Math.Sqrt(rows);
        sqrtRows = (int)sqrtOfRows;
        double sqrtOfColumns = Math.Sqrt(columns);
        sqrtColumns = (int)sqrtOfColumns;

        rand = seed != -1 ? new Random(seed) : new Random();
        sudoku = new int[rows, columns];
        hiddenNumbers = new bool[rows, columns];
    }

    public int[,] GetSudoku() {
        return sudoku;
    }

    public bool[,] GetHiddenNumbers() {
        return hiddenNumbers;
    }

    public void Generate() {
        FillDiagonal();
        FillRemaining(0, sqrtRows);
        RemoveKDigits();
    }

    private void FillDiagonal() {
        for (int y = 0; y < rows; y = y + sqrtRows) {
            FillBox(y, y);
        }
    }

    private void FillBox(int row, int col) {
        int num;
        for (int y = 0; y < sqrtRows; y++) {
            for (int x = 0; x < sqrtColumns; x++) {
                do {
                    num = RandomGenerator(rows);
                }
                while (!IsNumberUnUsedInBox(row, col, num));

                sudoku[row + y, col + x] = num;
            }
        }
    }

    private int RandomGenerator(int num) {
        double randomValue = rand.NextDouble() * num + 1;
        double floored = Math.Floor(randomValue);
        int result = (int)floored;
        return result;
    }

    private bool CanUseNumber(int x, int y, int num) {
        bool inUsedInRow = IsNumberUnusedInRow(y, num);
        bool unUsedInCol = IsNumberUnusedInCol(x, num);
        bool unUsedInBox = IsNumberUnUsedInBox(y - y % sqrtRows, x - x % sqrtColumns, num);
        bool result = inUsedInRow && unUsedInCol && unUsedInBox;
        return result;
    }

    private bool IsNumberUnusedInRow(int y, int num) {
        for (int x = 0; x < columns; x++)
            if (sudoku[y, x] == num) {
                return false;
            }

        return true;
    }

    private bool IsNumberUnusedInCol(int x, int num) {
        for (int y = 0; y < rows; y++)
            if (sudoku[y, x] == num) {
                return false;
            }

        return true;
    }

    private bool IsNumberUnUsedInBox(int rowStart, int colStart, int num) {
        for (int y = 0; y < sqrtRows; y++)
            for (int x = 0; x < sqrtColumns; x++)
                if (sudoku[rowStart + y, colStart + x] == num) {
                    return false;
                }

        return true;
    }

    private bool FillRemaining(int y, int x) {
        if (x >= columns && y < rows - 1) {
            y += 1;
            x = 0;
        }

        if (y >= rows && x >= columns) {
            return true;
        }

        if (y < sqrtRows) {
            if (x < sqrtColumns) {
                x = sqrtColumns;
            }
        }
        else if (y < rows - sqrtRows) {
            if (x == y / sqrtRows * sqrtRows) {
                x += sqrtColumns;
            }
        }
        else {
            if (x == columns - sqrtColumns) {
                y += 1;
                x = 0;
                if (y >= rows) {
                    return true;
                }
            }
        }

        for (int num = 1; num <= rows; num++) {
            if (CanUseNumber(x, y, num)) {
                sudoku[y, x] = num;
                if (FillRemaining(y, x + 1)) {
                    return true;
                }

                sudoku[y, x] = 0;
            }
        }

        return false;
    }

    private void RemoveKDigits() {
        int count = numberOfHiddenValues;
        const int safetyNumber = 100;
        int safetyCounter = 0;
        while (count != 0) {
            if (safetyCounter >= safetyNumber) {
                break;
            }

            int cellId = RandomGenerator(rows * columns) - 1;
            int y = cellId / rows;
            int x = cellId % columns;
            if (hiddenNumbers[y, x]) {
                safetyCounter++;
                continue;
            }

            safetyCounter = 0;
            count--;
            hiddenNumbers[y, x] = true;
        }
    }

    public void PrintSudoku() {
        StringBuilder stringBuilder = new();
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++) {
                stringBuilder.Append($"{sudoku[y, x]} ");
            }

            stringBuilder.Append("\n");
        }

        Debug.Log(stringBuilder);
    }
}