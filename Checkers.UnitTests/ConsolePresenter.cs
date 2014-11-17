using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleUI
{
    class ConsolePresenter
    {
        public string Render(GameState state)
        {
            StringBuilder stringBuilder = new StringBuilder();

            
            for (int i = 7; i >= 0; --i)
            {
                stringBuilder.Append("|");
                for (int j = 0; j < 8; ++j)
                {
                    string square = string.Format("{0}{1}", (char)('A'+j), i+1);

                    string v = " ";
                    if (state.Layout.Keys.Any(s => s.ToString() == square))
                    {
                        var kv = state.Layout.SingleOrDefault(x => x.Key.ToString() == square);
                        v = kv.Value.Color == ColorEnum.Black ? "x" : "o";
                    }
                    stringBuilder.Append(string.Format("{0}", v));
                }
                stringBuilder.AppendLine();

            }
            
            return stringBuilder.ToString();
        }

        public bool TryAccept(string move, out GameState state)
        {
            state = default(GameState);
            return false;
        }
    }
}
