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

        public static IEnumerable<FastState> CalculateMoves(FastState state, int rowNo, int colNo)
        {
            UInt32 white = state.WhiteFolks;
            UInt32 black = state.BlackFolks;
            int whiteKings = state.WhiteKings;
            int blackKings = state.BlackKings;
            UInt32 all = white | black;

            UInt32 row = 0xfu << (rowNo << 2);
            int squareNo = (rowNo << 2) + colNo;
            UInt32 position = 0x1u << squareNo;

            uint lr = BinaryMasks.LeftToRightDiags[squareNo];
            uint rl = BinaryMasks.RightToLeftDiags[squareNo];

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
