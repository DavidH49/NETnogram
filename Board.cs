namespace NETnogram; 

public struct Board {
    private bool[,] _board;

    public bool this[Point x] {
        get => _board[x.Y, x.X];
        set => _board[x.Y, x.X] = value;
    }

    public bool this[int y, int x] {
        get => _board[y, x];
        set => _board[y, x] = value;
    }

    public Board(bool[,] board) {
        _board = board;
    }

    public Board(Config config) {
        _board = new bool[config.Height, config.Width];
    }
}