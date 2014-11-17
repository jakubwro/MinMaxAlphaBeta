using Checkers;
using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class GameStateGauge : Gauge<GameState, int>
    {
        protected override int ComputeValue(GameState state)
        {
            if (state.WhiteScore <= state.BlackScore)
                return 0;

            if (state.WhiteScore - state.BlackScore > 5)
                return 5;

            return state.WhiteScore - state.BlackScore;
        }
    }
}
