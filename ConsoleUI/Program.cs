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
            Statistics.Instance = new Statistics();

            var whitePlayerGauge = new GameState01Gauge(ColorEnum.White);
            var blackPlayerGauge = new GameState01Gauge(ColorEnum.Black);

            MinMaxAlphaBeta<GameState, int> minMaxAlphaBetaWhite = new MinMaxAlphaBeta<GameState, int>(whitePlayerGauge);
            MinMaxAlphaBeta<GameState, int> minMaxAlphaBetaBlack = new MinMaxAlphaBeta<GameState, int>(blackPlayerGauge);
            
            var presenter = new ConsolePresenter();

            IPlayer<GameState> minMaxAlphaBetaWhitePlayer = new MinMaxPlayer<GameState>(minMaxAlphaBetaWhite);
            IPlayer<GameState> minMaxAlphaBetaBlackPlayer = new MinMaxPlayer<GameState>(minMaxAlphaBetaBlack);
            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();
            IPlayer<GameState> consolePlayer = new ConsolePlayer<GameState>(presenter);


            var settings = GameSettings.Default;
            while (true)
            {
                Statistics.Instance.measures = new List<int>();
                GameState gameState = new GameState(settings, Board.Board5x5);
                try
                {
                    SequencePlayer<GameState> sp = new SequencePlayer<GameState>();
                    Game<GameState> game = new Game<GameState>(minMaxAlphaBetaWhitePlayer, randomPlayer, presenter);

                    IEnumerable<GameState> gameplay = game.Play(gameState).ToList();

                    var finalState = gameplay.Last();
                    Debug.Assert(true == finalState.IsTerminal);

                    int player1Pts = finalState.WhiteScore;
                    int player2Pts = finalState.BlackScore;

                    Console.WriteLine(presenter.Render(finalState));

                    if (player1Pts == player2Pts)
                    {
                        Console.WriteLine("Draw.");
                        Statistics.Instance.draws++;
                    }
                    else if (player1Pts > player2Pts)
                    {
                        Statistics.Instance.player1Wins++;
                        Console.WriteLine("The winer is player 1");
                    }
                    else
                    {
                        Statistics.Instance.player2Wins++;
                        Console.WriteLine("The winer is player 2");
                    }

                    for (int i = 0; i < Statistics.Instance.measures.Count() - 1; ++i)
                        Debug.Assert(Statistics.Instance.measures[i] <= Statistics.Instance.measures[i + 1]);

                }
                catch (Exception exc)
                {
                    Console.Error.WriteLine(exc.Message);
                }
            }

            Console.ReadKey();

        }
    }
}
