using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    public static class CaptureCalculator
    {
        public static IEnumerable<CaptureState> CalculateCaptures(FastState state, int rowNo, int colNo)
        {
            return GetCaptures(new CaptureState(state.WhiteFolks, state.BlackFolks, rowNo, colNo));
        }

        public static IEnumerable<CaptureState> GetCaptures(CaptureState captureState)
        {
            UInt32 white = captureState.White;
            UInt32 black = captureState.Black;

            var lr = BinaryMasks.LeftToRightDiags[captureState.SquareNo];
            var rl = BinaryMasks.RightToLeftDiags[captureState.SquareNo];

            UInt32 captured = 0, destination = 0;
            UInt32 position = captureState.Position;
            
            //1
            captured = lr & captureState.NextRow;
            destination = lr & captureState.NextNextRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                var cs = new CaptureState((white & ~position & ~captured) | destination,
                                          black & ~position & ~captured,
                                          captureState.RowNo + 2,
                                          captureState.ColNo + 1);
                yield return cs;

                foreach (var s in GetCaptures(cs))
                    yield return s;
            }

            //2
            captured = lr & captureState.PrevRow;
            destination = lr & captureState.PrevPrevRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                var cs = new CaptureState((white & ~position & ~captured) | destination,
                                          black & ~position & ~captured,
                                          captureState.RowNo - 2,
                                          captureState.ColNo - 1);
                yield return cs;

                foreach (var s in GetCaptures(cs))
                    yield return s;

            }

            //3
            captured = rl & captureState.NextRow;
            destination = rl & captureState.NextNextRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                var cs = new CaptureState((white & ~position & ~captured) | destination,
                                          black & ~position & ~captured,
                                          captureState.RowNo + 2,
                                          captureState.ColNo - 1);

                yield return cs;

                foreach (var s in GetCaptures(cs))
                    yield return s;
            }

            //4
            captured = rl & captureState.PrevRow;
            destination = rl & captureState.PrevPrevRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                var cs = new CaptureState((white & ~position & ~captured) | destination,
                                          black & ~position & ~captured,
                                          captureState.RowNo - 2,
                                          captureState.ColNo + 1);

                yield return cs;

                foreach (var s in GetCaptures(cs))
                    yield return s;
            }


        }

        private static bool IsLegalCapture(UInt32 white, UInt32 black, UInt32 position, UInt32 captured, UInt32 destination)
        {
            Debug.Assert(((white & position) ^ (black & position)) == 0);

            if (destination == 0)
                return false; //out of board

            UInt32 all = white | black;

            if ((all & destination) > 0)
                return false;   // Destination square is taken

            //There is enemy next to current
            if ((white & position) > 0)
            {
                if ((black & captured) == 0)
                {
                    return false;
                }
            }
            else if ((black & position) > 0)
            {
                if ((white & captured) == 0)
                {
                    return false;
                }
            }
            else
            {
                Debug.Assert(false);
            }

            return true;
        }
    }
}
