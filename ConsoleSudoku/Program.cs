using System;
using System.Threading;

namespace ConsoleSudoku
{
    class Program
    {
        private static Board game_board;
        static void Main(string[] args)
        {
            // Start of application
            
            // Game initialization 
            
            //Boolean a = game_board.PassThroughAndFillOutTheCertainSquares(false);
            //game_board.PrintBoard();
            // ********************** FORTESTING PURPOSES YOU ALTERED the intermediate text file [0,6]:9***** /////

            Thread Brute = new Thread(Program.BruteForceSolve);
            Brute.Start("sudoku_input_intermediate_1.txt");

            //Console.WriteLine(game_board.BruteForceSolver(0, 0, 0));
            //game_board.PrintBoard();

            Console.ReadKey();
            /*DisplayControls();

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
                else if (user_input == "undo") // for some reason i am struggling to make this copy correctly
                {
                    if(game_board.Undo())
                        game_board.PrintBoard();
                    else
                        Console.WriteLine("Undo is not possible at this time.");
                }
                else if(user_input.Length==7)
                {
                    UserInput(user_input);
                }
                else if(user_input=="numpossible")
                {
                    Console.WriteLine("There are "+game_board.GetNumPossibleMoves() + " potential moves.");
                }
                else
                {
                    Console.WriteLine("Your syntax was incorrect. Type help to see some examples.");
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
            } //*/
            Console.WriteLine();
        }
        public static void BruteForceSolve(object data)
        {
            //string game_selection = Greeting();
            //Console.WriteLine("Game Selection Results: " + game_selection);

            game_board = new Board(data.ToString());
            game_board.PrintBoard();
            Console.WriteLine("Messing with threads");
            Boolean a = game_board.BruteForceSolver(0, 0, 0);
            game_board.PrintBoard();
        }

        public static void DisplayControls()
        {
            Console.WriteLine("The controls for this game are as follows: ");
            Console.WriteLine("\t[row,col]:x");
            Console.WriteLine("Where row is from 0-8, col is from 0-8 and x is the numeric input 1-9");
            Console.WriteLine("Put an underscore in place of x to clear a user input integer");
            Console.WriteLine("Put a question mark in place of x to receive a hint.");
            Console.WriteLine("Type solve to make a pass through and solve the obvious squares.");
            Console.WriteLine("Type exit to quit");
            Console.WriteLine("Type help for these controls to be displayed again.");
        }

        public static string Greeting()
        {
            Console.WriteLine("Welcome to the Sudoku console game!");
            string[] list_of_games = new string[] { "sudoku_input_easy_1.txt", "sudoku_input_easy_2.txt", "sudoku_input_intermediate_1.txt", "random.txt" };

            Console.WriteLine("We currently have " + list_of_games.Length + " games you can play.");
            Console.WriteLine();

            Console.WriteLine("Please select which game you would like to play.");
            Console.WriteLine("================================================");
            for (int i = 0; i < list_of_games.Length; i++)
            {
                Console.WriteLine("\t[" + i + "]: " + list_of_games[i]);
            }
            Console.WriteLine("================================================");
            int selection = ReturnSelection("Input the number of the game you would like to play here: ", list_of_games.Length);

            return list_of_games[selection];
        }
        public static int ReturnSelection(string prompt, int size)
        {
            Console.Write(prompt);
            int selector = -1;
            if (Int32.TryParse(Console.ReadLine(), out selector))
            {
                if (selector >= size || selector < 0)
                    return ReturnSelection("Please Try agian and only use the keys 0-" + (size - 1) + ": ", size);
                return selector;
            }
            else
            {
                return ReturnSelection("Please Try agian and only use the keys 0-" + (size - 1) + ": ", size);
            }
        }

        public static void UserInput(string user_input)
        {
            if (user_input[0] == '[' && user_input[2] == ',' && user_input[4] == ']' && user_input[5] == ':')
            {
                // using char properties to figure out if the coords are 0-8 and the input number is 1-9
                int row, col;
                string xin = "" + user_input[1];
                string yin = "" + user_input[3];
                if (Int32.TryParse(xin, out row))
                {
                    if (row < 9 && row > -1)
                    {
                        if (Int32.TryParse(yin, out col))
                        {
                            if (col < 9 && col > -1)
                            {
                                if (user_input[6] > 48 && user_input[6] < 58)
                                {
                                    Console.WriteLine("User input " + user_input[6] + " at [" + user_input[1] + ", " + user_input[3] + "]");
                                    // evaluate if possible
                                    Boolean WasMovePossible = game_board.isPossible(row, col, user_input[6]);
                                    if (WasMovePossible)
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
                                else if (user_input[6] == '_')
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
                                else if (user_input[6] == '?')
                                {
                                    Console.WriteLine("Add in hint functionality");
                                    //TODO add in hint functionality
                                    // have it display all the probabilities
                                    // Board.GetHintFor(int row, int col)
                                    // going to be a void method 
                                    // display input and its probability of being the answer

                                    //potentially later filter the display to include only the possible ones

                                    game_board.GetHint(row, col);
                                }
                            }
                            else
                            {
                                Console.WriteLine("The col value has to be from 0-8. Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The col value has to be an integer. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The row value has to be an integer from 0-8. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("The row value has to be an integer. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("See controls, your syntax is incorrect");
            }
        }
    }
}


