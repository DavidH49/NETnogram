namespace NETnogram; 

public readonly struct Point {
    public readonly int Y;
    public readonly int X;

    public Point(int y, int x) {
        Y = y;
        X = x;
    }

    public static Point operator -(Point a)
        => new Point(-a.Y, -a.X);

    public static Point operator +(Point a, Point b)
        => new Point(a.Y + b.Y, a.X + b.X);

    public static Point operator -(Point a, Point b)
        => new Point(a.Y - b.Y, a.X - b.X);

    public static Point operator *(Point a, int b)
        => new Point(a.Y * b, a.X * b);

    public static Point Zero => new Point(0, 0);
    public static Point One => new Point(1, 1);

    public static Point Up => new Point(-1, 0);
    public static Point Down => new Point(1, 0);
    public static Point Left => new Point(0, -1);
    public static Point Right => new Point(0, 1);

    public static Point UpLeft => Up + Left;
    public static Point UpRight => Up + Right;
    public static Point DownLeft => Down + Left;
    public static Point DownRight => Down + Right;
}