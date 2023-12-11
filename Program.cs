using System;
using static System.Console;

namespace NETnogram;

internal class Program {
    private GameBoard _gameBoard;
    private static Config Config;


    private static void Main() {
        var program = new Program();
        program.Init();
        program.GameLoop();
    }


    private void Init() {
        try {
            int w = 0, h = 0, c = 0;

            WriteLine("Board Width:");
            w = int.Parse(ReadLine() ?? "-1");

            WriteLine("Board Heigth");
            h = int.Parse(ReadLine() ?? "-1");

            WriteLine("Checked Tiles:");
            c = int.Parse(ReadLine() ?? "-1");

            Config = new Config(w, h, c);
            _gameBoard = new GameBoard(Config);
        }
        catch (Exception e) {
            WriteLine($"\n{e.Message}");
            Environment.Exit(1);
        }
    }


    /// <summary>
    /// Prints the current board,
    /// asks the player which row and column they want to place their check,
    /// check if the player has solved the nonogram,
    /// repeat
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    private void GameLoop() {
        while (true) {
            Clear();
            _gameBoard.PrintBoard();

            var tileInput = GetTileInput();

            try {
                if (!_gameBoard[tileInput]) {
                    EndGameFailed();
                    break;
                }
            }
            catch (IndexOutOfRangeException) {
                continue;
            }

            _gameBoard.PlayerBoard[tileInput] = true;

            if (_gameBoard.CheckBoardFinished()) {
                EndGameSolved();
                break;
            }
        }
    }


    /// <summary>
    /// Should be printed when the player lost by checking a wrong tile
    /// </summary>
    private void EndGameFailed() {
        WriteLine("\nGame Over");
        WriteLine("The solved board was:\n");
        _gameBoard.PrintSolvedBoard();
    }


    /// <summary>
    /// Should be printed when the player solved the board
    /// </summary>
    private void EndGameSolved() {
        WriteLine("\nYou won!");
    }


    /// <summary>
    /// Asks the player for the row and column they want to place their check in
    /// </summary>
    /// <returns>a Point object containing the row and column the player typed into the CLI</returns>
    /// <exception cref="FormatException"></exception>
    private Point GetTileInput() {
        Write("\n");

        try {
            WriteLine("Row: ");
            var y = int.Parse(ReadLine() ?? "-1");

            WriteLine("Column: ");
            var x = int.Parse(ReadLine() ?? "-1");

            return new Point(y - 1, x - 1);
        }
        catch (FormatException) {
            return new Point(-1, -1);
        }
    }
}