using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public class Statistics
    {
        public static Statistics Instance = null;

        public List<int> measures;

        public int player1Wins = 0;
        public int player2Wins = 0;
        public int draws = 0;

        public int totalPlayer1Pts = 0;
        public int totalPlayer2Pts = 0;
        public int totalMoves = 0;

        public int memoHits = 0;
        public int memoMiss = 0;

        public string ToString()
        {
            return String.Format(
@"Player 1 wins: {0}
Player 2 wins: {1}
Draws: {2}
Total player 1 pts: {3}
Total player 2 pts: {4}
Total moves: {5}
Memo hits: {6}
Momo misses: {7}
", player1Wins, player2Wins, draws, totalPlayer1Pts, totalPlayer2Pts, totalMoves, memoHits, memoMiss);
        }
    }
}
