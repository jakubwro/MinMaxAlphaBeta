using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Checkers.FastModel
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct FastState : IState<FastState>, IEquatable<FastState>
    {
        static readonly UInt32 WhiteFolkMask = 0x0fffffff;
        static readonly UInt32 WhiteKingMask = 0x70000000;
        static readonly UInt32 WhiteActiveMask = 0x80000000;

        static readonly UInt32 BlackFolkMask = 0xfffffff0;
        static readonly UInt32 BlackKingMask = 0x00000007;
        static readonly UInt32 BlackActiveMask = 0x00000008;

        public static readonly FastState InitialState = new FastState(0x00000fff, 0xfff00000, 0, 0, true);

        [FieldOffset(0)]
        readonly UInt32 white;
        [FieldOffset(4)]
        readonly UInt32 black;

        public FastState(UInt32 white, UInt32 black,
                           int whiteKings, int blackKings,
                           bool isWhiteActive)
        {
            if ((white & ~WhiteFolkMask) > 0)
                throw new ArgumentOutOfRangeException("white");
            if ((black & ~BlackFolkMask) > 0)
                throw new ArgumentOutOfRangeException("black");
            if (whiteKings < 0 || whiteKings > 7)
                throw new ArgumentOutOfRangeException("whiteKings");
            if (blackKings < 0 || blackKings > 7)
                throw new ArgumentOutOfRangeException("blackKings");

            this.white = white | (((UInt32)whiteKings) << 28);
            this.black = black | (((UInt32)blackKings));

            if (isWhiteActive)
                this.white = white | WhiteActiveMask;
            else
                this.black = black | BlackActiveMask;
        }

        //TODO: add [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public UInt32 WhiteFolks { get { return white & WhiteFolkMask; } }
        public int WhiteKings { get { return (int)((white & WhiteKingMask) >> 28); } }
        public bool IsWhiteActive { get { return (white & WhiteActiveMask) > 0; } }

        public UInt32 BlackFolks { get { return black & BlackFolkMask; } }
        public int BlackKings { get { return (int)((black & BlackKingMask)); } }
        public bool IsBlackActive { get { return (black & BlackActiveMask) > 0; } }

        public ColorEnum ActivePlayer { get { return IsWhiteActive ? ColorEnum.White : ColorEnum.Black; } }

        public bool Equals(FastState other)
        {
            return this.white == other.white && this.black == other.black;
        }

        override public bool Equals(object obj)
        {
            if (obj is FastState == false)
                return false;

            return this.Equals((FastState)obj);
        }

        override public int GetHashCode()
        {
            return (int)(white ^ black);
        }

        #region IState<FastState> Members

        public bool IsTerminal
        {
            get { return GetNextStates().Count() == 0; }
        }

        public IEnumerable<FastState> GetNextStates()
        {
            UInt32 folks = IsWhiteActive ? WhiteFolks : BlackFolks;

            for(int r = 0; r < 8; ++r)
            {
                UInt32 row = 0xfu << (r << 2);
                for(int i = 0; i < 4; ++i)
                {
                    UInt32 position = 0x1u << ((r<<2)+i);
                    if ((folks & position) > 0)
                        MoveCalculator.CalculateMoves(this, row, position);
                }
            }

            var res = MoveCalculator.result;
            MoveCalculator.result = new List<FastState>(10);
            return res;
        }

        #endregion

        public GameState ToGameState()
        {
            var board = Board.Board8x8;

            var builder = ImmutableDictionary.CreateBuilder<Square, Checker>();

            for (int r = 0; r < 8; ++r)
            {
                for (int i = 0; i < 4; ++i)
                {
                    UInt32 position = 0x1u << ((r << 2) + i);

                    var column = 'A' + ((i << 1) + (r & 0x1));
                    var row = (r + 1);
                    Square s = board.Squares.Single(sq => sq.Column == column && sq.Row == row);
                    
                    if ((WhiteFolks & position) > 0)
                        builder.Add(s, Checker.WhiteFolk);
                    if((BlackFolks & position) > 0)
                        builder.Add(s, Checker.BlackFolk);
                }
            }

            return new GameState(GameSettings.Default, board, builder.ToImmutable(),
                IsWhiteActive ? ColorEnum.White : ColorEnum.Black, WhiteKings, BlackKings);
        }
    }
}
