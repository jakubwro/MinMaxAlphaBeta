using Checkers;
using Checkers.FastModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class FastStatePresenter : IPresenter<FastState, string>
    {
        public string Render(FastState state)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("+");
            for (int i = 0; i < 8; ++i)
                stringBuilder.Append("---+");   
            stringBuilder.AppendLine();

            for (int i = 7; i >= 0; --i)
            {
                stringBuilder.Append("|");
                for (int j = 0; j < 8; ++j)
                {
                    string square = string.Format("{0}{1}", (char)('A'+j), i+1);

                    string v = " ";

                    if ((i+j) % 2 == 0)
                    {
                        if ((state.WhiteFolks & (0x1u << ((i<<2) + (3-(j>>1))))) > 0)
                            v = "o";
                        else if ((state.BlackFolks & (0x1u << ((i<<2) + (3-(j>>1))))) > 0)
                            v = "x";
                    }

                    stringBuilder.Append(string.Format(" {0} |", v));
                }
                stringBuilder.AppendLine((i+1).ToString());

                stringBuilder.Append("+");
                for (int k = 0; k < 8; ++k)
                    stringBuilder.Append("---+");
                stringBuilder.AppendLine();
            }

            stringBuilder.Append(" ");
            for (int k = 0; k < 8; ++k)
                stringBuilder.Append(string.Format(" {0}  ", (char)('A' + k)));
            stringBuilder.AppendLine();

            stringBuilder.AppendFormat("White points: {0}", state.WhiteKings);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("Black points: {0}", state.BlackKings);
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        public bool TryAccept(FastState state, string input, out FastState next)
        {
            throw new NotImplementedException();
        }
    }
}
