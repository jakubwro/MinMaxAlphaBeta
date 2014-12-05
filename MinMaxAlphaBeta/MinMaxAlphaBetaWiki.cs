﻿using System;
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

        private Dictionary<Tuple<TState, Measure<TMeasure>, Measure<TMeasure>>, Measure<TMeasure>> memo = new Dictionary<Tuple<TState, Measure<TMeasure>, Measure<TMeasure>>, Measure<TMeasure>>();

        private Dictionary<TState, Measure<TMeasure>> nextStatesMeasures = new Dictionary<TState, Measure<TMeasure>>();

        //not usable
        private MinMaxAlphaBetaWiki(Gauge<TState, TMeasure> gauge)
        {
            this.gauge = gauge;
        }

        public TState MinMax(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Cannot find next state for terminal state");

            var v = AlphaBeta(state, Measure<TMeasure>.MinusInfinity, Measure<TMeasure>.PlusInfinity, true, 0);
            var result = nextStatesMeasures.Where(kv => kv.Value == v).First().Key;
            
            nextStatesMeasures.Clear();

            Statistics.Instance.measures.Add(v.ToInt());


            return result;
        }

        public Measure<TMeasure> AlphaBeta(TState state, Measure<TMeasure> α, Measure<TMeasure> β, bool maximizingPlayer, int depth)
        {
            if (state.IsTerminal)
                return gauge.Measure(state);

            if (maximizingPlayer)
            {
                foreach (TState nextState in state.GetNextStates())
                {
                    var tuple = Tuple.Create(nextState, α, β);
                    Measure<TMeasure> measure;
                    if (!memo.TryGetValue(tuple, out measure))
                    {
                        Statistics.Instance.memoMiss++;
                        measure = AlphaBeta(nextState, α, β, false, depth + 1);
                        memo[tuple] = measure;
                    }
                    else
                    {
                        Statistics.Instance.memoHits++;
                    }

                    if (depth == 0)
                        nextStatesMeasures[nextState] = measure;


                    α = Max(α, measure);

                    if (β <= α)
                        break;
                }

                return α;
            }
            else
            {
                foreach (TState nextState in state.GetNextStates())
                {
                    var tuple = Tuple.Create(nextState, α, β);
                    Measure<TMeasure> measure;
                    if (!memo.TryGetValue(tuple, out measure))
                    {
                        Statistics.Instance.memoMiss++;
                        measure = AlphaBeta(nextState, α, β, true, depth + 1);
                        memo[tuple] = measure;
                    }
                    else
                    {
                        Statistics.Instance.memoHits++;
                    }

                    β = Min(β, measure);

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
