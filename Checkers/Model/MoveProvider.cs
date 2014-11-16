//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Checkers
//{
//    using Layout = IImmutableDictionary<Square, Checker>;

//    public class MoveProvider
//    {
//        private readonly Game game;

//        public MoveProvider(Game game)
//        {
//            this.game = game;
//        }

//        public void GetMovesFromSquare(Square square)
//        {
//            Checker checker = this.game.Layout[square];

//        }

//        public IEnumerable<Layout> GetMoves(Board board, Layout layout, ColorEnum activePlayer)
//        {
//            IEnumerable<IEnumerable<Square>> diagonals = board.Diagonals;

//            if (activePlayer == ColorEnum.Black)
//                diagonals = from d in diagonals select d.Reverse();

//            foreach (var square in layout.OfColor(activePlayer))
//                foreach (var move in GenerateMoves(diagonals, layout, square))
//                    yield return move;
//        }

//        private IEnumerable<Layout> GenerateMoves(IEnumerable<IEnumerable<Square>> diagonals, Layout layout, Square square)
//        {
//            foreach (var diagonal in diagonals)
//            {
//                if (!diagonal.Contains(square))
//                    continue;

//                var forward = diagonal.SkipWhile(s => s != square).Take(2);
//                if (forward.Count() == 2)
//                {
//                    var next = forward.Second();
//                    if (next != null && layout.ContainsKey(next) == false)
//                        yield return GetNextState(layout, square, next);
//                }
//            }
//        }

//        private IEnumerable<SequenceOfCaptures> GenerateCaptures(IEnumerable<IEnumerable<Square>> diagonals, Layout layout, Square square)
//        {
//            foreach (var diagonal in diagonals)
//            {
//                if (!diagonal.Contains(square))
//                    continue;

//                var forward = diagonal.SkipWhile(s => s != square);
//                foreach (var c in CaptureRec(diagonals, layout, forward.Take(3)))
//                    yield return c;

//                var backward = diagonal.Reverse().SkipWhile(s => s != square);
//                foreach (var c in CaptureRec(diagonals, layout, backward.Take(3)))
//                    yield return c;
//            }
//        }

//        private IEnumerable<SequenceOfCaptures> CaptureRec(IEnumerable<IEnumerable<Square>> diagonals, Layout layout, IEnumerable<Square> squares)
//        {
//            if (!IsLegalCapture(layout, squares))
//                yield break;

//            Layout nextLayout = GetNextState(layout, squares.First(), squares.Second(), squares.Third());

//            foreach (SequenceOfCaptures c in GenerateCaptures(diagonals, nextLayout, squares.Third()))
//                yield return c.ContinueSequence(nextLayout);
//        }

//        private Layout GetNextState(Layout layout, Square first, Square second, Square third)
//        {
//            return layout.Add(third, layout[first])
//                              .Remove(first)
//                              .Remove(second);
//        }

//        private Layout GetNextState(Layout layout, Square first, Square second)
//        {
//            return layout.Add(second, layout[first])
//                              .Remove(first);
//        }

//        private bool IsLegalCapture(Layout layout, IEnumerable<Square> squares)
//        {
//            if (squares.Count() != 3)
//                return false;

//            // 1. There is piece on the next square
//            Checker enemy;
//            if (!layout.TryGetValue(squares.Second(), out enemy))
//                return false;

//            // 2. This piece is an enemy
//            if (enemy.Color != layout[squares.First()].Color)
//                return false;

//            // Square behind the enemy is available
//            if (layout.ContainsKey(squares.Third()))
//                return false;

//            return true;
//        }
//    }
//}
