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
            }
            else
            {
                Conf = 10;
            }
        }
        public override string ToString()
        {
            return ""+Val ;
        }
    }
}
