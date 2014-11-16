using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;

namespace Checkers
{
    public class Gauge : IGauge<State, int>
    {
        public Measure<int> Measure(State state)
        {
            if (state.IsTerminal == false)
            {
                //TODO: Maybe non terminal state should be measurable too?
                throw new InvalidOperationException("Cannot simply measure non terminal state");
            }

            return Measure<int>.Create(state.WhiteKingsCount() - state.BlackKingsCount());
        }
    }
}
