using EFT.Interactive;
using Exception5.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception6.Instances
{
    public class CThrowables : MonoBehaviour
    {
        private static Camera tempCamera;
        private static class iGrenade
        {
            public static Throwable grenade;
            public static int DistanceToObject;
            public static Vector3 positionW2S = Vector3.zero;
            public static float positionOnScreenY = 0f;
            public static class label
            {
                public static string Text;
                public static Vector2 size = Vector2.zero;
            }
            public static GUIStyle Style = new GUIStyle { fontSize = MaociScreen.Scale.FontSize };
        }
        [ObfuscationAttribute(Exclude = true)]
        public void Update() {
            
        }
        private void isOnGui() {
            if (!Settings.CThrowable.Enable)
                return;
            if (LocalPlayer.Base == null)
                return;
            if (Event.current.type != EventType.Repaint)
                return;
            tempCamera = Camera.main;
            if (!tempCamera)
                return;
            var e = CGameWorld.Base.Grenades.GetValuesEnumerator().GetEnumerator();
            while (e.MoveNext())
            {
                iGrenade.grenade = e.Current;
                if (iGrenade.grenade == null)
                    continue;
                if (MaociScreen.onScreenStrict(tempCamera.WorldToScreenPoint(iGrenade.grenade.transform.position)))
                {
                    iGrenade.DistanceToObject = (int)Vector3.Distance(tempCamera.transform.position, iGrenade.grenade.transform.position);
                    if (iGrenade.DistanceToObject < Settings.CThrowable.Distance)
                    {
                        iGrenade.Style.fontSize = MaociScreen.Scale.FontSizer(iGrenade.DistanceToObject);
                        iGrenade.positionW2S = tempCamera.WorldToScreenPoint(iGrenade.grenade.transform.position);
                        iGrenade.positionOnScreenY = (Screen.height - iGrenade.positionW2S.y);
                        iGrenade.Style.normal.textColor = Settings.CThrowable.DrawColor;
                        iGrenade.label.Text = $"{iGrenade.DistanceToObject}m - {func.ThrowableName(iGrenade.grenade.name.Localized())}";
                        iGrenade.label.size = GUI.skin.GetStyle(iGrenade.label.Text).CalcSize(new GUIContent(iGrenade.label.Text));
                        Print.Special.DrawPoint(iGrenade.positionW2S.x - 1.5f, iGrenade.positionOnScreenY - 1.5f, 3f, Settings.CThrowable.DrawColor);
                        Print.Special.DrawText(
                            iGrenade.label.Text,
                            iGrenade.positionW2S.x - iGrenade.label.size.x / 2f,
                            iGrenade.positionOnScreenY - 25f,
                            iGrenade.label.size,
                            iGrenade.Style,
                            Settings.CThrowable.DrawColor
                        );
                    }
                }
            }
        }
        public void OnGUI() {
            isOnGui();
        }

        private class func {
            public static string ThrowableName(string name)
            {
                switch (name)
                {
                    case "weapon_rgd5_world(Clone)":
                        return "RGD5";
                    case "weapon_grenade_f1_world(Clone)":
                        return "F1";
                    case "weapon_rgd2_world(Clone)":
                        return "Smoke";
                    case "weapon_m67_world(Clone)":
                        return "M67";
                    case "weapon_zarya_world(Clone)":
                        return "Flash Bang";
                    default:
                        return name.Replace("weapon_", "").Replace("_world(Clone)", "").Replace("grenade_", "");
                }
            }
        }
    }
}
