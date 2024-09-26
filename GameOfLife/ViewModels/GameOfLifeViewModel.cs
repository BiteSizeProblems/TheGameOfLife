namespace GameOfLife.ViewModels;

/// <summary>
/// ViewModel representing the Game of Life state, including the board and session information.
/// </summary>
/// <param name="rows">The number of rows in the Game of Life board.</param>
/// <param name="cols">The number of columns in the Game of Life board.</param>
public class GameOfLifeViewModel(int rows, int cols)
{
    /// <summary>
    /// The ViewModel representing the Game of Life board.
    /// </summary>
    public BoardViewModel Board { get; set; } = new BoardViewModel
    {
        CellStates = new int[rows, cols]
    };

    /// <summary>
    /// The session statistics associated with the current state of the game.
    /// </summary>
    public SessionInfoViewModel SessionInfo { get; set; } = new SessionInfoViewModel();
}