using System.Drawing;

namespace NETnogram; 

public class GameBoard {
    // Public variables
    public Board PlayerBoard;
    
    // Private variables
    private Board _board;
    private readonly string[] _boardStatsCol;
    private readonly string[] _boardStatsRow;
    private readonly Config _config;
    private readonly Random _rng = new();

    // Properties
    public bool this[Point x] => _board[x];


    /// <summary>
    /// If only a Config is specified, the Board class generates the board by itself
    /// </summary>
    public GameBoard(Config config) {
        if ((config.Width * config.Height) < config.TilesChecked) {
            throw new ArgumentException("There are more checked tiles than tiles on the board.");
        }

        _config = config;
        
        // Initialize the boards and the check counts
        _board = new Board(_config);
        PlayerBoard = new Board(_config);
        _boardStatsCol = new string[_config.Width];
        
        // Generate the board and count it's checks
        GenerateBoard();
        _boardStatsRow = CountHorizontalChecks();
        _boardStatsCol = CountVerticalChecks();
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
        _boardStatsCol = new string[_config.Width];
        
        // Count the checks
        _boardStatsRow = CountHorizontalChecks();
        _boardStatsCol = CountVerticalChecks();
    }

    
    /// <summary>
    /// Checks if _board and CheckedBoard are equal
    /// to tell if the player has solved the nonogram
    /// </summary>
    public bool CheckBoardFinished() {
        for (var y = 0; y < _config.Height; y++) {
            for (var x = 0; x < _config.Width; x++) {
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
        for (var i = 0; i < _config.TilesChecked; i++) {
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
    /// Unholy method to count how many checks there are in each row
    /// </summary>
    private string[] CountHorizontalChecks() {
        string[] horiArr = new string[_config.Height];

        // Horizontal
        for (var y=0; y<_config.Height; y++) {
            var rowStr = "";
            var i = 0;

            for (var x=0; x<_config.Width; x++) {
                if (_board[y, x]) {
                    i++;
                } else {
                    rowStr += i > 0 ? i + " " : "";
                    i = 0;
                }

                if (x == _config.Width) {
                    rowStr += i;
                }
            }

            rowStr += i > 0 ? i : "";
            horiArr[y] = rowStr;
        }

        // Get how long the longest string in the array is
        var lastLen = 0;
        foreach (var horiStr in horiArr) {
            lastLen = Math.Max(lastLen, horiStr.Length);
        }

        // Add whitespaces to make all strings equally long
        for (var i = 0; i < horiArr.Length; i++) {
            if (horiArr[i].Length < lastLen) {
                var lenDiff = lastLen - horiArr[i].Length;

                for (int j = 0; j < lenDiff; j++) {
                    horiArr[i] += " ";
                }
            }
        }

        return horiArr;
    }


    private string[] CountVerticalChecks() {
        string[] vertArr = new string[_config.Width];

        for (var x=0; x<_config.Width; x++) {
            var colStr = "";
            var i = 0;

            for (var y= 0; y<_config.Height; ++y) {
                if (_board[y, x]) {
                    i++;
                } else {
                    colStr += i > 0 ? i : "";
                    i = 0;
                    continue;
                }
            }

            colStr += i > 0 ? i : "";
            vertArr[x] = colStr;
        }

        return vertArr;
    }
    
    
    /// <summary>
    /// Iterates through CheckedBoard,
    /// prints how many checks are in each row and column,
    /// prints every false as " - "
    /// and every true as " O "
    /// </summary>
    public void PrintBoard() {
        // Prints which tiles of PlayerBoard are checked and how many checks there are in each row
        for (var y = 0; y < _config.Height; y++) {
            Console.Write(_boardStatsRow[y] + "  ");

            for (var x = 0; x < _config.Width; x++) {
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
        for (var y = 0; y < _config.Height; y++) {
            for (var x = 0; x < _config.Width; x++) {
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