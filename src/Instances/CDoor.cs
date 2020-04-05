using EFT.Interactive;
using EFT.InventoryLogic;
using Exception5.Functions;
using Exception6.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using MaociScreen = Exception6.Functions.Screen;

namespace Exception6.Instances
{
    public class CDoor : MonoBehaviour
    {
        public static int errorCount = 0;
        public static int errorCount1 = 0;

        private static Vector2 vec = new Vector2(100f, 15f);
        private static WorldInteractiveObject autoSelectedCloseDoor = null;
        private static IEnumerable<WorldInteractiveObject> inumDoorList = null;
        private static List<WorldInteractiveObject> DoorList;
        private static List<WorldInteractiveObject> TempList;
        private static List<WorldInteractiveObject> tmpDoorList;
        private GUIStyle LabelSize = new GUIStyle { fontSize = MaociScreen.Scale.FontSize };
        [ObfuscationAttribute(Exclude = true)]
        void Update()
        {
            if (Settings.CDoor.Enable)
                isOnUpdate();
        }
        [ObfuscationAttribute(Exclude = true)]
        void OnGUI()
        {
            if (Settings.CDoor.Enable)
                isOnGui();
        }
        private static Vector3 OldDoorPosition = Vector3.zero;
        private static Item LocalPlayerKey = null;
        private static string SelectedKeyID = "";

        public static bool KeyRestored = true;
        public static bool CardRestored = true;
        private IEnumerator RevokeMechanicalKey(float time)
        {
            yield return new WaitForSeconds(time);
            LocalPlayerKey.Template._id = SelectedKeyID;
            KeyRestored = true;
        }
        private static List<string> excludedList = new List<string>() {
            "5c1d0c5f86f7744bb2683cf0", // Blue
            "5c1d0d6d86f7744bb2683e1f", // Yellow
            "5c1d0dc586f7744baf2e7b79", // Green
            "5c1d0efb86f7744baf2e7b7b", // Red
            "5c1e495a86f7743109743dfb", // Violet
            "5c1d0f4986f7744bb01837fa", // Black
            "5e42c81886f7742a01529f57", // Object 11
            "5e42c83786f7742a021fdf3c"  // Object 21
        };
        // 5d08d21286f774736e7c94c3 // 

        private void isOnUpdate()
        {
            if (LocalPlayer.Base == null)
                return;

            inumDoorList = LocationScene.GetAllObjects<WorldInteractiveObject>(false);
            try
            {
                TempList = Prioritize.SortData(inumDoorList.Cast<WorldInteractiveObject>().ToList());
                tmpDoorList = new List<WorldInteractiveObject>();
                for (int i = 0; i < TempList.Count; i++)
                {
                    if (TempList[i] is Door || TempList[i] is LootableContainer)
                    {
                        if (TempList[i].KeyId == "") continue;
                        //if (excludedList.IndexOf(TempList[i].KeyId) == -1) continue;
                        if (Settings.CDoor.Distance < (int)Vector3.Distance(Camera.main.transform.position, TempList[i].transform.position)) continue;
                        //if (TempList[i].Id.IndexOf("autoId") == -1) continue;
                        tmpDoorList.Add(TempList[i]);
                    }
                }
                DoorList = tmpDoorList;
                autoSelectedCloseDoor = DoorList.FirstOrDefault();
                Settings.CDoor.SelectedDoor = autoSelectedCloseDoor.Id.Replace("autoId_", "").Replace("0", "");
            }
            catch { errorCount1++; }

            if (Input.GetKeyDown(KeyCode.F6) && KeyRestored)
            {
                LocalPlayerKey = LocalPlayer.Features.GetLocalPlayerKeys();
                if (LocalPlayerKey != default(Item) && autoSelectedCloseDoor.KeyId != "")
                {
                    SelectedKeyID = LocalPlayerKey.Template._id;
                    KeyRestored = false;
                    LocalPlayerKey.Template._id = autoSelectedCloseDoor.KeyId;
                    StartCoroutine(RevokeMechanicalKey(5f));
                }
            }
        }
        private static int DoorDistance = 0;
        private static Vector3 Position;
        private static string KeyCardName = "";
        private void isOnGui()
        {
            if (LocalPlayer.Base == null)
            {
                return;
            }
            try
            {
                for (int i = 0; i < DoorList.Count; i++)
                {
                    Position = Camera.main.WorldToScreenPoint(DoorList[i].transform.position);
                    if (MaociScreen.onScreenStrict(Position))
                    {
                        KeyCardName = "";
                        if (excludedList.IndexOf(DoorList[i].KeyId) != -1)
                        {
                            KeyCardName = " (Card: " + GetKeyCardName(DoorList[i].KeyId) + ")";
                        }
                        Print.Special.DrawText(
                            $"{DoorDistance.ToString()}m" + KeyCardName,
                                        Position.x,
                                        MaociScreen.height - Position.y,
                                        vec,
                                        LabelSize,
                        ((Settings.CDoor.SelectedDoor == DoorList[i].Id.Replace("autoId_", "").Replace("0", "")) ? Settings.CDoor.DrawActiveColor : Settings.CDoor.DrawColor));
                    }
                }
            }
            catch { errorCount++; }
        }
        private static string GetKeyCardName(string ID)
        {
            switch (ID)
            {
                case "5c1d0c5f86f7744bb2683cf0":
                    return "Blue";
                case "5c1d0d6d86f7744bb2683e1f":
                    return "Yellow";
                case "5c1d0dc586f7744baf2e7b79":
                    return "Green";
                case "5c1d0efb86f7744baf2e7b7b":
                    return "Red";
                case "5c1e495a86f7743109743dfb":
                    return "Violet";
                case "5c1d0f4986f7744bb01837fa":
                    return "Black";
                case "5e42c81886f7742a01529f57":
                    return "Object 11";
                case "5e42c83786f7742a021fdf3c":
                    return "Object 21";
                default:
                    return "?ID?";
            }
        }
        //#endif
    }
}
