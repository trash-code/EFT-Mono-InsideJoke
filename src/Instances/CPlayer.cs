using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Reflection;
using Exception5.Functions;

namespace Exception6.Instances
{
    public class BasePlayer {
        public Vector3 PreviousPosition;
        public int Distance;
        public bool onScreen;
        public string Health;
        public string EquipmentValue;
        public string ItemInHands;
        public string WeaponAmmoCounter;
        public Vector3 onScreenHead;
        public Vector3 onScreenNeck;
        public Player player;
    }
    public class CPlayer : MonoBehaviour
    {
        private static List<Player>.Enumerator l_Player_temp;
        private static List<BasePlayer> l_Player = new List<BasePlayer>();
        public static string NoGroup = "";

        [ObfuscationAttribute(Exclude = true)]
        public void Update()
        {
            isOnUpdate();
        }
        [ObfuscationAttribute(Exclude = true)]
        public void OnGUI()
        {
            isOnGui();
        }
        private static Player tPlayer = null;
        private static BasePlayer tBasePlayer = null;
        private void isOnUpdate()
        {
            if (!Settings.CPlayer.Enable)
                return;
            if (LocalPlayer.Base == null)
            {
                if (NoGroup != "")
                    NoGroup = "";
                return;
            }
            
            if (CGameWorld.Base.RegisteredPlayers == null) return;
            if (CGameWorld.Base.RegisteredPlayers.Count < 1) return;

            l_Player_temp = CGameWorld.Base.RegisteredPlayers.GetEnumerator();
            l_Player = new List<BasePlayer>();
            while (l_Player_temp.MoveNext())
            {
                var current = l_Player_temp.Current;
                if (current == null) continue;
                if (Camera.main == null) continue;
                if (Settings.CPlayer.BaseDistance > Vector3.Distance(Camera.main.transform.position, current.Transform.position))
                {
                    try
                    {
                        tBasePlayer = new BasePlayer();
                        tBasePlayer.player = current;
                        tBasePlayer.onScreenHead = Camera.main.WorldToScreenPoint(current.PlayerBones.Head.position);
                        tBasePlayer.onScreenNeck = Camera.main.WorldToScreenPoint(current.PlayerBones.Neck.position);
                        tBasePlayer.Distance = (int)Vector3.Distance(Camera.main.transform.position, current.Transform.position);
                        tBasePlayer.onScreen = MaociScreen.onScreenStrict(Camera.main.WorldToScreenPoint(current.Transform.position));
                        tBasePlayer.Health = Function.GetPlayerTotalHealth(current);
                        tBasePlayer.EquipmentValue = Function.CalculateEQValue(current);
                        tBasePlayer.ItemInHands = current.HandsController.Item.ShortName.Localized();
                        if (tBasePlayer.ItemInHands.IndexOf("Short") != -1)
                            tBasePlayer.ItemInHands = "";
                        try
                        {
                            tBasePlayer.WeaponAmmoCounter = " |" + (current.Weapon.GetCurrentMagazineCount() + current.Weapon.ChamberAmmoCount).ToString() + "|";
                        }
                        catch 
                        {
                            tBasePlayer.WeaponAmmoCounter = "";
                        }
                        tBasePlayer.PreviousPosition = current.Transform.position;
                        l_Player.Add(tBasePlayer);
                    }
                    catch { }
                }
            }
        }
        private void isOnGui()
        {
            try
            {
                if (!Settings.CPlayer.Enable)
                    return;
                if (LocalPlayer.Base == null)
                    return;
                if (Event.current.type != EventType.Repaint)
                    return;
                Camera camera = Camera.main;
                if (!camera)
                    return;

                var e = l_Player.GetEnumerator();
                while (e.MoveNext())
                {
                    iPlayer.BPlayer = e.Current;
                    if (iPlayer.BPlayer.player == LocalPlayer.Base)
                        continue;
                    #region [Snap.Lines]
                    if (Settings.CPlayer.DrawSnapLines)
                    {
                        iPlayer.Name = Function.PlayerName(iPlayer.BPlayer.player, ref iPlayer.PlayerType);
                        iPlayer.UserColor = Function.PlayerColor(iPlayer.PlayerType);
                        Function.SnapLines(iPlayer.BPlayer.player, iPlayer.UserColor);
                    }
                    #endregion
                    if (MaociScreen.onScreenYZ(Camera.main.WorldToScreenPoint(iPlayer.BPlayer.player.Transform.position)))
                    {
                        //fix for colors not holds after leaving screen
                        if (!Settings.CPlayer.DrawSnapLines)
                        {
                            iPlayer.Name = Function.PlayerName(iPlayer.BPlayer.player, ref iPlayer.PlayerType);
                            iPlayer.UserColor = Function.PlayerColor(iPlayer.PlayerType);
                        }
                        if (MaociScreen.onScreenStrict(Camera.main.WorldToScreenPoint(iPlayer.BPlayer.player.Transform.position)))
                        {
                            iPlayer.DistanceToTarget = (int)Vector3.Distance(Camera.main.transform.position, iPlayer.BPlayer.player.Transform.position);
                            iPlayer.HeadPos = iPlayer.BPlayer.onScreenHead;// Camera.main.WorldToScreenPoint(iPlayer.BPlayer.PlayerBones.Head.position);
                            iPlayer.NeckPos = iPlayer.BPlayer.onScreenNeck;// Camera.main.WorldToScreenPoint(iPlayer.BPlayer.PlayerBones.Neck.position);
                            iPlayer.HeadNeck_Abs = Math.Abs(iPlayer.HeadPos.y - iPlayer.NeckPos.y);
                            iPlayer.HeadOnScreen = Screen.height - iPlayer.HeadPos.y;

                            iPlayer.HeadSize = Math.Min(Math.Max(iPlayer.HeadNeck_Abs * 1.5f, 20f), 2f);
                            iPlayer.HeadSize = (iPlayer.HeadSize > 30f) ? 30f : iPlayer.HeadSize;
                            iPlayer.halfHeadSize = iPlayer.HeadSize / 2f;

                            iPlayer.TextStyle.fontSize = MaociScreen.Scale.FontSizer(iPlayer.DistanceToTarget);

                            iPlayer.label.disText_1 = 20f + iPlayer.HeadNeck_Abs * 5;
                            iPlayer.label.disText_2 = iPlayer.label.disText_1 + iPlayer.TextStyle.fontSize + 1;
                            iPlayer.label.disText_3 = iPlayer.label.disText_2 + iPlayer.TextStyle.fontSize + 1;

                            iPlayer.Health = iPlayer.BPlayer.Health;// Function.GetPlayerTotalHealth(iPlayer.BPlayer); // Health here 

                            if (iPlayer.PlayerType != Function.PlayerType.TeamMate)
                            {
                                Print.Special.DrawPoint(iPlayer.HeadPos.x - iPlayer.halfHeadSize, iPlayer.HeadOnScreen - iPlayer.halfHeadSize, iPlayer.HeadSize, Function.Colors.Red);
                            }
                            if (Settings.CPlayer.DrawBones)
                            {
                                Function.PlayerBones(iPlayer.DistanceToTarget, iPlayer.BPlayer.player);
                            }
                            #region [Text]
                            iPlayer.label.Text_1 = $"{iPlayer.Name} <{iPlayer.BPlayer.EquipmentValue}>";
                            iPlayer.label.Text_2 = $"[{iPlayer.DistanceToTarget}m] {iPlayer.Health}";
                            iPlayer.label.Text_3 = "";
                            #endregion
                            #region [TRY-DecodeWeaponName]

                            iPlayer.label.Text_3 = iPlayer.BPlayer.ItemInHands + iPlayer.BPlayer.WeaponAmmoCounter;
                            #endregion

                            iPlayer.TextStyle.normal.textColor = iPlayer.UserColor;
                            #region Slot 0 - Player Name
                            if (iPlayer.label.Text_1 != "")
                            {
                                iPlayer.label.vecText_1 = GUI.skin.GetStyle(iPlayer.label.Text_1).CalcSize(iPlayer.label.GuiText_1);
                                iPlayer.label.sizeText_1 = iPlayer.label.vecText_1.x;
                                Print.Special.DrawText(
                                    iPlayer.label.Text_1,
                                    iPlayer.HeadPos.x - iPlayer.label.sizeText_1 / 2f,
                                    iPlayer.HeadOnScreen - iPlayer.label.disText_1,
                                    iPlayer.label.vecText_1,
                                    iPlayer.TextStyle,
                                    iPlayer.UserColor
                                );
                            }
                            #endregion
                            #region Slot 1 - distance, health
                            iPlayer.label.vecText_2 = GUI.skin.GetStyle(iPlayer.label.Text_2).CalcSize(iPlayer.label.GuiText_2);
                            iPlayer.label.sizeText_2 = iPlayer.label.vecText_2.x;
                            Print.Special.DrawText(
                                iPlayer.label.Text_2,
                                iPlayer.HeadPos.x - iPlayer.label.sizeText_2 / 2f,
                                iPlayer.HeadOnScreen - iPlayer.label.disText_2,
                                iPlayer.label.vecText_2,
                                iPlayer.TextStyle,
                                iPlayer.UserColor
                            );
                            #endregion
                            #region Slot 2 - Weapon Name
                            if (iPlayer.label.Text_3 != "")
                            {
                                iPlayer.label.vecText_3 = GUI.skin.GetStyle(iPlayer.label.Text_3).CalcSize(iPlayer.label.GuiText_3);
                                iPlayer.label.sizeText_3 = iPlayer.label.vecText_3.x;
                                Print.Special.DrawText(
                                    iPlayer.label.Text_3,
                                    iPlayer.HeadPos.x - iPlayer.label.sizeText_3 / 2f,
                                    iPlayer.HeadOnScreen - iPlayer.label.disText_3,
                                    iPlayer.label.vecText_3,
                                    iPlayer.TextStyle,
                                    iPlayer.UserColor
                                );
                            }
                            #endregion
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #region player temp data :)
        private static class iPlayer
        {
            public static BasePlayer BPlayer;
            public static string Name;
            //public static string Group;
            public static Color UserColor;
            public static int DistanceToTarget;
            public static Vector3 HeadPos;
            public static Vector3 NeckPos;
            public static float HeadOnScreen;
            public static float HeadNeck_Abs;
            public static float HeadSize;
            public static float halfHeadSize;
            //public static int FontSize;
            public static GUIStyle TextStyle = new GUIStyle { fontSize = 12 };
            public static string Health;
            public static Function.PlayerType PlayerType;
            public static EFT.InventoryLogic.Item equipItem = null;
            public static IEnumerator<EFT.InventoryLogic.Item> equipItemList = null;
            public static class label
            {
                public static string Text_1;
                public static string Text_2;
                public static string Text_3;
                public static Vector2 vecText_1 = Vector2.zero;
                public static Vector2 vecText_2 = Vector2.zero;
                public static Vector2 vecText_3 = Vector2.zero;
                public static float sizeText_1 = 0f;
                public static float sizeText_2 = 0f;
                public static float sizeText_3 = 0f;
                public static float disText_1; // first text from bottom
                public static float disText_2; // middle text
                public static float disText_3; // top text
                private static GUIContent tempGuiContent = new GUIContent();
                public static GUIContent GuiText_1
                {
                    get
                    {
                        tempGuiContent.text = Text_1;
                        return tempGuiContent;
                    }
                }
                public static GUIContent GuiText_2
                {
                    get
                    {
                        tempGuiContent.text = Text_2;
                        return tempGuiContent;
                    }
                }
                public static GUIContent GuiText_3
                {
                    get
                    {
                        tempGuiContent.text = Text_3;
                        return tempGuiContent;
                    }
                }
            }
        }
        #endregion
        public static class Function
        {
            private static List<EFT.InventoryLogic.Item> listToExclude = null;
            private static EFT.InventoryLogic.Item TempItem = null;
            public static string CalculateEQValue(Player player)
            {
                if (LocalPlayer.Base == null)
                    return "Not Loaded";
                int value = 0;
                try
                {
                    listToExclude = new List<EFT.InventoryLogic.Item>();
                    iPlayer.equipItemList = player.Profile.Inventory.Equipment.GetAllItems().GetEnumerator();
                    while (iPlayer.equipItemList.MoveNext())
                    {
                        TempItem = iPlayer.equipItemList.Current;
                        value += TempItem.Template.CreditsPrice;
                        if (TempItem.Template._parent == "5448bf274bdc2dfc2f8b456a")
                        {
                            var x = TempItem.GetAllItems().GetEnumerator();
                            while (x.MoveNext())
                            {
                                value -= x.Current.Template.CreditsPrice;
                            }
                        }
                    }
                    return (value / 1000).ToString() + "|k";
                }
                catch { return "0"; }
            }
            public static bool inGroup(Player p)
            {
                if (LocalPlayer.Base == null)
                    return false;
                string LocalGroup = LocalPlayer.Base.Profile.Info.GroupId;
                return LocalGroup == p.Profile.Info.GroupId && LocalGroup != "0" && LocalGroup != "" && LocalGroup != null;
            }
            public static class Colors
            {
                public static Color Red = new Color(1f, 0f, 0f, 1f);
                public static Color NotVisible = new Color(1f, 1f, 1f, 0.4f);
                public static Color Visible = new Color(1f, 1f, 1f, 1f);
                public static Color Red08 = new Color(1f, 0f, 0f, 0.8f);
                public static Color Green06 = new Color(0f, 1f, 0f, .6f);
                public static Color Player = new Color(1f, 0.75f, 0.0f, 1.0f);
                public static Color Group = new Color(0f, 0.8f, 1.0f, 1.0f);
                public static Color Scav = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                public static Color pScav = new Color(1f, 0.8f, 0.6f, 1f);
                public static Color Boss = new Color(1f, 0.8f, 0.6f, 1f);
                public static Color Follower = new Color(1f, 0.8f, 0.6f, 1f);
                public static Color iThrowables = new Color(0f, 1.0f, 0f, 1.0f);
                public static Color iLootitem = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                public static Color iMedicaments = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                public static Color iValuables = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                public static Color Corpse = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            }
            private static Vector2 vectorA = Vector2.zero;
            private static Vector2 vectorB = Vector2.zero;
            public static void SnapLines(Player p, Color c)
            {
                Vector3 w2s = Camera.main.WorldToScreenPoint(p.PlayerBones.RootJoint.position);
                if (w2s.y > 0.01f && w2s.z > 0.01f)
                {
                    vectorA.x = MaociScreen.width_half;
                    vectorA.y = MaociScreen.height;
                    vectorB.x = w2s.x;
                    vectorB.y = MaociScreen.height - w2s.y;
                    Print.Line.Draw(vectorA, vectorB, c);
                }
            }
            public static string GetPlayerTotalHealth(Player p)
            {
                return ((int)(p.HealthController.GetBodyPartHealth(EBodyPart.Common).Current)).ToString() + " hp";
            }
            public enum PlayerType
            {
                Scav,
                PlayerScav,
                Player,
                TeamMate,
                Follower,
                Boss
            }
            private static Color boneColor;
            private static float boneThicc = 2f;
            private static class TempBones
            {
                public static Vector3 RightPalm;
                public static Vector3 LeftPalm;
                public static Vector3 LeftShoulder;
                public static Vector3 RightShoulder;
                public static Vector3 Neck;
                public static Vector3 Pelvis;
                public static Vector3 KickingFoot;
                public static Vector3 LeftFoot;
                public static Vector3 LeftForearm;
                public static Vector3 RightForearm;
                public static Vector3 LeftCalf;
                public static Vector3 RightCalf;
                public static Vector3 GunPointingAt;
                public static Vector3 GunPointingAtForward;
            }
            public static void PlayerBones(float distance, Player player)
            {
                if (distance < Settings.CPlayer.BonesDistance)
                {
                    TempBones.RightPalm = Camera.main.WorldToScreenPoint(player.PlayerBones.RightPalm.position);
                    TempBones.LeftPalm = Camera.main.WorldToScreenPoint(player.PlayerBones.LeftPalm.position);
                    TempBones.LeftShoulder = Camera.main.WorldToScreenPoint(player.PlayerBones.LeftShoulder.position);
                    TempBones.RightShoulder = Camera.main.WorldToScreenPoint(player.PlayerBones.RightShoulder.position);
                    TempBones.Neck = Camera.main.WorldToScreenPoint(player.PlayerBones.Neck.position);
                    TempBones.Pelvis = Camera.main.WorldToScreenPoint(player.PlayerBones.Pelvis.position);
                    TempBones.KickingFoot = Camera.main.WorldToScreenPoint(player.PlayerBones.KickingFoot.position);
                    TempBones.LeftFoot = Camera.main.WorldToScreenPoint(Functions.PlayerBones.GetBoneById(player, 18));
                    TempBones.LeftForearm = Camera.main.WorldToScreenPoint(Functions.PlayerBones.GetBoneById(player, 91));
                    TempBones.RightForearm = Camera.main.WorldToScreenPoint(Functions.PlayerBones.GetBoneById(player, 112));
                    TempBones.LeftCalf = Camera.main.WorldToScreenPoint(Functions.PlayerBones.GetBoneById(player, 17));
                    TempBones.RightCalf = Camera.main.WorldToScreenPoint(Functions.PlayerBones.GetBoneById(player, 22));
                    boneColor = (Raycast.isVisible(iPlayer.BPlayer.player)) ? Colors.Visible : Colors.NotVisible;
                    /*
                    #region look direction - is buggy
                    var PositionOfLook = FUNC.W2S(player.PlayerBones.Head.position);
                    var PositionOfLookFront = FUNC.W2S(player.PlayerBones.Head.position + player.PlayerBones.Head.up * 2f);
                    Drawing.Bone.Draw(PositionOfLookFront, PositionOfLook, new Color(0f,1f,0f,.6f));
                    #endregion
                    */
                    #region Drawed Weapon Line -> best for looking at direction
                    TempBones.GunPointingAt = Camera.main.WorldToScreenPoint(player.PlayerBones.WeaponRoot.position);
                    TempBones.GunPointingAtForward = Camera.main.WorldToScreenPoint(player.PlayerBones.WeaponRoot.position - player.PlayerBones.WeaponRoot.up * 1.3f);
                    Print.Bone.Draw(TempBones.GunPointingAtForward, TempBones.GunPointingAt, Colors.Green06, boneThicc);
                    #endregion

                    if (MaociScreen.onScreenStrict(TempBones.Neck) && MaociScreen.onScreenStrict(TempBones.Pelvis))
                        Print.Bone.Draw(TempBones.Neck, TempBones.Pelvis, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.LeftShoulder) && MaociScreen.onScreenStrict(TempBones.LeftForearm))
                        Print.Bone.Draw(TempBones.LeftShoulder, TempBones.LeftForearm, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.RightShoulder) && MaociScreen.onScreenStrict(TempBones.RightForearm))
                        Print.Bone.Draw(TempBones.RightShoulder, TempBones.RightForearm, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.LeftForearm) && MaociScreen.onScreenStrict(TempBones.LeftPalm))
                        Print.Bone.Draw(TempBones.LeftForearm, TempBones.LeftPalm, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.RightForearm) && MaociScreen.onScreenStrict(TempBones.RightPalm))
                        Print.Bone.Draw(TempBones.RightForearm, TempBones.RightPalm, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.RightShoulder) && MaociScreen.onScreenStrict(TempBones.LeftShoulder))
                        Print.Bone.Draw(TempBones.RightShoulder, TempBones.LeftShoulder, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.LeftCalf) && MaociScreen.onScreenStrict(TempBones.Pelvis))
                        Print.Bone.Draw(TempBones.LeftCalf, TempBones.Pelvis, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.RightCalf) && MaociScreen.onScreenStrict(TempBones.Pelvis))
                        Print.Bone.Draw(TempBones.RightCalf, TempBones.Pelvis, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.LeftCalf) && MaociScreen.onScreenStrict(TempBones.LeftFoot))
                        Print.Bone.Draw(TempBones.LeftCalf, TempBones.LeftFoot, boneColor, boneThicc);
                    if (MaociScreen.onScreenStrict(TempBones.RightCalf) && MaociScreen.onScreenStrict(TempBones.KickingFoot))
                        Print.Bone.Draw(TempBones.RightCalf, TempBones.KickingFoot, boneColor, boneThicc);
                }
            }
            public static Color PlayerColor(PlayerType playerType)
            {
                switch (playerType)
                {
                    case PlayerType.TeamMate: // this one must be above Player
                        return Colors.Group;
                    case PlayerType.Player:
                        return Colors.Player;
                    case PlayerType.Scav:
                        return Colors.Scav;
                    case PlayerType.PlayerScav:
                        return Colors.pScav;
                    case PlayerType.Boss:
                        return Colors.Boss;
                    case PlayerType.Follower:
                        return Colors.Follower;
                    default:
                        return Colors.pScav;
                }
            }
            public static string PlayerName(Player player, ref PlayerType playerType)
            {
                if (player.Profile.Info.RegistrationDate <= 0)
                {
                    if (player.Profile.Info.Settings.Role.ToString().IndexOf("boss") != -1)
                    {
                        playerType = PlayerType.Boss;
                        return "BOSS";
                    }
                    else if (player.Profile.Info.Settings.Role.ToString().IndexOf("follower") != -1)
                    {
                        playerType = PlayerType.Follower;
                        return "Follower";
                    }
                    else if (player.Profile.Info.Settings.Role.ToString().ToLower().IndexOf("pmcbot") != -1)
                    {
                        playerType = PlayerType.Scav;
                        return "Raider";
                    }
                    else
                    {
                        playerType = PlayerType.Scav;
                        return "";
                    }
                }
                else if (Function.inGroup(player))
                {
                    playerType = PlayerType.TeamMate;
                    if (Settings.CPlayer.ReplaceToPlayerSide)
                    {
                        return "<T>";
                    }
                    return player.Profile.Info.Nickname;
                }
                else if (player.Profile.Info.Side == EPlayerSide.Savage)
                {
                    playerType = PlayerType.PlayerScav;
                    return "player";
                }
                playerType = PlayerType.Player;
                if (Settings.CPlayer.ReplaceToPlayerSide)
                {
                    return player.Profile.Info.Side.ToString() + " [" + player.Profile.Info.Level.ToString() + "]";
                }
                else
                {
                    return player.Profile.Info.Nickname + " [" + player.Profile.Info.Level.ToString() + "]";
                }


            }
        }
    }

}