using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class threadObject
    {
        public string FileName { get; set; }
        public int threadID { get; set; }

        public threadObject(string f, int t)
        {
            FileName = f;
            threadID = t;
        }
        public override string ToString()
        {
            return FileName + ": " + threadID;
        }
    }
}
