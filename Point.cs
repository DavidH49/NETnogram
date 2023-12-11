namespace NETnogram;

internal readonly struct Point {
    public readonly int Y;
    public readonly int X;

    /// <summary>
    /// Initializes a new Point object
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public Point(int y, int x) {
        Y = y;
        X = x;
    }
}