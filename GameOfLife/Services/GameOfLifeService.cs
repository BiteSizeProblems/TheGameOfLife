using GameOfLife.Extensions;
using GameOfLife.Models;

namespace GameOfLife.Services;

/// <summary>
/// Service to handle Game of Life logic and rules.
/// </summary>
public class GameOfLifeService
{
    /// <summary>
    /// Randomizes the Game of Life board with alive (1) and dead (0) cells.
    /// </summary>
    /// <param name="rows">The number of rows.</param>
    /// <param name="cols">The number of columns.</param>
    /// <returns>A new randomized board.</returns>
    public int[,] RandomizeBoard(int rows, int cols)
    {
        Random random = new();
        int[,] board = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board[i, j] = random.Next(0, 2);
            }
        }

        return board;
    }

    /// <summary>
    /// Determines if a cell should survive based on its alive neighbors.
    /// </summary>
    /// <param name="aliveNeighbors">The count of alive neighbors.</param>
    /// <returns>True if the cell should survive; otherwise, false.</returns>
    public bool ShouldCellSurvive(int aliveNeighbors)
    {
        return aliveNeighbors is 2 or 3;
    }

    /// <summary>
    /// Determines if a dead cell should reproduce based on its alive neighbors.
    /// </summary>
    /// <param name="aliveNeighbors">The count of alive neighbors.</param>
    /// <returns>True if the cell should reproduce; otherwise, false.</returns>
    public bool ShouldCellReproduce(int aliveNeighbors)
    {
        return aliveNeighbors == 3;
    }

    /// <summary>
    /// Applies the Game of Life rules to progress to the next generation.
    /// </summary>
    /// <param name="board">The current board state.</param>
    /// <param name="newBoard">The new board state after applying the rules.</param>
    /// <returns>True if the board has changed; otherwise, false.</returns>
    public bool ApplyRulesForNextGeneration(int[,] board, out int[,] newBoard)
    {
        int rows = board.GetRows();
        int cols = board.GetCols();
        newBoard = new int[rows, cols];
        bool boardChanged = false;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Cell cell = new() { Row = i, Col = j };

                int aliveNeighbors = board.CountAliveNeighbors(cell);

                bool shouldSurvive = board.IsAlive(cell) && ShouldCellSurvive(aliveNeighbors);
                bool shouldReproduce = !board.IsAlive(cell) && ShouldCellReproduce(aliveNeighbors);

                newBoard.ApplyNextGenerationState(cell, shouldSurvive, shouldReproduce);

                if (newBoard[cell.Row, cell.Col] != board[cell.Row, cell.Col])
                {
                    boardChanged = true;
                }
            }
        }

        return boardChanged;
    }
}