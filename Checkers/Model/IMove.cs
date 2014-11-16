using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    using Layout = System.Collections.Immutable.IImmutableDictionary<Square, Checker>; //TODO: Change to Setup

    public interface IMove
    {
        Layout LayoutBefore { get; }
        Layout LayoutAfter { get; }
    }
}
