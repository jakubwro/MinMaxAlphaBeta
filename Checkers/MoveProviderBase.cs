using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Checkers
{
    using Layout = IImmutableDictionary<Square, Checker>;

    abstract class MoveProviderBase
    {
        //public IEnumerable<Layout> GetMoves(Board board, Layout layout, ColorEnum color)
        //{
        //    IEnumerable<IEnumerable<Square>> diagonals = board.Diagonals;

        //    if (color == ColorEnum.Black)
        //        diagonals = from d in diagonals select d.Reverse();

        //    //foreach (var square in layout.OfColor(color))
        //    //    foreach (var move in GenerateMoves(diagonals, layout, square))
        //    //        yield return move;
        //}

        abstract public IEnumerable<object> GenerateMoves(Board board, Layout layout);
    }
}
