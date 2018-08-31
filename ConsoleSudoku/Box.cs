using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Box
    {
        // Used to develop heuristics and accelerate the check of the box the input is in
        public List<char> Contents { get; set; }
        public Box()
        {
            Contents = new List<char>();
        }
    }
}
