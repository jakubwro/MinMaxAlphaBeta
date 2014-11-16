using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Checkers
{
    using System;
using Layout = IImmutableDictionary<Square, Checker>;

    [DebuggerDisplay("{FromSquare} -> {ToSquare}")]
    public class Move : IMove, IEquatable<Move>
    {
        private readonly Layout layoutBefore;
        private readonly Layout layoutAfter;

        public Layout LayoutBefore { get { return layoutBefore; } }
        public Layout LayoutAfter { get { return layoutAfter; } }

        public Move(Layout layoutBefore, Layout layoutAfter)
        {
            this.layoutBefore = layoutBefore;
            this.layoutAfter = layoutAfter;
        }

        public Square FromSquare { get { return LayoutBefore.Keys.Except(LayoutAfter.Keys).Single(); } }
        public Square ToSquare { get { return LayoutAfter.Keys.Except(LayoutBefore.Keys).Single(); } }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", FromSquare, ToSquare);
        }

        public bool Equals(Move other)
        {
            return this.FromSquare == other.FromSquare && this.ToSquare == this.ToSquare;
        }

        public override bool Equals(object obj)
        {
            if (obj is Move == false)
                return false;

            return this.Equals((Move)obj);
        }

        public override int GetHashCode()
        {
            return this.FromSquare.GetHashCode() ^ this.ToSquare.GetHashCode();
        }
    }
}
