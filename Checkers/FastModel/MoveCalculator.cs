using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    public static class MoveCalculator
    {
        public static List<FastState> result = new List<FastState>(10);

        public static IEnumerable<FastState> CalculateMoves(FastState state, UInt32 row, UInt32 position)
        {
            UInt32 white = state.WhiteFolks;
            UInt32 black = state.BlackFolks;
            int whiteKings = state.WhiteKings;
            int blackKings = state.BlackKings;
            UInt32 all = white | black;

            uint lr = 0; //BinaryMasks.LeftToRightDiagonals.Single(d => (d & position) > 0);
            uint rl = 0; //BinaryMasks.RightToLeftDiagonals.Single(d => (d & position) > 0);

            for (int i = 0; i < BinaryMasks.LeftToRightDiagonals.Length; ++i)
            {
                if ((BinaryMasks.LeftToRightDiagonals[i] & position) > 0)
                {
                    lr = BinaryMasks.LeftToRightDiagonals[i];
                    break;
                }
            }
            for (int i = 0; i < BinaryMasks.RightToLeftDiagonals.Length; ++i)
            {
                if ((BinaryMasks.RightToLeftDiagonals[i] & position) > 0)
                {
                    rl = BinaryMasks.RightToLeftDiagonals[i];
                    break;
                }
            }


            if (state.IsWhiteActive)
            {
                UInt32 nextRow = row << 4;

                Debug.Assert(nextRow != 0);

                if ((lr & nextRow & ~all) > 0)
                {
                    white = white ^ position;
                    if (nextRow == 0xf0000000)
                        whiteKings += 1;
                    else
                        white = white ^ (lr & nextRow);

                    result.Add(new FastState(white, black, whiteKings, blackKings, !state.IsWhiteActive));
                }

                white = state.WhiteFolks;
                whiteKings = state.WhiteKings;

                if ((rl & nextRow & ~all) > 0)
                {
                    white = white ^ position;

                    if (nextRow == 0xf0000000)
                        whiteKings += 1;
                    else
                        white = white ^ (rl & nextRow);

                    result.Add(new FastState(white, black, whiteKings, blackKings, !state.IsWhiteActive));
                }
            }
            else
            {
                UInt32 prevRow = row >> 4;

                Debug.Assert(prevRow != 0);

                if ((lr & prevRow & ~all) > 0)
                {
                    black = black ^ position;
                    if (prevRow == 0x0000000f)
                        blackKings += 1;
                    else
                        black = black ^ (lr & prevRow);

                    result.Add(new FastState(white, black, whiteKings, blackKings, !state.IsWhiteActive));
                }

                black = state.BlackFolks;
                blackKings = state.BlackKings;

                if ((rl & prevRow & ~all) > 0)
                {
                    black = black ^ position;

                    if (prevRow == 0x0000000f)
                        blackKings += 1;
                    else
                        black = black ^ (rl & prevRow);

                    result.Add(new FastState(white, black, whiteKings, blackKings, !state.IsWhiteActive));
                }
            }

            return result;
        }
    }
}
