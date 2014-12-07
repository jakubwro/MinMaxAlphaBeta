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
            while (true)
            {
                Console.WriteLine("1. Gra z komputerem");
                Console.WriteLine("2. Testy automatyczne");
                Console.WriteLine("3. Zakończ");

                var ki = Console.ReadKey();
                Console.WriteLine();
                if (ki.KeyChar == '1')
                {
                    PlayWithComputer();
                }
                else if (ki.KeyChar == '2')
                {
                    SelectSize();
                }
                else if (ki.KeyChar == '3')
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Niepoprawy wybór!");
                }
            }
        }

        private static void SelectSize()
        {
            while (true)
            {
                Console.WriteLine("1. Rozmiar planszy 5x5");
                Console.WriteLine("2. Rozmiar planszy 6x6");
                Console.WriteLine("3. Powrót");

                var ki = Console.ReadKey();
                Console.WriteLine();
                if (ki.KeyChar == '1')
                {
                    SelectHeuristic(Board.Board5x5);
                }
                else if (ki.KeyChar == '2')
                {
                    SelectHeuristic(Board.Board6x6);
                }
                else if (ki.KeyChar == '3')
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Niepoprawy wybór!");
                }
            }
        }

        private static void SelectHeuristic(Board board)
        {
            while (true)
            {
                Console.WriteLine("1. Heurystyka oceny {-1, 0 ,1}");
                Console.WriteLine("2. Liniowa heurystyka oceny (zdobytePunkty - punktyPrzeciwnika)");
                Console.WriteLine("3. Powrót");

                var ki = Console.ReadKey();
                Console.WriteLine();
                if (ki.KeyChar == '1')
                {
                    var gaugeWhite = new GameState01Gauge(ColorEnum.White);
                    var gaugeBlack = new GameState01Gauge(ColorEnum.Black);

                    SelectPlayers(board, gaugeWhite, gaugeBlack);
                }
                else if (ki.KeyChar == '2')
                {
                    var gaugeWhite = new GameStateLinearGauge(ColorEnum.White);
                    var gaugeBlack = new GameStateLinearGauge(ColorEnum.Black);


                    SelectPlayers(board, gaugeWhite, gaugeBlack);
                }
                else if (ki.KeyChar == '3')
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Niepoprawy wybór!");
                }
            }
        }

        private static void SelectPlayers(Board board, Gauge<GameState, int> gaugeWhite, Gauge<GameState, int> gaugeBlack)
        {
            while (true)
            {
                Console.WriteLine("1. Minimax alpha beta vs Losowy ruch");
                Console.WriteLine("2. Minimax alpha beta vs Minimax alpha beta z częstotliwością błędów 1/10)");
                Console.WriteLine("3. Minimax alpha beta vs Minimax alpha beta");
                Console.WriteLine("4. Losowy ruch vs Minimax alpha beta");
                Console.WriteLine("5. Minimax alpha beta z częstotliwością błędów 1/10	vs Minimax alpha beta");
                Console.WriteLine("6. Powrót");

                var ki = Console.ReadKey();
                Console.WriteLine();
                if (ki.KeyChar == '1')
                {
                    IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                        new MinMaxPlayer<GameState>(
                            new MinMaxAlphaBeta<GameState, int>(gaugeWhite));

                    IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

                    GameState gameState = new GameState(GameSettings.Default, board);
                    Play(gameState, minMaxAlphaBetaWhitePlayer, randomPlayer);
                }
                else if (ki.KeyChar == '2')
                {
                    IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(gaugeWhite));

                    IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                        new FaultyMinMaxPlayer<GameState>(
                            new MinMaxAlphaBeta<GameState, int>(gaugeBlack));


                    GameState gameState = new GameState(GameSettings.Default, board);
                    Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
                }
                else if (ki.KeyChar == '3')
                {
                    IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                    new MinMaxPlayer<GameState>(
                        new MinMaxAlphaBeta<GameState, int>(gaugeWhite));

                    IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                        new MinMaxPlayer<GameState>(
                            new MinMaxAlphaBeta<GameState, int>(gaugeBlack));

                    GameState gameState = new GameState(GameSettings.Default, board);
                    Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
                }
                else if (ki.KeyChar == '4')
                {
                    IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                        new MinMaxPlayer<GameState>(
                            new MinMaxAlphaBeta<GameState, int>(gaugeBlack));

                    IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

                    GameState gameState = new GameState(GameSettings.Default, board);
                    Play(gameState, randomPlayer, minMaxAlphaBetaBlackPlayer);
                }
                else if (ki.KeyChar == '5')
                {
                    IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                      new FaultyMinMaxPlayer<GameState>(
                          new MinMaxAlphaBeta<GameState, int>(gaugeWhite));

                    IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                        new MinMaxPlayer<GameState>(new MinMaxAlphaBeta<GameState, int>(gaugeBlack));

                    GameState gameState = new GameState(GameSettings.Default, board);
                    Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
                }
                else if (ki.KeyChar == '6')
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Niepoprawy wybór!");
                }

            }
        }

        static void Play(GameState initialState, IPlayer<GameState> whitePlayer, IPlayer<GameState> blackPlayer)
        {
            Statistics.Instance = new Statistics();
            ConsolePresenter presenter = new ConsolePresenter();

            for (int i = 0; i < 100; ++i)
            {
                var state = initialState;

                Console.WriteLine(presenter.Render(state));

                IPlayer<GameState> activePlayer = whitePlayer;
                while (state.IsTerminal == false)
                {
                    GameState nextState = activePlayer.MakeMove(state);

                    if (false == state.GetNextStates().Any(s => s.Equals(nextState)))
                        throw new InvalidOperationException("Illegal move!");

                    Statistics.Instance.totalMoves++;

                    state = nextState;
                    Console.WriteLine(presenter.Render(state));
                    activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
                }

                int player1Pts = state.WhiteScore;
                int player2Pts = state.BlackScore;

                Statistics.Instance.totalPlayer1Pts += player1Pts;
                Statistics.Instance.totalPlayer2Pts += player2Pts;

                if (player1Pts == player2Pts)
                {
                    Statistics.Instance.draws++;
                }
                else if (player1Pts > player2Pts)
                {
                    Statistics.Instance.player1Wins++;
                }
                else
                {
                    Statistics.Instance.player2Wins++;
                }
            }

            Console.WriteLine(Statistics.Instance.ToString());
        }

        private static void PlayWithComputer()
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
            Statistics.Instance.measures = new List<int>();
            GameState gameState = new GameState(settings, Board.Board5x5);
            try
            {
                SequencePlayer<GameState> sp = new SequencePlayer<GameState>();
                Game<GameState> game = new Game<GameState>(consolePlayer, minMaxAlphaBetaBlackPlayer, presenter);

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

            Console.ReadKey();
            Console.WriteLine();

        }
    }
}
