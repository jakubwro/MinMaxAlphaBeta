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
            GameSettings settings = new GameSettings();
            Board board = Board.Board8x8;
            Layout layout = BoardState.GetInitialLayout(board);
            var game = new GameState(settings, board, layout, ColorEnum.White);

            Assert.IsTrue(game.AvailableMoves.Count() == 7);

        }

        [TestMethod]
        public void GameState_CaptureSequence()
        {
            GameSettings settings = new GameSettings();
            Board board = Board.Board8x8;
            Layout layout = BoardState.GetInitialLayout(board);
            layout = layout.Add(board.Squares.Skip(12).First(), Checker.BlackFolk);
            layout = layout.Remove(board.Squares.Skip(25).First());
            layout = layout.Remove(board.Squares.Skip(27).First());

            var game = new GameState(settings, board, layout, ColorEnum.White);

            Assert.IsTrue(game.AvailableMoves.Count() == 1);
            Assert.IsTrue(((SequenceOfCaptures)game.AvailableMoves.Single()).Length == 4);

            game = game.MakeMove(game.AvailableMoves.First());

            Assert.IsTrue(game.AvailableMoves.Count() == 1);

        }

        [TestMethod]
        public void GameState_CaptureCycle()
        {
            GameSettings settings = new GameSettings();
            Board board = Board.Board8x8;
            Layout layout = BoardState.GetInitialLayout(board);
            layout = layout.Add(board.Squares.Skip(12).First(), Checker.BlackFolk);
            layout = layout.Remove(board.Squares.Skip(25).First());
            layout = layout.Remove(board.Squares.Skip(27).First());
            layout = layout.Remove(board.Squares.Skip(22).First());
            layout = layout.Add(board.Squares.Skip(13).First(), Checker.BlackFolk);

            var game = new GameState(settings, board, layout, ColorEnum.White);

            Assert.IsTrue(game.AvailableMoves.Count() == 2);
            Assert.IsTrue(game.AvailableMoves.OfType<SequenceOfCaptures>().All(m => m.FromSquare == m.ToSquare));
        }
    }
}
