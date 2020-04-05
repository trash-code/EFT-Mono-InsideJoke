using Exception5.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception6.Functions
{
    class FastMath
    {
        // not used cause of optimisations
        // use if no optimisations are "on"
        /*
        #if ReleaseMaoci
        #region FastDistance Vector3 - fDistance
                public static float fDistance(Vector3 c1, Vector3 c2) { float cx, cy, cz, n; cx = c2.x - c1.x; cy = c2.y - c1.y; cz = c2.z - c1.z; n = (cx * cx + cy * cy + cz * cz); return fSqrt(n); }
        #endregion
        #region FastDistance Vector2 - fDistance2d
                public static float fDistance2d(Vector2 c1, Vector2 c2) { float cx, cy, n; cx = c2.x - c1.x; cy = c2.y - c1.y; n = (cx * cx + cy * cy); return fSqrt(n); }
        #endregion
        #region Fast SQRT calculations - fSqrt
                [StructLayout(LayoutKind.Explicit)]
                private struct FloatIntUnion {[FieldOffset(0)] public float f;[FieldOffset(0)] public int tmp; }
                private static float fSqrt(float z)
                { if (z == 0) { return 0; } FloatIntUnion c; c.tmp = 0; c.f = z; c.tmp -= 1 << 23; c.tmp >>= 1; c.tmp += 1 << 29; return c.f; }
        #endregion
        #endif*/
    }
}
