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
            var presenter = new ConsolePresenter();
            var fspresenter = new FastStatePresenter();
            Board board = Board.Board8x8;
            Layout layout = board.InitialLayout;
            layout = layout.Add(board.Squares.Skip(12).First(), Checker.BlackFolk);
            layout = layout.Remove(board.Squares.Skip(25).First());
            layout = layout.Remove(board.Squares.Skip(27).First());

            var g = new GameState(GameSettings.Default, board, layout, ColorEnum.White, 0, 0);
            Console.WriteLine(presenter.Render(g));
            var fastState = g.ToFastState();
            Console.WriteLine(fspresenter.Render(fastState));

            var gg = fastState.ToGameState();
            Console.WriteLine(presenter.Render(gg));
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


            //while (true)
            //{
            //    var settings = new GameSettings();
            //    ConsolePresenter presenter = new ConsolePresenter();
            //    GameState game = new GameState(settings, Board.Board8x8);

            //    Console.WriteLine(presenter.Render(game));

            //    var moves = game.AvailableMoves.ToList();
            //    while (moves.Count() > 0)
            //    {
            //        game = game.MakeMove(moves.Random());
            //        moves = game.AvailableMoves.ToList();
            //        Console.WriteLine(presenter.Render(game));
            //    }

            //    Console.WriteLine(presenter.Render(game));
            //}

            var gauge = new GameStateGauge(ColorEnum.White);
            MinMaxAlphaBeta<GameState, int> minMaxAlphaBeta = new MinMaxAlphaBeta<GameState, int>(gauge);
            //var presenter = new ConsolePresenter();

            IPlayer<GameState> minMaxAlphaBetaPlayer = new MinMaxPlayer<GameState>(minMaxAlphaBeta);
            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();
            IPlayer<GameState> consolePlayer = new ConsolePlayer<GameState>(presenter);

            var settings = new GameSettings(true, true, false);
            GameState gameState = new GameState(settings, Board.Board6x6);
            try
            {
                Game<GameState> game = new Game<GameState>(minMaxAlphaBetaPlayer, randomPlayer, presenter);

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
