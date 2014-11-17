//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MinMaxAlphaBeta
//{
//    public class MinMaxAlphaBetaMemo<TState, TMeasure> : MinMaxAlphaBeta<TState, TMeasure>
//        where TState : IState<TState>, IStructuralEquatable, IStructuralComparable
//        where TMeasure : IComparable<TMeasure>, IComparable, IEquatable<TMeasure>
//    {
//        Dictionary<TState, Measure<TMeasure>> memo = new Dictionary<TState, Measure<TMeasure>>();

//        public MinMaxAlphaBetaMemo(IGauge<TState, TMeasure> gauge) : base(gauge) { }

//        override internal Measure<TMeasure> MaxEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β)
//        {
//            Measure<TMeasure> memoized;
//            if (memo.TryGetValue(state, out memoized))
//            {
//                return memoized;
//            }

//            var measure = base.MaxEvaluation(state, α, β);
            
//            memo.Add(state, measure);
//            return measure;
//        }

//        override internal Measure<TMeasure> MinEvaluation(TState state, Measure<TMeasure> α, Measure<TMeasure> β)
//        {
//            Measure<TMeasure> memoized;
//            if (memo.TryGetValue(state, out memoized))
//            {
//                return memoized;
//            }

//            var measure = MinEvaluation(state, α, β);

//            memo.Add(state, measure);
//            return measure;
//        }
//    }
//}
