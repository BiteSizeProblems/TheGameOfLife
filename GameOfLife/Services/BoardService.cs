using GameOfLife.Exceptions;
using Newtonsoft.Json;

namespace GameOfLife.Services;

/// <summary>
/// Service to manage the Game of Life board and generation in the session.
/// </summary>
public class BoardService(IHttpContextAccessor httpContextAccessor)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null. Ensure this method is called within the context of an HTTP request.");

    /// <summary>
    /// Gets the current board state from the session.
    /// </summary>
    /// <returns>The current board as a 2D array.</returns>
    /// <exception cref="BoardException">Thrown when the board cannot be retrieved.</exception>
    public int[,] GetBoard()
    {
        string value = _httpContext.Session.GetString("Board")
            ?? throw new BoardException("Board not found in the session context.");

        return JsonConvert.DeserializeObject<int[,]>(value)
            ?? throw new BoardException("Failed to deserialize the board from the session.");
    }

    /// <summary>
    /// Saves the given board to the session.
    /// </summary>
    /// <param name="board">The board to save.</param>
    public void SetBoard(int[,] board)
    {
        _httpContext.Session.SetString("Board", JsonConvert.SerializeObject(board));
    }

    /// <summary>
    /// Gets the current generation count from the session.
    /// </summary>
    /// <returns>The current generation count.</returns>
    /// <exception cref="BoardException">Thrown when the generation cannot be retrieved.</exception>
    public int GetGeneration()
    {
        int value = _httpContext.Session.GetInt32("Generation")
            ?? throw new BoardException("Generation not found in the session context.");

        return value;
    }

    /// <summary>
    /// Saves the generation count to the session.
    /// </summary>
    /// <param name="generation">The generation count to save.</param>
    public void SetGeneration(int generation)
    {
        _httpContext.Session.SetInt32("Generation", generation);
    }

    /// <summary>
    /// Clears the board and resets it to a blank state.
    /// </summary>
    /// <param name="rows">The number of rows for the new board.</param>
    /// <param name="cols">The number of columns for the new board.</param>
    public void ClearBoard(int rows, int cols)
    {
        int[,] newBoard = new int[rows, cols];
        SetBoard(newBoard);
        SetGeneration(0);
    }
}