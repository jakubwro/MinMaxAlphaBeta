using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Checkers
{
    using System;
    using Layout = IImmutableDictionary<Square, Checker>;

    //TODO: change property LayoutAfter to Lazy<> to avoid multiple dictionary creation

    [DebuggerDisplay("{FromSquare} -> {ToSquare}")]
    public class Move : IEquatable<Move>
    {
        private readonly Layout layoutBefore;
        private readonly Square fromSquare;
        private readonly Square toSquare;

        virtual public Layout LayoutBefore { get { return layoutBefore; } }
        virtual public Layout LayoutAfter { get { return layoutBefore.Add(toSquare, layoutBefore[fromSquare]).Remove(fromSquare); } }
        virtual public Square FromSquare { get { return fromSquare; } }
        virtual public Square ToSquare { get { return toSquare; } }


        virtual public IEnumerable<Square> CapturedSquares { get { yield break; } }
        virtual public IEnumerable<Square> VisitedSquares
        {
            get
            {
                yield return fromSquare;
                yield return toSquare;
            }
        }

        public Move(Layout layoutBefore, Square fromSquare, Square toSquare)
        {
            this.layoutBefore = layoutBefore;
            this.fromSquare = fromSquare;
            this.toSquare = toSquare;
        }

        override public string ToString()
        {
            return string.Format("{0} -> {1}", FromSquare, ToSquare);
        }

        public bool Equals(Move other)
        {
            if (other == null)
                return false;

            return this.VisitedSquares.SequenceEqual(other.VisitedSquares);
        }

        override public bool Equals(object obj)
        {
            if (obj is Move == false)
                return false;

            return this.Equals((Move)obj);
        }

        override public int GetHashCode()
        {
            int hash = 0;
            foreach (Square s in this.VisitedSquares)
                hash ^= s.GetHashCode();

            return hash;
        }
    }
}
