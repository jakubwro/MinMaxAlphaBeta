using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.FastModel;
using Checkers;

namespace UnitTests
{
    using Layout = System.Collections.Immutable.IImmutableDictionary<Square, Checker>;
 
    /// <summary>
    /// Summary description for FastState
    /// </summary>
    [TestClass]
    public class FastStateTests
    {
        [TestMethod]
        public void FastState_InitialState()
        {
            for (int i = 0; i < 6000000; ++i)
            {
                FastState state = FastState.InitialState;

                var moves = state.GetNextStates();
                Assert.IsTrue(moves.Count() == 7);
                state = moves.First();
                moves = state.GetNextStates();
                Assert.IsTrue(moves.Count() == 7);
            }
        }

        [TestMethod]
        public void FastState_Promotion()
        {
            FastState state = new FastState(0x0f000000, 0x000000f0, 0, 0, true);

            var moves = state.GetNextStates();
            Assert.IsTrue(moves.Count() == 7);

            state = moves.First();

            Assert.IsTrue(state.WhiteFolks.CountBits() == 3);
            Assert.IsTrue(state.WhiteKings == 1);

            moves = state.GetNextStates();
            Assert.IsTrue(moves.Count() == 7);
            state = moves.First();
            Assert.IsTrue(state.BlackFolks.CountBits() == 3);
            Assert.IsTrue(state.BlackKings == 1);

        }

        [TestMethod]
        public void FastState_BlockedNoMove()
        {
            //for (int i = 0; i < 100000; ++i)
            {
                FastState state = new FastState(0x0000000f, 0x00000330, 0, 0, true);
                var moves = state.GetNextStates().ToList();
                Assert.IsTrue(moves.Count() == 4);
            }
        }

        [TestMethod]
        public void FastState_ToGameState()
        {
            GameState gs = FastState.InitialState.ToGameState();
            GameState game = new GameState(GameSettings.Default, Board.Board8x8);
            Assert.IsTrue(gs.Equals(game));
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

            var fastState = game.ToFastState();

            var moves = fastState.GetNextStates().ToList();

            Assert.IsTrue(moves.Count() == 2);
        }
    }
}
