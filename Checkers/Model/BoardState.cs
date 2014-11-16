using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    using Layout = IImmutableDictionary<Square, Checker>;

    public class BoardState
    {
        public readonly Board Board;
        public readonly Layout Layout;
        //public readonly ColorEnum ActivePlayer;

        public BoardState(Board board, Layout layout)
        {
            this.Board = board;
            this.Layout = layout;
        }

        public static BoardState GetInitialState(Board board)
        {
            return new BoardState(board, GetInitialLayout(board));
        }

        public static Layout GetInitialLayout(Board board)
        {
            var initialPiecesCount = (board.Squares.Count() - board.Size) / 2;

            var white = board.Squares.Take(initialPiecesCount);
            var black = board.Squares.Reverse().Take(initialPiecesCount);

            var builder = ImmutableDictionary.CreateBuilder<Square, Checker>();

            foreach (var square in white)
                builder.Add(square, Checker.WhiteFolk);

            foreach (var square in black)
                builder.Add(square, Checker.BlackFolk);

            return builder.ToImmutableDictionary();
        }

    }
}
