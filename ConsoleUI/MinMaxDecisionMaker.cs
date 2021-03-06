﻿using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class MinMaxDecisionMaker<TState> : IDecisionMaker<TState>
        where TState : IState<TState>
    {
        IMinMax<TState> minMax;

        public MinMaxDecisionMaker(IMinMax<TState> minMax)
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
