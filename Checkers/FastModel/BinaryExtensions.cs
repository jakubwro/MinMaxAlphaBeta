using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.FastModel
{
    public static class CheckersExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ReverseBits(this UInt64 v)
        {
            v = ((v & 0x5555555555555555) << 1) | ((v & 0xaaaaaaaaaaaaaaaa) >> 1);
            v = ((v & 0x3333333333333333) << 2) | ((v & 0xcccccccccccccccc) >> 2);
            v = ((v & 0x0f0f0f0f0f0f0f0f) << 4) | ((v & 0xf0f0f0f0f0f0f0f0) >> 4);
            v = ((v & 0x00ff00ff00ff00ff) << 8) | ((v & 0xff00ff00ff00ff00) >> 8);
            v = ((v & 0x0000ffff0000ffff) << 16) | ((v & 0xffff0000ffff0000) >> 16);
            v = ((v & 0x00000000ffffffff) << 32) | ((v & 0xffffffff00000000) >> 32);
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ReverseBits(this UInt32 v)
        {
            v = ((v & 0x55555555) << 1) | ((v & 0xaaaaaaaa) >> 1);
            v = ((v & 0x33333333) << 2) | ((v & 0xcccccccc) >> 2);
            v = ((v & 0x0f0f0f0f) << 4) | ((v & 0xf0f0f0f0) >> 4);
            v = ((v & 0x00ff00ff) << 8) | ((v & 0xff00ff00) >> 8);
            v = ((v & 0x0000ffff) << 16) | ((v & 0xffff0000) >> 16);
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountBits(this UInt64 v)
        {
            v = (v & 0x5555555555555555) + ((v & 0xaaaaaaaaaaaaaaaa) >> 1);
            v = (v & 0x3333333333333333) + ((v & 0xcccccccccccccccc) >> 2);
            v = (v & 0x0f0f0f0f0f0f0f0f) + ((v & 0xf0f0f0f0f0f0f0f0) >> 4);
            v = (v & 0x00ff00ff00ff00ff) + ((v & 0xff00ff00ff00ff00) >> 8);
            v = (v & 0x0000ffff0000ffff) + ((v & 0xffff0000ffff0000) >> 16);
            v = (v & 0x00000000ffffffff) + ((v & 0xffffffff00000000) >> 32);

            return (int)v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountBits(this UInt32 v)
        {
            v = (v & 0x55555555) + ((v & 0xaaaaaaaa) >> 1);
            v = (v & 0x33333333) + ((v & 0xcccccccc) >> 2);
            v = (v & 0x0f0f0f0f) + ((v & 0xf0f0f0f0) >> 4);
            v = (v & 0x00ff00ff) + ((v & 0xff00ff00) >> 8);
            v = (v & 0x0000ffff) + ((v & 0xffff0000) >> 16);

            return (int)v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBit(this UInt64 v, int position)
        {
            Debug.Assert(position >= 0 && position < 64);
            return (v & (0x1ul << position)) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GetBit(this UInt32 v, int position)
        {
            Debug.Assert(position >= 0 && position < 32);
            return (v & (0x1ul << position)) > 0;
        }
    }
}
