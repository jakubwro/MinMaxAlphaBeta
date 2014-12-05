using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class MinMaxPlayer<TState> : IPlayer<TState>
        where TState : IState<TState>
    {
        IMinMax<TState> minMax;

        public MinMaxPlayer(IMinMax<TState> minMax)
        {
            this.minMax = minMax;
        }

        public TState MakeMove(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Game is over");

            return minMax.MinMax(state);
        }
    }
}
