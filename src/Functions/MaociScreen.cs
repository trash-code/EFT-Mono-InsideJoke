using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception5.Functions
{
    class MaociScreen
    {
        public static float width = UnityEngine.Screen.width;
        public static float height = UnityEngine.Screen.height;
        public static float width_half = width / 2f;
        public static float height_half = height / 2f;
        public static float ratio 
        { 
            get 
            {
                var temp_ratio = UnityEngine.Screen.width / 1920;
                return (temp_ratio < 1f)?1f:temp_ratio;
            } 
        }
        public static float ratioX
        {
            get
            {
                var temp_ratio = UnityEngine.Screen.width / 1920;
                return (temp_ratio < 1f) ? 1f : temp_ratio;
            }
        }

        public static float ratioY
        {
            get
            {
                var temp_ratio = UnityEngine.Screen.height / 1080;
                return (temp_ratio < 1f) ? 1f : temp_ratio;
            }
        }

        public static Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f);

        public static bool onScreenStrict(Vector3 V)
        {
            if (V.x > 0.01f && V.y > 0.01f && V.x < width && V.y < height && V.z > 0.01f)
                return true;
            return false;
        }
        public static bool onScreenYZ(Vector3 V)
        {
            if (V.y > 0.01f && V.y < (height - 5f) && V.z > 0.01f)
                return true;
            return false;
        }
        public static bool onScreenSnapLines(Vector3 V)
        {
            if (V.y > 0.01f && V.y < (height - 5f) && V.z > 0.01f)
                return true;
            return false;
        }
        public class Scale {
            public static int FontSize = 12; // for 1920
            private static void CalculateFont(ref int initialFont, float distance, int limit_to) {
                initialFont = (int)(initialFont + (100 / distance));
                if (initialFont > limit_to)
                    initialFont = limit_to;
            }
            private static int workspaceFont = 14;
            public static int FontSizer(float distance)
            {
                workspaceFont = 8; // minimum
                CalculateFont(ref workspaceFont, distance, 14); // 14 is maximum
                workspaceFont = (int)(workspaceFont * MaociScreen.ratio); // multiply by ratio from 1920
                return workspaceFont;
            }
        }
        private static Color transparent = new Color(0f,0f,0f,0f);
        public static Texture2D CreateTexture2DWithColor(Color color)
        {
            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture2D.hideFlags = HideFlags.DontUnloadUnusedAsset;
            UnityEngine.Object.DontDestroyOnLoad(texture2D);
            texture2D.SetPixel(0, 0, color);
            texture2D.Apply();
            return texture2D;
        }
    }
}
