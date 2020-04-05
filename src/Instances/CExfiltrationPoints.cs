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
    public class CExfiltrationPoints : MonoBehaviour
    {
        private static Camera camera;
        private static ScavExfiltrationPoint[] scavExfilList;
        private static ExfiltrationPoint[] pmcExfilList;
        private static Vector3 positionW2S = Vector3.zero;
        [ObfuscationAttribute(Exclude = true)]
        void Awake()
        {
            isOnAwake();
        }
        #region OnGUI()
        [ObfuscationAttribute(Exclude = true)]
        public void OnGUI()
        {
            if (Settings.CExfiltration.Enable)
                isOnGui();
        }
        #endregion
        #region Obfuscated functions
        private void isOnAwake()
        {
            data.Style.fontSize = MaociScreen.Scale.FontSize;
            data.size.full = (data.size.full * MaociScreen.ratioX);
            data.size.half = (data.size.half * MaociScreen.ratioX);
        }
        private void isOnGui()
        {
            if (LocalPlayer.Base == null)
                return;
            camera = Camera.main;
            if (!camera)
                return;
            ExfilLoop();
        }
        private static void ExfilLoop()
        {
            if (LocalPlayer.Base.Side == EFT.EPlayerSide.Savage)
            {
                scavExfilList = CGameWorld.Base.ExfiltrationController.ScavExfiltrationPoints;
                for (int i = 0; i < scavExfilList.Length; i++)
                {
                    DrawScavExfil(scavExfilList[i]);
                }
            }
            else
            {
                pmcExfilList = CGameWorld.Base.ExfiltrationController.ExfiltrationPoints;
                for (int i = 0; i < pmcExfilList.Length; i++)
                {
                    if(pmcExfilList[i])
                    DrawPmcExfil(pmcExfilList[i]);
                }
            }
        }
        private static class data
        {
            //public static string Name;
            public static int DistanceToObject = 0;
            public static float onScreenY = 0f;
            public static GUIStyle Style = new GUIStyle { fontSize = 10 };
            public static class size
            {
                public static float full = 3f;
                public static float half = 1.5f;
            }
            public static class text
            {
                public static string Txt_1 = "";
                public static string Txt_2 = "";
                public static string Txt_3 = "";
                public static Vector2 sizeTxt_1 = Vector2.zero;
                public static Vector2 sizeTxt_2 = Vector2.zero;
                public static Vector2 sizeTxt_3 = Vector2.zero;
                private static GUIContent baseContentBox = new GUIContent();
                public static GUIContent content_1
                {
                    get
                    {
                        baseContentBox.text = Txt_1;
                        return baseContentBox;
                    }
                }
                public static GUIContent content_2
                {
                    get
                    {
                        baseContentBox.text = Txt_2;
                        return baseContentBox;
                    }
                }
                public static GUIContent content_3
                {
                    get
                    {
                        baseContentBox.text = Txt_3;
                        return baseContentBox;
                    }
                }
            }
        }
        private static void DrawScavExfil(ScavExfiltrationPoint tExfil = null)
        {
            if (tExfil == null) return;
            if (MaociScreen.onScreenStrict(camera.WorldToScreenPoint(tExfil.transform.position)))
            {
                data.DistanceToObject = (int)Vector3.Distance(camera.transform.position, tExfil.transform.position);
                if (data.DistanceToObject < Settings.CExfiltration.Distance)
                {
                    positionW2S = camera.WorldToScreenPoint(tExfil.transform.position);
                    data.Style.fontSize = MaociScreen.Scale.FontSizer(data.DistanceToObject);
                    data.Style.normal.textColor = Settings.CExfiltration.DrawColor;
                    data.text.Txt_1 = tExfil.Settings.Name;
                    data.text.Txt_2 = $"({func.TypeOfExfiltration(tExfil.Status)})";
                    data.text.Txt_3 = $"{data.DistanceToObject}m";
                    data.text.sizeTxt_1 = GUI.skin.GetStyle(data.text.Txt_1).CalcSize(data.text.content_1);
                    data.text.sizeTxt_2 = GUI.skin.GetStyle(data.text.Txt_2).CalcSize(data.text.content_2);
                    data.text.sizeTxt_3 = GUI.skin.GetStyle(data.text.Txt_3).CalcSize(data.text.content_3);
                    data.onScreenY = Screen.height - positionW2S.y;
                    // drawing starts
                    Print.Special.DrawPoint(positionW2S.x - data.size.half, (float)(Screen.height - positionW2S.y) - data.size.half, data.size.full, Settings.CExfiltration.DrawColor);
                    Print.Special.DrawText(
                        data.text.Txt_1,
                        positionW2S.x - data.text.sizeTxt_1.x / 2f,
                        data.onScreenY - Settings.CExfiltration.DeltaDistance - 2,
                        data.text.sizeTxt_1,
                        data.Style,
                        Settings.CExfiltration.DrawColor
                    );
                    Print.Special.DrawText(
                        data.text.Txt_2,
                        positionW2S.x - data.text.sizeTxt_2.x / 2f,
                        data.onScreenY - Settings.CExfiltration.DeltaDistance - data.Style.fontSize - 2,
                        data.text.sizeTxt_2,
                        data.Style,
                        Settings.CExfiltration.DrawColor
                    );
                    Print.Special.DrawText(
                        data.text.Txt_3,
                        positionW2S.x - data.text.sizeTxt_3.x / 2f,
                        data.onScreenY - Settings.CExfiltration.DeltaDistance - data.Style.fontSize - data.Style.fontSize - 2,
                        data.text.sizeTxt_3,
                        data.Style,
                        Settings.CExfiltration.DrawColor
                    );
                }
            }
        }
        private static void DrawPmcExfil(ExfiltrationPoint tExfil = null)
        {
            if (tExfil == null) return;
            if (MaociScreen.onScreenStrict(camera.WorldToScreenPoint(tExfil.transform.position)))
            {
                data.DistanceToObject = (int)Vector3.Distance(camera.transform.position, tExfil.transform.position);
                if (data.DistanceToObject < Settings.CExfiltration.Distance)
                {
                    positionW2S = camera.WorldToScreenPoint(tExfil.transform.position);
                    data.Style.normal.textColor = Settings.CExfiltration.DrawColor;
                    data.text.Txt_1 = tExfil.Settings.Name;
                    data.text.Txt_2 = $"({func.TypeOfExfiltration(tExfil.Status)})";
                    data.text.Txt_3 = $"{data.DistanceToObject}m";
                    data.text.sizeTxt_1 = GUI.skin.GetStyle(data.text.Txt_1).CalcSize(data.text.content_1);
                    data.text.sizeTxt_2 = GUI.skin.GetStyle(data.text.Txt_2).CalcSize(data.text.content_2);
                    data.text.sizeTxt_3 = GUI.skin.GetStyle(data.text.Txt_3).CalcSize(data.text.content_3);
                    data.onScreenY = Screen.height - positionW2S.y;
                    // drawing starts
                    Print.Special.DrawPoint(positionW2S.x - data.size.half, (float)(Screen.height - positionW2S.y) - data.size.half, data.size.full, Settings.CExfiltration.DrawColor);
                    Print.Special.DrawText(
                        data.text.Txt_1,
                        positionW2S.x - data.text.sizeTxt_1.x / 2f,
                        data.onScreenY - Settings.CExfiltration.DeltaDistance - 2,
                        data.text.sizeTxt_1,
                        data.Style,
                        Settings.CExfiltration.DrawColor
                    );
                    Print.Special.DrawText(
                        data.text.Txt_2,
                        positionW2S.x - data.text.sizeTxt_2.x / 2f,
                        data.onScreenY - Settings.CExfiltration.DeltaDistance - data.Style.fontSize - 2,
                        data.text.sizeTxt_2,
                        data.Style,
                        Settings.CExfiltration.DrawColor
                    );
                    Print.Special.DrawText(
                        data.text.Txt_3,
                        positionW2S.x - data.text.sizeTxt_3.x / 2f,
                        data.onScreenY - Settings.CExfiltration.DeltaDistance - data.Style.fontSize - data.Style.fontSize - 2,
                        data.text.sizeTxt_3,
                        data.Style,
                        Settings.CExfiltration.DrawColor
                    );
                }
            }
        }
        private class func
        {
            public static string TypeOfExfiltration(EExfiltrationStatus status)
            {
                switch (status)
                {
                    case EExfiltrationStatus.AwaitsManualActivation:
                        return "Activate";
                    case EExfiltrationStatus.Countdown:
                        return "Timer";
                    case EExfiltrationStatus.NotPresent:
                        return "Closed";
                    case EExfiltrationStatus.Pending:
                        return "Pending";
                    case EExfiltrationStatus.RegularMode:
                        return "Open";
                    case EExfiltrationStatus.UncompleteRequirements:
                        return "Req.";
                    default:
                        return "";
                }
            }
        }
        #endregion
    }
}
