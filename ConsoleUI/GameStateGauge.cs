using Checkers;
using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class GameStateGauge : IGauge<GameState, int>
    {
        private readonly ColorEnum playerColor;

        public GameStateGauge(ColorEnum playerColor)
        {
            this.playerColor = playerColor;
        }

        public Measure<int> GetMeasure(GameState state)
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
