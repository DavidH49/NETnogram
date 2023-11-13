namespace NETnogram;

internal class Program {
    private readonly GameBoard _gameBoard = new(Config);
    private static readonly Config Config = new(10, 5, 4);


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
            Console.Clear();
            _gameBoard.PrintBoard();

            var tileInput = GetTileInput();

            try {
                if (!_gameBoard[tileInput]) {
                    EndGameFailed();
                    break;
                }
            } catch (IndexOutOfRangeException) {
                Console.WriteLine("Your row or column was out of range!");
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
        Console.WriteLine("\nGame Over");
        Console.WriteLine("\nThe solved board was:");
        _gameBoard.PrintSolvedBoard();
    }


    private void EndGameSolved() {
        Console.WriteLine("\nYou won!");
    }
    
    
    /// <summary>
    /// Asks the player for the row and column they want to place their check in
    /// </summary>
    private Point GetTileInput() {
        Console.Write("\n");
            
        Console.WriteLine("Row: ");
        var y = int.Parse(Console.ReadLine() ?? "-1");
            
        Console.WriteLine("Column: ");
        var x = int.Parse(Console.ReadLine() ?? "-1");

        return new Point(y, x);
    }
}