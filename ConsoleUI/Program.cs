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


            ///
            ///
            ///

            var gauge = new GameStateGauge(ColorEnum.White);
            var gauge2 = new GameStateGauge(ColorEnum.Black);

            MinMaxAlphaBeta<GameState, int> minMaxAlphaBetaX = new MinMaxAlphaBeta<GameState, int>(gauge);

            MinMaxAlphaBetaWiki<GameState, int> minMaxAlphaBeta = new MinMaxAlphaBetaWiki<GameState, int>(gauge);
            MinMaxAlphaBetaWiki<GameState, int> minMaxAlphaBeta2 = new MinMaxAlphaBetaWiki<GameState, int>(gauge2);
            var presenter = new ConsolePresenter();


            IPlayer<GameState> minMaxAlphaBetaPlayerX = new MinMaxPlayer<GameState>(minMaxAlphaBetaX);

            IPlayer<GameState> minMaxAlphaBetaPlayer = new MinMaxPlayer<GameState>(minMaxAlphaBeta);
            IPlayer<GameState> minMaxAlphaBetaPlayer2 = new MinMaxPlayer<GameState>(minMaxAlphaBeta2);
            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();
            IPlayer<GameState> consolePlayer = new ConsolePlayer<GameState>(presenter);


            var settings = new GameSettings(true, true, false);
            while (true)
            {
                Statistics.hashes = new List<int>();
                Statistics.measures = new List<int>();
                GameState gameState = new GameState(settings, Board.Board6x6);
                try
                {
                    SequencePlayer<GameState> sp = new SequencePlayer<GameState>();
                    Game<GameState> game = new Game<GameState>(randomPlayer, minMaxAlphaBetaPlayer2, presenter);

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

                    for (int i = 0; i < Statistics.measures.Count() - 1; ++i)
                        Debug.Assert(Statistics.measures[i] <= Statistics.measures[i+1]);

                }
                catch (Exception exc)
                {

                }
            }



            ///
            ///
            ///


            //var gauge = new BinaryStateGauge(ColorEnum.Black);
            //MinMaxAlphaBeta<FastState, int> minMaxAlphaBeta = new MinMaxAlphaBeta<FastState, int>(gauge);
            //var presenter = new ConsolePresenter();

            //IPlayer<GameState> minMaxAlphaBetaPlayer = new FastMinMaxPlayer(minMaxAlphaBeta);
            //IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();
            //IPlayer<GameState> consolePlayer = new ConsolePlayer<GameState>(presenter);

            //var settings = new GameSettings(true, true, false);
            //GameState gameState = new GameState(settings, Board.Board8x8);
            //try
            //{
            //    Game<GameState> game = new Game<GameState>(consolePlayer, minMaxAlphaBetaPlayer, presenter);

            //    IEnumerable<GameState> gameplay = game.Play(gameState).ToList();

            //    var finalState = gameplay.Last();
            //    Debug.Assert(true == finalState.IsTerminal);

            //    int player1Pts = finalState.WhiteScore;
            //    int player2Pts = finalState.BlackScore;

            //    Console.WriteLine(presenter.Render(finalState));

            //    if (player1Pts == player2Pts)
            //        Console.WriteLine("Draw.");
            //    else
            //        Console.WriteLine("The winer is player {0}", player1Pts > player2Pts ? "1" : "2");

            //}
            //catch(Exception exc)
            //{

            //}

            Console.ReadKey();

        }
    }
}
