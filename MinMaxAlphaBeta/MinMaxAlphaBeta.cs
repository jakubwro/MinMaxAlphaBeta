using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Dictionary<Tuple<TState, Measure<TMeasure>, Measure<TMeasure>>, Measure<TMeasure>> memo = new Dictionary<Tuple<TState, Measure<TMeasure>, Measure<TMeasure>>, Measure<TMeasure>>();
        private List<Tuple<TState, Measure<TMeasure>>> nextStatesMeasures = new List<Tuple<TState, Measure<TMeasure>>>();

        public MinMaxAlphaBeta(Gauge<TState, TMeasure> gauge)
        {
            this.gauge = gauge;
        }

        public TState MinMax(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Cannot find next state for terminal state");

            var v = MaxEvaluation(state, Measure<TMeasure>.MinusInfinity, Measure<TMeasure>.PlusInfinity, 0);

            var result = nextStatesMeasures.Where(t => t.Item2 == v).First().Item1;
            nextStatesMeasures.Clear();

            return result;
        }

        internal Measure<TMeasure> MaxEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β, int depth)
        {
            if (state.IsTerminal)
                return gauge.Measure(state);

            Measure<TMeasure> v = α;

            foreach (TState nextState in state.GetNextStates())
            {
                var tuple = Tuple.Create(nextState, α, β);
                Measure<TMeasure> measure;
                if (!memo.TryGetValue(tuple, out measure))
                {
                    Statistics.Instance.memoMiss++;
                    measure = MinEvaluation(nextState, α, β, depth + 1);
                    memo[tuple] = measure;
                }
                else
                {
                    Statistics.Instance.memoHits++;
                }

                if (depth == 0)
                    nextStatesMeasures.Add(Tuple.Create(nextState, measure));

                v = Max(v, measure);
                
                if (v >= β)
                {
                    return v;
                }
                α = Max(α, v);
            }

            return v;
        }

        internal Measure<TMeasure> MinEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β, int depth)
        {
            if (state.IsTerminal)
                return gauge.Measure(state);

            Measure<TMeasure> v = β;

            foreach (TState nextState in state.GetNextStates())
            {
                var tuple = Tuple.Create(nextState, α, β);
                Measure<TMeasure> measure;
                if (!memo.TryGetValue(tuple, out measure))
                {
                    Statistics.Instance.memoMiss++;
                    measure = MaxEvaluation(nextState, α, β, depth + 1);
                    memo[tuple] = measure;
                }
                else
                {
                    Statistics.Instance.memoHits++;
                }

                v = Min(v, measure);

                if (v <= α)
                {
                    return v;
                }

                β = Min(β, v);
            }

            return v;
        }

        static Measure<TMeasure> Max(Measure<TMeasure> first, Measure<TMeasure> second)
        {
            return first >= second ? first : second;
        }

        static Measure<TMeasure> Min(Measure<TMeasure> first, Measure<TMeasure> second)
        {
            return first <= second ? first : second;
        }

    }
}
