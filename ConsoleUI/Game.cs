using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;

namespace ConsoleUI
{
    public class Game<TState>
        where TState : IState<TState>
    {
        readonly IPlayer<TState> player1;
        readonly IPlayer<TState> player2;

        public Game(IPlayer<TState> player1, IPlayer<TState> player2)
        {
            this.player1 = player1;
            this.player2 = player2;
        }

        public IEnumerable<TState> Play(TState initialState)
        {
            TState state = initialState;
            IPlayer<TState> activePlayer = player1;
            while (state.IsTerminal == false)
            {
                //TODO: add loop until valid
                //TODO: add message, state number, etc
                TState nextState = activePlayer.MakeMove(state);

                if (false == state.GetNextStates().Any(s => s.Equals(nextState)))
                    throw new InvalidOperationException("Illegal move!");

                yield return state;
                state = nextState;
                activePlayer = activePlayer == player1 ? player2 : player1;
            }

            yield return state;
        }
    }
}
