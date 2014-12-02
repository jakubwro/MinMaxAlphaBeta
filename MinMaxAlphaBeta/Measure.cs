using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    [DebuggerDisplay("{Value}")]
    public class Measure<TMeasure> : IComparable<Measure<TMeasure>>, IEquatable<Measure<TMeasure>>
        where TMeasure : IComparable<TMeasure>, IComparable, IEquatable<TMeasure>
    {
        private Measure() { }

        private Measure(TMeasure measure)
        {
            this.Value = measure;
        }

        public static Measure<TMeasure> Create(TMeasure measure)
        {
            return new Measure<TMeasure>(measure);
        }

        protected virtual TMeasure Value { get; set; }

        public static readonly Measure<TMeasure> MinusInfinity = new MinusInfinityMeasure();
        public static readonly Measure<TMeasure> PlusInfinity = new PlusInfinityMeasure();

        [DebuggerDisplay("-∞")]
        class MinusInfinityMeasure : Measure<TMeasure>
        {
            protected override TMeasure Value { get { throw new InvalidOperationException("Unable to measure infinity"); } }
        }

        [DebuggerDisplay("+∞")]
        class PlusInfinityMeasure : Measure<TMeasure>
        {
            protected override TMeasure Value { get { throw new InvalidOperationException("Unable to measure infinity"); } }
        }

        public static bool operator <=(Measure<TMeasure> e1, Measure<TMeasure> e2) { return e1.CompareTo(e2) <= 0; }
        public static bool operator >=(Measure<TMeasure> e1, Measure<TMeasure> e2) { return e1.CompareTo(e2) >= 0; }
        public static bool operator <(Measure<TMeasure> e1, Measure<TMeasure> e2) { return e1.CompareTo(e2) < 0; }
        public static bool operator >(Measure<TMeasure> e1, Measure<TMeasure> e2) { return e1.CompareTo(e2) > 0; }
        public static bool operator ==(Measure<TMeasure> e1, Measure<TMeasure> e2) { return e1.CompareTo(e2) == 0; }
        public static bool operator !=(Measure<TMeasure> e1, Measure<TMeasure> e2) { return e1.CompareTo(e2) != 0; }

        public int CompareTo(Measure<TMeasure> other)
        {
            if (this is MinusInfinityMeasure && other is MinusInfinityMeasure)
                throw new InvalidOperationException("Unable to compare two minus infinities");

            if (this is PlusInfinityMeasure && other is PlusInfinityMeasure)
                throw new InvalidOperationException("Unable to compare two infinities");

            if (this is MinusInfinityMeasure || other is PlusInfinityMeasure)
                return -1;

            if (this is PlusInfinityMeasure || other is MinusInfinityMeasure)
                return 1;

            return Value.CompareTo(other.Value);
        }

        public bool Equals(Measure<TMeasure> other)
        {
            return this.CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Measure<TMeasure> == false)
                return false;

            return this.Equals((Measure<TMeasure>)obj);
        }

        public override int GetHashCode()
        {
            if (this is MinusInfinityMeasure || this is PlusInfinityMeasure)
                return base.GetHashCode();

            return this.Value.GetHashCode();
        }

        public int ToInt()
        {
            return (int)Convert.ChangeType(this.Value, typeof(int));
        }

    }
}
