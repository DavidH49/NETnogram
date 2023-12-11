using static System.Console;

namespace NETnogram;

internal class Program {
    private readonly GameBoard _gameBoard = new(Config);
    private static readonly Config Config = new(5, 5, 15);


    private static void Main(string[] args) {
        var program = new Program();
        program.GameLoop();
    }


    /// <summary>
    /// Prints the current board,
    /// asks the player which row and column they want to place their check,
    /// check if the player has solved the nonogram,
    /// repeat
    /// </summary>
    private void GameLoop() {
        while (true) {
            Clear();
            _gameBoard.PrintBoard();

            var tileInput = GetTileInput();

            try {
                if (!_gameBoard[tileInput]) {
                    EndGameFailed();
                    break;
                }
            }

            catch (IndexOutOfRangeException) {
                continue;
            }

            _gameBoard.PlayerBoard[tileInput] = true;

            if (_gameBoard.CheckBoardFinished()) {
                EndGameSolved();
                break;
            }
        }
    }


    private void EndGameFailed() {
        WriteLine("\nGame Over");
        WriteLine("\nThe solved board was:");
        _gameBoard.PrintSolvedBoard();
    }


    private void EndGameSolved() {
        WriteLine("\nYou won!");
    }


    /// <summary>
    /// Asks the player for the row and column they want to place their check in
    /// </summary>
    private Point GetTileInput() {
        Write("\n");

        try {
            WriteLine("Row: ");
            var y = int.Parse(ReadLine() ?? "-1");

            WriteLine("Column: ");
            var x = int.Parse(ReadLine() ?? "-1");

            return new Point(y, x);
        }

        catch (FormatException) {
            return new Point(-1, -1);
        }
    }
}