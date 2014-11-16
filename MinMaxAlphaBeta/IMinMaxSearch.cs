using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public interface IMinMax<TState>
        where TState : IState<TState>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">Current state of a game</param>
        /// <returns>Next state selected by an algorithm</returns>
        TState MinMax(TState state);
    }
}
