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
            // Start of application
            string game_selection = Greeting();
            Console.WriteLine("Game Selection Results: " + game_selection);

            // Game initialization 
            Board game_board = new Board(game_selection);
            game_board.PrintBoard();
            DisplayControls();

            // Game Console interface loop
            Boolean not_exited = true;
            while(!game_board.Done() && not_exited)
            {
                Console.Write(">");
                string user_input = Console.ReadLine().ToLower();
                if (user_input == "help")
                {
                    DisplayControls();
                }
                else if(user_input == "solve")
                {
                    Boolean SolveChangedSomething = game_board.PassThroughAndFillOutTheCertainSquares(false);
                    if(SolveChangedSomething)
                        game_board.PrintBoard();
                    else
                        Console.WriteLine("Solve was unable to make changes, it is up to user input to make progress.");
                }
                else if (user_input == "exit")
                {
                    not_exited = false;
                }
                //else if (user_input == "undo") // for some reason i am struggling to make this copy correctly
                //{
                //    game_board.Undo();
                //    game_board.PrintBoard();
                //}
                else if(user_input.Length==7)
                {
                    if(user_input[0]=='[' && user_input[2]==',' &&user_input[4]==']' && user_input[5]==':')
                    {
                        // using char properties to figure out if the coords are 0-8 and the input number is 1-9
                        int row, col;
                        string xin = "" + user_input[1];
                        string yin = "" + user_input[3];
                        if(Int32.TryParse(xin, out row))
                        {
                            if(row < 9 && row >-1)
                            {
                                if(Int32.TryParse(yin, out col))
                                {
                                    if(col < 9 && col > -1)
                                    {
                                        if (user_input[6] > 48 && user_input[6] < 58)
                                        {
                                            Console.WriteLine("User input " + user_input[6] + " at [" + user_input[1] + ", " + user_input[3] + "]");
                                            // evaluate if possible
                                            Boolean WasMovePossible = game_board.isPossible(row,col,user_input[6]);
                                            if(WasMovePossible)
                                            {
                                                Console.WriteLine("This move is possible put it on the board");
                                                game_board.AlterCell(row, col, user_input[6], 5);
                                                game_board.PrintBoard();
                                            }
                                            else
                                            {
                                                Console.WriteLine("This move was NOT possible");
                                            }
                                        }
                                        if(user_input[6] == '_')
                                        {
                                            if (game_board.UserInputSquare(row, col))
                                            {
                                                Console.WriteLine("Clearing out this decision");
                                                game_board.AlterCell(row, col, 'k', 10);
                                                game_board.PrintBoard();
                                            }
                                            else
                                                Console.WriteLine("Cannot clear specified cell.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("See controls, your syntax is incorrect");
                    }
                }
            }
            if(not_exited)
            {
                Console.WriteLine("You Won! Congrats");
                Console.ReadKey();
                // Display statistics like time and number of moves
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
                Console.ReadKey();
            }
        }
        public static void DisplayControls()
        {
            Console.WriteLine("The controls for this game are as follows: ");
            Console.WriteLine("\t[row,col]:x");
            Console.WriteLine("Where row is from 0-8, col is from 0-8 and x is the numeric input 1-9");
            Console.WriteLine("Type solve to make a pass through and solve the obvious squares.");
            Console.WriteLine("Type exit to quit");
            Console.WriteLine("Type help for these controls to be displayed again.");
        }

        public static string Greeting()
        {
            Console.WriteLine("Welcome to the Sudoku console game!");
            string[] list_of_games = new string[] { "sudoku_input_easy_1.txt", "sudoku_input_easy_2.txt", "sudoku_input_intermediate_1.txt" };

            Console.WriteLine("We currently have " + list_of_games.Length + " games you can play.");
            Console.WriteLine();

            Console.WriteLine("Please select which game you would like to play.");
            Console.WriteLine("================================================");
            for (int i =0; i < list_of_games.Length; i++)
            {
                Console.WriteLine("\t[" + i + "]: " + list_of_games[i]);
            }
            Console.WriteLine("================================================");
            int selection = ReturnSelection("Input the number of the game you would like to play here: ", list_of_games.Length);

            return list_of_games[selection];
            // render start board
            //Board game_board = new Board();
            //game_board.PrintBoard();
            //game_board.fillInBoard();

            // render instruction set
            //Board easy1 = new Board("sudoku_input_easy_1.txt");
            //easy1.PrintBoard();
            //easy1.fillInBoard();

        }
        public static int ReturnSelection(string prompt, int size)
        {
            Console.Write(prompt);
            int selector = -1;
            if (Int32.TryParse(Console.ReadLine(), out selector))
            {
                if(selector>=size ||selector<0)
                    return ReturnSelection("Please Try agian and only use the keys 0-" + (size-1)+": ", size);
                return selector;
            }
            else
            {
                return ReturnSelection("Please Try agian and only use the keys 0-" + (size-1) + ": ", size);
            }

        }
    }
}

