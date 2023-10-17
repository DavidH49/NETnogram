using System;
using System.Collections.Generic;

namespace NETnogram;

internal class Program {
    public Board Board = new Board(Config);
    private static readonly Config Config = new Config {
        Checked = 15,
        Height = 5,
        Width = 5
    };


    private static void Main(string[] args) {
        var program = new Program();
        program.StartGame();
    }
    
    
    public void StartGame() {
        PlayGame();
    }
    
    
    public void PlayGame() {
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
                break;
            }

            Board.CheckedBoard[row, col] = true;

            if (Board.CheckBoardFinished()) {
                Console.WriteLine("\nYou won!");
                break;
            }
        }
        
        Console.WriteLine("\n\nThe solved board was:");
        Board.PrintSolvedBoard();
    }
}