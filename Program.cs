using System;
using System.Collections.Generic;

namespace NETnogram;

internal class Program {
    public readonly Board Board = new Board(Config);
    private static readonly Config Config = new Config {
        Width = 5,
        Height = 5,
        Checked = 15,
    };


    private static void Main(string[] args) {
        var program = new Program();
        program.GameLoop();
    }
    
    
    /// <summary>
    /// Prints the current board,
    /// asks the player which row and column they want to place their check,
    /// check if the player has solved the nonogram,
    /// repeat
    ///
    /// When lost: prints the solved board
    /// </summary>
    public void GameLoop() {
        while (true) {
            Console.Clear();
            Board.PrintBoard();

            Console.Write("\n");
            
            Console.WriteLine("Row: ");
            int row = int.Parse(Console.ReadLine() ?? string.Empty);
            
            Console.WriteLine("Column: ");
            int col = int.Parse(Console.ReadLine() ?? string.Empty);

            if (!Board[row, col]) {
                Console.WriteLine("\nGame Over");
                Console.WriteLine("\nThe solved board was:");
                Board.PrintSolvedBoard();
                break;
            }

            Board.CheckedBoard[row, col] = true;

            if (Board.CheckBoardFinished()) {
                Console.WriteLine("\nYou won!");
                break;
            }
        }
        
    }
}