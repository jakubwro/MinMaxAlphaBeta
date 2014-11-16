using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class RandomPlayer<TState> : IPlayer<TState>
        where TState : IState<TState>
    {
        Random random = new Random(DateTime.Now.Millisecond);

        public TState MakeMove(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Game is over");

            var states = state.GetNextStates();
            var index = random.Next(0, states.Count() - 1);
            return states.ElementAt(index);
        }
    }
}
