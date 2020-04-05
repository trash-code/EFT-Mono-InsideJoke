using System;
using System.Collections.Generic;
using UnityEngine;

using iCorpsData = EFT.Interactive.LootItem;
using idBodyData = EFT.Interactive.Corpse;
using idBodyDataN = EFT.Interactive.ObservedCorpse;
using uiStyle = UnityEngine.GUIStyle;
using uiContent = UnityEngine.GUIContent;
using uV3 = UnityEngine.Vector3;
using uV2 = UnityEngine.Vector2;
using System.Reflection;
using Exception5.Functions;

namespace Exception6.Instances
{
    public class CCorpse : MonoBehaviour
    {
        private static class update {
            public static Type Corpse = new idBodyData().GetType();
            public static Type ObserverCorpse = new idBodyDataN().GetType();
            public static List<iCorpsData> l_Corpse = new List<iCorpsData>();
            public static List<iCorpsData> tCorpses = new List<iCorpsData>();
            public static iCorpsData tmpItem = null;
            public static List<iCorpsData>.Enumerator list_tmp;
        }
        private static class iCorpse {
            public static Camera tempCamera = null;
            public static uiStyle LabelSize = new uiStyle { fontSize = MaociScreen.Scale.FontSize };
            public static iCorpsData tCorpse = null;
            public static uV3 itemPositionW2S = uV3.zero;
            public static int DistanceToObject = 0;
        }
        public static class text
        {
            public static string Txt_1 = "";
            public static uV2 sizeTxt_1 = uV2.zero;
            public static string Txt_2 = "";
            public static uV2 sizeTxt_2 = uV2.zero;
            public static string Txt_3 = "";
            public static uV2 sizeTxt_3 = uV2.zero;
            private static uiContent baseContentBox = new uiContent();
            public static uiContent content_1
            {
                get
                {
                    baseContentBox.text = Txt_1;
                    return baseContentBox;
                }
            }
            public static uiContent content_2
            {
                get
                {
                    baseContentBox.text = Txt_2;
                    return baseContentBox;
                }
            }
            public static uiContent content_3
            {
                get
                {
                    baseContentBox.text = Txt_3;
                    return baseContentBox;
                }
            }
        }
        [ObfuscationAttribute(Exclude = true)]
        public void Update() {
            if (Settings.CCorpse.Enable) isOnUpdate();
        }
        private void isOnUpdate() {
            if (LocalPlayer.Base == null)
                return;
            try
            {
                update.tCorpses = new List<iCorpsData>();
                update.list_tmp = CGameWorld.Base.LootItems.GetValuesEnumerator().GetEnumerator();
                while (update.list_tmp.MoveNext())
                {
                    update.tmpItem = update.list_tmp.Current;
                    if (update.tmpItem is idBodyData || update.tmpItem is idBodyDataN)
                        update.tCorpses.Add(update.tmpItem);
                }
                update.l_Corpse = update.tCorpses;
            }
            catch { }
        }
        [ObfuscationAttribute(Exclude = true)]
        public void OnGUI() {
            if (!Settings.CCorpse.Enable)
                return; //enable?
            if (Event.current.type != EventType.Repaint)
                return; //should we repaint?
            if (LocalPlayer.Base == null)
                return; // gameworld is iniciated?
            if(iCorpse.tempCamera == null)
                iCorpse.tempCamera = Camera.main;
            if (!iCorpse.tempCamera)
                return;

            var e = update.l_Corpse.GetEnumerator();
            while (e.MoveNext())
            {
                try
                {
                    iCorpse.tCorpse = e.Current;
                    if (iCorpse.tCorpse == null) continue;
                    iCorpse.itemPositionW2S = iCorpse.tempCamera.WorldToScreenPoint(iCorpse.tCorpse.transform.position);
                    if (MaociScreen.onScreenStrict(iCorpse.itemPositionW2S))
                    {
                        iCorpse.DistanceToObject = (int)Vector3.Distance(iCorpse.tempCamera.transform.position, iCorpse.tCorpse.transform.position);
                        if (iCorpse.DistanceToObject < Settings.CCorpse.Distance)
                        {
                            iCorpse.LabelSize.fontSize = MaociScreen.Scale.FontSizer(iCorpse.DistanceToObject);
                            iCorpse.LabelSize.normal.textColor = Settings.CCorpse.DrawColor;
                            text.Txt_1 = $"{iCorpse.DistanceToObject}m";
                            text.sizeTxt_1 = GUI.skin.GetStyle(text.Txt_1).CalcSize(text.content_1);
                            Print.Special.DrawPoint(iCorpse.itemPositionW2S.x - 1.5f, Screen.height - iCorpse.itemPositionW2S.y - 1.5f, 3f, Settings.CCorpse.DrawColor);
                            Print.Special.DrawText(
                                text.Txt_1,
                                iCorpse.itemPositionW2S.x - text.sizeTxt_1.x / 2f,
                                Screen.height - iCorpse.itemPositionW2S.y - 25f,
                                text.sizeTxt_1,
                                iCorpse.LabelSize,
                                Settings.CCorpse.DrawColor
                            );
                        }
                    }
                } catch { }
            }
        }
    }
}
