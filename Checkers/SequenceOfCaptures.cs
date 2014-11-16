using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Checkers
{
    using Layout = IImmutableDictionary<Square, Checker>;

    //TODO: Add assertions

    [DebuggerDisplay("{FromSquare} -> [{CapturedString}] -> {ToSquare}")]
    public abstract class SequenceOfCaptures : Move//, IEquatable<SequenceOfCaptures>
    {
        public SequenceOfCaptures(Layout layoutBefore, Square fromSquare, Square toSquare)
            : base(layoutBefore, fromSquare, toSquare)
        {

        }

        //just for debug
        public string CapturedString { get { return string.Join(",", CapturedSquares.Select(s => s.ToString())); } }

        public static SequenceOfCaptures BeginSequence(Layout currentState, Square fromSquare)
        {
            return new EmptySequenceOfCaptures(currentState, fromSquare);
        }

        public SequenceOfCaptures ContinueSequence(Square from, Square to, Square captured)
        {
            Debug.Assert(this.ToSquare == from);

            return new CombinedSequenceOfCaptures(to, captured, this);
        }

        private class EmptySequenceOfCaptures : SequenceOfCaptures
        {
            public EmptySequenceOfCaptures(Layout layoutBefore, Square fromSquare)
                : base(layoutBefore, fromSquare, fromSquare) { }

            override public  Layout LayoutAfter { get {return base.LayoutBefore; } }

            override public IEnumerable<Square> CapturedSquares { get { yield break; } }
            override public IEnumerable<Square> VisitedSquares { get { yield return this.FromSquare; } }
        }

        private class CombinedSequenceOfCaptures : SequenceOfCaptures
        {
            private readonly Square captured;
            private readonly SequenceOfCaptures restOfSequence;

            public CombinedSequenceOfCaptures(Square toSquare, Square captured, SequenceOfCaptures restOfSequence)
                : base(restOfSequence.LayoutAfter, restOfSequence.ToSquare, toSquare)
            {
                this.captured = captured;
                this.restOfSequence = restOfSequence;
            }

            override public Layout LayoutBefore { get { return restOfSequence.LayoutBefore; } }
            override public Square FromSquare { get { return restOfSequence.FromSquare; } }
            
            override public IEnumerable<Square> CapturedSquares
            {
                get
                {
                    foreach (var s in restOfSequence.CapturedSquares)
                        yield return s;

                    yield return this.captured;
                }
            }

            override public  IEnumerable<Square> VisitedSquares
            {
                get
                {
                    foreach (var s in restOfSequence.VisitedSquares)
                        yield return s;

                    yield return this.ToSquare;
                }
            }

            override public Layout LayoutAfter
            {
                get
                {
                    var layout = restOfSequence.LayoutAfter;
                    Square from = restOfSequence.ToSquare;
                    return layout.Add(ToSquare, layout[from]).Remove(from).Remove(captured);
                }
            }
        }
    }
}
