using System;
using UnityEngine;

namespace Exception6
{
    class Print
    {
        public class Menu {
            private static GUIStyle baseStyle = new GUIStyle();
            public static void Button(string name, ref bool switcher) {
                switcher = GUILayout.Button(name);
            }
            public static void Label(string name, bool center = false) {
                if (center)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                }
                GUILayout.Label(name); 
                if (center)
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }
            public static void Label(string name) { GUILayout.Label(name); }
            public static void Label(string name1, string name2) { GUILayout.Label(name1 + name2); }
            public static void Label(string name1, string name2, string name3) { GUILayout.Label(name1 + name2 + name3); }
            public static void Label(string name1, string name2, string name3, string name4) { GUILayout.Label(name1 + name2 + name3 + name4); }
            public static class Slider {
                public static class Horizontal {
                    public static void Int(ref int value, float min = 0f, float max = 1000f, string name = "")
                    {
                        if (name != "")
                            Menu.Label(name + ": " + value.ToString());
                        value = (int)GUILayout.HorizontalSlider(value, min, max);
                    }
                    public static void Float(ref float value, float min = 0f, float max = 1000f, string name = "", bool round = false)
                    {
                        if (name != "")
                            Menu.Label(name + ": " + value.ToString());
                        if (round)
                            value = (float)(int)value;
                        value = GUILayout.HorizontalSlider(value, min, max);
                    }
                }
                public static class Vertical {
                    public static void Int(ref int value, float min = 0f, float max = 1000f, string name = "")
                    {
                        if (name != "")
                            Menu.Label(name + ": " + value.ToString());
                        value = (int)GUILayout.VerticalSlider(value, min, max);
                    }
                    public static void Float(ref float value, float min = 0f, float max = 1000f, string name = "")
                    {
                        if (name != "")
                            Menu.Label(name + ": " + value.ToString());
                        value = GUILayout.VerticalSlider(value, min, max);
                    }
                }
            }
            public static class TextBox {
                public static void Int(ref int value) { value = Int32.Parse(GUILayout.TextField(value.ToString())); }
                public static void Long(ref long value) { value = Int64.Parse(GUILayout.TextField(value.ToString())); }
                public static void String(ref string value) { value = GUILayout.TextField(value); }
                public static void Float(ref float value) { value = float.Parse(GUILayout.TextField(value.ToString())); }
            }
            public static void Checkbox(string name, ref bool switcher) { switcher = GUILayout.Toggle(switcher, name); }

        }
        private class dColors {
            public static Color Black = new Color(0f, 0f, 0f, 1f);
            public static Color HeavyGrey = new Color(1f, 1f, 1f, 0.6f);
            public static Color White = new Color(1f, 1f, 1f, 1f);
        }
        public class Special
        {
            private static class d {
                public static Rect rectA = Rect.zero;
                public static GUIContent tGUIContent = GUIContent.none;

