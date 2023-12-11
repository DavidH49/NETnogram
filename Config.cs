namespace NETnogram;

internal readonly struct Config {
    public readonly int Width;
    public readonly int Height;
    public readonly int TilesChecked;

    public Config(int width, int height, int tilesChecked) {
        Width = width;
        Height = height;
        TilesChecked = tilesChecked;
    }
}