using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Board
    {
        public Board(string filename)
        {
            /*_board = new char[,]
                {
                    { '1', '2', '3', '4', '5', '6', '7', '8', '9'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                    { 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k', 'k'},
                };
            _board = new Square[,]
            {
                { new Square('k',10), new Square('k',10), new Square('k',10), new Square('2',1), new Square('6',1), new Square('k',1),new Square('7',1), new Square('k',10), new Square('1',1)},
                { new Square('6',1), new Square('8',1), new Square('k',10), new Square('k',10), new Square('7',1), new Square('k',10),new Square('k',10), new Square('9',1), new Square('k',10)},
                { new Square('1',1), new Square('9',1), new Square('k',10), new Square('k',10), new Square('k',10), new Square('4',1),new Square('5',1), new Square('k',10), new Square('k',10)},
                { new Square('8',1), new Square('2',1), new Square('k',10), new Square('1',1), new Square('k',10), new Square('k',10),new Square('k',10), new Square('4',1), new Square('k',10)},
                { new Square('k',10), new Square('k',10), new Square('4',1), new Square('6',1), new Square('k',10), new Square('2',1),new Square('9',10), new Square('k',10), new Square('k',10)},
                { new Square('k',10), new Square('5',1), new Square('k',10), new Square('k',10), new Square('k',10), new Square('3',1),new Square('k',1), new Square('2',1), new Square('8',1)},
                { new Square('k',10), new Square('k',10), new Square('9',1), new Square('3',1), new Square('k',10), new Square('k',10),new Square('k',10), new Square('7',1), new Square('4',1)},
                { new Square('k',10), new Square('4',1), new Square('k',10), new Square('k',10), new Square('5',1), new Square('k',10),new Square('k',10), new Square('3',1), new Square('6',1)},
                { new Square('7',1), new Square('k',10), new Square('3',1), new Square('k',10), new Square('1',1), new Square('8',1),new Square('k',10), new Square('k',10), new Square('k',10)},
            };//*/
            _board = new Square[9, 9];
            ReadInSudokuBoard(filename);

            _boxes = new Box[] { new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box(), new Box()};
            AssignBoxIds();

            //PrintBoxContents();
            //BoxContentLoading();
            //PrintBoxContents();
            //TestingIsPossibleDoubleZero();
            //TestingIsPossible();

            //Console.WriteLine("Attempting to fill the board");
            //fillInBoad();
        }

        // TODO: come up with a way to create input for some beginner, medium and expert puzzles,
            // I am thinking a comma seperated list with semicolons at the end with text files
            // Get a stream reader together that handles those files
            // Package up and clean up comment and commit to the gits of hubs
            // come up with a box id assigning tool that takes the need hardcoding away
            // Come up with a timing funcitonality and create a row based solver
                // column based
                // box based
                // row+column based
                // row+column+box based
                // attempt to fiddle with multi level depth algorithms
        public void ReadInSudokuBoard(string filename)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(filename);
                int row = 0;
                foreach (var line in lines)
                {
                    if (!line.Equals("START") && !line.Equals("END"))
                    {
                        //Console.WriteLine(line);
                        string[] chars = line.Split(',');
                        int column = 0;
                        foreach (string item in chars)
                        {
                            Console.Write(item[0] + " ");
                            _board[row, column] = new Square(item[0]);
                            column++;

                        }
                        row++;
                        Console.WriteLine();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public void fillInBoard()
        {
            int moveCounter = 0;
            Boolean changed = true;
            while (!Done() && changed)
            {
                changed = false;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if(_board[i,j].Val == 'k')
                        {
                            List<char> options = AvailableOptions(i, j);
                            if(options.Count()==1)
                            {
                                Console.WriteLine("Making changes to "+i+", "+j);
                                _board[i, j].Val = options[0];
                                _board[i, j].Conf = 2;
                                changed = true;
                                moveCounter++;
                            }
                        }
                    }
                }
                PrintBoard();

            }
            if (Done())
                Console.WriteLine("Board completed in " + moveCounter + " moves.");
            else
                Console.WriteLine("This failed to be completed after " + moveCounter + " moves.");
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

        public void PrintLineOfHyphens()
        {
            Console.WriteLine("---------------------");
        }

        public Boolean isPossible(int x, int y, char a)
        {
            if(_board[x,y].Val !='k')
            {
                return false;
            }
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
    }
}
