using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;

namespace Checkers.FastModel
{
    public class BinaryStateGauge : Gauge<FastState, int>
    {
        private ColorEnum colorEnum;

        public BinaryStateGauge(ColorEnum colorEnum)
        {
            this.colorEnum = colorEnum;
        }
        protected override int ComputeValue(FastState state)
        {
            if (this.colorEnum == ColorEnum.White)
            {
                if (state.WhiteKings < state.BlackKings)
                    return 0;

                if (state.WhiteKings > state.BlackKings)
                    return 1;

                return 0;
            }
            else
            {
                if (state.WhiteKings < state.BlackKings)
                    return 1;

                if (state.WhiteKings > state.BlackKings)
                    return 0;

                return 0;
            }

            //if (this.colorEnum == ColorEnum.White)
            //{
            //    if (state.WhiteKings <= state.BlackKings)
            //        return 0;

            //    if (state.WhiteKings - state.BlackKings > 5)
            //        return 5;

            //    return state.WhiteKings - state.BlackKings;
            //}

            //if (state.BlackKings <= state.WhiteKings)
            //    return 0;

            //if (state.BlackKings - state.WhiteKings > 5)
            //    return 5;

            //return state.BlackKings - state.WhiteKings;
        }
    }
}
