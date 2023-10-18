namespace NETnogram;

internal class Program {
    private readonly Board _board = new(Config);
    private static readonly Config Config = new() {
        Width = 5,
        Height = 5,
        Checked = 25,
    };


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
            _board.PrintBoard();

            var tile = GetTileInput();

            try {
                if (!_board[tile.y, tile.x]) {
                    EndGameFailed();
                    break;
                }
            } catch (IndexOutOfRangeException) {
                Console.WriteLine("Your row or column was out of range!");
                continue;
            }
            
            _board.CheckedBoard[tile.Item1, tile.Item2] = true;

            if (_board.CheckBoardFinished()) {
                EndGameSolved();
                break;
            }
        }
    }


    private void EndGameFailed() {
        Console.WriteLine("\nGame Over");
        Console.WriteLine("\nThe solved board was:");
        _board.PrintSolvedBoard();
    }


    private void EndGameSolved() {
        Console.WriteLine("\nYou won!");
    }
    
    
    /// <summary>
    /// Asks the player for the row and column they want to place their check in
    /// </summary>
    private (int y, int x) GetTileInput() {
        Console.Write("\n");
            
        Console.WriteLine("Row: ");
        int row = int.Parse(Console.ReadLine() ?? string.Empty);
            
        Console.WriteLine("Column: ");
        int col = int.Parse(Console.ReadLine() ?? string.Empty);

        return (row, col);
    }
}