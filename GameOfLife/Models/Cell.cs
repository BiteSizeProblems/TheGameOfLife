namespace GameOfLife.Models;

/// <summary>
/// Represents a cell on the Game of Life board.
/// </summary>
public class Cell
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class with the specified row and column.
    /// </summary>
    /// <param name="row">The row of the cell.</param>
    /// <param name="col">The column of the cell.</param>
    public Cell(int row, int col)
    {
        Row = row;
        Col = col;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    public Cell()
    {

    }

    /// <summary>
    /// The row of the cell.
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// The column of the cell.
    /// </summary>
    public int Col { get; set; }
}