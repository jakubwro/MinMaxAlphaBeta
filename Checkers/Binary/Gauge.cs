using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;

namespace Checkers.FastModel
{
    public class BinaryStateGauge : Gauge<State, int>
    {
        protected override int ComputeValue(State state)
        {
            return state.WhiteKingsCount() - state.BlackKingsCount();
        }
    }
}
