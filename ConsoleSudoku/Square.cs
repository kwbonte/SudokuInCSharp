using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Square
    {
        // val is either a char of 1 to 9 or k as a special character to denote a break 
        public char Val { get; set; }
        // This is a confidence meter 1 means user input from the readfile thus immutable, 2 means very confident, 3 means questioning
        // 10 means empty
        public int Conf { get; set; }
        // used to show group identity with the corresponding 3x3 grid the input belongs to
        public int BoxID { get; set; }

        public Queue<char> possibleMoves { get; set; }

        // testing constructor
        public Square(char v, int c, int bID)
        {
            Val = v;
            Conf = c;
            BoxID = bID;
        }

        // general purpose constructor
        public Square(char v, int c)
        {
            Val = v;
            Conf = c;
        }

        // Initializing from file constructor
        public Square(char v)
        {
            Val = v;
            if (Val != 'k')
            {
                Conf = 1;
                possibleMoves = new Queue<char>(); //makes a new empty queue attempt to overcome the StackO error
            }
            else
            {
                Conf = 10;
                possibleMoves = new Queue<char>();
                possibleMoves.Enqueue('1');
                possibleMoves.Enqueue('2');
                possibleMoves.Enqueue('3');
                possibleMoves.Enqueue('4');
                possibleMoves.Enqueue('5');
                possibleMoves.Enqueue('6');
                possibleMoves.Enqueue('7');
                possibleMoves.Enqueue('8');
                possibleMoves.Enqueue('9');
            }
        }
        public Square(char v, int c, int bID, Queue<char> pm)
        {
            Val = v;
            Conf = c;
            BoxID = bID;
            possibleMoves = new Queue<char>();
            //while(pm.Count > 0)
            //{
             //   possibleMoves.Enqueue(pm.Dequeue());
            //}
            possibleMoves = pm;
        }

        public override string ToString()
        {
            
            return ""+Val ;
        }
        

        public void RefreshPossibleOptions()
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
            this.possibleMoves = refreshOptions;
        }

    }
}
