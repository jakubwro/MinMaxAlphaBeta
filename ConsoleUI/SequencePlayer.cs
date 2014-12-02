using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class SequencePlayer<TState> : IPlayer<TState>
        where TState : IState<TState>
    {
        List<int> hashes = new List<int>()
        {
 471765186,
 713696836,
 115074854,
 35020246,
 -1846612794,
 559819156,
 287517896,
 1907011142,
        };

        int stepNo = 0;

        public TState MakeMove(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Game is over");

            var next = state.GetNextStates().First(s => s.GetHashCode() == hashes[stepNo]);
            stepNo++;
            return next;
        }
    }
}
