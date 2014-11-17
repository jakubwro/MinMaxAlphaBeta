using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaxAlphaBeta;

namespace MinMaxAlphaBeta.UnitTests
{
    [TestClass]
    public class MeasureTests
    {
        [TestMethod]
        public void Measure_Comparation()
        {
            var _0 = Measure<int>.Create(0);
            var _1 = Measure<int>.Create(1);

            Assert.IsTrue(_0 < _1);
            Assert.IsTrue(_0 <= _1);
            Assert.IsTrue(_0 != _1);
            Assert.IsFalse(_0 > _1);
            Assert.IsFalse(_0 >= _1);
            Assert.IsFalse(_0 == _1);

            Assert.IsTrue(Measure<int>.MinusInfinity <= _1);
            Assert.IsTrue(Measure<int>.PlusInfinity >= _1);

            Assert.IsTrue(Measure<int>.MinusInfinity <= Measure<int>.PlusInfinity);
            Assert.IsTrue(Measure<int>.PlusInfinity >= Measure<int>.MinusInfinity);

        }
    }
}
