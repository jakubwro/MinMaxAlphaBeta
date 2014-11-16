using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public class Memoizer<T1, T2, T3, TResult>
    {
        public Func<T1, T2, T3, TResult> Memoize(Func<T1, T2, T3, TResult> func)
        {
            Dictionary<Tuple<T1, T2, T3>, TResult> memo = new Dictionary<Tuple<T1, T2, T3>, TResult>();
            
            return (t1, t2, t3) =>
            {
                TResult result;
                var tuple = new Tuple<T1,T2,T3>(t1, t2, t3);
                if (memo.TryGetValue(tuple, out result))
                    return result;
                
                result = func(t1, t2, t3);
                memo.Add(tuple, result);
                return result;
            };
        }
    }
}
