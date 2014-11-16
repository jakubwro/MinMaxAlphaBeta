using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct State : IState<State>
    {
        [FieldOffset(0)]
        readonly UInt32 white;
        [FieldOffset(4)]
        readonly UInt32 black;

        private State(UInt32 white, UInt32 black)
        {
            this.white = white;
            this.black = black;
        }

        static UInt32 ActivePlayerMask = 0x80000000;
        static UInt32 KingCountMask = 0x70000000;
        public static readonly State InitialState = new State(0x00000fff, 0xfff00000);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int WhiteKingsCount()
        {
            return (int)((white >> 56) & 0x7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int BlackKingsCount()
        {
            return (int)(black & 0x7);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int WhitePawnsCount()
        {
            return (white & 0x0fffffff).CountBits();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int BlackPawnsCount()
        {
            return (black & 0xfffffff0).CountBits();
        }

        
        public bool IsTerminal
        {
            get
            {
                return (white & 0x0fffffff) == 0 || (black & 0x0fffffff) == 0; //no more pawns in a color
            }
        }

        public IEnumerable<State> GetNextStates()
        {
            return new List<State>();
        }

        private void GetMoves()
        {
            char[,] board = new char[8, 8];

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    int tile = 0x1 << (i<<2) + j;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is State == false)
                return false;

            State other = (State)obj;

            return this.white == other.white && this.black == other.black;
        }

        public override int GetHashCode()
        {
            return (int)(white ^ black);
        }

        public ColorEnum ActivePlayer
        {
            get
            {
                return (white & 0x80000000) == 0 ? ColorEnum.White : ColorEnum.Black;
            }
        }

        public bool Equals(State other)
        {
            return this.white == other.white && this.black == other.black;
        }
    }
}
