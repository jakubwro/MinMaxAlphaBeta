﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;
using MinMaxAlphaBeta;

namespace Checkers
{
    using Diagonal = IEnumerable<Square>;
    using Layout = IImmutableDictionary<Square, Checker>;

    public class GameState : IState<GameState>
    {
        private readonly GameSettings settings;
        private readonly Board board;
        private readonly Layout layout;
        private readonly ColorEnum activePlayer;
        //private readonly IEnumerable<IMove> availableMoves;

        //public Board Board { get { return this.board; } }
        public Layout Layout { get { return this.layout; } }
        public ColorEnum ActivePlayer { get { return this.activePlayer; } }
        public IEnumerable<IMove> AvailableMoves
        {
            get
            {
                var moves = GetPossibleMoves().ToList();

                if (moves.Any(m => m is SequenceOfCaptures))
                {
                    if (settings.MandatoryCaptures)
                        moves = moves.Where(m => m is SequenceOfCaptures).ToList();

                    if (settings.LongestCaptureSequence)
                    {
                        int maxLength = moves.OfType<SequenceOfCaptures>().Max(m => m.Length);
                        moves = moves.OfType<SequenceOfCaptures>().Where(m => m.Length == maxLength).OfType<IMove>().ToList();
                    }
                }

                return moves;
            }
        }

        public IEnumerable<Diagonal> Diagonals
        {
            get
            {
                return from d in board.Diagonals
                       select activePlayer == ColorEnum.Black ? d.Reverse() : d;
            }
        }

        public GameSettings Settings { get { return settings; } }

        public GameState(GameSettings settings)
        {
            this.settings = settings;

            if (this.settings.BoardSize == 8) this.board = Board.Board8x8;
            else if (this.settings.BoardSize == 10) this.board = Board.Board10x10;
            else if (this.settings.BoardSize == 12) this.board = Board.Board12x12;
            else throw new ArgumentOutOfRangeException("settings.BoardSize", "Valid board sizes: 8, 10 or 12");

            this.layout = BoardState.GetInitialLayout(this.board);

            //Test
            this.layout = this.Layout.Add(this.board.Squares.Skip(12).First(), Checker.BlackFolk);
            this.layout = this.Layout.Remove(this.board.Squares.Skip(25).First());
            this.layout = this.Layout.Remove(this.board.Squares.Skip(27).First());
            this.layout = this.Layout.Remove(this.board.Squares.Skip(22).First());
            this.layout = this.Layout.Add(this.board.Squares.Skip(13).First(), Checker.BlackFolk);
            this.activePlayer = ColorEnum.White;
        }

        private GameState(GameState gameState, IMove move)
        {
            this.settings = gameState.Settings;
            this.board = gameState.board;
            this.layout = move.LayoutAfter;
            this.activePlayer = gameState.ActivePlayer == ColorEnum.White ? ColorEnum.Black : ColorEnum.White;
        }


        public GameState MakeMove(IMove move) //TODO: change to TryMakeMove
        {
            if (!this.AvailableMoves.Any(m => m.Equals(move)))
                throw new InvalidOperationException(string.Format("{0} is illegal move!", move));

            return new GameState(this, move);
        }

        private IEnumerable<IMove> GetPossibleMoves()
        {
            foreach (var square in layout.Keys)
            {
                Checker checker = layout[square];
                if (checker.Color == activePlayer)
                    foreach (var move in checker.ExecuteRules(this, square))
                        yield return move;
            }
        }

        bool IState<GameState>.IsTerminal
        {
            get { return this.AvailableMoves.Count() == 0;  }
        }

        IEnumerable<GameState> IState<GameState>.GetNextStates()
        {
            return from move in AvailableMoves
                   select new GameState(this, move);
        }
    }
}