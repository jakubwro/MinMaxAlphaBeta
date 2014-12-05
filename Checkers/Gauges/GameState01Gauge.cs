using Checkers;
using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class GameState01Gauge : Gauge<GameState, int>
    {
        private readonly ColorEnum playerColor;

        public GameState01Gauge(ColorEnum playerColor)
        {
            this.playerColor = playerColor;
        }

        protected override int ComputeValue(GameState state)
        {
            if (state.WhiteScore == state.BlackScore)
                return 0;

            if (this.playerColor == ColorEnum.White)
                return state.WhiteScore > state.BlackScore ? 1 : -1;

            return state.BlackScore > state.WhiteScore ? 1 : -1; 

            if (this.playerColor == ColorEnum.White)
            {
                if (state.WhiteScore <= state.BlackScore)
                    return 0;

                if (state.WhiteScore - state.BlackScore > 5)
                    return 5;

                return state.WhiteScore - state.BlackScore;
            }

            if (state.BlackScore <= state.WhiteScore)
                return 0;

            if (state.BlackScore - state.WhiteScore > 5)
                return 5;

            return state.BlackScore - state.WhiteScore;
        }
    }
}
