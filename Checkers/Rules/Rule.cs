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

        public abstract IEnumerable<Move> Execute(GameState game, Square square);
    }
}
