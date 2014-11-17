using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public abstract class Gauge<TState, TMeasure>
        where TState : IState<TState>
        where TMeasure : IComparable<TMeasure>, IComparable, IEquatable<TMeasure>
    {
        protected abstract TMeasure ComputeValue(TState state);
        
        public Measure<TMeasure> Measure(TState state)
        {
            if (!state.IsTerminal)
                throw new InvalidOperationException("Cannot simply measure non terminal state");

            return Measure<TMeasure>.Create(ComputeValue(state));
        }
    }
}
