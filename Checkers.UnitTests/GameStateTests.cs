using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CheckersTests
{
    using Diagonal = IEnumerable<Square>;
    using Layout = IImmutableDictionary<Square, Checker>;

    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void GameState_InitialState()
        {
            //for (int i = 0; i < 10000; ++i)
            {
                GameSettings settings = GameSettings.Default;
                Board board = Board.Board8x8;
                Layout layout = board.InitialLayout;
                var game = new GameState(settings, board, layout, ColorEnum.White, 0, 0);

                Assert.IsTrue(game.AvailableMoves.Count() == 7);
            }
        }

        [TestMethod]
        public void GameState_CaptureSequence()
        {
            GameSettings settings = GameSettings.Default;
            Board board = Board.Board8x8;
            Layout layout = board.InitialLayout;
            layout = layout.Add(board.Squares.Skip(12).First(), Checker.BlackFolk);
            layout = layout.Remove(board.Squares.Skip(25).First());
            layout = layout.Remove(board.Squares.Skip(27).First());

            var game = new GameState(settings, board, layout, ColorEnum.White, 0, 0);

            Assert.IsTrue(game.AvailableMoves.Count() == 1);
            Assert.IsTrue(((Capture)game.AvailableMoves.Single()).CapturedSquares.Count() == 4);

            game = game.MakeMove(game.AvailableMoves.First());

            Assert.IsTrue(game.AvailableMoves.Count() == 1);

        }

        [TestMethod]
        public void GameState_CaptureCycle()
        {
            GameSettings settings = GameSettings.Default;
            Board board = Board.Board8x8;
            Layout layout = board.InitialLayout;
            layout = layout.Add(board.Squares.Skip(12).First(), Checker.BlackFolk);
            layout = layout.Remove(board.Squares.Skip(25).First());
            layout = layout.Remove(board.Squares.Skip(27).First());
            layout = layout.Remove(board.Squares.Skip(22).First());
            layout = layout.Add(board.Squares.Skip(13).First(), Checker.BlackFolk);

            var game = new GameState(settings, board, layout, ColorEnum.White, 0, 0);

            Assert.IsTrue(game.AvailableMoves.Count() == 2);
            Assert.IsTrue(game.AvailableMoves.OfType<Capture>().All(m => m.FromSquare == m.ToSquare));
        }

        [TestMethod]
        public void GameState_Search()
        {
            GameSettings settings = GameSettings.Default;
            Board board = Board.Board8x8;
            var game = new GameState(settings, board);

            SearchRec(game, 0, 1);
        }

        private void SearchRec(GameState game, int level, int limit)
        {
            if (level >= limit)
                return;

            foreach (var move in game.AvailableMoves)
            {
                SearchRec(game.MakeMove(move), level+1, limit);
            }
        }

        [TestMethod]
        public void GameState_Equality()
        {
            GameSettings settings = GameSettings.Default;
            Board board = Board.Board8x8;

            var game1 = new GameState(settings, board);
            var game2 = new GameState(settings, board);

            Assert.IsTrue(game1.Equals(game2));
            Assert.IsTrue(game1.Equals((object)game2));
            Assert.IsTrue(game1.GetHashCode() == game2.GetHashCode());

            game1 = game1.MakeMove(game1.AvailableMoves.Single(m => m.ToString() == "A3>B4"));

            Assert.IsFalse(game1.Equals(game2));
            Assert.IsFalse(game1.Equals((object)game2));
            Assert.IsFalse(game1.GetHashCode() == game2.GetHashCode());

            game2 = game2.MakeMove(game2.AvailableMoves.Single(m => m.ToString() == "A3>B4"));

            Assert.IsTrue(game1.Equals(game2));
            Assert.IsTrue(game1.Equals((object)game2));
            Assert.IsTrue(game1.GetHashCode() == game2.GetHashCode());
        }
    }
}
