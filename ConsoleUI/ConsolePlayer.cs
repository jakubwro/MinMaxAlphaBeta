using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;

namespace ConsoleUI
{
    class ConsolePlayer<TState>: IPlayer<TState>
        where TState : IState<TState>
    {
        IPresenter<TState, string> presenter;

        public ConsolePlayer(IPresenter<TState, string> presenter)
        {
            this.presenter = presenter;
        }

        public TState MakeMove(TState state)
        {
            if (state.IsTerminal)
                throw new InvalidOperationException("Game is over");

            //Console.WriteLine(presenter.Render(state));

            TState move;
            while (false == presenter.TryAccept(state, Console.ReadLine(), out move))
            {
                Console.WriteLine("Invalid move, try again");
            }

            return move;


        }
    }
}
