using MinMaxAlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public interface IPlayer<TState>
        where TState : IState<TState>
    {
        TState MakeMove(TState state);
    }
}
