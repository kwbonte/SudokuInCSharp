using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Statistic
    {
        public int RouteNumber { get; set; }
        public int NumPossibleMoves { get; set; }
        public int NumMovesMade { get; set; }
        public int NumBackTracks { get; set; }

        public Statistic(int r, int np, int nm, int nb)
        {
            RouteNumber = r;
            NumPossibleMoves = np;
            NumMovesMade = nm;
            NumBackTracks = nb;
        }
        public override string ToString()
        {
            return "Route #" + RouteNumber + " has " + NumPossibleMoves + " possible moves and the algorithm used " + NumMovesMade + " moves with " + NumBackTracks + " backtracks";
        }
    }
}
