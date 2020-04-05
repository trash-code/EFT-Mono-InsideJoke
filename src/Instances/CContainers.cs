using EFT.Interactive;
using EFT.InventoryLogic;
using Exception5.Functions;
using Exception6;
using Exception6.Functions;
using Exception6.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ContainerData = GInterface160;
using ContainerLocation = EFT.GameWorld.GStruct65;

namespace Exception6.Instances
{
    class CContainers : MonoBehaviour
    {
        private static IEnumerable<WorldInteractiveObject> WorldList;
        private static List<WorldInteractiveObject> tmpContainerList;
        private static List<LootableContainer> ContainerList;
        private static List<LootableContainer> DrawingList;
        private static List<LootItem>.Enumerator lootlist;
        private static Vector2 vec = Vector2.zero;
        [ObfuscationAttribute(Exclude = true)]
        public void Update()
        {
            if (!Settings.CContainer.Enable) // if not activated
                return;
            if (LocalPlayer.Base == null) // if gameworld is not initiated already
                return;
            try
            {
                lootlist = CGameWorld.Base.LootItems.GetValuesEnumerator().GetEnumerator();
                WorldList = LocationScene.GetAllObjects<WorldInteractiveObject>(false);
                tmpContainerList = Prioritize.SortData(WorldList.Cast<WorldInteractiveObject>().ToList());
                ContainerList = new List<LootableContainer>();
                for (int i = 0; i < tmpContainerList.Count; i++)
                {
                    if (tmpContainerList[i] is LootableContainer)
                    {
                        var tempDistanceToObject = Vector3.Distance(Camera.main.transform.position, tmpContainerList[i].transform.position);
                        if (tempDistanceToObject < Settings.CContainer.Distance)
                        {

                            var tempLootContainer = tmpContainerList[i] as LootableContainer;
                            IEnumerable<Item> tmpList = tempLootContainer.ItemOwner.RootItem.GetAllItems(false);

                            if (Settings.CContainer.SearchFor == "" || !Settings.switches.sb_searchForContainers)
                            {

                                var enumerator = tmpList.GetEnumerator();
                                int count = 0;
                                while (enumerator.MoveNext())
                                {
                                    if (count == 0) {
                                        count++; continue;
                                    }
                                    var item = enumerator.Current;
                                        #region ~~Super rare search + name search
                                        if (Settings.CItem.DisplayLootSrare) // selected super rare
                                        {
                                            if (item.Template.Rarity == JsonType.ELootRarity.Superrare)
                                            {
                                                if (Settings.CItem.SearchFor == "")
                                                {
                                                    ContainerList.Add(tempLootContainer);
                                                    continue;
                                                }
                                                else
                                                {
                                                    if ((item.ShortName.Localized() + item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                                    {
                                                        ContainerList.Add(tempLootContainer);
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        #region ~~Everything disabled and/or search for item
                                        if (!Settings.CItem.DisplayLootSrare && !Settings.CItem.DisplayLootCustom1 && !Settings.CItem.DisplayLootCustom2 && !Settings.CItem.DisplayLootCustom3)
                                        {
                                            if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                            {
                                                ContainerList.Add(tempLootContainer);
                                                continue;
                                            }
                                            else if ((item.ShortName.Localized() + item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                            {
                                                ContainerList.Add(tempLootContainer);
                                                continue;
                                            }
                                        }
                                        #endregion
                                    #region ~~Custom1 file search + name search
                                    if (Settings.CItem.DisplayLootCustom1) // custom search from file
                                    {
                                        if (Settings.CItem.CustomDistance1 > tempDistanceToObject) // limit number of items on screen also checking distance
                                        {
                                            if (Array.IndexOf(CItems.IDTable1, item.TemplateId) != -1 || Array.IndexOf(CItems.IDTable1, item.Template._parent) != -1)
                                            {
                                                if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                                {
                                                    ContainerList.Add(tempLootContainer);
                                                }
                                                else
                                                {
                                                    if ((item.ShortName.Localized() + item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                                    {
                                                        ContainerList.Add(tempLootContainer);
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
                                            if (Array.IndexOf(CItems.IDTable2, item.TemplateId) != -1 || Array.IndexOf(CItems.IDTable2, item.Template._parent) != -1)
                                            {
                                                if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                                {
                                                    ContainerList.Add(tempLootContainer);
                                                    continue;
                                                }
                                                else
                                                {
                                                    if ((item.ShortName.Localized() + item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                                    {
                                                        ContainerList.Add(tempLootContainer);
                                                        continue;
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
                                            if (Array.IndexOf(CItems.IDTable3, item.TemplateId) != -1 || Array.IndexOf(CItems.IDTable3, item.Template._parent) != -1)
                                            {
                                                if (Settings.CItem.SearchFor == "" || !Settings.switches.sb_searchForItems)
                                                {
                                                    ContainerList.Add(tempLootContainer);
                                                    continue;
                                                }
                                                else
                                                {
                                                    if ((item.ShortName.Localized() + item.Name.Localized()).ToLower().IndexOf(Settings.CItem.SearchFor) >= 0)
                                                    {
                                                        ContainerList.Add(tempLootContainer);
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                if (tempLootContainer.ItemOwner.Name.Localized().IndexOf(Settings.CContainer.SearchFor) != -1)
                                {
                                    ContainerList.Add(tempLootContainer);
                                    continue;
                                }
                            }
                        }
                    }
                }
                DrawingList = ContainerList;
            }
            catch { }
        }
        private static Camera tempCamera;
        private GUIStyle LabelSize = new GUIStyle { fontSize = 10 };
        private GUIStyle LabelSize_Internals = new GUIStyle { fontSize = 10 };
        [ObfuscationAttribute(Exclude = true)]
        public void OnGUI()
        {
            if (!Settings.CContainer.Enable)
                return;
            if (Event.current.type != EventType.Repaint)
                return;
            if (LocalPlayer.Base == null)
                return;
            tempCamera = Camera.main;
            if (!tempCamera)
                return;
            for (int i = 0; i < DrawingList.Count; i++)
            {
                if (func.onScreenStrict(tempCamera.WorldToScreenPoint(DrawingList[i].transform.position)))
                {
                    int amount = DrawingList[i].ItemOwner.RootItem.GetAllItems(false).Count<Item>() - 1;
                    if (amount != 0)
                    {
                        vec.x = 100f;
                        vec.y = 12f;
                        LabelSize.fontSize = MaociScreen.Scale.FontSizer(Vector3.Distance(Camera.main.transform.position, DrawingList[i].transform.position));
                        var loc = tempCamera.WorldToScreenPoint(DrawingList[i].transform.position);
                        Print.Special.DrawText(
                                DrawingList[i].ItemOwner.Name.Localized() + "|" + amount.ToString() + " item" + ((amount != 1) ? "s" : ""),
                                loc.x,
                                (float)Screen.height - loc.y,
                                vec,
                                LabelSize,
                                Settings.CContainer.DrawColor
                            );
                    }
                }
            }
        }
        private class func
        {
            public static bool onScreenStrict(Vector3 V)
            {
                if (V.x > 0.01f && V.y > 0.01f && V.x < Screen.width && V.y < Screen.height && V.z > 0.01f)
                    return true;
                return false;
            }
        }
    }
}
