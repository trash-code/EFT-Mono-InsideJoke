using System;
using System.IO;
using System.Collections.Generic;
using Exception6.Instances;
using UnityEngine;
using Newtonsoft.Json;
using System.Reflection;

namespace Exception6.Functions
{
    public static class Json
	{
		public static string Serialize<T>(T data)
		{
			return JsonConvert.SerializeObject(data);
		}

		public static void Save<T>(string filepath, T data)
		{
			string json = Serialize<T>(data);
			File.WriteAllText(filepath, json);
		}

		public static T Load<T>(string filepath) where T : new()
		{
			if (!File.Exists(filepath))
			{
				Save(filepath, new T());
				return Load<T>(filepath);
			}

			string json = File.ReadAllText(filepath);
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
    [ObfuscationAttribute(Exclude = true)]
    class LoadConfigs
    {
        public static UserConfig userConfig = new UserConfig();
        #region LoadUser LootLists
        public static string[] LoadItmParentFromFile(string num)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\\_MLH\\";
            string filename = "itmList_" + num + ".mao";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            if (!File.Exists(path + filename))
            {
                return new string[1] { "none" };
            }
            List<string> listLines = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path + filename);
            while ((line = file.ReadLine()) != null)
            {
                listLines.Add(line);
            }
            file.Close();
            return listLines.ToArray();
        }
        public static void LoadItemsConfig()
        {
            CItems.IDTable1 = LoadItmParentFromFile("1");
            CItems.IDTable2 = LoadItmParentFromFile("2");
            CItems.IDTable3 = LoadItmParentFromFile("3");
        }
        #endregion
        public static UserConfig ReadUserConfig()
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\\_MLH\\";
            string filename = "usrCfg.mao";
            if (!File.Exists(path + filename)) {
                SaveUserConfig();
            }
            return Json.Load<UserConfig>(path + filename);
        }
        public static void LoadUserConfig()
        {
            userConfig = ReadUserConfig();
        }
        [ObfuscationAttribute(Exclude = true)]
        public static void ApplySettings()
        {
            Settings.CItem.Enable = userConfig.customButton.itemsEnable;
            Settings.CItem.DisplayLootSrare = userConfig.customButton.itemsSuperRare;
            Settings.CItem.DisplayLootCustom1 = userConfig.customButton.itemsCustom_1;
            Settings.CItem.DisplayLootCustom2 = userConfig.customButton.itemsCustom_2;
            Settings.CItem.DisplayLootCustom3 = userConfig.customButton.itemsCustom_3;
            Settings.CItem.CustomDistance1 = userConfig.customButton.itemsDistanceCustom_1;
            Settings.CItem.CustomDistance2 = userConfig.customButton.itemsDistanceCustom_2;
            Settings.CItem.CustomDistance3 = userConfig.customButton.itemsDistanceCustom_3;
            Settings.CItem.BaseDistance = userConfig.customButton.itemsDistance;
            Settings.CItem.ScreenLimitItems = userConfig.customButton.itemsLimitOnScreen;
            Settings.switches.sb_searchForItems = userConfig.customButton.itemsEnableSearchFor;
            Settings.CItem.SearchFor = userConfig.customButton.itemsSearchFor;
            Settings.CPlayer.Enable = userConfig.customButton.playersEnable;
            Settings.CPlayer.DrawSnapLines = userConfig.customButton.playersSnapLines;
            Settings.CPlayer.DrawBones = userConfig.customButton.playersDrawBones;
            Settings.CPlayer.ReplaceToPlayerSide = userConfig.customButton.playersOnlyPlayerSide;
            Settings.CPlayer.BonesDistance = userConfig.customButton.playersBoneDistance;
            Settings.CPlayer.BaseDistance = userConfig.customButton.playersDistance;
            Settings.CThrowable.Enable = userConfig.customButton.throwablesEnable;
            Settings.CThrowable.Distance = userConfig.customButton.throwablesDistance;
            Settings.CExfiltration.Enable = userConfig.customButton.exfilsEnable;
            Settings.CExfiltration.Distance = userConfig.customButton.exfilsDistance;
            Settings.CDoor.Enable = userConfig.customButton.doorsEnable;
            Settings.CDoor.Distance = userConfig.customButton.doorsDistance;
            Settings.CCorpse.Enable = userConfig.customButton.corpsesEnable;
            Settings.CCorpse.Distance = userConfig.customButton.corpsesDistance;
            Settings.CContainer.Enable = userConfig.customButton.containersEnable;
            Settings.CContainer.Distance = userConfig.customButton.containersDistance;
            Settings.switches.sb_searchForContainers = userConfig.customButton.containersEnableSearchFor;
            Settings.CContainer.SearchFor = userConfig.customButton.containersSearchFor;
            Settings.CAimH.Enable = userConfig.customButton.autoaimEnable;
            Settings.CAimH.Distance = userConfig.customButton.autoaimDistance;
            Settings.CAimH.FOV = userConfig.customButton.autoaimDistanceFromCenter;
            Settings.CAimH.DrawSnapLine = userConfig.customButton.autoaimTargetSnapLine;
            Settings.switches.m_AimFovRange = userConfig.customButton.autoaimDrawAimFovRange;
            Settings.CGameWorld.Enable_RenamingSession = userConfig.customButton.featuresRenameSessionID;
            Settings.CGameWorld.ReplaceSessionIDto = userConfig.customButton.featuresSessionID;
            Settings.CGameWorld.Enable_PseudoStreamMode = userConfig.customButton.featuresStreamerMode;
            Settings.switches.core_NameSpoof = userConfig.customButton.featuresSpoofName;
            Settings.variables.SpoofedName = userConfig.customButton.featuresSpoofedName;
            Settings.variables.m_snicSpeed = userConfig.customButton.featuresSH_Speed;
            Settings.switches.m_rapegun = userConfig.customButton.featuresMaociPreset;
            Settings.switches.m_thermal = userConfig.customButton.featuresThermals;
            Settings.switches.m_nvg = userConfig.customButton.featuresNVG;
            Settings.switches.m_visor = userConfig.customButton.featuresNoVisor;
            Settings.switches.m_snicPenel = userConfig.customButton.featuresMovementNoPenelty;
            Settings.switches.m_nRecoil = userConfig.customButton.featuresNoRecoil;
            Settings.switches.m_LegitRecoil = userConfig.customButton.featuresLegitRecoil;
            Settings.switches.m_fullBright = userConfig.customButton.featuresFullBright;
            Settings.switches.m_infStam = userConfig.customButton.featuresInfinityStamina;
            Settings.switches.m_moveWcloser = userConfig.customButton.featuresWeaponNoClip;
            Settings.switches.vb_Menu = userConfig.customButton.featuresDisplayMenu;
            Settings.switches.vb_lpValue = userConfig.customButton.featuresDisplayEQMenu;
            Settings.switches.vb_lpWeapon = userConfig.customButton.featuresDisplayWeaponMenu;
            Settings.variables.OldMenuStyle = userConfig.customButton.featuresOldMenuStyle;
            #region Colors
            CPlayer.Function.Colors.NotVisible = RenderVariable(userConfig.colors.NotVisible);
            CPlayer.Function.Colors.Visible = RenderVariable(userConfig.colors.Visible);
            CPlayer.Function.Colors.Boss = RenderVariable(userConfig.colors.ScavBoss);
            CPlayer.Function.Colors.Follower = RenderVariable(userConfig.colors.BossFollower);
            CPlayer.Function.Colors.Scav = RenderVariable(userConfig.colors.Scav);
            CPlayer.Function.Colors.pScav = RenderVariable(userConfig.colors.PlayerScav);
            CPlayer.Function.Colors.Player = RenderVariable(userConfig.colors.Player);
            CPlayer.Function.Colors.Group = RenderVariable(userConfig.colors.Group);
            CPlayer.Function.Colors.Green06 = RenderVariable(userConfig.colors.LookDirection);
            Settings.CAimH.FovDrawColor = RenderVariable(userConfig.colors.FovCircle);
            Settings.CCorpse.DrawColor = RenderVariable(userConfig.colors.Corpse);
            Settings.CContainer.DrawColor = RenderVariable(userConfig.colors.Container);
            Settings.CThrowable.DrawColor = RenderVariable(userConfig.colors.Throwables);
            Settings.CItem.DrawBaseColor = RenderVariable(userConfig.colors.ItemsBase);
            Settings.CItem.DrawCustom1 = RenderVariable(userConfig.colors.ItemsCustom1);
            Settings.CItem.DrawCustom2 = RenderVariable(userConfig.colors.ItemsCustom2);
            Settings.CItem.DrawCustom3 = RenderVariable(userConfig.colors.ItemsCustom3);
            Settings.CItem.DrawSRare = RenderVariable(userConfig.colors.ItemsSRare);
            #endregion

        }
        [ObfuscationAttribute(Exclude = true)]
        private static void PrepareDataToSave()
        {
            userConfig = new UserConfig();
            userConfig.customButton.itemsEnable = Settings.CItem.Enable;
            userConfig.customButton.itemsSuperRare = Settings.CItem.DisplayLootSrare;
            userConfig.customButton.itemsCustom_1 = Settings.CItem.DisplayLootCustom1;
            userConfig.customButton.itemsCustom_2 = Settings.CItem.DisplayLootCustom2;
            userConfig.customButton.itemsCustom_3 = Settings.CItem.DisplayLootCustom3;
            userConfig.customButton.itemsDistanceCustom_1 = Settings.CItem.CustomDistance1;
            userConfig.customButton.itemsDistanceCustom_2 = Settings.CItem.CustomDistance2;
            userConfig.customButton.itemsDistanceCustom_3 = Settings.CItem.CustomDistance3;
            userConfig.customButton.itemsDistance = Settings.CItem.BaseDistance;
            userConfig.customButton.itemsLimitOnScreen = Settings.CItem.ScreenLimitItems;
            userConfig.customButton.itemsEnableSearchFor = Settings.switches.sb_searchForItems;
            userConfig.customButton.itemsSearchFor = Settings.CItem.SearchFor;
            userConfig.customButton.playersEnable = Settings.CPlayer.Enable;
            userConfig.customButton.playersSnapLines = Settings.CPlayer.DrawSnapLines;
            userConfig.customButton.playersDrawBones = Settings.CPlayer.DrawBones;
            userConfig.customButton.playersOnlyPlayerSide = Settings.CPlayer.ReplaceToPlayerSide;
            userConfig.customButton.playersBoneDistance = Settings.CPlayer.BonesDistance;
            userConfig.customButton.playersDistance = Settings.CPlayer.BaseDistance;
            userConfig.customButton.throwablesEnable = Settings.CThrowable.Enable;
            userConfig.customButton.throwablesDistance = Settings.CThrowable.Distance;
            userConfig.customButton.exfilsEnable = Settings.CExfiltration.Enable;
            userConfig.customButton.exfilsDistance = Settings.CExfiltration.Distance;
            userConfig.customButton.doorsEnable = Settings.CDoor.Enable;
            userConfig.customButton.doorsDistance = Settings.CDoor.Distance;
            userConfig.customButton.corpsesEnable = Settings.CCorpse.Enable;
            userConfig.customButton.corpsesDistance = Settings.CCorpse.Distance;
            userConfig.customButton.containersEnable = Settings.CContainer.Enable;
            userConfig.customButton.containersDistance = Settings.CContainer.Distance;
            userConfig.customButton.containersEnableSearchFor = Settings.switches.sb_searchForContainers;
            userConfig.customButton.containersSearchFor = Settings.CContainer.SearchFor;
            userConfig.customButton.autoaimEnable = Settings.CAimH.Enable;
            userConfig.customButton.autoaimDistance = Settings.CAimH.Distance;
            userConfig.customButton.autoaimDistanceFromCenter = Settings.CAimH.FOV;
            userConfig.customButton.autoaimTargetSnapLine = Settings.CAimH.DrawSnapLine;
            userConfig.customButton.autoaimDrawAimFovRange = Settings.switches.m_AimFovRange;
            userConfig.customButton.featuresRenameSessionID = Settings.CGameWorld.Enable_RenamingSession;
            userConfig.customButton.featuresSessionID = Settings.CGameWorld.ReplaceSessionIDto;
            userConfig.customButton.featuresStreamerMode = Settings.CGameWorld.Enable_PseudoStreamMode;
            userConfig.customButton.featuresSpoofName = Settings.switches.core_NameSpoof;
            userConfig.customButton.featuresSpoofedName = Settings.variables.SpoofedName;
            userConfig.customButton.featuresSH_Speed = Settings.variables.m_snicSpeed;
            userConfig.customButton.featuresMaociPreset = Settings.switches.m_rapegun;
            userConfig.customButton.featuresThermals = Settings.switches.m_thermal;
            userConfig.customButton.featuresNVG = Settings.switches.m_nvg;
            userConfig.customButton.featuresNoVisor = Settings.switches.m_visor;
            userConfig.customButton.featuresMovementNoPenelty = Settings.switches.m_snicPenel;
            userConfig.customButton.featuresNoRecoil = Settings.switches.m_nRecoil;
            userConfig.customButton.featuresLegitRecoil = Settings.switches.m_LegitRecoil;
            userConfig.customButton.featuresFullBright = Settings.switches.m_fullBright;
            userConfig.customButton.featuresInfinityStamina = Settings.switches.m_infStam;
            userConfig.customButton.featuresWeaponNoClip = Settings.switches.m_moveWcloser;
            userConfig.customButton.featuresDisplayMenu = Settings.switches.vb_Menu;
            userConfig.customButton.featuresDisplayEQMenu = Settings.switches.vb_lpValue;
            userConfig.customButton.featuresDisplayWeaponMenu = Settings.switches.vb_lpWeapon;
            userConfig.customButton.featuresOldMenuStyle = Settings.variables.OldMenuStyle;
            
            #region Colors
            userConfig.colors.NotVisible = RenderVariable(CPlayer.Function.Colors.NotVisible);
            userConfig.colors.Visible = RenderVariable(CPlayer.Function.Colors.Visible);
            userConfig.colors.ScavBoss = RenderVariable(CPlayer.Function.Colors.Boss);
            userConfig.colors.BossFollower = RenderVariable(CPlayer.Function.Colors.Follower);
            userConfig.colors.Scav = RenderVariable(CPlayer.Function.Colors.Scav);
            userConfig.colors.PlayerScav = RenderVariable(CPlayer.Function.Colors.pScav);
            userConfig.colors.Player = RenderVariable(CPlayer.Function.Colors.Player);
            userConfig.colors.Group = RenderVariable(CPlayer.Function.Colors.Group);
            userConfig.colors.LookDirection = RenderVariable(CPlayer.Function.Colors.Green06);
            userConfig.colors.FovCircle = RenderVariable(Settings.CAimH.FovDrawColor);
            userConfig.colors.Corpse = RenderVariable(Settings.CCorpse.DrawColor);
            userConfig.colors.Container = RenderVariable(Settings.CContainer.DrawColor);
            userConfig.colors.Throwables = RenderVariable(Settings.CThrowable.DrawColor);
            userConfig.colors.ItemsBase = RenderVariable(Settings.CItem.DrawBaseColor);
            userConfig.colors.ItemsCustom1 = RenderVariable(Settings.CItem.DrawCustom1);
            userConfig.colors.ItemsCustom2 = RenderVariable(Settings.CItem.DrawCustom2);
            userConfig.colors.ItemsCustom3 = RenderVariable(Settings.CItem.DrawCustom3);
            userConfig.colors.ItemsSRare = RenderVariable(Settings.CItem.DrawSRare);
            #endregion
        }
        public static void SaveUserConfig()
        {
            PrepareDataToSave();
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\\_MLH\\";
            string filename = "usrCfg.mao";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            if (!File.Exists(path + filename))
            {
                File.Create(path + filename);
            }
            while(true)
            {
                if (File.Exists(path + filename))
                {
                    File.WriteAllText(path + filename, JsonConvert.SerializeObject(userConfig, Formatting.Indented));
                    break;
                }
            }
            
        }
        #region UserConfig Class
        [ObfuscationAttribute(Exclude = true)]
        [JsonObject(MemberSerialization.OptOut)]
        public class UserConfig
        {
            [ObfuscationAttribute(Exclude = true)]
            public Colors colors = new Colors();
            [ObfuscationAttribute(Exclude = true)]
            public CustomButton customButton = new CustomButton();
        }
        [ObfuscationAttribute(Exclude = true)]
        public class Colors
        {
            [ObfuscationAttribute(Exclude = true)]
            public string NotVisible { get; set; } // bone not visible
            [ObfuscationAttribute(Exclude = true)]
            public string Visible { get; set; } // bone visible
            [ObfuscationAttribute(Exclude = true)]
            public string LookDirection { get; set; } // lookdirection color
            [ObfuscationAttribute(Exclude = true)]
            public string Player { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string Group { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string Scav { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string ScavBoss { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string BossFollower { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string PlayerScav { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string Throwables { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string Corpse { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string Container { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string ItemsBase { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string ItemsSRare { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string ItemsCustom1 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string ItemsCustom2 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string ItemsCustom3 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string FovCircle { get; set; }
        }
        [ObfuscationAttribute(Exclude = true)]
        public class CustomButton
        {
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsEnable { get; set; }
            /*[ObfuscationAttribute(Exclude = true)]
            public bool itemsCommonLoot { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsRare { get; set; }*/
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsSuperRare { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsCustom_1 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsCustom_2 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsCustom_3 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float itemsDistanceCustom_1 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float itemsDistanceCustom_2 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float itemsDistanceCustom_3 { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float itemsDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public int itemsLimitOnScreen { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool itemsEnableSearchFor { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string itemsSearchFor { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool playersEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool playersSnapLines { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool playersDrawBones { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool playersOnlyPlayerSide { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float playersBoneDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float playersDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool throwablesEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float throwablesDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool exfilsEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float exfilsDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool doorsEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public int doorsDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool corpsesEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float corpsesDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool containersEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float containersDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool containersEnableSearchFor { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string containersSearchFor { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool autoaimEnable { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float autoaimDistance { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float autoaimDistanceFromCenter { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool autoaimTargetSnapLine { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool autoaimDrawAimFovRange { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresRenameSessionID { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string featuresSessionID { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresStreamerMode { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresSpoofName { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public string featuresSpoofedName { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public float featuresSH_Speed { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresMaociPreset { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresThermals { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresNVG { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresNoVisor { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresMovementNoPenelty { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresNoRecoil { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresLegitRecoil { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresFullBright { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresInfinityStamina { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresWeaponNoClip { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresDisplayMenu { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresDisplayEQMenu { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresDisplayWeaponMenu { get; set; }
            [ObfuscationAttribute(Exclude = true)]
            public bool featuresOldMenuStyle { get; set; }
        }
        #endregion
        #region Rendering the Variables
        private static Color tmpColorRenderVariable = Color.black;
        private static string RenderVariable(Color color)
        {
            return color.r.ToString() + "," + color.g.ToString() + "," + color.b.ToString() + "," + color.a;
        }
        private static Color RenderVariable(string color) {
            string[] SplitColor = color.Split(',');
            return RenderVariable(
                float.Parse(SplitColor[0]),
                float.Parse(SplitColor[1]),
                float.Parse(SplitColor[2]),
                float.Parse(SplitColor[3])
                ); 
        }
        private static Color RenderVariable(float c1 = 1f, float c2 = 1f, float c3 = 1f, float c4 = 1f)
        {
            tmpColorRenderVariable.r = c1;
            tmpColorRenderVariable.g = c2;
            tmpColorRenderVariable.b = c3;
            tmpColorRenderVariable.a = c4;
            return tmpColorRenderVariable;
        }
        #endregion
    }
}
