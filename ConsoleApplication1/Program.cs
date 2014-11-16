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
            //var board = Board.Board12x12;

            //foreach(var diagonal in board.Diagonals)
            //{
            //    foreach (var square in diagonal)
            //        Console.Write("{0}, ", square);

            //    Console.WriteLine();
            //}
            while (true)
            {
                var settings = new GameSettings();
                ConsolePresenter presenter = new ConsolePresenter();
                GameState game = new GameState(settings, Board.Board8x8);

                while (game.AvailableMoves.Count() > 0)
                {
                    Console.WriteLine(presenter.Render(game));
                    var moves = game.AvailableMoves.ToList();
                    game = game.MakeMove(moves.Random());
                }

                Console.WriteLine(presenter.Render(game));
            }
            //var squares = board.Squares;
            
            //MinMaxAlphaBeta<State, int> minMaxAlphaBeta = new MinMaxAlphaBeta<State, int>(new Gauge());
               
            IPlayer<State> player1 = new RandomPlayer<State>();
            //IPlayer<State> player2 = new MinMaxPlayer<State>(minMaxAlphaBeta);

            //Game<State> game = new Game<State>(player1, player2);

            //IEnumerable<State> gameplay = game.Play(State.InitialState);

            //var finalState = gameplay.Last();
            //Debug.Assert(true == finalState.IsTerminal);

            //int player1Pts = finalState.WhiteKingsCount();
            //int player2Pts = finalState.BlackKingsCount();

            //if (player1Pts == player2Pts)
            //    Console.WriteLine("Draw.");
            //else
            //    Console.WriteLine("The winer is player {0}", player1Pts > player2Pts ? "1" : "2" );

        }
    }
}
