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
        private readonly Square fromSquare;
        private readonly Square toSquare;

        public Layout LayoutBefore { get { return layoutBefore; } }
        public Square FromSquare { get { return fromSquare; } }
        public Square ToSquare { get { return toSquare; } }
        
        public Layout LayoutAfter { get { return layoutBefore.Add(toSquare, layoutBefore[fromSquare]).Remove(fromSquare); } }

        public Move(Layout layoutBefore, Square fromSquare, Square toSquare)
        {
            this.layoutBefore = layoutBefore;
            this.fromSquare = fromSquare;
            this.toSquare = toSquare;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", FromSquare, ToSquare);
        }

        public bool Equals(Move other)
        {
            return this.fromSquare == other.fromSquare && this.toSquare == other.toSquare;
        }

        public override bool Equals(object obj)
        {
            if (obj is Move == false)
                return false;

            return this.Equals((Move)obj);
        }

        public override int GetHashCode()
        {
            return this.fromSquare.GetHashCode() ^ this.toSquare.GetHashCode();
        }
    }
}
