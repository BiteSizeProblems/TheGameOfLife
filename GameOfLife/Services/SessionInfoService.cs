using GameOfLife.Extensions;
using GameOfLife.Models;
using GameOfLife.ViewModels;

namespace GameOfLife.Services;

/// <summary>
/// Service to update session statistics based on the board state.
/// </summary>
public class SessionInfoService
{
    /// <summary>
    /// Updates session statistics like total alive cells, dead cells, and cells at risk of underpopulation or overcrowding.
    /// </summary>
    /// <param name="board">The current board state.</param>
    /// <param name="generation">The current generation number.</param>
    /// <returns>An updated <see cref="SessionInfoViewModel"/> with session statistics.</returns>
    public SessionInfoViewModel UpdateSessionStatistics(int[,] board, int generation)
    {
        SessionInfoViewModel sessionInfo = new()
        {
            Generation = generation,
            TotalAliveCells = 0,
            TotalDeadCells = 0,
            CellsAtRiskOfUnderpopulation = 0,
            CellsAtRiskOfOvercrowding = 0,
            CellsThatWillSurvive = 0,
            CellsThatWillReproduce = 0
        };

        for (int i = 0; i < board.GetRows(); i++)
        {
            for (int j = 0; j < board.GetCols(); j++)
            {
                Cell cell = new() { Row = i, Col = j };

                int aliveNeighbors = board.CountAliveNeighbors(cell);

                if (board.IsAlive(cell))
                {
                    sessionInfo.TotalAliveCells++;

                    // Underpopulation: Fewer than 2 neighbors
                    if (aliveNeighbors < 2)
                    {
                        sessionInfo.CellsAtRiskOfUnderpopulation++;
                    }
                    // Overcrowding: More than 3 neighbors
                    else if (aliveNeighbors > 3)
                    {
                        sessionInfo.CellsAtRiskOfOvercrowding++;
                    }

                    // Survival: Exactly 2 or 3 neighbors
                    if (aliveNeighbors is 2 or 3)
                    {
                        sessionInfo.CellsThatWillSurvive++;
                    }
                }
                else
                {
                    sessionInfo.TotalDeadCells++;

                    // Reproduction: Dead cell with exactly 3 alive neighbors
                    if (aliveNeighbors == 3)
                    {
                        sessionInfo.CellsThatWillReproduce++;
                    }
                }
            }
        }

        return sessionInfo;
    }
}