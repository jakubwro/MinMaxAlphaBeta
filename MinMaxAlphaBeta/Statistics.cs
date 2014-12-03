using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxAlphaBeta
{
    public class Statistics
    {
        public static List<int> hashes;
        public static List<int> measures;
        public static bool clearMemo = false;

        public static int player1Wins = 0;
        public static int player2Wins = 0;
        public static int draws = 0;

        public static int memoHits = 0;
        public static int memoMiss = 0;
    }
}
