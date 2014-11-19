using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    public static class BinaryMasks
    {
        public static UInt32[] RightToLeftDiagonals = new UInt32[]
        {
            0x00000011u,
            0x00001122u,
            0x00112244u,
            0x11224488u,
            0x22448800u,
            0x44880000u,
            0x88000000u,
        };

        public static UInt32[] LeftToRightDiagonals = new UInt32[]
        {
            0x00000008u,
            0x00000884u,
            0x00088442u,
            0x08844221u,
            0x84422110u,
            0x42211000u,
            0x21100000u,
            0x10000000u,
        };
    }
}
