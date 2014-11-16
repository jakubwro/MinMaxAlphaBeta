﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    using Diagonal = ImmutableSortedSet<Square>;

    public partial class Board
    {
        public static readonly Board Board8x8 = new Board(8);
        public static readonly Board Board10x10 = new Board(10);
        public static readonly Board Board12x12 = new Board(12);

        private readonly ImmutableHashSet<Diagonal> diagonals;
        private readonly ImmutableSortedSet<Square> squares;
        private readonly int size;

        public IEnumerable<Diagonal> Diagonals { get { return diagonals; } }
        public IEnumerable<Square> Squares { get { return squares; } }
        public int Size { get { return size; } }

        public IEnumerable<Square> FirstRow { get { return squares.Where(s => s.Row == 1); } }
        public IEnumerable<Square> LastRow { get { return squares.Where(s => s.Row == this.size); } }

        private Board(int boardSize)
        {
            if (boardSize < 4 || boardSize > 100)
                throw new ArgumentOutOfRangeException("Board size should be in a reasonable range.");

            this.size = boardSize;
            this.squares = ImmutableSortedSet.CreateRange<Square>(GenerateSquares(this.size));
            this.diagonals = ImmutableHashSet.CreateRange<Diagonal>(GenerateDiagonals(this.squares));
        }

        private static IEnumerable<Diagonal> GenerateDiagonals(IEnumerable<Square> squares)
        {
            foreach(var c in squares.Select(s => s.Row - s.Column).Distinct())
                yield return ImmutableSortedSet.CreateRange<Square>(from s in squares where s.Row - s.Column == c select s);

            foreach (var c in squares.Select(s => s.Row + s.Column).Distinct())
                yield return ImmutableSortedSet.CreateRange<Square>(from s in squares where s.Row + s.Column == c select s);
        }

        private static IEnumerable<Square> GenerateSquares(int boardSize)
        {
            for (int i = 0; i < boardSize; ++i)
                for (int k = 0; k < boardSize; ++k)
                    if ((i + k) % 2 == 0)
                        yield return new Square((char)('A' + i), (byte)(k+1));
        }
    }
}