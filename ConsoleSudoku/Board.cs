using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSudoku
{
    public class Board
    {
        /// <summary>
        /// Constructor for the Board class, this establishes the 9x9 Square board, initializes the 9 boxes in for data collections as well
        /// the boxes are represented as:
        /// 0 1 2
        /// 3 4 5
        /// 6 7 8
        /// The filename is passed off to the read in function in the try catch block.
        /// </summary>
        /// <param name="filename"></param>
        public Board(string filename)
        {
            // Assigning memory spaces to the global variables
            _board = new Square[9, 9];
            _boxes = new Box[] { new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box() };
            
            // Fill in the sudoku board from the file
            try { ReadInSudokuBoard(filename); }
            catch(Exception ex) { Console.WriteLine(ex.Message); }

            _previousBoards = new Stack<Square[,]>();

            List<Coord> Rows0to8 = new List<Coord>(new Coord[] {
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(0, 3), new Coord(0, 4), new Coord(0, 5), new Coord(0, 6), new Coord(0, 7), new Coord(0, 8), // Row 0
                new Coord(1, 0), new Coord(1, 1), new Coord(1, 2), new Coord(1, 3), new Coord(1, 4), new Coord(1, 5), new Coord(1, 6), new Coord(1, 7), new Coord(1, 8), // Row 1
                new Coord(2, 0), new Coord(2, 1), new Coord(2, 2), new Coord(2, 3), new Coord(2, 4), new Coord(2, 5), new Coord(2, 6), new Coord(2, 7), new Coord(2, 8), // Row 2
                new Coord(3, 0), new Coord(3, 1), new Coord(3, 2), new Coord(3, 3), new Coord(3, 4), new Coord(3, 5), new Coord(3, 6), new Coord(3, 7), new Coord(3, 8), // Row 3
                new Coord(4, 0), new Coord(4, 1), new Coord(4, 2), new Coord(4, 3), new Coord(4, 4), new Coord(4, 5), new Coord(4, 6), new Coord(4, 7), new Coord(4, 8), // Row 4
                new Coord(5, 0), new Coord(5, 1), new Coord(5, 2), new Coord(5, 3), new Coord(5, 4), new Coord(5, 5), new Coord(5, 6), new Coord(5, 7), new Coord(5, 8), // Row 5
                new Coord(6, 0), new Coord(6, 1), new Coord(6, 2), new Coord(6, 3), new Coord(6, 4), new Coord(6, 5), new Coord(6, 6), new Coord(6, 7), new Coord(6, 8), // Row 6
                new Coord(7, 0), new Coord(7, 1), new Coord(7, 2), new Coord(7, 3), new Coord(7, 4), new Coord(7, 5), new Coord(7, 6), new Coord(7, 7), new Coord(7, 8), // Row 7
                new Coord(8, 0), new Coord(8, 1), new Coord(8, 2), new Coord(8, 3), new Coord(8, 4), new Coord(8, 5), new Coord(8, 6), new Coord(8, 7), new Coord(8, 8)  // Row 8
            });

            List<Coord> Rows8to0 = new List<Coord>(new Coord[] {
                new Coord(8, 0), new Coord(8, 1), new Coord(8, 2), new Coord(8, 3), new Coord(8, 4), new Coord(8, 5), new Coord(8, 6), new Coord(8, 7), new Coord(8, 8), // Row 8
                new Coord(7, 0), new Coord(7, 1), new Coord(7, 2), new Coord(7, 3), new Coord(7, 4), new Coord(7, 5), new Coord(7, 6), new Coord(7, 7), new Coord(7, 8), // Row 7
                new Coord(6, 0), new Coord(6, 1), new Coord(6, 2), new Coord(6, 3), new Coord(6, 4), new Coord(6, 5), new Coord(6, 6), new Coord(6, 7), new Coord(6, 8), // Row 6
                new Coord(5, 0), new Coord(5, 1), new Coord(5, 2), new Coord(5, 3), new Coord(5, 4), new Coord(5, 5), new Coord(5, 6), new Coord(5, 7), new Coord(5, 8), // Row 5
                new Coord(4, 0), new Coord(4, 1), new Coord(4, 2), new Coord(4, 3), new Coord(4, 4), new Coord(4, 5), new Coord(4, 6), new Coord(4, 7), new Coord(4, 8), // Row 4
                new Coord(3, 0), new Coord(3, 1), new Coord(3, 2), new Coord(3, 3), new Coord(3, 4), new Coord(3, 5), new Coord(3, 6), new Coord(3, 7), new Coord(3, 8), // Row 3
                new Coord(2, 0), new Coord(2, 1), new Coord(2, 2), new Coord(2, 3), new Coord(2, 4), new Coord(2, 5), new Coord(2, 6), new Coord(2, 7), new Coord(2, 8), // Row 2
                new Coord(1, 0), new Coord(1, 1), new Coord(1, 2), new Coord(1, 3), new Coord(1, 4), new Coord(1, 5), new Coord(1, 6), new Coord(1, 7), new Coord(1, 8), // Row 1
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(0, 3), new Coord(0, 4), new Coord(0, 5), new Coord(0, 6), new Coord(0, 7), new Coord(0, 8)  // Row 0
            });

            List<Coord> Cols0to8 = new List<Coord>(new Coord[] {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(3, 0), new Coord(4, 0), new Coord(5, 0), new Coord(6, 0), new Coord(7, 0), new Coord(8, 0), // Col 0
                new Coord(0, 1), new Coord(1, 1), new Coord(2, 1), new Coord(3, 1), new Coord(4, 1), new Coord(5, 1), new Coord(6, 1), new Coord(7, 1), new Coord(8, 1), // Col 1
                new Coord(0, 2), new Coord(1, 2), new Coord(2, 2), new Coord(3, 2), new Coord(4, 2), new Coord(5, 2), new Coord(6, 2), new Coord(7, 2), new Coord(8, 2), // Col 2
                new Coord(0, 3), new Coord(1, 3), new Coord(2, 3), new Coord(3, 3), new Coord(4, 3), new Coord(5, 3), new Coord(6, 3), new Coord(7, 3), new Coord(8, 3), // Col 3
                new Coord(0, 4), new Coord(1, 4), new Coord(2, 4), new Coord(3, 4), new Coord(4, 4), new Coord(5, 4), new Coord(6, 4), new Coord(7, 4), new Coord(8, 4), // Col 4
                new Coord(0, 5), new Coord(1, 5), new Coord(2, 5), new Coord(3, 5), new Coord(4, 5), new Coord(5, 5), new Coord(6, 5), new Coord(7, 5), new Coord(8, 5), // Col 5
                new Coord(0, 6), new Coord(1, 6), new Coord(2, 6), new Coord(3, 6), new Coord(4, 6), new Coord(5, 6), new Coord(6, 6), new Coord(7, 6), new Coord(8, 6), // Col 6
                new Coord(0, 7), new Coord(1, 7), new Coord(2, 7), new Coord(3, 7), new Coord(4, 7), new Coord(5, 7), new Coord(6, 7), new Coord(7, 7), new Coord(8, 7), // Col 7
                new Coord(0, 8), new Coord(1, 8), new Coord(2, 8), new Coord(3, 8), new Coord(4, 8), new Coord(5, 8), new Coord(6, 8), new Coord(7, 8), new Coord(8, 8)  // Col 8
            });

            List<Coord> Cols8to0 = new List<Coord>(new Coord[]
            {
                new Coord(8, 8), new Coord(7, 8), new Coord(6, 8), new Coord(5, 8), new Coord(4, 8), new Coord(3, 8), new Coord(2, 8), new Coord(1, 8), new Coord(0, 8), // Col 8
                new Coord(8, 7), new Coord(7, 7), new Coord(6, 7), new Coord(5, 7), new Coord(4, 7), new Coord(3, 7), new Coord(2, 7), new Coord(1, 7), new Coord(0, 7), // Col 7
                new Coord(8, 6), new Coord(7, 6), new Coord(6, 6), new Coord(5, 6), new Coord(4, 6), new Coord(3, 6), new Coord(2, 6), new Coord(1, 6), new Coord(0, 6), // Col 6
                new Coord(8, 5), new Coord(7, 5), new Coord(6, 5), new Coord(5, 5), new Coord(4, 5), new Coord(3, 5), new Coord(2, 5), new Coord(1, 5), new Coord(0, 5), // Col 5
                new Coord(8, 4), new Coord(7, 4), new Coord(6, 4), new Coord(5, 4), new Coord(4, 4), new Coord(3, 4), new Coord(2, 4), new Coord(1, 4), new Coord(0, 4), // Col 4
                new Coord(8, 3), new Coord(7, 3), new Coord(6, 3), new Coord(5, 3), new Coord(4, 3), new Coord(3, 3), new Coord(2, 3), new Coord(1, 3), new Coord(0, 3), // Col 3
                new Coord(8, 2), new Coord(7, 2), new Coord(6, 2), new Coord(5, 2), new Coord(4, 2), new Coord(3, 2), new Coord(2, 2), new Coord(1, 2), new Coord(0, 2), // Col 2
                new Coord(8, 1), new Coord(7, 1), new Coord(6, 1), new Coord(5, 1), new Coord(4, 1), new Coord(3, 1), new Coord(2, 1), new Coord(1, 1), new Coord(0, 1), // Col 1
                new Coord(8, 0), new Coord(7, 0), new Coord(6, 0), new Coord(5, 0), new Coord(4, 0), new Coord(3, 0), new Coord(2, 0), new Coord(1, 0), new Coord(0, 0)  // Col 0
            });

            List<Coord> Boxes_012345678 = new List<Coord>(new Coord[]{
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 0), new Coord(1, 1), new Coord(1, 2), new Coord(2, 0), new Coord(2, 1), new Coord(2, 2), // BoxID 0
                new Coord(0, 3), new Coord(0, 4), new Coord(0, 5), new Coord(1, 3), new Coord(1, 4), new Coord(1, 5), new Coord(2, 3), new Coord(2, 4), new Coord(2, 5), // BoxID 1
                new Coord(0, 6), new Coord(0, 7), new Coord(0, 8), new Coord(1, 6), new Coord(1, 7), new Coord(1, 8), new Coord(2, 6), new Coord(2, 7), new Coord(2, 8), // BoxID 2
                new Coord(3, 0), new Coord(3, 1), new Coord(3, 2), new Coord(4, 0), new Coord(4, 1), new Coord(4, 2), new Coord(5, 0), new Coord(5, 1), new Coord(5, 2), // BoxID 3
                new Coord(3, 3), new Coord(3, 4), new Coord(3, 5), new Coord(4, 3), new Coord(4, 4), new Coord(4, 5), new Coord(5, 3), new Coord(5, 4), new Coord(5, 5), // BoxID 4
                new Coord(3, 6), new Coord(3, 7), new Coord(3, 8), new Coord(4, 6), new Coord(4, 7), new Coord(4, 8), new Coord(5, 6), new Coord(5, 7), new Coord(5, 8), // BoxID 5
                new Coord(6, 0), new Coord(6, 1), new Coord(6, 2), new Coord(7, 0), new Coord(7, 1), new Coord(7, 2), new Coord(8, 0), new Coord(8, 1), new Coord(8, 2), // BoxID 6
                new Coord(6, 3), new Coord(6, 4), new Coord(6, 5), new Coord(7, 3), new Coord(7, 4), new Coord(7, 5), new Coord(8, 3), new Coord(8, 4), new Coord(8, 5), // BoxID 7
                new Coord(6, 6), new Coord(6, 7), new Coord(6, 8), new Coord(7, 6), new Coord(7, 7), new Coord(7, 8), new Coord(8, 6), new Coord(8, 7), new Coord(8, 8)  // BodID 8
            });

            List<Coord> Boxes_876543210  = new List<Coord>(new Coord[]{
                new Coord(6, 6), new Coord(6, 7), new Coord(6, 8), new Coord(7, 6), new Coord(7, 7), new Coord(7, 8), new Coord(8, 6), new Coord(8, 7), new Coord(8, 8), // BodID 8
                new Coord(6, 3), new Coord(6, 4), new Coord(6, 5), new Coord(7, 3), new Coord(7, 4), new Coord(7, 5), new Coord(8, 3), new Coord(8, 4), new Coord(8, 5), // BoxID 7
                new Coord(6, 0), new Coord(6, 1), new Coord(6, 2), new Coord(7, 0), new Coord(7, 1), new Coord(7, 2), new Coord(8, 0), new Coord(8, 1), new Coord(8, 2), // BoxID 6
                new Coord(3, 6), new Coord(3, 7), new Coord(3, 8), new Coord(4, 6), new Coord(4, 7), new Coord(4, 8), new Coord(5, 6), new Coord(5, 7), new Coord(5, 8), // BoxID 5
                new Coord(3, 3), new Coord(3, 4), new Coord(3, 5), new Coord(4, 3), new Coord(4, 4), new Coord(4, 5), new Coord(5, 3), new Coord(5, 4), new Coord(5, 5), // BoxID 4
                new Coord(3, 0), new Coord(3, 1), new Coord(3, 2), new Coord(4, 0), new Coord(4, 1), new Coord(4, 2), new Coord(5, 0), new Coord(5, 1), new Coord(5, 2), // BoxID 3
                new Coord(0, 3), new Coord(0, 4), new Coord(0, 5), new Coord(1, 3), new Coord(1, 4), new Coord(1, 5), new Coord(2, 3), new Coord(2, 4), new Coord(2, 5), // BoxID 1
                new Coord(0, 6), new Coord(0, 7), new Coord(0, 8), new Coord(1, 6), new Coord(1, 7), new Coord(1, 8), new Coord(2, 6), new Coord(2, 7), new Coord(2, 8), // BoxID 2
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 0), new Coord(1, 1), new Coord(1, 2), new Coord(2, 0), new Coord(2, 1), new Coord(2, 2)  // BoxID 0
            });

            List<Coord> Boxes_036147258 = new List<Coord>(new Coord[]
            {
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 0), new Coord(1, 1), new Coord(1, 2), new Coord(2, 0), new Coord(2, 1), new Coord(2, 2), // BoxID 0
                new Coord(3, 0), new Coord(3, 1), new Coord(3, 2), new Coord(4, 0), new Coord(4, 1), new Coord(4, 2), new Coord(5, 0), new Coord(5, 1), new Coord(5, 2), // BoxID 3
                new Coord(6, 0), new Coord(6, 1), new Coord(6, 2), new Coord(7, 0), new Coord(7, 1), new Coord(7, 2), new Coord(8, 0), new Coord(8, 1), new Coord(8, 2), // BoxID 6
                new Coord(0, 3), new Coord(0, 4), new Coord(0, 5), new Coord(1, 3), new Coord(1, 4), new Coord(1, 5), new Coord(2, 3), new Coord(2, 4), new Coord(2, 5), // BoxID 1
                new Coord(3, 3), new Coord(3, 4), new Coord(3, 5), new Coord(4, 3), new Coord(4, 4), new Coord(4, 5), new Coord(5, 3), new Coord(5, 4), new Coord(5, 5), // BoxID 4
                new Coord(6, 3), new Coord(6, 4), new Coord(6, 5), new Coord(7, 3), new Coord(7, 4), new Coord(7, 5), new Coord(8, 3), new Coord(8, 4), new Coord(8, 5), // BoxID 7
                new Coord(0, 6), new Coord(0, 7), new Coord(0, 8), new Coord(1, 6), new Coord(1, 7), new Coord(1, 8), new Coord(2, 6), new Coord(2, 7), new Coord(2, 8), // BoxID 2
                new Coord(3, 6), new Coord(3, 7), new Coord(3, 8), new Coord(4, 6), new Coord(4, 7), new Coord(4, 8), new Coord(5, 6), new Coord(5, 7), new Coord(5, 8), // BoxID 5
                new Coord(6, 6), new Coord(6, 7), new Coord(6, 8), new Coord(7, 6), new Coord(7, 7), new Coord(7, 8), new Coord(8, 6), new Coord(8, 7), new Coord(8, 8)  // BodID 8
            });

            List<Coord> Boxes_852741630 = new List<Coord>(new Coord[] {
                new Coord(6, 6), new Coord(6, 7), new Coord(6, 8), new Coord(7, 6), new Coord(7, 7), new Coord(7, 8), new Coord(8, 6), new Coord(8, 7), new Coord(8, 8), // BodID 8
                new Coord(3, 6), new Coord(3, 7), new Coord(3, 8), new Coord(4, 6), new Coord(4, 7), new Coord(4, 8), new Coord(5, 6), new Coord(5, 7), new Coord(5, 8), // BoxID 5
                new Coord(0, 6), new Coord(0, 7), new Coord(0, 8), new Coord(1, 6), new Coord(1, 7), new Coord(1, 8), new Coord(2, 6), new Coord(2, 7), new Coord(2, 8), // BoxID 2
                new Coord(6, 3), new Coord(6, 4), new Coord(6, 5), new Coord(7, 3), new Coord(7, 4), new Coord(7, 5), new Coord(8, 3), new Coord(8, 4), new Coord(8, 5), // BoxID 7
                new Coord(3, 3), new Coord(3, 4), new Coord(3, 5), new Coord(4, 3), new Coord(4, 4), new Coord(4, 5), new Coord(5, 3), new Coord(5, 4), new Coord(5, 5), // BoxID 4
                new Coord(0, 3), new Coord(0, 4), new Coord(0, 5), new Coord(1, 3), new Coord(1, 4), new Coord(1, 5), new Coord(2, 3), new Coord(2, 4), new Coord(2, 5), // BoxID 1
                new Coord(6, 0), new Coord(6, 1), new Coord(6, 2), new Coord(7, 0), new Coord(7, 1), new Coord(7, 2), new Coord(8, 0), new Coord(8, 1), new Coord(8, 2), // BoxID 6
                new Coord(3, 0), new Coord(3, 1), new Coord(3, 2), new Coord(4, 0), new Coord(4, 1), new Coord(4, 2), new Coord(5, 0), new Coord(5, 1), new Coord(5, 2), // BoxID 3
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 0), new Coord(1, 1), new Coord(1, 2), new Coord(2, 0), new Coord(2, 1), new Coord(2, 2)  // BoxID 0
            });

            List_Coords = new List<List<Coord>>(); // add the new lists
            List_Coords.Add(Rows0to8); // 0
            List_Coords.Add(Rows8to0); // 1
            List_Coords.Add(Cols0to8); // 2
            List_Coords.Add(Cols8to0); // 3
            List_Coords.Add(Boxes_012345678); // 4
            List_Coords.Add(Boxes_876543210); // 5 
            List_Coords.Add(Boxes_036147258); // 6 
            List_Coords.Add(Boxes_852741630); // 7
            MoveCounter = 0;
            //addPrevious();
            PotentialMoves = GetNumPossibleMoves();
            Console.WriteLine();
        }

        // Package up and clean up comment and commit to the gits of hubs
        // Come up with a timing funcitonality and create a row based solver
        // column based
        // box based
        // row+column based
        // row+column+box based
        // attempt to fiddle with multi level depth algorithms

        /// <summary>
        /// Function that reads in a file starting with START and ending with END
        /// Turns a preformatted file into a sudoku board
        /// Inside a try block when called thus exceptions handled elsewhere
        /// </summary>
        /// <param name="filename"></param>
        public void ReadInSudokuBoard(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            int row = 0;
            foreach (string line in lines)
            {
                if (!line.Equals("START") && !line.Equals("END"))
                {
                    string[] chars = line.Split(',');
                    int column = 0;
                    foreach (string item in chars)
                    {
                        _board[row, column] = new Square(item[0]);
                        column++;
                    }
                    row++;
                }
            }
            AssignBoxIds();
        }

        // Assigns the BoxIDs used for evaluating the potential moves
        public void AssignBoxIds()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i < 3)
                    {
                        if (j < 3)
                            _board[i, j].BoxID = 0;
                        else if (j < 6)
                            _board[i, j].BoxID = 1;
                        else
                            _board[i, j].BoxID = 2;
                    }
                    else if (i < 6)
                    {
                        if (j < 3)
                            _board[i, j].BoxID = 3;
                        else if (j < 6)
                            _board[i, j].BoxID = 4;
                        else
                            _board[i, j].BoxID = 5;
                    }
                    else
                    {
                        if (j < 3)
                            _board[i, j].BoxID = 6;
                        else if (j < 6)
                            _board[i, j].BoxID = 7;
                        else
                            _board[i, j].BoxID = 8;
                    }
                }
            }
        }

        // self explanatory
        public void PrintBoard()
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                if (i % 3 == 0)
                {
                    PrintLineOfHyphens();
                }
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    Console.Write('|');

                    if (_board[i, j].Val == 'k')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(_board[i, j].Val);
                    }
                    if (j % 3 == 2 && j > 0)
                    {
                        Console.Write('|');
                    }
                }
                Console.WriteLine();
            }
            PrintLineOfHyphens();
        }

        // used for beautifying command line
        public void PrintLineOfHyphens()
        {
            Console.WriteLine("---------------------");
        }

        // iterates through the board to see if there are any unfilled spaces
        public Boolean Done()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j].Val == 'k')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Takes the current board and from the top left to right, top to bottom iterates through and sees if there 
        /// is a square with only one possible way to be valued. If that is the case it is changed to that value and
        /// given a confidence metric of 2, this confidence metric is used to show that it is not the original immutable
        /// kind of value but we are very certain it is correct.
        /// </summary>
        /// <param name="changed"></param>
        /// <returns> Changed is used to show that we actually modifed the board with this pass through. </returns>
        public Boolean PassThroughAndFillOutTheCertainSquares(Boolean changed)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j].Val == 'k')
                    {
                        List<char> options = AvailableOptions(i, j);
                        if (options.Count() == 1)
                        {
                            _board[i, j].Val = options[0];
                            _board[i, j].Conf = 2;
                            changed = true;
                        }
                    }
                }
            }
            if(changed)
            {
                addPrevious();
            }
            return changed;
        }

        public List<char> AvailableOptions(int i, int j)
        {
            List<char> options = new List<char>();
            if(isPossible(i,j,'1'))
            {
                options.Add('1');
            }
            if (isPossible(i, j, '2'))
            {
                options.Add('2');
            }
            if (isPossible(i, j, '3'))
            {
                options.Add('3');
            }
            if (isPossible(i, j, '4'))
            {
                options.Add('4');
            }
            if (isPossible(i, j, '5'))
            {
                options.Add('5');
            }
            if (isPossible(i, j, '6'))
            {
                options.Add('6');
            }
            if (isPossible(i, j, '7'))
            {
                options.Add('7');
            }
            if (isPossible(i, j, '8'))
            {
                options.Add('8');
            }
            if(isPossible(i,j, '9'))
            {
                options.Add('9');
            }

            return options;
        }

        public Boolean isPossible(int x, int y, char a)
        {
            if (_board[x, y].Val != 'k')
            {
                return false;
            }
            // loading box content
            _boxes = new Box[] { new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box() };

            for (int i = 0; i < 9; i++) //potentially could be optimized
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j].Val != 'k')
                    {
                        int currBoxID = _board[i, j].BoxID;
                        _boxes[currBoxID].Contents.Add(_board[i, j].Val);
                    }
                }
            }
            // box checking
            if (!InBox(a, BoxIdLookUp(x, y)))
            {
                return false;
            }
            // row checking
            for (int i = 0; i < 9; i++)
            {
                if (i != y)
                {
                    if (_board[x, i].Val == a)
                    {
                        return false;
                    }
                }
                if (i != x)
                {
                    if (_board[i, y].Val == a)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // used to improve code readability
        public int BoxIdLookUp(int x, int y)
        {
            return _board[x, y].BoxID;
        }

        // Verifies if a that char is already in the box
        public Boolean InBox(char a, int bId)
        {
            if (_boxes[bId].Contents.Contains(a))
                return false;
            return true;
        }

        public void AlterCell(int x, int y, char a, int c)
        {
            _board[x, y].Val = a;
            _board[x, y].Conf = c;
            if (true)
            {
                MoveCounter = MoveCounter + 1;
            }

            if (a == 'k') { BacktrackCounter = BacktrackCounter + 1; }
            //addPrevious();
        }

        public Boolean UserInputSquare(int x, int y)
        {
            if (_board[x, y].Conf != 1)
                return true;
            return false;
        }



        public Boolean Undo()
        {
            if(_previousBoards.Count>1)
            {
                _previousBoards.Pop();
                Square[,] temp = _previousBoards.Pop();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if(temp[i,j].Val == 'k')
                        {
                            _board[i, j] = new Square(temp[i, j].Val, 10, temp[i, j].BoxID);
                        }
                        else
                            _board[i, j] = new Square(temp[i, j].Val, temp[i, j].Conf, temp[i, j].BoxID);
                    }
                }
                addPrevious();
                return true;
            }
            else
            {
                return false;
            }
        }

        // has to use deep copying cus C# is a snowflake
        public void addPrevious()
        {
            //Console.WriteLine("Add previous was called!");
            Square[,] temp = new Square[9, 9];
            for(int i =0; i < 9; i++)
            {
                for(int j=0; j<9; j++)
                {
                    temp[i, j] = new Square(_board[i, j].Val, _board[i,j].Conf, _board[i,j].BoxID, _board[i,j].possibleMoves);
                }
            }
            _previousBoards.Push(temp);
        }

        public void GetHint(int row, int col)
        {
            Console.WriteLine("Inside the get Hint functionality");
            List<char> availableOptions = AvailableOptions(row,col);

            foreach(char op in availableOptions)
            {
                Console.WriteLine("At ["+row+", "+col+"] there is a probability of "+ ((double)1.0/availableOptions.Count)+" the value is "+op);
            }

            // build out a hint system that stores this information into a square potentially
        }

        public int GetNumPossibleMoves()
        {
            int totalMoves = 0;
            _boxes = new Box[] { new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box() };

            for (int i = 0; i < 9; i++) //potentially could be optimized
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j].Val != 'k')
                    {
                        int currBoxID = _board[i, j].BoxID;
                        _boxes[currBoxID].Contents.Add(_board[i, j].Val);
                    }
                }
            }
            foreach(Box b in _boxes)
            {
                int emptySquares = 9-b.Contents.Count;
                totalMoves += emptySquares * emptySquares;
            }

            return totalMoves;
        }
        
        public Boolean BoxBruteForceSolver(int index, int whichList, Boolean needToBackTrack)
        {
            if (index == List_Coords[whichList].Count)
            {
                DisplayStatistics(whichList);
                return true;
            }
            else
            {
                if (!needToBackTrack)
                {
                    Coord current = List_Coords[whichList][index];
                    int row = current.row;
                    int col = current.col;
                    if (_board[row, col].Conf == 1 || _board[row, col].Conf == 2)
                    {
                        index = index + 1;
                        return BoxBruteForceSolver(index, whichList, false);
                    }
                    if (_board[row, col].possibleMoves.Count > 0)
                    {
                        char option = _board[row, col].possibleMoves.Dequeue();
                        if (isPossible(row, col, option))
                        {
                            AlterCell(row, col, option, 5); // 5 is a confidence metric used to show that the cell has been messed with
                            index = index + 1;
                            return BoxBruteForceSolver(index, whichList, false);
                        }
                        else
                        {
                            return BoxBruteForceSolver(index, whichList, false);
                        }
                    }
                    else
                    {
                        Queue<char> refreshOptions = new Queue<char>();// = new Queue<char>(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
                        refreshOptions.Enqueue('1');
                        refreshOptions.Enqueue('2');
                        refreshOptions.Enqueue('3');
                        refreshOptions.Enqueue('4');
                        refreshOptions.Enqueue('5');
                        refreshOptions.Enqueue('6');
                        refreshOptions.Enqueue('7');
                        refreshOptions.Enqueue('8');
                        refreshOptions.Enqueue('9');

                        _board[row, col].possibleMoves = refreshOptions;
                        return BoxBruteForceSolver(index, whichList, true);
                    }
                }
                else
                {
                    Coord current = List_Coords[whichList][index];
                    int row = current.row;
                    int col = current.col;
                    // flag triggered back tracking
                    if (_board[row, col].Conf == 5)
                    {
                        AlterCell(row, col, 'k', 10);
                        return BoxBruteForceSolver(index, whichList, false);
                    }
                    else
                    {
                        if (index > 0)
                        {
                            index = index - 1;
                            return BoxBruteForceSolver(index, whichList, true);
                        }
                        else
                            return false; // if here that means this problem is NOT solveable somehow come up with a flag for this
                    }
                }
            }
            
        }
        List<Statistic> statistics = new List<Statistic>();
        public void DisplayStatistics(int whichList)
        {
            Statistic stat = new Statistic(whichList, PotentialMoves, MoveCounter, BacktrackCounter);
            statistics.Add(stat);
            Console.Write(stat);
        }

        /***Testing funciton section***/
        /// <summary>
        /// Functions used for Testing/ printing in development
        /// </summary>
        // print method used for testing purposes
        public void PrintBoxContents()
        {
            Console.WriteLine("Box contents below");
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("BoxID: " + i + "= ");
                for (int j = 0; j < _boxes[i].Contents.Count(); j++)
                {
                    Console.Write(_boxes[i].Contents[j] + "  ");
                }
                Console.WriteLine();
            }
        }
        public void TestingIsPossibleDoubleZero()
        {
            Console.WriteLine("[0,0] 1: " + isPossible(0, 0, '1'));
            Console.WriteLine("[0,0] 2: " + isPossible(0, 0, '2'));
            Console.WriteLine("[0,0] 3: " + isPossible(0, 0, '3'));
            Console.WriteLine("[0,0] 4: " + isPossible(0, 0, '4'));
            Console.WriteLine("[0,0] 5: " + isPossible(0, 0, '5'));
            Console.WriteLine("[0,0] 6: " + isPossible(0, 0, '6'));
            Console.WriteLine("[0,0] 7: " + isPossible(0, 0, '7'));
            Console.WriteLine("[0,0] 8: " + isPossible(0, 0, '8'));
            Console.WriteLine("[0,0] 9: " + isPossible(0, 0, '9'));

        }
        public void TestingIsPossible()
        {
            Console.WriteLine("Testing everything");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j].Val == 'k')
                    {
                        Console.WriteLine("[" + i + "," + j + "]: 1 " + isPossible(i, j, '1'));
                        Console.WriteLine("[" + i + "," + j + "]: 2 " + isPossible(i, j, '2'));
                        Console.WriteLine("[" + i + "," + j + "]: 3 " + isPossible(i, j, '3'));
                        Console.WriteLine("[" + i + "," + j + "]: 4 " + isPossible(i, j, '4'));
                        Console.WriteLine("[" + i + "," + j + "]: 5 " + isPossible(i, j, '5'));
                        Console.WriteLine("[" + i + "," + j + "]: 6 " + isPossible(i, j, '6'));
                        Console.WriteLine("[" + i + "," + j + "]: 7 " + isPossible(i, j, '7'));
                        Console.WriteLine("[" + i + "," + j + "]: 8 " + isPossible(i, j, '8'));
                        Console.WriteLine("[" + i + "," + j + "]: 9 " + isPossible(i, j, '9'));
                        Console.WriteLine();
                    }
                }
            }
        }
        public void PrintStack()
        {
            while(_previousBoards.Count>0)
            {
                Square[,] b = _previousBoards.Pop();
                Console.WriteLine("Printing a stack");
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Console.Write(b[i,j]+" ");
                    }
                    Console.WriteLine();
                }
            }
        }

        // Global Variables
        //public char[,] _board;
        public Square[,] _board;
        public Box[] _boxes;
        public Stack<Square[,]> _previousBoards;
        public int MoveCounter;
        public int PotentialMoves;
        public int BacktrackCounter;
        List<List<Coord>> List_Coords;
    }
}
