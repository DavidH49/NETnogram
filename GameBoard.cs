namespace NETnogram; 

public class GameBoard {
    // Public variables
    public Board PlayerBoard;
    
    // Private variables
    private Board _board;
    private readonly int[] _boardStatsCol;
    private readonly int[] _boardStatsRow;
    private readonly Config _config;
    private readonly Random _rng = new();

    // Properties
    public bool this[Point x] => _board[x];


    /// <summary>
    /// If only a Config is specified, the Board class generates the board by itself
    /// </summary>
    public GameBoard(Config config) {
        _config = config;
        
        // Initialize the boards and the check counts
        _board = new Board(_config);
        PlayerBoard = new Board(_config);
        _boardStatsCol = new int[_config.Width];
        _boardStatsRow = new int[_config.Height];
        
        // Generate the board and count it's checks
        GenerateBoard();
        CountChecksOnBoard();
    }
    
    
    /// <summary>
    /// If a board is specified, the Board class only manages the interaction with
    /// and numbering of the specified board
    /// </summary>
    public GameBoard(Config config, Board board) {
        _config = config;
        _board = board;

        // Initialize the player's board and the check counts
        PlayerBoard = new Board(_config);
        _boardStatsCol = new int[_config.Width];
        _boardStatsRow = new int[_config.Height];
        
        // Count the checks
        CountChecksOnBoard();
    }

    
    /// <summary>
    /// Checks if _board and CheckedBoard are equal
    /// to tell if the player has solved the nonogram
    /// </summary>
    public bool CheckBoardFinished() {
        for (int y = 0; y < _config.Height; y++) {
            for (int x = 0; x < _config.Width; x++) {
                if (_board[y, x] != PlayerBoard[y, x]) return false;
            }
        }

        return true;
    }

    
    /// <summary>
    /// Randomly sets _config.Checked many tiles in _board to true
    /// </summary>
    private void GenerateBoard() {
        // Place random checks
        for (int i = 0; i < _config.TilesChecked; i++) {
            bool repeat;
            Point p;
            
            // Will fill the entire board if Checks = Width * Height
            do {
                p = new Point(
                    _rng.Next(_config.Height),
                    _rng.Next(_config.Width));

                repeat = _board[p];
            } while (repeat);

            _board[p] = true;
        }
    }


    /// <summary>
    /// Counts how many checks there are in each row and column
    /// </summary>
    private void CountChecksOnBoard() {
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
        for (int x = 0; x < _config.Width; x++) {
            if (_boardStatsCol[x] == 0) {
                Console.Write("   ");
                continue;
            }
            
            Console.Write(_boardStatsCol[x] + "  ");
        }
        
        Console.Write("\n");
        
        // Prints which tiles of PlayerBoard are checked and how many checks there are in each row
        for (int y = 0; y < _config.Height; y++) {
            Console.Write(_boardStatsRow[y] + "  ");

            for (int x = 0; x < _config.Width; x++) {
                if (PlayerBoard[y, x]) {
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
        for (int y = 0; y < _config.Height; y++) {
            for (int x = 0; x < _config.Width; x++) {
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