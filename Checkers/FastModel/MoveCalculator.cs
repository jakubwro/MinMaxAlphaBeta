using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    public class MoveCalculator
    {
        public void CalculateMoves(FastState state, UInt32 row, UInt32 position)
        {
            UInt32 white = 0u, black = 0u;
            UInt32 all = white | black;

            var lr = BinaryMasks.LRDiagonals.Single(d => (d & position) > 0);
            var rl = BinaryMasks.RLDiagonals.Single(d => (d & position) > 0);

            UInt32 nextRow = row << 4;

            Debug.Assert(nextRow != 0);

            if ((lr & nextRow & all) == 0)
            {
                if (nextRow == 0xf0000000u)
                {

                }
            }

            if ((rl & nextRow & all) == 0)
            {
                //yield state
            }


        }
    }
}