                public static Vector2 vectorA = Vector2.zero;
                public static Vector2 vector1x1 = new Vector2(1, 1);
            }
            public static void DrawPoint(float axis_x, float axis_y, float size, Color pixel_color)
            {
                d.vectorA.x = axis_x;
                d.vectorA.y = axis_y;
                Dot.Draw(
                    d.vectorA,
                    pixel_color,
                    size
                );
            }
            public static void DrawText(string text, Rect size, GUIStyle style) {
                d.vectorA.x = size.width;
                d.vectorA.y = size.height;
                DrawText(text, size.x, size.y, d.vectorA, style, style.normal.textColor);
            }
            public static void DrawText(string text, float axis_x, float axis_y, Vector2 size, GUIStyle styleOfText, Color front_color)
            {
                d.rectA.x = axis_x;
                d.rectA.y = axis_y;
                d.rectA.width = size.x;
                d.rectA.height = size.y;
                d.tGUIContent.text = text;
                Text.DrawShadowed(
                    d.rectA,
                    d.tGUIContent,
                    styleOfText,
                    front_color,
                    dColors.Black,
                    d.vector1x1
                );
            }
        }
        public static class Dot {
            private static class d {
                public static Texture2D lineTex = new Texture2D(1, 1);
                public static Vector2 vectorA = Vector2.zero;
                public static Rect rectA = Rect.zero;
                public static Color bColor;
                public static float tOffset = 0f;
                public static Texture2D patternTexture = new Texture2D(1, 1);
                public static Vector2 Crosshair2dCenter_sw = new Vector2(Screen.width / 2f - 2f, Screen.height / 2f - 2f);
                public static Vector2 Crosshair2dCenter_ac = new Vector2(Screen.width / 2f - 1f, Screen.height / 2f - 1f);
                public static Vector2 Crosshair2dVector = Vector2.zero;
            }
            public static void DrawCenterCrosshair() {
                Dot.Draw(d.Crosshair2dCenter_sw, dColors.Black, 4f);
                Dot.Draw(d.Crosshair2dCenter_ac, dColors.White, 2f);
            }
            public static void DrawVectorCrosshair(Vector3 vector) {
                if (vector.x == 0f && vector.y == 0f)
                    return; // if [0,0] - return;
                d.Crosshair2dVector.x = vector.x - 2f;
                d.Crosshair2dVector.y = Screen.height - vector.y - 1f;
                Dot.Draw(d.Crosshair2dVector, dColors.Black, 4f);
                d.Crosshair2dVector.x += 1f;
                d.Crosshair2dVector.y += 1f;
                Dot.Draw(d.Crosshair2dVector, dColors.White, 2f);
            }
            public static void Draw(Vector2 Position, Color color, float thickness)
            {
                if (!d.lineTex) { d.lineTex = d.patternTexture; }
                d.tOffset = Mathf.Ceil(thickness / 2f);
                d.bColor = GUI.color;
                d.rectA.x = Position.x;
                d.rectA.y = Position.y - d.tOffset;
                d.rectA.width = thickness;
                d.rectA.height = thickness;
                GUI.color = color;
                GUI.DrawTexture(d.rectA, d.lineTex);
                GUI.color = d.bColor;
            }
        }
        public static class Text {
            private static class d {
                public static Vector2 vectorA = Vector2.zero;
                public static GUIStyle bStyle;
                public static GUIContent bContent = new GUIContent();
                public static GUIStyle cStyle = new GUIStyle();
            }
            public static void Draw(Rect rect, string content, Color txtColor, bool shadow = true) {
                if (!shadow) {
                    d.bStyle = d.cStyle;
                    d.bStyle.normal.textColor = txtColor;
                    GUI.Label(rect, content, d.bStyle);
                    return;
                }
                d.bContent.text = content;
                d.vectorA.x = 1f;
                d.vectorA.y = 1f;
                DrawShadowed(rect, d.bContent, d.cStyle, txtColor, dColors.Black, d.vectorA);

            }
            public static void DrawShadowed(Rect rect, GUIContent content, GUIStyle style, Color txtColor, Color shadowColor, Vector2 direction) {
                d.bStyle = style;
                style.normal.textColor = shadowColor;
                rect.x += direction.x;
                rect.y += direction.y;
                GUI.Label(rect, content, style);
                style.normal.textColor = txtColor;
                rect.x -= direction.x;
                rect.y -= direction.y;
                GUI.Label(rect, content, style);
                style = d.bStyle;
            }
        }
        public static class Bone {
            private static float halfthicc = 1f;
            public static void Draw(Vector2 pointA, Vector2 pointB, Color color, float thickness = 2f, bool shouldDrawShadow = true) {
                if (shouldDrawShadow)
                {
                    halfthicc = thickness / 2f;
                    pointA.x -= halfthicc;
                    pointB.x -= halfthicc;
                    pointA.y = Screen.height - pointA.y - halfthicc;
                    pointB.y = Screen.height - pointB.y - halfthicc;
                    Line.Draw(pointA, pointB, dColors.Black, thickness);
                    thickness /= 2f;
                    halfthicc /= 2f;
                    pointA.x += halfthicc;
                    pointB.x += halfthicc;
                    pointA.x += halfthicc;
                    pointB.x += halfthicc;
                    Line.Draw(pointA, pointB, color, thickness);
                }
                else 
                {
                    halfthicc = thickness / 2f;
                    pointA.x -= halfthicc;
                    pointB.x -= halfthicc;
                    pointA.y = Screen.height - pointA.y - halfthicc;
                    pointB.y = Screen.height - pointB.y - halfthicc;
                    Line.Draw(pointA, pointB, color, thickness);
                }
            }
        }
        public static class Line {
            // temporal iCorpse used only in this scope to remove ram overuse and speed up code
            private static class d {
                public static Texture2D lineTex = new Texture2D(1, 1);
                public static Texture2D patternTexture = new Texture2D(1, 1);
                public static Matrix4x4 i_M4x4 = Matrix4x4.zero;
                public static Vector2 vectorA = Vector2.zero;
                public static Vector2 vectorB = Vector2.zero;
                public static float Angle = 0f;
                public static Color bColor;
                public static Rect rectA = Rect.zero;
            }
            // add more overloads below if needed
            public static void Draw(Rect rect, Color color, float width) {
                d.vectorA.x = rect.x;
                d.vectorA.y = rect.y;
                d.vectorB.x = rect.x + rect.width;
                d.vectorB.y = rect.y + rect.height;
                Draw(d.vectorA, d.vectorB, color, width);
            }
            public static void Draw(Vector2 pointA, Vector2 pointB, Color color) { Draw(pointA, pointB, color, 1.0f); }
            // main drawing function
            public static void Draw(Vector2 pointA, Vector2 pointB, Color color, float width) {
                d.i_M4x4 = GUI.matrix;
                if (!d.lineTex) { d.lineTex = d.patternTexture; }
                d.bColor = GUI.color;
                GUI.color = color;
                d.Angle = Vector3.Angle(pointB - pointA, Vector2.right);
                if (pointA.y > pointB.y) { d.Angle = -d.Angle; }
                d.vectorA.x = (pointB - pointA).magnitude;
                d.vectorA.y = width;
                d.vectorB.x = pointA.x;
                d.vectorB.y = pointA.y + 0.5f;
                GUIUtility.ScaleAroundPivot(d.vectorA, d.vectorB);
                GUIUtility.RotateAroundPivot(d.Angle, pointA);
                d.rectA.x = pointA.x;
                d.rectA.y = pointA.y;
                d.rectA.width = 1f;
                d.rectA.height = 1f;
                GUI.DrawTexture(d.rectA, d.lineTex);
                GUI.matrix = d.i_M4x4;
                GUI.color = d.bColor;
            }
        }
        public static class Box {
            private static class d {
                public static Vector2 vectorA = Vector2.zero;
                public static Vector2 vectorB = Vector2.zero;
            }
            public static void Draw(float x, float y, float w, float h, Color color) {
                d.vectorA.x = x;
                d.vectorA.y = y;
                d.vectorB.x = x + w;
                d.vectorB.y = y;
                Line.Draw(d.vectorA, d.vectorA, color); //Top left-right
                //d.vectorA.x = x;//same as prev.
                //d.vectorA.y = y;//same as prev.
                d.vectorB.x = x;
                d.vectorB.y = y + h;
                Line.Draw(d.vectorA, d.vectorA, color); //Left top-bottom
                d.vectorA.x = x + w;
                //d.vectorA.y = y;//same as prev.
                d.vectorB.x = x + w;
                //d.vectorB.y = y + h;//same as prev.
                Line.Draw(d.vectorA, d.vectorA, color); //Right top-bottom
                d.vectorA.x = x;
                d.vectorA.y = y + h;
                //d.vectorB.x = x + w;//same as prev.
                //d.vectorB.y = y + h;//same as prev.
                Line.Draw(d.vectorA, d.vectorA, color); //Bottom left-right
            }
        }
        public static class Circle {
            private static class d {
                public static float[] cos = new float[] {
                    0.871147401032343f,
                    0.517795588650813f,
                    0.0310051616059934f,
                    -0.463775456747515f,
                    -0.839038729222366f,
                    -0.998077359907573f,
                    -0.899906267003044f,
                    -0.569824651437267f,
                    -0.0928962612844285f,
                    0.407971978270164f,
                    0.803703718412583f,
                    0.99231683272014f,
                    0.92520474123701f
                };
                public static float[] sin = new float[] {
                    -0.491021593898469f,
                    -0.855504370750821f,
                    -0.999519224404306f,
                    -0.88595277849253f,
                    -0.544071696437995f,
                    -0.0619805101619054f,
                    0.43608337575359f,
                    0.821766309004207f,
                    0.995675792936323f,
                    0.912994449570384f,
                    0.595029690864067f,
                    0.123722687896238f,
                    -0.37946829484498f
                };
                public static Vector2 vectorA = Vector2.zero;
                public static Vector2 vectorB = Vector2.zero;
            }
            public static void Draw(int X, int Y, float radius, float thickness = 1f)
            {
                Draw(X, Y, radius, dColors.HeavyGrey, thickness);
            }
            public static void Draw(int X, int Y, float radius, Color color)
            {
                Draw(X, Y, radius, color, 1f);
            }
            public static void Draw(int X, int Y, float radius, Color color, float thickness = 1f) {
                for (int i = 0; i < 12; i++)
                {
                    d.vectorA.x = X + d.cos[i] * radius;
                    d.vectorA.y = Y + d.sin[i] * radius;
                    d.vectorB.x = X + d.cos[i + 1] * radius;
                    d.vectorB.y = Y + d.sin[i + 1] * radius;
                    Print.Line.Draw(d.vectorA, d.vectorB, color, thickness);
                }
            }
        }
    }
}
