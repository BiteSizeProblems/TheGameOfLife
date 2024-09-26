using GameOfLife.Extensions;

namespace GameOfLife.ViewModels;

/// <summary>
/// ViewModel representing the state of the Game of Life board.
/// </summary>
public class BoardViewModel
{
    /// <summary>
    /// The 2D array of cell states, where 1 represents alive and 0 represents dead.
    /// </summary>
    public int[,] CellStates { get; set; } = new int[1, 1];

    /// <summary>
    /// Gets the number of rows in the board.
    /// </summary>
    public int Rows => CellStates.GetRows();

    /// <summary>
    /// Gets the number of columns in the board.
    /// </summary>
    public int Cols => CellStates.GetCols();
}