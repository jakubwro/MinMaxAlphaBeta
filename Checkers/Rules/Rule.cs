using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Checkers.Rules
{
    using Diagonal = IEnumerable<Square>;
    using Layout = IImmutableDictionary<Square, Checker>;

    public abstract class Rule
    {
        protected Rule() { }

        public static readonly IEnumerable<Rule> FolkRules = ImmutableList.CreateRange<Rule>(
            new Rule[] { FolkMoveRule.FolkMove, FolkCaptureRule.FolkCapture });

        public abstract IEnumerable<IMove> Execute(GameState game, Square square);

        internal Layout GetNextState(Layout layout, IEnumerable<Square> squares)
        {
            return layout.Add(squares.Third(), layout[squares.First()])
                         .Remove(squares.First())
                         .Remove(squares.Second());
        }

        internal Move GetMove(Layout layout, Square first, Square second)
        {
            return new Move(layout, first, second);
        }

    }
}
