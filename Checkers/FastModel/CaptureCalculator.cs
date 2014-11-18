using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    public class CaptureCalculator
    {
        public void CalculateCaptures(FastState state, UInt32 row, UInt32 position)
        {
            UInt32 white = 0u, black = 0u;
            UInt32 all = white | black;

            var lr = BinaryMasks.LRDiagonals.Single(d => (d & position) > 0);
            var rl = BinaryMasks.RLDiagonals.Single(d => (d & position) > 0);

            UInt32 nextRow = row << 4;
            UInt32 nextNextRow = row << 8;
            UInt32 prevRow = row << 4;
            UInt32 prevprevRow = row << 8;

            if (nextNextRow != 0)
            {
                if ((lr & nextRow & ~black) == 0 && (lr & nextNextRow & all) == 0)
                {
                    //yield new state
                    //find rec
                    //CalculateCaptures(newState, nextNextRow, nextNextRow & lr);
                }

                if ((lr & prevRow & ~black) == 0 && (lr & prevprevRow & all) == 0)
                {
                    //yield
                    //find rec
                    //CalculateCaptures(newState, prevprevRow, prevprevRow & lr);
                }
            }

            if (prevprevRow != 0)
            {
                if ((rl & nextRow & ~black) == 0 && (rl & nextNextRow & all) == 0)
                {
                    //yield
                    //find rec
                    //CalculateCaptures(newState, nextNextRow, nextNextRow & rl);
                }

                if ((rl & prevRow & ~black) == 0 && (rl & prevprevRow & all) == 0)
                {
                    //yield
                    //find rec
                    //CalculateCaptures(newState, prevprevRow, prevprevRow & r;);
                }
            }
        }
    }
}
