using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Box
    {
        public List<char> Contents { get; set; }
        public Box()
        {
            Contents = new List<char>();
        }
    }
}
