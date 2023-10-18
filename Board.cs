namespace NETnogram; 

public class Board {
    // Public variables
    public readonly bool[,] CheckedBoard;
    
    // Private variables
    private readonly bool[,] _board;
    private readonly int[] _boardStatsCol;
    private readonly int[] _boardStatsRow;
    private readonly Config _config;
    private readonly Random _rng = new();

    // Properties
    public bool this[int i, int j] => _board[i, j];


    /// <summary>
    /// If only a Config is specified, the Board class generates the board by itself
    /// </summary>
    public Board(Config config) {
        _config = config;
        
        // Initialize the boards and the check counts
        _board = new bool[_config.Width, _config.Height];
        CheckedBoard = new bool[_config.Width, _config.Height];
        _boardStatsCol = new int[_config.Width];
        _boardStatsRow = new int[_config.Height];
        
        // Generate the board and count it's checks
        MakeBoard();
        NumberBoard();
    }
    
    
    /// <summary>
    /// If a board is specified, the Board class only manages the interaction with
    /// and numbering of the specified board
    /// </summary>
    public Board(Config config, bool[,] board) {
        _config = config;
        _board = board;

        // Initialize the player's board and the check counts
        CheckedBoard = new bool[_config.Width, _config.Height];
        _boardStatsCol = new int[_config.Height];
        _boardStatsRow = new int[_config.Width];
        
        // Count the checks
        NumberBoard();
    }

    
    /// <summary>
    /// Checks if _board and CheckedBoard are equal
    /// to tell if the player has solved the nonogram
    /// </summary>
    public bool CheckBoardFinished() {
        for (int y = 0; y < _config.Width; y++) {
            for (int x = 0; x < _config.Height; x++) {
                if (_board[y, x] != CheckedBoard[y, x]) return false;
            }
        }

        return true;
    }

    
    /// <summary>
    /// Randomly sets _config.Checked many tiles in _board to true
    /// </summary>
    private void MakeBoard() {
        // Place random checks
        for (int i = 0; i < _config.Checked; i++) {
            bool repeat;
            int y;
            int x;
            
            // Should fill the entire board if Checks = Width * Height, but doesn't work
            do {
                y = _rng.Next(_config.Height);
                x = _rng.Next(_config.Width);

                repeat = _board[y, x];
            } while (repeat);

            _board[x, y] = true;
        }
    }


    /// <summary>
    /// Counts how many checks there are in each row and column
    /// </summary>
    private void NumberBoard() {
        for (int y = 0; y < _config.Height; y++) {
            for (int x = 0; x < _config.Width; x++) {
                if (!_board[y, x]) continue;
                
                _boardStatsRow[y]++;
                _boardStatsCol[x]++;
            }
        }
    }
    
    
    /// <summary>
    /// Iterates through CheckedBoard,
    /// prints how many checks are in each row and column,
    /// prints every false as " - "
    /// and every true as " O "
    /// </summary>
    public void PrintBoard() {
        Console.Write("    ");
        
        // Prints how many checks are in each column
        for (int x = 0; x < _config.Height; x++) {
            if (_boardStatsCol[x] == 0) {
                Console.Write("   ");
                continue;
            }
            
            Console.Write(_boardStatsCol[x] + "  ");
        }
        
        Console.Write("\n");
        
        // Prints which tiles of CheckedBoard are checked and how many checks there are in each row
        for (int y = 0; y < _config.Width; y++) {
            if (_boardStatsRow[y] == 0) {
                Console.Write("   ");
            } else {
                Console.Write(_boardStatsRow[y] + "  ");
            }
            
            for (int x = 0; x < _config.Height; x++) {
                if (CheckedBoard[y, x]) {
                    Console.Write(" O ");
                    continue;
                }
                
                Console.Write(" - ");
            }
            
            Console.Write("\n");
        }
    }
    
    
    /// <summary>
    /// Iterates through _board,
    /// prints every false as " - "
    /// and every true as " O "
    /// </summary>
    public void PrintSolvedBoard() {
        for (int y = 0; y < _config.Width; y++) {
            for (int x = 0; x < _config.Height; x++) {
                if (_board[y, x]) {
                    Console.Write(" O ");
                    continue;
                }
                
                Console.Write(" - ");
            }
            
            Console.Write("\n");
        }
    }
}