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
        IGauge<TState, TMeasure> gauge;

        public MinMaxAlphaBeta(IGauge<TState, TMeasure> gauge)
        {
            this.gauge = gauge;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns>Next state</returns>
        virtual public TState MinMax(TState state)
        {
            if (state.IsTerminal)
                return state; //TODO: throw?

            Measure<TMeasure> v = MaxEvaluation(state, Measure<TMeasure>.MinusInfinity, Measure<TMeasure>.PlusInfinity);
            
            //TODO: randomize selection of equal nodes
            return state.GetNextStates().First(s => gauge.Measure(s) == v);
        }

        //TODO: not necessary to make this virtual, memoize by private inheritence
        virtual internal Measure<TMeasure> MaxEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β)
        {          
            if (state.IsTerminal)
                return gauge.Measure(state);
            
            Measure<TMeasure> v = α;

            foreach (TState nextState in state.GetNextStates())
            {
                v = Max(v, MinEvaluation(nextState, α, β));
             
                if (v >= β)
                    return v;

                α = Min(α, v);
            }

            return v;
        }

        virtual internal Measure<TMeasure> MinEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β)
        {
            if (state.IsTerminal)
                return gauge.Measure(state);

            Measure<TMeasure> v = β;

            foreach (TState nextState in state.GetNextStates())
            {
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
