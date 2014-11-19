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
        public IEnumerable<FastState> CalculateCaptures(FastState state, UInt32 row, UInt32 position)
        {
            UInt32 white = state.WhiteFolks;
            UInt32 black = state.BlackFolks;
            UInt32 all = white | black;

            var lr = BinaryMasks.LeftToRightDiagonals.Single(d => (d & position) > 0);
            var rl = BinaryMasks.RightToLeftDiagonals.Single(d => (d & position) > 0);

            UInt32 nextRow = row << 4;
            UInt32 nextNextRow = row << 8;
            UInt32 prevRow = row >> 4;
            UInt32 prevPrevRow = row >> 8;

            if (state.IsWhiteActive)
            {
                if ((lr & nextRow & black) > 0 && (lr & nextNextRow & ~all) > 0)
                {
                    FastState newState = new FastState(white ^ ~position ^ (lr & nextNextRow),
                                                       black ^ ~(lr & nextRow),
                                                       state.WhiteKings,
                                                       state.BlackKings,
                                                       !state.IsWhiteActive);

                    if (nextNextRow == 0xf0000000)
                    {
                        FastState newPromotion = new FastState(white ^ ~position,
                                                               black ^ ~(lr & nextRow),
                                                               state.WhiteKings + 1,
                                                               state.BlackKings,
                                                               !state.IsWhiteActive);
                        yield return newPromotion;
                    }
                    else
                    {
                        yield return newState;
                    }

                    foreach (var s in CalculateCaptures(newState, nextNextRow , (lr & nextNextRow)))
                        yield return s;

                }

                if ((lr & prevRow & ~black) == 0 && (lr & prevPrevRow & all) == 0)
                {
                    FastState newState = new FastState(white ^ ~position ^ (lr & prevPrevRow),
                                   black ^ ~(lr & prevRow),
                                   state.WhiteKings,
                                   state.BlackKings,
                                   !state.IsWhiteActive);

                    if (prevPrevRow == 0xf0000000)
                    {
                        FastState newPromotion = new FastState(white ^ ~position,
                                                               black ^ ~(lr & prevRow),
                                                               state.WhiteKings + 1,
                                                               state.BlackKings,
                                                               !state.IsWhiteActive);
                        yield return newPromotion;
                    }
                    else
                    {
                        yield return newState;
                    }

                    foreach (var s in CalculateCaptures(newState, prevPrevRow, (lr & prevPrevRow)))
                        yield return s;
                }

                if ((rl & nextRow & ~black) == 0 && (rl & nextNextRow & all) == 0)
                {
                    FastState newState = new FastState(white ^ ~position ^ (rl & nextNextRow),
                                                       black ^ ~(rl & nextRow),
                                                       state.WhiteKings,
                                                       state.BlackKings,
                                                       !state.IsWhiteActive);

                    if (nextNextRow == 0xf0000000)
                    {
                        FastState newPromotion = new FastState(white ^ ~position,
                                                               black ^ ~(rl & nextRow),
                                                               state.WhiteKings + 1,
                                                               state.BlackKings,
                                                               !state.IsWhiteActive);
                        yield return newPromotion;
                    }
                    else
                    {
                        yield return newState;
                    }

                    foreach (var s in CalculateCaptures(newState, nextNextRow, (rl & nextNextRow)))
                        yield return s;
                }

                if ((rl & prevRow & ~black) == 0 && (rl & prevPrevRow & all) == 0)
                {
                    FastState newState = new FastState(white ^ ~position ^ (rl & prevPrevRow),
                                   black ^ ~(rl & prevRow),
                                   state.WhiteKings,
                                   state.BlackKings,
                                   !state.IsWhiteActive);

                    if (prevPrevRow == 0xf0000000)
                    {
                        FastState newPromotion = new FastState(white ^ ~position,
                                                               black ^ ~(rl & prevRow),
                                                               state.WhiteKings + 1,
                                                               state.BlackKings,
                                                               !state.IsWhiteActive);
                        yield return newPromotion;
                    }
                    else
                    {
                        yield return newState;
                    }

                    foreach (var s in CalculateCaptures(newState, prevPrevRow, (rl & prevPrevRow)))
                        yield return s;
                }
            }
        }
    }
}
