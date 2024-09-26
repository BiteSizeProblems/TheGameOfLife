namespace GameOfLife.ViewModels;

/// <summary>
/// ViewModel representing the session information for the Game of Life, including statistics about the cells.
/// </summary>
public class SessionInfoViewModel
{
    /// <summary>
    /// The current generation number.
    /// </summary>
    public int Generation { get; set; }

    /// <summary>
    /// The total number of alive cells in the current generation.
    /// </summary>
    public int TotalAliveCells { get; set; }

    /// <summary>
    /// The total number of dead cells in the current generation.
    /// </summary>
    public int TotalDeadCells { get; set; }

    /// <summary>
    /// The number of cells at risk of underpopulation (fewer than two alive neighbors).
    /// </summary>
    public int CellsAtRiskOfUnderpopulation { get; set; }

    /// <summary>
    /// The number of cells at risk of overcrowding (more than three alive neighbors).
    /// </summary>
    public int CellsAtRiskOfOvercrowding { get; set; }

    /// <summary>
    /// The number of cells that will survive to the next generation (exactly two or three neighbors).
    /// </summary>
    public int CellsThatWillSurvive { get; set; }

    /// <summary>
    /// The number of dead cells that will reproduce in the next generation (exactly three neighbors).
    /// </summary>
    public int CellsThatWillReproduce { get; set; }
}