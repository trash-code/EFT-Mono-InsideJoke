/* Created by TheMaoci */

using UnityEngine;
using Exception6.Instances;
using System.Reflection;
using Exception6.Functions;
using Exception5.Functions;
using System.Runtime.InteropServices;
using System;
using System.Collections;
//using static Exception6.Instances.CSAimH;

namespace Exception6
{
    
    public class Menu : MonoBehaviour
    {
        private readonly static string commitVersion = "31";
        private static Rect MenuDragWindow = new Rect(0, 0, Menus.Menu_0.width, 20);
        private static Vector2 prevWindowLoc = Vector2.zero;
        //private static Color circleColor = new Color(1f, 1f, 1f, 0.6f);
        private static string LocalPlayerTotalValue = "";
        private static string LocalPlayerWeaponStat = "";
        private static string LocalPlayerWeaponStatInChamber = "";
        public static GUIStyle labelStyle = new GUIStyle();
        #region Not Obfuscated UNITY INICIATORS
        [ObfuscationAttribute(Exclude = true)]
        void Awake()
        {
            onAwake(gameObject);
        }
        [ObfuscationAttribute(Exclude = true)]
        void Update()
        {
            onUpdate();
        }
        [ObfuscationAttribute(Exclude = true)]
        void OnGUI()
        {
            inTheGui();
        }
        #endregion
        private static void onAwake(GameObject gameObject)
        {
            ConfigLoader();
            labelStyle.normal.textColor = Color.white;
            labelStyle.fontSize = (int)(25 * MaociScreen.ratio);  
            
            gameObject.AddComponent<CGameWorld>();
            gameObject.AddComponent<CPlayer>();
            gameObject.AddComponent<CThrowables>();
            gameObject.AddComponent<CCorpse>();
            gameObject.AddComponent<CExfiltrationPoints>();
            gameObject.AddComponent<LocalPlayer>();
            gameObject.AddComponent<CAimH>();
            gameObject.AddComponent<CContainers>();
            gameObject.AddComponent<CItems>();
            gameObject.AddComponent<CDoor>();
            
        }
        private static void ConfigLoader() {
            LoadConfigs.LoadItemsConfig();
            LoadConfigs.LoadUserConfig();
            LoadConfigs.ApplySettings();
        }
        private static Rect tempRect = Rect.zero;
        private static Vector2 WeaponStatsPreviousSize = Vector2.zero;
        private static void onUpdate()
        {
            if (Input.GetKeyDown(Keys.MenuAct))
            {
                Settings.switches.vb_Menu = !Settings.switches.vb_Menu;
            }
            if (LocalPlayer.Base != null)
            {
                #if ReleaseMaoci
                if (Input.GetKeyDown(Keys.teleport.front))
                {
                    LocalPlayer.Base.Transform.position = LocalPlayer.Base.Transform.position + LocalPlayer.Base.Transform.forward/* * 1f*/;
                }

                if (Input.GetKeyDown(Keys.DoorsHotkey))
                    Settings.CDoor.Enable = !Settings.CDoor.Enable;

                if (Input.GetKeyDown(Keys.HelpMenuHotkey))
                    Settings.switches.vb_HelpMenu = true;
                if (Input.GetKeyUp(Keys.HelpMenuHotkey))
                    Settings.switches.vb_HelpMenu = false;

                if (Input.GetKeyDown(Keys.CiriHotkey))
                    Settings.switches.vb_ciriMode = true;
                if(Input.GetKeyUp(Keys.CiriHotkey))
                    Settings.switches.vb_ciriMode = false;
                #endif
                RunUpdateFeatures();
            }
            //if (WeaponStatsPreviousSize.x//
            if (prevWindowLoc.x != Menus.Menu_0.x || prevWindowLoc.y != Menus.Menu_0.y)
            {
                tempRect = Menus.Menu_1;
                tempRect.x = Menus.Menu_0.x + Menus.Menu_0.width + 1f;
                tempRect.y = Menus.Menu_0.y;
                Menus.Menu_1 = tempRect;
                tempRect = Menus.Menu_2;
                tempRect.x = Menus.Menu_1.x + Menus.Menu_1.width + 1f;
                tempRect.y = Menus.Menu_0.y;
                Menus.Menu_2 = tempRect;
                tempRect = Menus.Menu_3;
                tempRect.x = Menus.Menu_2.x + Menus.Menu_2.width + 1f;
                tempRect.y = Menus.Menu_0.y;
                Menus.Menu_3 = tempRect;
                prevWindowLoc.x = Menus.Menu_0.x;
                prevWindowLoc.y = Menus.Menu_0.y;
            }
        }
        private static Rect WeaponStats_ammo = Rect.zero;
        private static Rect WeaponStats_name = Rect.zero;
        private static Rect tempSaveRect = Rect.zero;
        private static Vector2 tempCalculateSize = Vector2.zero;
        private static Color BackupMainGUIColor;
        private static bool SettedNewStyle = false;
        //private static Texture2D backupOldStyle = GUI.skin.window.active.background;
        private static void inTheGui()
        {
            if (!SettedNewStyle)
            {
                if (MaociScreen.ratio > 1f)
                {
                    GUI.skin.window.fontSize = (int)(GUI.skin.window.fontSize * MaociScreen.ratio);
                    GUI.skin.label.fontSize = (int)(GUI.skin.label.fontSize * MaociScreen.ratio);
                    GUI.skin.button.fontSize = (int)(GUI.skin.button.fontSize * MaociScreen.ratio);
                    GUI.skin.textField.fontSize = (int)(GUI.skin.textField.fontSize * MaociScreen.ratio);
                }
                    ///Texture2D newGuiTexture = MaociScreen.CreateTexture2DWithColor(new Color(0f,0f,0f,0.7f));
                if (!Settings.variables.OldMenuStyle)
                {
                    GUI.skin.window.active.background = GUI.skin.box.active.background;
                    GUI.skin.window.normal.background = GUI.skin.box.normal.background;
                    GUI.skin.window.onActive.background = GUI.skin.box.onActive.background;
                    GUI.skin.window.onFocused.background = GUI.skin.box.onFocused.background;
                    GUI.skin.window.focused.background = GUI.skin.box.focused.background;
                    GUI.skin.window.onNormal.background = GUI.skin.box.onNormal.background;
                    GUI.skin.window.richText = true;
                }
                SettedNewStyle = true;
            }
            if (LocalPlayer.Base != null)
            {
                if (Settings.switches.m_AimFovRange)
                {
                    Print.Circle.Draw((int)MaociScreen.width_half, (int)MaociScreen.height_half, Settings.CAimH.FOV, Settings.CAimH.FovDrawColor, 2f);
                }
                try
                {
                    var MobileCrosshair = Raycast.BarrelRaycast(LocalPlayer.Base);
                    if (MobileCrosshair != Vector3.zero)
                        Print.Dot.DrawVectorCrosshair(Camera.main.WorldToScreenPoint(MobileCrosshair));
                }
                catch { }
                //if (CDoor.vb_main)
                   // GUI.Label(SelectedDoorMenu, "Door: " + CDoor.selectedName);
            }

            if (!CDoor.KeyRestored)
                GUI.Label(Menus.Menu_KeyM_Spoofed, "Mechanical KeySpoof Active");

            if (Settings.switches.vb_HelpMenu)
                Menus.Menu_0 = GUILayout.Window(103, Menus.Menu_0, Manu, Menus.SetGuiContent("Help Menu"));

            if (LocalPlayer.Base != null)
            {
                if (Settings.switches.vb_lpValue)
                    Menus.Menu_LP_EQValue = GUILayout.Window(101, Menus.Menu_LP_EQValue, Manu, Menus.SetGuiContent("Equipment"));
                if (Settings.switches.vb_lpWeapon)
                {
                    Menus.Menu_LP_Weapon = GUILayout.Window(102, Menus.Menu_LP_Weapon, Manu, Menus.SetGuiContent("Weapon"));
                    //DrawWeaponBox();
                }
            }
            if (!Settings.switches.vb_Menu)
            {
                return;
            }
            Menus.Menu_0 = GUILayout.Window(90, Menus.Menu_0, Manu, Menus.SetGuiContent("Mao " + commitVersion));
            Menus.Menu_1 = GUILayout.Window(91, Menus.Menu_1, Manu, Menus.SetGuiContent("Switch"));
            Menus.Menu_2 = GUILayout.Window(92, Menus.Menu_2, Manu, Menus.SetGuiContent("Items"));
            Menus.Menu_3 = GUILayout.Window(93, Menus.Menu_3, Manu, Menus.SetGuiContent("Function"));
        }

