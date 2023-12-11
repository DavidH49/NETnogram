namespace NETnogram;

internal readonly struct Config {
    public readonly int Width;
    public readonly int Height;
    public readonly int TilesChecked;

    /// <summary>
    /// Initializes a new Config object
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="tilesChecked"></param>
    /// <exception cref="ArgumentException"></exception>
    public Config(int width, int height, int tilesChecked) {
        if (width * height < tilesChecked) {
            throw new ArgumentException("There are more checked tiles than tiles on the board.");
        }

        if (width < 1 || height < 1) {
            throw new ArgumentException("Width and Height cannot be negative or 0!");
        }

        if (tilesChecked < 0) {
            throw new ArgumentException("TilesChecked cannot be negative!");
        }

        Width = width;
        Height = height;
        TilesChecked = tilesChecked;
    }
}