namespace GameOfLife.Exceptions;

/// <summary>
/// Exception thrown when there are errors related to the board state.
/// </summary>
public class BoardException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoardException"/> class with a default message.
    /// </summary>
    public BoardException() : base("The board is invalid.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BoardException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BoardException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BoardException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public BoardException(string message, Exception innerException) : base(message, innerException)
    {
    }
}