        private static void RunUpdateFeatures() {
            LocalPlayer.Features.StartCiriMode(Settings.switches.vb_ciriMode);
            LocalPlayer.Features.ThermalEffect(Settings.switches.m_thermal);
            LocalPlayer.Features.NightVisionEffect(Settings.switches.m_nvg);
            LocalPlayer.Features.FullBright_UpdateObject(Settings.switches.m_fullBright);
            LocalPlayer.Features.FullBright_SpawnObject();
            LocalPlayer.Features.SetRapeWeapon(Settings.switches.m_rapegun);
            if (!Settings.switches.m_LegitRecoil) // make sure legitrecoil is not ON
                LocalPlayer.Features.NoRecoil(Settings.switches.m_nRecoil);
            if (!Settings.switches.m_nRecoil) // make sure NoRecoil is not ON
                LocalPlayer.Features.LegitRecoil(Settings.switches.m_LegitRecoil);
            LocalPlayer.Features.NoMovementPenelty(Settings.switches.m_snicPenel);
            LocalPlayer.Features.SonicTeachMeThis(Settings.variables.m_snicSpeed);
            LocalPlayer.Features.Visor(Settings.switches.m_visor);
            LocalPlayer.Features.InfStramina(Settings.switches.m_infStam);
            LocalPlayer.Features.MoveWeaponCloser(Settings.switches.m_moveWcloser);
            LocalPlayerTotalValue = LocalPlayer.Features.CalculateEQValue();
            LocalPlayerWeaponStat = LocalPlayer.Features.ActualWeaponStats();
            LocalPlayerWeaponStatInChamber = LocalPlayer.Features.ActualWeaponBulletInChamber();
        }
        private static bool WaitOnFinish = false;
        private static float MenuWidth = Menus.Menu_0.width / 2 - 5;
        private static void Manu(int id)
        {
            switch (id)
            {
                case 90:
                    #region Buttons menu

                    GUI.DragWindow(MenuDragWindow);
#if ReleaseMaoci
                    Print.Menu.Label("-= Presets =-", true);
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Ma0ci", GUILayout.Width(MenuWidth)) && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        ButtonAction(3);
                        WaitOnFinish = false;
                    }
                    #endif
                    if (GUILayout.Button("Legit", GUILayout.Width(MenuWidth)) && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        ButtonAction(1);
                        WaitOnFinish = false;
                    }
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button("Clear") && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        ButtonAction(2);
                        WaitOnFinish = false;
                    }
                    Print.Menu.Label("-= Config =-", true);
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Load", GUILayout.Width(MenuWidth)) && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        LoadConfigs.LoadUserConfig();
                        LoadConfigs.ApplySettings();
                        WaitOnFinish = false;
                    }
                    if (GUILayout.Button("Save", GUILayout.Width(MenuWidth)) && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        LoadConfigs.SaveUserConfig();
                        WaitOnFinish = false;
                    }
                    GUILayout.EndHorizontal();
                    Print.Menu.Label(" ");
                    if (GUILayout.Button("Load Item Configs") && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        LoadConfigs.LoadItemsConfig();
                        WaitOnFinish = false;
                    }
                    Print.Menu.Label("Addon Menus", true);
                    Print.Menu.Checkbox("Equipment Value", ref Settings.switches.vb_lpValue);
                    Print.Menu.Checkbox("Weapon Stats", ref Settings.switches.vb_lpWeapon);
                    Print.Menu.Label("Informations:");
                    Print.Menu.Label("SpoofedName: " + ((Settings.switches.core_NameSpoof)?"Yes":"No"));
                    Print.Menu.Label("|" + Settings.variables.SpoofedName + "|");
                    break;
                    #endregion
                case 91:
                    #region MainMenu - Main ESP
                    Print.Menu.Label("-= Players =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CPlayer.Enable);
                    if (Settings.CPlayer.Enable)
                    {
                        Print.Menu.Checkbox("Draw Skeleton", ref Settings.CPlayer.DrawBones);
                        Print.Menu.Checkbox("Draw Snap Lines", ref Settings.CPlayer.DrawSnapLines);
                        Print.Menu.Checkbox("Only Player Side", ref Settings.CPlayer.ReplaceToPlayerSide);
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CPlayer.BaseDistance, 100f, 2000f, "Draw Distance", true);
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CPlayer.BonesDistance, 100f, 2000f, "Draw Bone Distance", true);
                    }
                    Print.Menu.Label("-= A1mB0t =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CAimH.Enable);
                    if (Settings.CAimH.Enable)
                    {
                        Print.Menu.Checkbox("ForceAim", ref Settings.CAimH.ForceAim);
                        Print.Menu.Checkbox("Triggerbot", ref Settings.CAimH.AutoShoot);
                        Print.Menu.Checkbox("No Visual Check", ref Settings.CAimH.AimThroughWalls);
                        Print.Menu.Checkbox("Draw Snap Line", ref Settings.CAimH.DrawSnapLine);
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CAimH.Distance, 100f, 2000f, "Distance", true);
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CAimH.FOV, 1f, 600f, "FOV", true);
                    }
                    Print.Menu.Label("-= Exfils =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CExfiltration.Enable);
                    if (Settings.CExfiltration.Enable)
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CExfiltration.Distance, 100f, 2000f, "Distance", true);
                    Print.Menu.Label("-= Grenades =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CThrowable.Enable);
                    if (Settings.CThrowable.Enable)
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CThrowable.Distance, 100f, 2000f, "Distance", true);
                    Print.Menu.Label("-= Corpses =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CCorpse.Enable);
                    if (Settings.CCorpse.Enable)
                        Print.Menu.Slider.Horizontal.Float(ref Settings.CCorpse.Distance, 100f, 2000f, "Distance", true);
                    Print.Menu.Label("-= Unlockables =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CDoor.Enable);
                    if (Settings.CDoor.Enable) {
                        Print.Menu.Slider.Horizontal.Int(ref Settings.CDoor.Distance, 1f, 100f, "Distance");
                        Print.Menu.Label("F6 - Spoof Door Key");
                        Print.Menu.Label("F7 - ON/OFF");
                    }
                    break;
                    #endregion
                case 92:
                    #region MainMenu - Items/Containers
                    Print.Menu.Label("-= Item Options =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CItem.Enable);
                    Print.Menu.Checkbox(" | Search for", ref Settings.switches.sb_searchForItems);
                    if (Settings.switches.sb_searchForItems)
                        Print.Menu.TextBox.String(ref Settings.CItem.SearchFor);
                    Print.Menu.Label("-= Container Options =-", true);
                    Print.Menu.Checkbox("Enable", ref Settings.CContainer.Enable);
                    Print.Menu.Checkbox(" |- Search for", ref Settings.switches.sb_searchForContainers);
                    if (Settings.switches.sb_searchForContainers)
                        Print.Menu.TextBox.String(ref Settings.CContainer.SearchFor);

                    Print.Menu.Label("-= PresetSearch =-", true);
                    Print.Menu.Label("Options above need to be enabled.");
                    Print.Menu.Checkbox("> QuestItems", ref Settings.CItem.DisplayQuestItems);
                    Print.Menu.Checkbox("> Superrare", ref Settings.CItem.DisplayLootSrare);
                    Print.Menu.Checkbox("> Custom #1", ref Settings.CItem.DisplayLootCustom1);
                    Print.Menu.Checkbox("> Custom #2", ref Settings.CItem.DisplayLootCustom2);
                    Print.Menu.Checkbox("> Custom #3", ref Settings.CItem.DisplayLootCustom3);
                    Print.Menu.Label("-= Limitations =-", true);
                    Print.Menu.Slider.Horizontal.Float(ref Settings.CItem.BaseDistance, 0f, 1000f, " -| Distance", true);
                    Print.Menu.Slider.Horizontal.Float(ref Settings.CItem.CustomDistance1, 0f, 1000f, " -| Custom #1", true);
                    Print.Menu.Slider.Horizontal.Float(ref Settings.CItem.CustomDistance2, 0f, 1000f, " -| Custom #2", true);
                    Print.Menu.Slider.Horizontal.Float(ref Settings.CItem.CustomDistance3, 0f, 1000f, " -| Custom #3", true);
                    Print.Menu.Slider.Horizontal.Int(ref Settings.CItem.ScreenLimitItems, 0f, 1000f, " -| Items on screen");
                   // Print.Menu.Label("Additional Options", true);
                   /* if (GUILayout.Button("Reset Items Settings") && !WaitOnFinish)
                    {
                        WaitOnFinish = true;
                        ButtonAction(0);
                        WaitOnFinish = false;
                    }*/
                    break;
                    #endregion
                case 93:
                    #region MainMenu - Misc Features
                    Print.Menu.Checkbox("No Visior", ref Settings.switches.m_visor);
                    Print.Menu.Checkbox("Thermal on/off", ref Settings.switches.m_thermal);
                    Print.Menu.Checkbox("NVG on/off", ref Settings.switches.m_nvg);
                    Print.Menu.Checkbox("Full Bright", ref Settings.switches.m_fullBright);
                    Print.Menu.Checkbox("Streamer Mode", ref Settings.CGameWorld.Enable_PseudoStreamMode);
                    Print.Menu.Checkbox("Hide Session", ref Settings.CGameWorld.Enable_RenamingSession);
                    Print.Menu.Checkbox("Infinity Stam.", ref Settings.switches.m_infStam);
                    Print.Menu.Label("Weapon", true);
                    Print.Menu.Checkbox("No Weapon Collision", ref Settings.switches.m_moveWcloser);
                    if (!Settings.switches.m_LegitRecoil)
                        Print.Menu.Checkbox("God Recoil", ref Settings.switches.m_nRecoil);
                    if (!Settings.switches.m_nRecoil)
                        Print.Menu.Checkbox("Legit Recoil -50%", ref Settings.switches.m_LegitRecoil);
                    Print.Menu.Checkbox("Always 100% Speed", ref Settings.switches.m_snicPenel);
                    Print.Menu.Checkbox("Maoci Gun Preset", ref Settings.switches.m_rapegun);
                    Print.Menu.Label("Speedhack", true);
                    Print.Menu.Slider.Horizontal.Float(ref Settings.variables.m_snicSpeed, 1f, 2.24f, "Speed:" + Settings.variables.m_snicSpeed.ToString());
                    Print.Menu.Slider.Horizontal.Float(ref Settings.variables.m_snicSpeed, 1f, 10f, "Speed#2:" + Settings.variables.m_snicSpeed.ToString());

                    break;
                    #endregion
                case 101:
                    #region Draw Total LocalPlayer Value
                    GUI.DragWindow(MenuDragWindow);
                    GUILayout.Label(LocalPlayerTotalValue, labelStyle);
                    break;
                    #endregion
                case 102:
                    #region Draw Total LocalPlayer Value
                    GUI.DragWindow(MenuDragWindow);
                    GUILayout.Label(LocalPlayerWeaponStat, labelStyle);
                    GUILayout.Label(LocalPlayerWeaponStatInChamber, labelStyle);
                    break;
                #endregion
                case 103:
                    Print.Menu.Label("Hotkeys Menu");
                    Print.Menu.Label("");
                    Print.Menu.Label(Keys.DoorsHotkey.ToString() + " - Door [Activate/Deactivate]");
                    Print.Menu.Label(Keys.CiriHotkey.ToString() + " - Ciri [Hold to Activate]");
                    Print.Menu.Label(Keys.AimKey_1.ToString() + " - Aimbot FullScreen [Hold to Activate]");
                    Print.Menu.Label(Keys.AimKey_2.ToString() + " - Aimbot FOV [Hold to Activate]");
                    Print.Menu.Label(Keys.MenuAct.ToString() + " - MainMenu [Activate/Deactivate]");
                    Print.Menu.Label(Keys.teleport.front.ToString() + " - Teleport in front [Press to Run Once]");
                    break;
                case 110:
                    /*Print.Menu.Label(changeKeyOnPress);
                    Print.Menu.Label(Keys.AimKey_1.ToString());
                    if (GUILayout.Button("< Change Aimkey 1 >"))
                    {
                        changeKeyOnPress = "Press key to change Aimkey1";
                        while (true)
                        { // stop everything and get a key pressed
                            Event e = Event.current;
                            if (e.type == EventType.KeyUp)
                            {
                                Keys.AimKey_1 = e.keyCode;
                                changeKeyOnPress = "Aimkey 1: " + e.keyCode.ToString();
                                break;
                            }
                        }
                    }
                    Print.Menu.Label(Keys.AimKey_2.ToString());
                    if (GUILayout.Button("< Change Aimkey 2 >"))
                    {
                        changeKeyOnPress = "Press key to change Aimkey2";
                        while (true)
                        { // stop everything and get a key pressed
                            Event e = Event.current;
                            if (e.type == EventType.KeyUp)
                            {
                                Keys.AimKey_2 = e.keyCode;
                                changeKeyOnPress = "Aimkey 2: " + e.keyCode.ToString();
                                break;
                            }
                        }
                    }*/
                    break;
/*#if ReleaseMaoci
                case 1:
                    break;
                case 2:
#region Display Masks
                    GUILayout.Label("LayerMasks:");
                    GUILayout.Label("AI:" + GClass363.AI.value.ToString());
                    GUILayout.Label("DefaultLayer:" + GClass363.DefaultLayer.value.ToString());
                    GUILayout.Label("DisablerCullingObjectLayer:" + GClass363.DisablerCullingObjectLayer.ToString());
                    GUILayout.Label("DisablerCullingObjectLayerMask:" + GClass363.DisablerCullingObjectLayerMask.ToString());
                    GUILayout.Label("DoorLayer:" + GClass363.DoorLayer.ToString());
                    GUILayout.Label("Grass:" + GClass363.Grass.value.ToString());
                    GUILayout.Label("GrenadeAffectedMask:" + GClass363.GrenadeAffectedMask.value.ToString());
                    GUILayout.Label("GrenadeObstaclesColliderMask:" + GClass363.GrenadeObstaclesColliderMask.value.ToString());
                    GUILayout.Label("HighPolyCollider:" + GClass363.HighPolyCollider.value.ToString());
                    GUILayout.Label("HighPolyWithTerrainMask:" + GClass363.HighPolyWithTerrainMask.value.ToString());
                    GUILayout.Label("HighPolyWithTerrainMaskAI:" + GClass363.HighPolyWithTerrainMaskAI.value.ToString());
                    GUILayout.Label("HighPolyWithTerrainNoGrassMask:" + GClass363.HighPolyWithTerrainNoGrassMask.value.ToString());
                    GUILayout.Label("HitColliderMask:" + GClass363.HitColliderMask.value.ToString());
                    GUILayout.Label("InteractiveLayer:" + GClass363.InteractiveLayer.value.ToString());
                    GUILayout.Label("InteractiveMask:" + GClass363.InteractiveMask.value.ToString());
                    GUILayout.Label("layerMask_0:" + GClass363.layerMask_0.value.ToString());
                    GUILayout.Label("LootCollisionMask:" + GClass363.LootCollisionMask.value.ToString());
                    GUILayout.Label("LootLayer:" + GClass363.LootLayer.value.ToString());
                    GUILayout.Label("LootLayerMask:" + GClass363.LootLayerMask.value.ToString());
                    GUILayout.Label("LowPolyColliderLayer:" + GClass363.LowPolyColliderLayer.ToString());
                    GUILayout.Label("LowPolyColliderLayerMask:" + GClass363.LowPolyColliderLayerMask.value.ToString());
                    GUILayout.Label("PlayerCollisionsMask:" + GClass363.PlayerCollisionsMask.value.ToString());
                    GUILayout.Label("PlayerCollisionTestMask:" + GClass363.PlayerCollisionTestMask.value.ToString());
                    GUILayout.Label("PlayerLayer:" + GClass363.PlayerLayer.ToString());
                    GUILayout.Label("PlayerMask:" + GClass363.PlayerMask.value.ToString());
                    GUILayout.Label("PlayerStaticCollisionsMask:" + GClass363.PlayerStaticCollisionsMask.value.ToString());
                    GUILayout.Label("ShellsCollisionsMask:" + GClass363.ShellsCollisionsMask.value.ToString());
                    GUILayout.Label("ShellsLayer:" + GClass363.ShellsLayer.ToString());
                    GUILayout.Label("TerrainLayer:" + GClass363.TerrainLayer.value.ToString());
                    GUILayout.Label("TerrainLowPoly:" + GClass363.TerrainLowPoly.value.ToString());
                    GUILayout.Label("TerrainMask:" + GClass363.TerrainMask.value.ToString());
                    GUILayout.Label("TriggersLayer:" + GClass363.TriggersLayer.value.ToString());
                    GUILayout.Label("TriggersMask:" + GClass363.TriggersMask.value.ToString());
                    GUILayout.Label("WaterLayer:" + GClass363.WaterLayer.value.ToString());
#endregion
                    break;
                case 3:
#region errors
                    //Print.Menu.Label("DID: " + SystemInfo.deviceUniqueIdentifier);
                    Print.Menu.Label("InMatch: ",CGameWorld.IsSpawnedInWorld().ToString());
                    Print.Menu.Label("CountLootLooped ",CItems.countLoot.ToString());
                    Print.Menu.Label("Error Loog GUI: ", CItems.errorCounterG.ToString());
                    Print.Menu.Label("Error Loog Upd ", CItems.errorCounterU.ToString());
                    Print.Menu.Label("Error1 Door: ", CDoor.errorCount.ToString());
                    Print.Menu.Label("Error2 Door: ", CDoor.errorCount1.ToString());
#endregion
                    break;
#endif*/
                default:
                    break;
            }
        }
        private static void ButtonAction(int id)
        {
            switch (id)
            {
                //Set Default items
                case 0:
                    Settings.CItem.BaseDistance = 1000f;
                    Settings.CItem.ScreenLimitItems = 300;
                    Settings.CItem.DisplayLootSrare = false;
                    Settings.CItem.DisplayLootCustom1 = false;
                    Settings.CItem.DisplayLootCustom2 = false;
                    Settings.CItem.DisplayLootCustom3 = false;
                    Settings.CItem.SearchFor = "";
                    break;
                case 1:
                    Settings.CPlayer.Enable = true;
                    Settings.CPlayer.DrawBones = true;
                    Settings.CThrowable.Enable = true;
                    Settings.CCorpse.Enable = true;
                    Settings.CAimH.Enable = true;
                    Settings.CAimH.Distance = 150f;
                    Settings.switches.m_visor = true;
                    Settings.switches.m_nRecoil = true;
                    Settings.switches.m_AimFovRange = false;
                    Settings.switches.m_infStam = true;
                    Settings.variables.m_snicSpeed = 1f;
                    Settings.switches.m_snicPenel = false;
                    break;
                case 2:
                    Settings.CPlayer.Enable = false;
                    Settings.CPlayer.DrawBones = false;
                    Settings.CPlayer.DrawSnapLines = false;
                    Settings.CAimH.Enable = false;
                    Settings.switches.m_AimFovRange = false;
                    Settings.CExfiltration.Enable = false;
                    Settings.CThrowable.Enable = false;
                    Settings.CCorpse.Enable = false;
                    Settings.CContainer.Enable = false;
                    Settings.CItem.Enable = false;
                    Settings.CContainer.SearchFor = "";
                    Settings.switches.m_infStam = false;
                    Settings.switches.m_nRecoil = false;
                    Settings.switches.m_moveWcloser = false;
                    Settings.switches.m_snicPenel = false;
                    Settings.switches.m_visor = false;
                    Settings.switches.m_thermal = false;
                    Settings.switches.m_nvg = false;
                    Settings.switches.m_fullBright = false;
                    Settings.CGameWorld.Enable_RenamingSession = false;
                    Settings.CGameWorld.Enable_PseudoStreamMode = false;
                    break;
                case 3:
                    Settings.CPlayer.Enable = true;
                    Settings.CPlayer.DrawBones = true;
                    Settings.CThrowable.Enable = true;
                    Settings.CCorpse.Enable = true;
                    Settings.switches.m_snicPenel = true;
                    Settings.variables.m_snicSpeed = 2.25f;
                    Settings.switches.m_infStam = true;
                    Settings.CAimH.Enable = true;
                    Settings.switches.m_visor = true;
                    Settings.switches.m_AimFovRange = false;
                    Settings.switches.m_nRecoil = true;
                    break;
                case 4:

                    break;
                default: break;
            }
        }
        private static void DrawWeaponBox() {

           /* Color Bup = GUI.color;
            labelStyle.normal.textColor = Color.white;
            Vector2 calculate_size = GUI.skin.GetStyle(LocalPlayerWeaponStatInChamber).CalcSize(new GUIContent(LocalPlayerWeaponStatInChamber));
            tempSaveRect = Menus.Menu_LP_Weapon;
            tempSaveRect.width = calculate_size.x * MaociScreen.ratio + 50f;
            if (tempSaveRect.width < 200f * MaociScreen.ratio) tempSaveRect.width = 200f * MaociScreen.ratio;
            tempSaveRect.x = MaociScreen.width - tempSaveRect.width;
            Menus.Menu_LP_Weapon = tempSaveRect; // update size of box compared to text inside
            GUI.Box(Menus.Menu_LP_Weapon, "");
            WeaponStats_ammo = Menus.Menu_LP_Weapon;
            WeaponStats_name = Menus.Menu_LP_Weapon;
            WeaponStats_ammo.x += 10f;
            WeaponStats_ammo.y += 10f;
            WeaponStats_name.x += 10f;
            WeaponStats_name.y += 10f + calculate_size.y + MaociScreen.ratio * 2 + 3;
            Print.Special.DrawText(LocalPlayerWeaponStat + "", WeaponStats_ammo, labelStyle);
            Print.Special.DrawText(LocalPlayerWeaponStatInChamber, WeaponStats_name, labelStyle);
            labelStyle.normal.textColor = Bup;*/
        }
    }

}
