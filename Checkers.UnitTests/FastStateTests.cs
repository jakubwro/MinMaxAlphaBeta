using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers.FastModel;

namespace UnitTests
{
    /// <summary>
    /// Summary description for FastState
    /// </summary>
    [TestClass]
    public class FastStateTests
    {
        [TestMethod]
        public void InitialState()
        {
            FastState state = FastState.InitialState;

            var moves = state.GetNextStates();
            Assert.IsTrue(moves.Count() == 7);

            Assert.IsTrue(moves.First().GetNextStates().Count() == 7);
        }

        [TestMethod]
        public void Promotion()
        {
            FastState state = new FastState(0x0f000000, 0x000000f0, 0, 0, true);

            var moves = state.GetNextStates();
            Assert.IsTrue(moves.Count() == 7);

            state = moves.First();

            Assert.IsTrue(state.GetNextStates().Count() == 7);

            Assert.IsTrue(state.WhiteFolks.CountBits() == 3);
            Assert.IsTrue(state.WhiteKings == 1);
        }

        [TestMethod]
        public void BlockedNoMove()
        {
            for (int i = 0; i < 10000000; ++i)
            {
                FastState state = new FastState(0x0000000f, 0x00000330, 0, 0, true);

                var moves = state.GetNextStates().ToList();
                Assert.IsTrue(moves.Count() == 4);
            }
        }
    }
}
