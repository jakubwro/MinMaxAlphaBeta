using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Square : IEquatable<Square>, IComparable<Square>
    {
        readonly char column;
        readonly byte row;

        public char Column { get { return column; } }
        public int Row { get { return row; } }

        public Square(char column, byte row)
        {
            this.column = column;
            this.row = row;
        }

        public int CompareTo(Square other)
        {
            if (this.row > other.row)
                return 1;

            if (this.row < other.row)
                return -1;

            if (this.column > other.column)
                return 1;

            if (this.column < other.column)
                return -1;

            return 0;
        }

        public bool Equals(Square other)
        {
            return this.CompareTo(other) == 0;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", this.column, this.row);
        }

        public override bool Equals(object obj)
        {
            if (obj is Square == false)
                return false;

            return this.Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + this.row.GetHashCode();
                hash = hash * 23 + this.column.GetHashCode();
                return hash;
            }
        }
    }
}
