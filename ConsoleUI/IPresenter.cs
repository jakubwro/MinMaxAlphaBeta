using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinMaxAlphaBeta;

namespace ConsoleUI
{
    public interface IPresenter<TState, TResult>
        where TState : IState<TState>
    {
        TResult Render(TState state);

        bool TryAccept(TState state, TResult input, out TState next);
    }
}
