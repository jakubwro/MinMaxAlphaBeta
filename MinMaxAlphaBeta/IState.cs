using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public interface IState<TState>
    {
        /// <summary>
        /// Tells if state if terminal.
        /// </summary>
        bool IsTerminal { get; }

        /// <summary>
        /// Gets moves possible from current state
        /// </summary>
        /// <returns>Sequence of moves possible from current state</returns>
        IEnumerable<TState> GetNextStates();
    }
}
