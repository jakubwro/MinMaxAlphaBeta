using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Checkers
{
    using Layout = IImmutableDictionary<Square, Checker>;

    [DebuggerDisplay("{FromSquare} -> [{CapturedString}] -> {ToSquare}")]
    public abstract class SequenceOfCaptures : IMove, IEquatable<SequenceOfCaptures>
    {
        public abstract Layout LayoutBefore { get; }
        public abstract Layout LayoutAfter { get; }
        public abstract int Length { get; }

        public abstract Square ToSquare { get; }
        public abstract Square FromSquare { get; }
        
        public IEnumerable<Square> Captured { get {return LayoutBefore.Keys.Except(LayoutAfter.Keys).Where(s => s != FromSquare); } }
        public string CapturedString { get { return string.Join(",", Captured.Select(s => s.ToString())); } }

        public static SequenceOfCaptures GetEmptySequence(Layout currentState, Square fromSquare)
        {
            return new EmptySequenceOfCaptures(currentState, fromSquare);
        }

        public SequenceOfCaptures ContinueSequence(IEnumerable<Square> squares)
        {
            Debug.Assert(squares.Count() == 3);
            var layout = this.LayoutAfter;
            var from = squares.First();
            var captured = squares.Second();
            var to = squares.Third();
            var layoutAfter = layout.Add(to, layout[from]).Remove(from).Remove(captured);
            
            return new CombinedSequenceOfCaptures(layoutAfter, this);
        }

        private class EmptySequenceOfCaptures : SequenceOfCaptures
        {
            private readonly Layout currentState;
            private readonly Square fromSquare;
            public EmptySequenceOfCaptures(Layout currentState, Square fromSquare)
            {
                this.currentState = currentState;
                this.fromSquare = fromSquare;
            }

            override public int Length { get { return 0; } }
            override public Layout LayoutBefore { get { return this.currentState; } }
            override public Layout LayoutAfter { get { return this.currentState; } }
            override public Square FromSquare { get { return fromSquare; } }
            override public Square ToSquare { get { return fromSquare; } }
        }

        private class CombinedSequenceOfCaptures : SequenceOfCaptures
        {
            private readonly Layout stateAfter;
            private readonly SequenceOfCaptures restOfSequence;

            public CombinedSequenceOfCaptures(Layout stateAfter, SequenceOfCaptures restOfSequence)
            {
                this.stateAfter = stateAfter;
                this.restOfSequence = restOfSequence;
            }

            override public Layout LayoutBefore { get { return restOfSequence.LayoutBefore; } }
            override public Layout LayoutAfter { get { return this.stateAfter; } }
            override public int Length { get { return restOfSequence.Length + 1; } }
            override public Square ToSquare { get { return LayoutAfter.Keys.Except(LayoutBefore.Keys).SingleOrDefault() ?? FromSquare; } }
            override public Square FromSquare { get { return restOfSequence.FromSquare; } }
        }

        //TODO: make better move comparer
        public bool Equals(SequenceOfCaptures other)
        {
            return this.FromSquare == other.FromSquare && this.ToSquare == this.ToSquare;
        }

        public override bool Equals(object obj)
        {
            if (obj is SequenceOfCaptures == false)
                return false;

            return this.Equals((SequenceOfCaptures)obj);
        }

        public override int GetHashCode()
        {
            return this.FromSquare.GetHashCode() ^ this.ToSquare.GetHashCode();
        }

    }
}
