using System;
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
        private readonly int whiteScore;
        private readonly int blackScore;

        public int BoardSize { get { return board.Size; } }
        public Layout Layout { get { return this.layout; } }
        public ColorEnum ActivePlayer { get { return this.activePlayer; } }
        public int WhiteScore { get { return whiteScore; } }
        public int BlackScore { get { return blackScore; } }

        public IEnumerable<Move> AvailableMoves
        {
            get
            {
                var moves = GetPossibleMoves().ToList();
                var captures = moves.Where(m => m.CapturedSquares.Any()).ToList();

                if (captures.Any())
                {
                    if (settings.MandatoryCaptures)
                    {
                        if (settings.LongestCaptureSequence)
                        {
                            int maxLength = captures.Max(m => m.CapturedSquares.Count());
                            captures = captures.Where(m => m.CapturedSquares.Count() == maxLength).ToList();
                        }

                        moves = captures;
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

        public GameState(GameSettings settings, Board board)
        {
            this.settings = settings;
            this.board = board;
            this.layout = board.InitialLayout;         
            this.activePlayer = ColorEnum.White;
            this.whiteScore = 0;
            this.blackScore = 0;
        }

        ///Constructor for tests
        internal GameState(GameSettings settings, Board board, Layout layout, ColorEnum activePlayer, int whiteScore, int blackScore)
        {
            this.settings = settings;
            this.board = board;
            this.layout = layout;this.activePlayer = activePlayer;
            this.whiteScore = whiteScore;
            this.blackScore = blackScore;
        }

        public GameState MakeMove(Move move)
        {
            if (!this.AvailableMoves.Any(m => m.Equals(move)))
                throw new InvalidOperationException(string.Format("{0} is illegal move!", move));

            bool isLastSquare = move.ToSquare.Row == (activePlayer == ColorEnum.White ? board.Size : 1);

            var nextLayout = move.LayoutAfter;
            var nextWhiteScore = whiteScore;
            var nextBlackScore = blackScore;

            if (isLastSquare)
            {
                nextLayout = nextLayout.Remove(move.ToSquare);

                if (activePlayer == ColorEnum.White)
                    nextWhiteScore += 1;
                else
                    nextBlackScore += 1;
            }

            var nextPlayer = activePlayer == ColorEnum.White ? ColorEnum.Black : ColorEnum.White;

            return new GameState(this.settings, this.board, nextLayout, nextPlayer, nextWhiteScore, nextBlackScore);
        }

        private IEnumerable<Move> GetPossibleMoves()
        {
            foreach (var square in layout.Keys)
            {
                Checker checker = layout[square];
                if (checker.Color == activePlayer)
                    foreach (var move in checker.ExecuteRules(this, square))
                        yield return move;
            }
        }

        public bool IsTerminal
        {
            get { return this.AvailableMoves.Count() == 0;  }
        }

        public IEnumerable<GameState> GetNextStates()
        {
            return from move in AvailableMoves
                   select MakeMove(move);
        }

    }
}
