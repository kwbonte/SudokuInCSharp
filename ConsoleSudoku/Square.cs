using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Square
    {
        public char Val { get; set; }
        // val is either a char of 1 to 9 or k as a special character to denot a break 
        public int Conf { get; set; }
        // This is a confidence meter 1 means user input from the readfile thus immutable, 2 means very confident, 3 means questioning
        // 10 means empty
        public int BoxID { get; set; }
        public Square(char v, int c, int bID)
        {
            Val = v;
            Conf = c;
            BoxID = bID;
        }
        public Square(char v, int c)
        {
            Val = v;
            Conf = c;
        }
        public Square(char v)
        {
            Val = v;
            if (Val != 'k')
                Conf = 1;
            else
                Conf = 10;
        }
    }
}
