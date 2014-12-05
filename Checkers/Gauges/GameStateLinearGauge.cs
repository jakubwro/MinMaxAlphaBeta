using Checkers;
using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class GameStateLinearGauge : Gauge<GameState, int>
    {
        private readonly ColorEnum playerColor;

        public GameStateLinearGauge(ColorEnum playerColor)
        {
            this.playerColor = playerColor;
        }

        protected override int ComputeValue(GameState state)
        {
            if (this.playerColor == ColorEnum.White)
            {
                return state.WhiteScore - state.BlackScore;
            }

            return state.BlackScore - state.WhiteScore;
        }
    }
}
