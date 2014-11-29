using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;
using Checkers;
using System.Collections;
using System.Diagnostics;
using Checkers.FastModel;

namespace ConsoleUI
{
    using Layout = System.Collections.Immutable.IImmutableDictionary<Square, Checker>;

    class Program
    {
        static void Main(string[] args)
        {
            //FastState fs = FastState.InitialState;
            //var fspresenter = new FastStatePresenter();
            //Console.WriteLine(fspresenter.Render(fs));

            //var movs = fs.GetNextStates().ToList();

            //while (movs.Count > 0)
            //{
            //    fs = movs.Random();
            //    Console.WriteLine(fspresenter.Render(fs));
            //    movs = fs.GetNextStates().ToList();

            //}
            //Console.WriteLine(fspresenter.Render(fs));


            //var fastState = new FastState(2147516394 & 0x0fffffff, 3728179200 & 0xfffffff0, 0, 0, true);

            //Console.WriteLine(fspresenter.Render(fastState));
            //var moves = fastState.GetNextStates().ToList();

            //foreach(var m in moves)
            //    Console.WriteLine(fspresenter.Render(m));


            var gauge = new BinaryStateGauge(ColorEnum.White);
            MinMaxAlphaBeta<FastState, int> minMaxAlphaBeta = new MinMaxAlphaBeta<FastState, int>(gauge);
            var presenter = new ConsolePresenter();

            IPlayer<GameState> minMaxAlphaBetaPlayer = new FastMinMaxPlayer(minMaxAlphaBeta);
            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();
            IPlayer<GameState> consolePlayer = new ConsolePlayer<GameState>(presenter);

            var settings = new GameSettings(true, true, false);
            GameState gameState = new GameState(settings, Board.Board8x8);
            try
            {
                Game<GameState> game = new Game<GameState>(minMaxAlphaBetaPlayer, consolePlayer, presenter);

                IEnumerable<GameState> gameplay = game.Play(gameState).ToList();

                var finalState = gameplay.Last();
                Debug.Assert(true == finalState.IsTerminal);

                int player1Pts = finalState.WhiteScore;
                int player2Pts = finalState.BlackScore;

                Console.WriteLine(presenter.Render(finalState));

                if (player1Pts == player2Pts)
                    Console.WriteLine("Draw.");
                else
                    Console.WriteLine("The winer is player {0}", player1Pts > player2Pts ? "1" : "2");

            }
            catch(Exception exc)
            {

            }

            Console.ReadKey();

        }
    }
}
