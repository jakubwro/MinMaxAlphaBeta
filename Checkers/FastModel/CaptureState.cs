using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    /// <summary>
    /// Transitive state for generating captures
    /// </summary>
    public struct CaptureState
    {
        private readonly UInt32 white;
        private readonly UInt32 black;
        private readonly int rowNo;
        private readonly int colNo;

        public CaptureState(UInt32 white, UInt32 black, int rowNo, int colNo)
        {
            if (rowNo < 0 || rowNo >= 8)
                throw new IndexOutOfRangeException("Row no out of range");

            if (colNo < 0 || colNo >= 8)
                throw new IndexOutOfRangeException("Row no out of range");

            this.white = white;
            this.black = black;
            this.rowNo = rowNo;
            this.colNo = colNo;

            if ((White & Position) == 0 && (Black & Position) == 0)
                throw new InvalidOperationException("There is no checker on given square");
        }

        public UInt32 White { get { return white; } }
        public UInt32 Black { get { return black; } }
        public int RowNo { get { return rowNo; } }
        public int ColNo { get { return colNo; } }
        public UInt32 Row { get { return 0xfu << (rowNo << 2); } }
        public int SquareNo { get { return (rowNo << 2) + colNo; } }
        public UInt32 Position { get { return 0x1u << SquareNo; } }
        public UInt32 LastRow { get { return (White & Position) > 0 ? 0xf0000000 : 0x0000000f; } }
        public UInt32 NextRow { get { return Row << 4; } }
        public UInt32 NextNextRow { get { return Row << 8; } }
        public UInt32 PrevRow { get { return Row >> 4; } }
        public UInt32 PrevPrevRow { get { return Row >> 8; } }

        public bool IsWhiteActive { get { return (White & Position) > 0; } }
        public bool IsBlackActive { get { return (Black & Position) > 0; } }

    }
}
