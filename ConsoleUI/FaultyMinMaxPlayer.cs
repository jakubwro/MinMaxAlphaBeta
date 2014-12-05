using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class FaultyMinMaxPlayer<TState> : IPlayer<TState>
        where TState : IState<TState>
    {
        IMinMax<TState> minMax;
        double errorRate = 0.1;
        Random random = new Random(System.DateTime.Now.Millisecond);

        public FaultyMinMaxPlayer(IMinMax<TState> minMax)
        {
            this.minMax = minMax;
        }

        public TState MakeMove(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Game is over");
            
            if (errorRate < random.NextDouble())
            {
                var moves = state.GetNextStates().ToList();
                return moves.ElementAt(random.Next(0, moves.Count - 1));
            }

            var result = minMax.MinMax(state);

            return result;
        }
    }
}
