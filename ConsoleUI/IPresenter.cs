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

        bool TryAccept(TResult input, out TState state);
    }
}
