using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using ConsoleUI;
using MinMaxAlphaBeta;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void AlphaBetaVsRandom_Size5_01()
        {           
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, randomPlayer);
        }

        [TestMethod]
        public void RandomVsAlphaBeta_Size5_01()
        {
            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, randomPlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsAlphaBeta_Size5_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsFaultyAlphaBeta_Size5_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void FaultyAlphaBetaVsAlphaBeta_Size5_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsRandom_Size5_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, randomPlayer);
        }

        [TestMethod]
        public void RandomVsAlphaBeta_Size5_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, randomPlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsAlphaBeta_Size5_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsFaultyAlphaBeta_Size5_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void FaultyAlphaBetaVsAlphaBeta_Size5_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board5x5);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsRandom_Size6_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, randomPlayer);
        }

        [TestMethod]
        public void RandomVsAlphaBeta_Size6_01()
        {
            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, randomPlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsAlphaBeta_Size6_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsFaultyAlphaBeta_Size6_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void FaultyAlphaBetaVsAlphaBeta_Size6_01()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsRandom_Size6_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, randomPlayer);
        }

        [TestMethod]
        public void RandomVsAlphaBeta_Size6_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameState01Gauge(ColorEnum.Black)));

            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();

            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, randomPlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsAlphaBeta_Size6_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void AlphaBetaVsFaultyAlphaBeta_Size6_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new MinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        [TestMethod]
        public void FaultyAlphaBetaVsAlphaBeta_Size6_Linear()
        {
            IPlayer<GameState> minMaxAlphaBetaWhitePlayer =
                new FaultyMinMaxPlayer<GameState>(
                    new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.White)));

            IPlayer<GameState> minMaxAlphaBetaBlackPlayer =
                new MinMaxPlayer<GameState>(new MinMaxAlphaBeta<GameState, int>(
                        new GameStateLinearGauge(ColorEnum.Black)));


            GameState gameState = new GameState(GameSettings.Default, Board.Board6x6);

            Play(gameState, minMaxAlphaBetaWhitePlayer, minMaxAlphaBetaBlackPlayer);
        }

        void Play(GameState initialState, IPlayer<GameState> whitePlayer, IPlayer<GameState> blackPlayer)
        {
            Statistics.Instance = new Statistics();

            for (int i = 0; i < 100; ++i)
            {
                var state = initialState;

                IPlayer<GameState> activePlayer = whitePlayer;
                while (state.IsTerminal == false)
                {
                    GameState nextState = activePlayer.MakeMove(state);

                    if (false == state.GetNextStates().Any(s => s.Equals(nextState)))
                        throw new InvalidOperationException("Illegal move!");

                    Statistics.Instance.totalMoves++;

                    state = nextState;
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
    }
}
