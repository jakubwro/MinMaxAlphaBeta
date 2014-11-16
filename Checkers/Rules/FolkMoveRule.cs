using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Checkers.Rules
{
    using Diagonal = IEnumerable<Square>;
    using Layout = IImmutableDictionary<Square, Checker>;

    class FolkMoveRule : Rule
    {
        public static readonly FolkMoveRule FolkMove = new FolkMoveRule();
        private FolkMoveRule() { }

        public override IEnumerable<Move> Execute(GameState game, Square square)
        {
            foreach (var diagonal in game.Diagonals)
            {
                if (!diagonal.Contains(square))
                    continue;

                var next = diagonal.SkipWhile(s => s != square).SecondOrDefault();
                if (next != null && game.Layout.ContainsKey(next) == false)
                    yield return GetMove(game.Layout, square, next);
            }
        }
    }
}
