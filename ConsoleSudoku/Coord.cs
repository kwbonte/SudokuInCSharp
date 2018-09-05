using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Coord
    {
        public int row { get; set; }
        public int col { get; set; }
        public Coord(int x, int y)
        {
            row = x;
            col = y;
        }
    }
}
