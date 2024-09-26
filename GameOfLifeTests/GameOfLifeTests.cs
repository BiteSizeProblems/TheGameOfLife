using GameOfLife.Extensions;
using GameOfLife.Models;

namespace GameOfLifeTests;

[TestClass]
public class BoardExtensionsTests
{
    /// <summary>
    /// Test the ToggleCell method to ensure it toggles the cell from alive to dead.
    /// </summary>
    [TestMethod]
    public void ToggleCell_AliveToDead_TogglesSuccessfully()
    {
        // Arrange
        int[,] board = new int[3, 3];
        Cell cell = new(1, 1);
        board[cell.Row, cell.Col] = 1; // Set cell to alive

        // Act
        board.ToggleCell(cell);

        // Assert
        Assert.AreEqual(0, board[cell.Row, cell.Col]); // The cell should be dead now
    }

    /// <summary>
    /// Test the ToggleCell method to ensure it toggles the cell from dead to alive.
    /// </summary>
    [TestMethod]
    public void ToggleCell_DeadToAlive_TogglesSuccessfully()
    {
        // Arrange
        int[,] board = new int[3, 3];
        Cell cell = new(1, 1);
        board[cell.Row, cell.Col] = 0; // Set cell to dead

        // Act
        board.ToggleCell(cell);

        // Assert
        Assert.AreEqual(1, board[cell.Row, cell.Col]); // The cell should be alive now
    }

    /// <summary>
    /// Test the CountAliveNeighbors method when all neighbors are dead.
    /// </summary>
    [TestMethod]
    public void CountAliveNeighbors_AllDead_ReturnsZero()
    {
        // Arrange
        int[,] board = new int[3, 3]; // All dead cells
        Cell cell = new(1, 1);

        // Act
        int aliveNeighbors = board.CountAliveNeighbors(cell);

        // Assert
        Assert.AreEqual(0, aliveNeighbors); // Should return 0 alive neighbors
    }

    /// <summary>
    /// Test the CountAliveNeighbors method when three neighbors are alive.
    /// </summary>
    [TestMethod]
    public void CountAliveNeighbors_ThreeAliveNeighbors_ReturnsThree()
    {
        // Arrange
        int[,] board = new int[3, 3];
        board[0, 1] = 1; // Top neighbor alive
        board[1, 0] = 1; // Left neighbor alive
        board[2, 1] = 1; // Bottom neighbor alive
        Cell cell = new(1, 1); // Middle cell

        // Act
        int aliveNeighbors = board.CountAliveNeighbors(cell);

        // Assert
        Assert.AreEqual(3, aliveNeighbors); // Should return 3 alive neighbors
    }

    /// <summary>
    /// Test the IsValidCell method for an in-bound cell.
    /// </summary>
    [TestMethod]
    public void IsValidCell_ValidCell_ReturnsTrue()
    {
        // Arrange
        int[,] board = new int[3, 3];
        int row = 1;
        int col = 1;

        // Act
        bool isValid = board.IsValidCell(row, col);

        // Assert
        Assert.IsTrue(isValid); // The cell is within the board
    }

    /// <summary>
    /// Test the IsValidCell method for an out-of-bound cell.
    /// </summary>
    [TestMethod]
    public void IsValidCell_InvalidCell_ReturnsFalse()
    {
        // Arrange
        int[,] board = new int[3, 3];
        int row = -1;
        int col = 4;

        // Act
        bool isValid = board.IsValidCell(row, col);

        // Assert
        Assert.IsFalse(isValid); // The cell is out of bounds
    }
}