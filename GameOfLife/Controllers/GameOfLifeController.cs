using GameOfLife.Extensions;
using GameOfLife.Models;
using GameOfLife.Services;
using GameOfLife.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.Controllers;

/// <summary>
/// Controller responsible for handling the Game of Life actions.
/// </summary>
/// <param name="boardService">Service to manage board state in the session.</param>
/// <param name="gameOfLifeService">Service to apply Game of Life rules.</param>
/// <param name="sessionInfoService">Service to manage session statistics.</param>
public class GameOfLifeController(BoardService boardService, GameOfLifeService gameOfLifeService, SessionInfoService sessionInfoService) : Controller
{
    private const int Rows = 10;
    private const int Cols = 10;

    /// <summary>
    /// Renders the initial Game of Life board and session information.
    /// </summary>
    /// <returns>The game view with an empty board.</returns>
    public IActionResult Index()
    {
        int[,] board = new int[Rows, Cols];
        boardService.SetBoard(board);
        boardService.SetGeneration(0);

        GameOfLifeViewModel viewModel = new(Rows, Cols)
        {
            Board = new BoardViewModel
            {
                CellStates = board
            },
            SessionInfo = sessionInfoService.UpdateSessionStatistics(board, 0)
        };

        return View(viewModel);
    }

    /// <summary>
    /// Toggles the state of a cell in the board (alive or dead).
    /// </summary>
    /// <param name="cell">The cell coordinates to toggle.</param>
    /// <returns>JSON result containing the new state of the cell.</returns>
    [HttpPost]
    public IActionResult ToggleCell([FromBody] Cell cell)
    {
        int[,] board = boardService.GetBoard();

        board.ToggleCell(cell);

        boardService.SetBoard(board);

        return Json(new { newState = board[cell.Row, cell.Col] });
    }

    /// <summary>
    /// Randomizes the entire Game of Life board.
    /// </summary>
    /// <returns>JSON result with a newly randomized board and generation set to 0.</returns>
    [HttpPost]
    public IActionResult RandomizeBoard()
    {
        int[,] board = gameOfLifeService.RandomizeBoard(Rows, Cols);
        boardService.SetBoard(board);
        boardService.SetGeneration(0);

        return Json(new { success = true, board, generation = 0 });
    }

    /// <summary>
    /// Progresses the board to the next generation based on the Game of Life rules.
    /// </summary>
    /// <returns>JSON result containing the new board, generation, and whether the board changed.</returns>
    [HttpPost]
    public IActionResult NextGeneration()
    {
        int[,] board = boardService.GetBoard();
        int generation = boardService.GetGeneration();

        bool boardChanged = gameOfLifeService.ApplyRulesForNextGeneration(board, out int[,] newBoard);
        boardService.SetBoard(newBoard);
        generation++;
        boardService.SetGeneration(generation);

        SessionInfoViewModel sessionInfo = sessionInfoService.UpdateSessionStatistics(newBoard, generation);

        return Json(new
        {
            success = true,
            board = newBoard,
            generation,
            boardChanged,
            sessionInfo
        });
    }

    /// <summary>
    /// Clears the current board and resets it to a blank state.
    /// </summary>
    /// <returns>JSON result with a cleared board and the generation reset to 0.</returns>
    [HttpPost]
    public IActionResult ClearBoard()
    {
        boardService.ClearBoard(Rows, Cols);
        return Json(new { success = true, board = new int[Rows, Cols] });
    }
}