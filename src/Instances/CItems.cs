using EFT.Interactive;
using Exception5.Functions;
using Exception6.Functions;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Exception6.Instances
{
    class CItems : MonoBehaviour
    {
        public static int errorCounterU = 0;
        public static int errorCounterG = 0;
        public static int countLoot = 0;
        private static Vector2 vec = new Vector2(100f, 12f);

        public static string[] IDTable1 = new string[] { "none" };
        public static string[] IDTable2 = new string[] { "none" };
        public static string[] IDTable3 = new string[] { "none" };
        #region ~MAIN~
        [ObfuscationAttribute(Exclude = true)]
        void Awake()
        {
            isOnAwake();
        }
        [ObfuscationAttribute(Exclude = true)]
        public void Update()
        {
            if (Settings.CItem.Enable)
                isOnUpdate();
        }
        [ObfuscationAttribute(Exclude = true)]
        public void OnGUI()
        {
            if (Settings.CItem.Enable)
                isOnGui();
        }
        #endregion
        private void isOnAwake()
        {
            iItem.LabelSize.fontSize = (int)(iItem.LabelSize.fontSize * MaociScreen.ratioX);
        }
        private static float tempDistanceToObject = 0f;
        private void isOnUpdate()
        {
            if (LocalPlayer.Base == null) // if gameworld is not initiated already
                return;
            try
            {
                upd.tItemsList = new List<LootItem>();
                upd.temporalItemsEnum = CGameWorld.Base.LootItems.GetValuesEnumerator().GetEnumerator();
                countLoot = 0;
                while (upd.temporalItemsEnum.MoveNext())
                {
                    if (countLoot > Settings.CItem.ScreenLimitItems) break; // skips great chunk of data if set low :*)
                    upd.tmpLootItem = upd.temporalItemsEnum.Current;
                    if (upd.tmpLootItem.GetType() == upd.LootItem || upd.tmpLootItem.GetType() == upd.ObservedLootItem)
                    {
                        tempDistanceToObject = Vector3.Distance(Camera.main.transform.position, upd.tmpLootItem.transform.position);
                        if (tempDistanceToObject < Settings.CItem.BaseDistance) // limit number of items on screen also checking distance
                        {
                            if (Settings.CItem.DisplayQuestItems) {
                                if (upd.tmpLootItem.Item.Template.QuestItem)
                                {
                                    if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                    {
                                        upd.tItemsList.Add(upd.tmpLootItem);
                                        countLoot++;
                                    }
                                    else
                                    {
                                        if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                        {
                                            upd.tItemsList.Add(upd.tmpLootItem);
                                            countLoot++;
                                        }
                                    }
                                }
                            }
                            #region ~~Super rare search + name search
                            if (Settings.CItem.DisplayLootSrare) // selected super rare
                            {
                                if (upd.tmpLootItem.Item.Template.Rarity == JsonType.ELootRarity.Superrare)
                                {
                                    if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                    {
                                        upd.tItemsList.Add(upd.tmpLootItem);
                                        countLoot++;
                                    }
                                    else
                                    {
                                        if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                        {
                                            upd.tItemsList.Add(upd.tmpLootItem);
                                            countLoot++;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region ~~Rare search + name search
                            /* if (vb_LootType_rare) // selected rare
                             {
                                 if (upd.tmpLootItem.Item.Template.Rarity == JsonType.ELootRarity.Rare)
                                 {
                                     if (vs_search == "")
                                     {
                                         upd.tItemsList.Add(upd.tmpLootItem);
                                         countLoot++;
                                     }
                                     else
                                     {
                                         if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(vs_search) >= 0)
                                         {
                                             upd.tItemsList.Add(upd.tmpLootItem);
                                             countLoot++;
                                         }
                                     }
                                 }
                             }*/
                            #endregion
                            #region ~~Common search + name search
                            /*if (vb_LootType_common) // selected common
                            {
                                if (upd.tmpLootItem.Item.Template.Rarity == JsonType.ELootRarity.Common)
                                {
                                    if (vs_search == "")
                                    {
                                        upd.tItemsList.Add(upd.tmpLootItem);
                                        countLoot++;
                                    }
                                    else
                                    {
                                        if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(vs_search) >= 0)
                                        {
                                            upd.tItemsList.Add(upd.tmpLootItem);
                                            countLoot++;
                                        }
                                    }
                                }
                            }*/
                            #endregion
                            #region ~~Everything disabled and/or search for item
                            if (!Settings.CItem.DisplayQuestItems && !Settings.CItem.DisplayLootSrare && !Settings.CItem.DisplayLootCustom1 && !Settings.CItem.DisplayLootCustom2 && !Settings.CItem.DisplayLootCustom3)
                            {
                                if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                {
                                    upd.tItemsList.Add(upd.tmpLootItem);
                                    countLoot++;
                                }
                                else if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                {
                                    upd.tItemsList.Add(upd.tmpLootItem);
                                    countLoot++;
                                }
                            }
                            #endregion
                        }
                        #region ~~Custom1 file search + name search
                        if (Settings.CItem.DisplayLootCustom1) // custom search from file
                        {
                            if (Settings.CItem.CustomDistance1 > tempDistanceToObject) // limit number of items on screen also checking distance
                            {
                                if (Array.IndexOf(IDTable1, upd.tmpLootItem.Item.TemplateId) != -1 || Array.IndexOf(IDTable1, upd.tmpLootItem.Item.Template._parent) != -1)
                                {
                                    if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                    {
                                        //Found item now adding
                                        // light brown as color
                                        upd.tItemsList.Add(upd.tmpLootItem);
                                        countLoot++;
                                    }
                                    else
                                    {
                                        if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                        {
                                            upd.tItemsList.Add(upd.tmpLootItem);
                                            countLoot++;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #region ~~Custom2 file search + name search
                        if (Settings.CItem.DisplayLootCustom2) // custom search from file
                        {
                            if (Settings.CItem.CustomDistance2 > tempDistanceToObject) // limit number of items on screen also checking distance
                            {
                                if (Array.IndexOf(IDTable2, upd.tmpLootItem.Item.TemplateId) != -1 || Array.IndexOf(IDTable2, upd.tmpLootItem.Item.Template._parent) != -1)
                                {
                                    if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                    {
                                        //Found item now adding
                                        // light brown as color
                                        upd.tItemsList.Add(upd.tmpLootItem);
                                        countLoot++;
                                    }
                                    else
                                    {
                                        if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                        {
                                            upd.tItemsList.Add(upd.tmpLootItem);
                                            countLoot++;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        #region ~~Custom3 file search + name search
                        if (Settings.CItem.DisplayLootCustom3) // custom search from file
                        {
                            if (Settings.CItem.CustomDistance3 > tempDistanceToObject) // limit number of items on screen also checking distance
                            {
                                if (Array.IndexOf(IDTable3, upd.tmpLootItem.Item.TemplateId) != -1 || Array.IndexOf(IDTable3, upd.tmpLootItem.Item.Template._parent) != -1)
                                {
                                    if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                    {
                                        //Found item now adding
                                        // light brown as color
                                        upd.tItemsList.Add(upd.tmpLootItem);
                                        countLoot++;
                                    }
                                    else
                                    {
                                        if ((upd.tmpLootItem.Item.ShortName.Localized() + upd.tmpLootItem.Item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                        {
                                            upd.tItemsList.Add(upd.tmpLootItem);
                                            countLoot++;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
                upd.ItemsList = upd.tItemsList;
            }
            catch { errorCounterU++; }
        }
        private void isOnGui()
        {
            if (!LocalPlayer.isLocalPlayerLoaded) return;

            upd.tmpCamera = Camera.main;
            if (!upd.tmpCamera)
                return;

            var e = upd.ItemsList.GetEnumerator();
            while (e.MoveNext())
            {
                iItem.tmpItem = e.Current;
                iItem.location = upd.tmpCamera.WorldToScreenPoint(iItem.tmpItem.transform.position);
                if (Function.onScreenStrict(iItem.location))
                {
                    iItem.onScreenY = Screen.height - iItem.location.y;
                    iItem.distance = (int)Vector3.Distance(Camera.main.transform.position, iItem.tmpItem.transform.position);
                    iItem.LabelSize.fontSize = MaociScreen.Scale.FontSizer(iItem.distance);
                    iItem.tempColor = Function.GetItemColor(iItem.tmpItem);
                    Print.Special.DrawText(iItem.tmpItem.Item.ShortName.Localized(), iItem.location.x, iItem.onScreenY - iItem.LabelSize.fontSize, vec, iItem.LabelSize, iItem.tempColor);
                    Print.Special.DrawText($"{iItem.distance.ToString()}m", iItem.location.x, iItem.onScreenY, vec, iItem.LabelSize, iItem.tempColor);
                }
            }
        }
        private class Function
        {
            public static Color GetItemColor(LootItem itm)
            {
                if (!Settings.CItem.DisplayLootSrare && !Settings.CItem.DisplayLootCustom1 && !Settings.CItem.DisplayLootCustom2 && !Settings.CItem.DisplayLootCustom3)
                { // nothing is enabled to color staff so return basic color
                    return Settings.CItem.DrawBaseColor;
                }
                if (Settings.CItem.DisplayLootSrare && itm.Item.Template.Rarity == JsonType.ELootRarity.Superrare)
                { // enabled super rare and item is super rare
                    return Settings.CItem.DrawSRare;
                }
                if (Settings.CItem.DisplayLootCustom1 && (Array.IndexOf(IDTable1, itm.Item.TemplateId) != -1 || Array.IndexOf(IDTable1, itm.Item.Template._parent) != -1))
                { // enabled rare and item is rare
                    return Settings.CItem.DrawCustom1;
                }
                if (Settings.CItem.DisplayLootCustom2 && (Array.IndexOf(IDTable2, itm.Item.TemplateId) != -1 || Array.IndexOf(IDTable2, itm.Item.Template._parent) != -1))
                { // enabled common and item is common
                    return Settings.CItem.DrawCustom2;
                }
                if (Settings.CItem.DisplayLootCustom3 && (Array.IndexOf(IDTable3, itm.Item.TemplateId) != -1 || Array.IndexOf(IDTable3, itm.Item.Template._parent) != -1))
                { // enabled custom search and item is on list (as parent or template id)
                    return Settings.CItem.DrawCustom3;
                }
                return Settings.CItem.DrawBaseColor;
            }
            public static bool onScreenStrict(Vector3 V)
            {
                if (V.x > 0.01f && V.y > 0.01f && V.x < Screen.width && V.y < Screen.height && V.z > 0.01f)
                    return true;
                return false;
            }
        }
        private static class upd
        {
            public static List<LootItem>.Enumerator temporalItemsEnum;
            public static Type ObservedLootItem = new ObservedLootItem().GetType();
            public static Type LootItem = new LootItem().GetType();
            public static List<LootItem> ItemsList;
            public static List<LootItem> tItemsList;
            // public static List<LootItem>.Enumerator tempItemsEnum;
            public static LootItem tmpLootItem;
            public static Camera tmpCamera = null;
        }
        private static class iItem
        {
            public static LootItem tmpItem = null;
            public static Vector3 location = Vector3.zero;
            public static int distance = 0;
            public static float onScreenY = 0f;
            public static Color tempColor;
            public static int FontSize = MaociScreen.Scale.FontSize;
            public static GUIStyle LabelSize = new GUIStyle();
        }
    }
}
