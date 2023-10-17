using static System.Random;

namespace NETnogram; 

public class Board {
    public bool[,] CheckedBoard;
    
    private bool[,] _board;
    private int[] _boardStatsRow;
    private int[] _boardStatsCol;
    private Config _config;

    private Random _rng = new();


    public bool this[int i, int j] => _board[i, j];


    public Board(Config c, bool[,] board) {
        _config = c;
        this._board = board;

        CheckedBoard = new bool[_config.Width, _config.Height];
        _boardStatsCol = new int[_config.Width];
        _boardStatsRow = new int[_config.Height];
    }


    public Board(Config c) {
        _config = c;
        
        _board = new bool[_config.Width, _config.Height];
        CheckedBoard = new bool[_config.Width, _config.Height];
        _boardStatsCol = new int[_config.Width];
        _boardStatsRow = new int[_config.Height];
        
        MakeBoard();
    }
    
    
    public void PrintBoard() {
        Console.Write("    ");
        for (int i = 0; i < _config.Width; i++) {
            Console.Write(_boardStatsCol[i] + "  ");
        }
        
        Console.Write("\n");
        
        for (int i = 0; i < _config.Width; i++) {
            Console.Write(_boardStatsRow[i] + "  ");
            
            for (int j = 0; j < _config.Height; j++) {
                if (CheckedBoard[i, j]) {
                    Console.Write(" O ");
                    continue;
                }
                
                Console.Write(" - ");
            }
            
            Console.Write("\n");
        }
    }
    
    
    public void PrintSolvedBoard() {
        Console.Write("    ");
        for (int i = 0; i < _config.Width; i++) {
            Console.Write(_boardStatsCol[i] + "  ");
        }
        
        Console.Write("\n");
        
        for (int i = 0; i < _config.Width; i++) {
            Console.Write(_boardStatsRow[i] + "  ");
            
            for (int j = 0; j < _config.Height; j++) {
                if (_board[i, j]) {
                    Console.Write(" O ");
                    continue;
                }
                
                Console.Write(" - ");
            }
            
            Console.Write("\n");
        }
    }


    public bool CheckBoardFinished() {
        for (int i = 0; i < _config.Width; i++) {
            for (int j = 0; j < _config.Height; j++) {
                if (_board[i, j] != CheckedBoard[i, j]) return false;
            }
        }

        return true;
    }
    
    
    private void MakeBoard() {
        // Fill the board with nothing
        for (int i = 0; i < _config.Width; i++) {
            for (int j = 0; j < _config.Height; j++) {
                _board[i, j] = false;
            }
        }
        
        // Place random checks
        for (int i = 0; i < _config.Checked; i++) {
            int x = _rng.Next(_config.Width - 1);
            int y = _rng.Next(_config.Height - 1);

            if (!_board[x, y]) {
                _board[x, y] = true;

                _boardStatsRow[x]++;
                _boardStatsCol[y]++;
            }
        }
    }
}