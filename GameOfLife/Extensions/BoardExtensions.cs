using GameOfLife.Models;

namespace GameOfLife.Extensions;

/// <summary>
/// Extension methods for handling board operations in the Game of Life.
/// </summary>
public static class BoardExtensions
{
    /// <summary>
    /// Gets the number of rows in the board.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <returns>The number of rows.</returns>
    public static int GetRows(this int[,] board) => board.GetLength(0);

    /// <summary>
    /// Gets the number of columns in the board.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <returns>The number of columns.</returns>
    public static int GetCols(this int[,] board) => board.GetLength(1);

    /// <summary>
    /// Toggles the state of a cell (alive or dead).
    /// </summary>
    /// <param name="board">The board.</param>
    /// <param name="cell">The cell to toggle.</param>
    public static void ToggleCell(this int[,] board, Cell cell)
    {
        board[cell.Row, cell.Col] = board[cell.Row, cell.Col] == 1 ? 0 : 1;
    }

    /// <summary>
    /// Counts the number of alive neighbors around a given cell.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <param name="cell">The cell to check neighbors for.</param>
    /// <returns>The count of alive neighbors.</returns>
    public static int CountAliveNeighbors(this int[,] board, Cell cell)
    {
        int aliveNeighbors = 0;
        int[] directions = [-1, 0, 1];

        foreach (int x in directions)
        {
            foreach (int y in directions)
            {
                if (x == 0 && y == 0)
                {
                    continue; // Skip the current cell
                }

                int newRow = cell.Row + x;
                int newCol = cell.Col + y;

                if (board.IsValidCell(newRow, newCol))
                {
                    aliveNeighbors += board[newRow, newCol];
                }
            }
        }

        return aliveNeighbors;
    }

    /// <summary>
    /// Checks if a cell is valid within the board's boundaries.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <param name="row">The row of the cell.</param>
    /// <param name="col">The column of the cell.</param>
    /// <returns>True if the cell is valid; otherwise, false.</returns>
    public static bool IsValidCell(this int[,] board, int row, int col)
    {
        return row >= 0 && row < board.GetRows() && col >= 0 && col < board.GetCols();
    }

    /// <summary>
    /// Determines if the specified cell is alive.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <param name="row">The row of the cell.</param>
    /// <param name="col">The column of the cell.</param>
    /// <returns>True if the cell is alive; otherwise, false.</returns>
    public static bool IsAlive(this int[,] board, int row, int col) => board[row, col] == 1;

    /// <summary>
    /// Determines if the specified cell is alive.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <param name="cell">The cell to describe.</param>
    /// <returns>True if the cell is alive; otherwise, false.</returns>
    public static bool IsAlive(this int[,] board, Cell cell) => board.IsAlive(cell.Row, cell.Col);

    /// <summary>
    /// Applies the next generation state to a cell.
    /// </summary>
    /// <param name="newBoard">The new board state.</param>
    /// <param name="cell">The cell to update.</param>
    /// <param name="shouldSurvive">Whether the cell should survive.</param>
    /// <param name="shouldReproduce">Whether the cell should reproduce.</param>
    public static void ApplyNextGenerationState(this int[,] newBoard, Cell cell, bool shouldSurvive, bool shouldReproduce)
    {
        if (shouldSurvive)
        {
            newBoard[cell.Row, cell.Col] = 1; // Cell survives
        }
        else if (shouldReproduce)
        {
            newBoard[cell.Row, cell.Col] = 1; // Cell reproduces
        }
        else
        {
            newBoard[cell.Row, cell.Col] = 0; // Cell dies
        }
    }
}