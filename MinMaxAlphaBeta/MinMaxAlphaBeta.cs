using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public class MinMaxAlphaBeta<TState, TMeasure> : IMinMax<TState>
        where TState : IState<TState>
        where TMeasure : IComparable<TMeasure>, IComparable, IEquatable<TMeasure>
    {
        Gauge<TState, TMeasure> gauge;

        long counter = 0;

        public MinMaxAlphaBeta(Gauge<TState, TMeasure> gauge)
        {
            this.gauge = gauge;
        }

        public TState MinMax(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Cannot find next state for terminal state");

            TState result = default(TState);
            var α = Measure<TMeasure>.MinusInfinity;
            var β = Measure<TMeasure>.PlusInfinity;

            foreach(TState s in state.GetNextStates())
            {
                counter++;

                var min = MinEvaluation(s, α, β);

                if (min > α)
                {
                    α = min;
                    result = s;
                }
            }

            return result;
        }

        internal Measure<TMeasure> MaxEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β)
        {          
            if (state.IsTerminal)
                return gauge.Measure(state);
            
            Measure<TMeasure> v = α;

            foreach (TState nextState in state.GetNextStates())
            {
                counter++;

                v = Max(v, MinEvaluation(nextState, α, β));
             
                if (v >= β)
                    return v;

                α = Max(α, v);
            }

            return v;
        }

        internal Measure<TMeasure> MinEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β)
        {
            if (state.IsTerminal)
                return gauge.Measure(state);

            Measure<TMeasure> v = β;

            foreach (TState nextState in state.GetNextStates())
            {
                counter++;

                v = Min(v, MaxEvaluation(nextState, α, β));

                if (v <= α)
                    return v;

                β = Min(β, v);
            }

            return v;
        }

        static Measure<TMeasure> Max(Measure<TMeasure> first, Measure<TMeasure> second)
        {
            return first >= second ? first : second;
        }

        static Measure< TMeasure> Min(Measure<TMeasure> first, Measure<TMeasure> second)
        {
            return first <= second ? first : second;
        }

    }
}
