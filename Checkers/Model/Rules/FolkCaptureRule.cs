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

    public class FolkCaptureRule : Rule
    {
        public static readonly FolkCaptureRule FolkCapture = new FolkCaptureRule();

        private FolkCaptureRule() { }

        override public IEnumerable<IMove> Execute(GameState game, Square square)
        {
            var captures = GenerateCaptures(game, SequenceOfCaptures.BeginSequence(game.Layout, square));
        
            return captures;
        }

        private IEnumerable<SequenceOfCaptures> GenerateCaptures(GameState game, SequenceOfCaptures sequence)
        {
            foreach (var diagonal in game.Diagonals)
            {
                if (!diagonal.Contains(sequence.ToSquare))
                    continue;

                var forward = diagonal.SkipWhile(s => s != sequence.ToSquare).Take(3);

                if (IsLegalCapture(sequence.LayoutAfter, forward))
                    foreach (var c in CaptureRec(game, sequence.ContinueSequence(forward.Third(), forward.Second())))
                        yield return c;

                if (game.Settings.CaptureBackwards)
                {
                    var backward = diagonal.Reverse().SkipWhile(s => s != sequence.ToSquare).Take(3);

                    if (IsLegalCapture(sequence.LayoutAfter, backward))
                        foreach (var c in CaptureRec(game, sequence.ContinueSequence(backward.Third(), backward.Second())))
                            yield return c;
                }
            }
        }

        private IEnumerable<SequenceOfCaptures> CaptureRec(GameState game, SequenceOfCaptures sequence)
        {
            foreach (SequenceOfCaptures c in GenerateCaptures(game, sequence))
                yield return c;

            yield return sequence;
        }

        private bool IsLegalCapture(Layout layout, IEnumerable<Square> squares)
        {
            if (squares.Count() != 3)
                return false;

            // 1. There is piece on the next square
            Checker enemy;
            if (!layout.TryGetValue(squares.Second(), out enemy))
                return false;

            // 2. This piece is an enemy
            if (enemy.Color == layout[squares.First()].Color)
                return false;

            // Square behind the enemy is available
            if (layout.ContainsKey(squares.Third()))
                return false;

            return true;
        }
    }
}
