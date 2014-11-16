using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    class ParallelMinMaxAlpha<TState, TMeasure>
        where TState : IState<TState>
        where TMeasure : IComparable<TMeasure>, IComparable, IEquatable<TMeasure>
    {
    }
}
