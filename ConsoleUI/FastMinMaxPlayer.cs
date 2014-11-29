using Checkers;
using Checkers.FastModel;
using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class FastMinMaxPlayer : IPlayer<GameState>
    {
        IMinMax<FastState> minMax;

        public FastMinMaxPlayer(IMinMax<FastState> minMax)
        {
            this.minMax = minMax;
        }

        public GameState MakeMove(GameState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Game is over");

            var result = minMax.MinMax(state.ToFastState());

            return state.GetNextStates().First(s => s.ToFastState().Equals(result));

        }
    }
}
