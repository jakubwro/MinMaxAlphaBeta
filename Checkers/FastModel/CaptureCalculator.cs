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
        public IEnumerable<FastState> CalculateCaptures(FastState state, int rowNo, int colNo)
        {
            UInt32 white = state.WhiteFolks;
            UInt32 black = state.BlackFolks;
            UInt32 all = white | black;

            UInt32 row = 0xfu << (rowNo << 2);
            int squareNo = (rowNo << 2) + colNo;
            UInt32 position = 0x1u << squareNo;

            var lr = BinaryMasks.LeftToRightDiags[squareNo];
            var rl = BinaryMasks.RightToLeftDiags[squareNo];

            UInt32 nextRow = row << 4;
            UInt32 nextNextRow = row << 8;
            UInt32 prevRow = row >> 4;
            UInt32 prevPrevRow = row >> 8;


            yield return default(FastState);
        }

        public  IEnumerable<FastState> GetCaptures(UInt32 white, UInt32 black, int rowNo, int colNo)
        {
            //UInt32 all = white | black;
            UInt32 row = 0xfu << (rowNo << 2);
            int squareNo = (rowNo << 2) + colNo;
            UInt32 position = 0x1u << squareNo;

            UInt32 nextRow = row << 4;
            UInt32 nextNextRow = row << 8;
            UInt32 prevRow = row >> 4;
            UInt32 prevPrevRow = row >> 8;

            var lr = BinaryMasks.LeftToRightDiags[squareNo];
            var rl = BinaryMasks.RightToLeftDiags[squareNo];

            UInt32 captured = 0, destination = 0;

            UInt32 lastRow = (white & position) > 0 ? 0xf0000000 : 0x0000000f;

            //1
            captured = lr & nextRow;
            destination = lr & nextNextRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                //foreach (var s in GetCaptures(white & ~position & ~capture,
                //                              black & ~position & ~capture, )
                //{

                //}
            }

            //2
            captured = lr & prevRow;
            destination = lr & prevPrevRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                if (nextNextRow == lastRow)
                {

                }
            }

            //3
            captured = rl & nextRow;
            destination = rl & nextNextRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                if (nextNextRow == lastRow)
                {

                }
            }

            //4
            captured = rl & prevRow;
            destination = rl & prevPrevRow;
            if (IsLegalCapture(white, black, position, captured, destination))
            {
                if (nextNextRow == lastRow)
                {

                }
            }

            yield return default(FastState);
        }

        private bool IsLegalCapture(UInt32 white, UInt32 black, UInt32 position, UInt32 captured, UInt32 destination)
        {
            Debug.Assert(((white & position) ^ (black & position)) == 0);

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
