using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Greeting();
            Console.ReadKey();
        }
        public static void Greeting()
        {
            Console.WriteLine("Welcome to the Sudoku console game!");
            // render start board
            //Board game_board = new Board();
            //game_board.PrintBoard();
            //game_board.fillInBoard();

            // render instruction set
            Board easy1 = new Board("sudoku_input_easy_2.txt");
            easy1.PrintBoard();
            easy1.fillInBoard();

        }
    }
}
