using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;

namespace Checkers.FastModel
{
    public class BinaryStateGauge : Gauge<FastState, int>
    {
        protected override int ComputeValue(FastState state)
        {
            return state.WhiteKings - state.BlackKings;
        }
    }
}
