using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxAlphaBeta;
using Checkers;
using System.Collections;
using System.Diagnostics;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
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


            MinMaxAlphaBeta<GameState, int> minMaxAlphaBeta = new MinMaxAlphaBeta<GameState, int>(new GameStateGauge());
            var presenter = new ConsolePresenter();

            IPlayer<GameState> minMaxAlphaBetaPlayer = new MinMaxPlayer<GameState>(minMaxAlphaBeta);
            IPlayer<GameState> randomPlayer = new RandomPlayer<GameState>();
            IPlayer<GameState> consolePlayer = new ConsolePlayer<GameState>(presenter);

            var settings = new GameSettings(true, true, false);
            GameState gameState = new GameState(settings, Board.Board4x4);

            Game<GameState> game = new Game<GameState>(randomPlayer, minMaxAlphaBetaPlayer);

            IEnumerable<GameState> gameplay = game.Play(gameState).ToList();

            var finalState = gameplay.Last();
            Debug.Assert(true == finalState.IsTerminal);
           
            int player1Pts = finalState.WhiteScore;
            int player2Pts = finalState.BlackScore;

            if (player1Pts == player2Pts)
                Console.WriteLine("Draw.");
            else
                Console.WriteLine("The winer is player {0}", player1Pts > player2Pts ? "1" : "2");

            Console.ReadKey();

        }
    }
}
