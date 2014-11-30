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

        private Dictionary<TState, Measure<TMeasure>> memo = new Dictionary<TState, Measure<TMeasure>>(); 

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
                          where memo[s] == v
                          select s).First();

            //memo.Clear();

            return result;
        }

        public Measure<TMeasure> AlphaBeta(TState state, Measure<TMeasure> α, Measure<TMeasure> β, bool maximizingPlayer)
        {
            if (state.IsTerminal)
                return gauge.Measure(state);

            if (maximizingPlayer)
            {
                foreach (TState nextState in state.GetNextStates())
                {
                    if (memo.ContainsKey(nextState))
                    {
                        α = memo[nextState];
                    }
                    else
                    {
                        α = Max(α, AlphaBeta(nextState, α, β, false));
                        memo[nextState] = α;
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
                    if (memo.ContainsKey(nextState))
                    {
                        β = memo[nextState];
                    }
                    else
                    {
                        β = Min(β, AlphaBeta(nextState, α, β, true));
                        memo[nextState] = β;
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
