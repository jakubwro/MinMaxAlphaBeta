using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Checkers.Rules;

namespace Checkers
{
    using Diagonal = IEnumerable<Square>;
    using Layout = IImmutableDictionary<Square, Checker>;

    public sealed class Checker
    {
        public static readonly Checker WhiteFolk = new Checker(ColorEnum.White, Rule.FolkRules);
        public static readonly Checker BlackFolk = new Checker(ColorEnum.Black, Rule.FolkRules);
        //public static readonly Checker WhiteKing = new Checker(ColorEnum.White, null);
        //public static readonly Checker BlackKing = new Checker(ColorEnum.Black, null);

        private readonly ColorEnum color;
        private readonly IEnumerable<Rule> rules;

        public ColorEnum Color { get { return color; } }

        private Checker(ColorEnum color, IEnumerable<Rule> rules)
        {

            this.rules = rules;
            this.color = color;
        }

        public IEnumerable<IMove> ExecuteRules(GameState game, Square square)
        {
            System.Diagnostics.Debug.Assert(game.Layout.ContainsKey(square), 
                string.Format("Square '{0}' does not exist in a layout", square));
            System.Diagnostics.Debug.Assert(game.Layout[square] == this,
                string.Format("Square '{0}' does not contain this checker", square));

            foreach(var rule in rules)
                foreach(IMove move in rule.Execute(game, square))
                    yield return move;
        }
    }
}