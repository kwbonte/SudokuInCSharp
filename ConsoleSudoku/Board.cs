﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            addPrevious();
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
        public Boolean Done()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_board[i, j].Val == 'k')
                        return false;
                }
            }
            return true;
        }


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

                    if (_board[i,j].Val == 'k')
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(_board[i, j].Val);
                    }
                    if(j%3==2 && j>0)
                    {
                        Console.Write('|');
                    }
                        
                }
                Console.WriteLine();
            }
            PrintLineOfHyphens();
        }

        public void Undo()
        {
            _previousBoards.Pop();
            Square[,] temp = _previousBoards.Pop();
            _board = temp;
        }

        public void PrintLineOfHyphens()
        {
            Console.WriteLine("---------------------");
        }

        public void addPrevious()
        {
            Square[,] temp = new Square[9, 9];
            for(int i =0; i < 9; i++)
            {
                for(int j=0; j<9; j++)
                {
                    temp[i, j] = _board[i, j];
                }
            }
            _previousBoards.Push(temp);
        }

        public void AlterCell(int x, int y, char a, int c)
        {
            _board[x, y].Val = a;
            _board[x, y].Conf = c;
addPrevious();
        }

        public Boolean UserInputSquare(int x, int y)
        {
            if (_board[x, y].Conf != 1)
                return true;
            return false;
        }

        public Boolean isPossible(int x, int y, char a)
        {
            if(_board[x,y].Val !='k')
            {
                return false;
            }
            // loading box content
            for (int i = 0; i < 9; i++)
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
            int box = BoxIdLookUp(x, y);
            if (!InBox(a, box))
            {
                return false;
            }
            // row checking
            for (int i = 0; i < 9; i++)
            {
                if(i != y)
                {
                    if(_board[x,i].Val == a)
                    {
                        return false;
                    }
                }
                if( i != x)
                {
                    if(_board[i,y].Val ==a)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

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

        // Assigns the BoxIDs used for evaluating the potential moves
        public void AssignBoxIds()
        {
            for(int i =0; i<9; i++)
            {
                for(int j=0; j<9;j++)
                {
                    if (i < 3)
                    {
                        if(j<3)
                            _board[i, j].BoxID = 0;
                        else if(j<6)
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


        //public char[,] _board;
        public Square[,] _board;
        public Box[] _boxes;
        public Stack<Square[,]> _previousBoards;
    }
}
