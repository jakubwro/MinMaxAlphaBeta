using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public class MinMaxAlphaBetaWiki<TState, TMeasure> : IMinMax<TState>
        where TState : IState<TState>
        where TMeasure : IComparable<TMeasure>, IComparable, IEquatable<TMeasure>
    {
        Gauge<TState, TMeasure> gauge;

        private Dictionary<TState, Measure<TMeasure>> memoMax = new Dictionary<TState, Measure<TMeasure>>();
        private Dictionary<TState, Measure<TMeasure>> memoMin = new Dictionary<TState, Measure<TMeasure>>(); 

        public MinMaxAlphaBetaWiki(Gauge<TState, TMeasure> gauge)
        {
            this.gauge = gauge;
        }

        public TState MinMax(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Cannot find next state for terminal state");

            var v = AlphaBeta(state, Measure<TMeasure>.MinusInfinity, Measure<TMeasure>.PlusInfinity, true);

            var result = (from s in state.GetNextStates()
                          where memoMax[s] == v
                          select s).First();


            //Statistics.hashes.Add(result.GetHashCode());
            Statistics.measures.Add(v.ToInt());

            if (Statistics.clearMemo == true)
            {
                memoMax.Clear();
                memoMin.Clear();
            }
            return result;
        }

        public Measure<TMeasure> AlphaBeta(TState state, Measure<TMeasure> α, Measure<TMeasure> β, bool maximizingPlayer)
        {
            //Console.WriteLine(state.GetHashCode());
            if (state.IsTerminal)
                return gauge.Measure(state);

            if (maximizingPlayer)
            {
                foreach (TState nextState in state.GetNextStates())
                {
                    Measure<TMeasure> memoized;
                    if (memoMax.TryGetValue(nextState, out memoized))
                    {
                        α = Max(α, memoized);
                    }
                    else
                    {
                        var measure = AlphaBeta(nextState, α, β, false);
                        memoMax[nextState] = measure;
                        α = Max(α, measure);
                    }

                    if (β <= α)
                        break;
                }

                return α;
            }
            else
            {
                foreach (TState nextState in state.GetNextStates())
                {
                    Measure<TMeasure> memoized;
                    if (memoMin.TryGetValue(nextState, out memoized))
                    {
                        β = Min(β, memoized);
                    }
                    else
                    {
                        var measure = AlphaBeta(nextState, α, β, true);
                        memoMin[nextState] = measure;
                        β = Min(β, measure);
                    }

                    if (β <= α)
                        break;
                }
            }

            return β;
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
