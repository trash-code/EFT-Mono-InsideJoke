using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception6
{
    [ObfuscationAttribute(Exclude = true)]
    public static class Settings
    {
        public static class CAimH
        {
            public static bool Enable = false;
            public static float Distance = 200f;
            public static string ForceAimAtBodyPart = "head";
            public static float FOV = 100f;
            public static bool DrawSnapLine = false;
            public static bool ForceAim = false;
            public static bool AimkeyFS = false;
            public static bool AimkeyFOV = false;
            public static bool AutoShoot = true;
            public static bool AimThroughWalls = false;
            public static bool KillaMode = false;
            public static Color FovDrawColor = new Color(1f, 1f, 1f, 0.5f);

        }
        public static class CContainer
        {
            public static bool Enable = false;
            public static string SearchFor = "";
            public static float Distance = 300f;
            public static Color DrawColor = new Color(1f, 1f, 1f, .6f);
        }
        public static class CCorpse
        {
            public static bool Enable = false;
            public static float Distance = 300f;
            public static Color DrawColor = new Color(1f, 1f, 1f, .6f);
        }
        public static class CDoor
        {
            public static bool Enable = false;
            public static string SelectedDoor = "";
            public static int Distance = 10;
            public static Color DrawColor = new Color(0f, 1f, 0f, 1f);
            public static Color DrawActiveColor = new Color(1f, 0f, 0f, 1f);

        }
        public static class CExfiltration
        {
            public static bool Enable = false;
            public static float Distance = 2000f;
            public static Color DrawColor = new Color(.392f, .667f, .392f, 1f);
            private static float dDistance = 25f;
            public static float DeltaDistance { get { return dDistance; } }

        }
        [ObfuscationAttribute(Exclude = true)]
        public static class CGameWorld
        {
            public static string ReplaceSessionIDto = "IllegalMemes";
            public static bool Enable_PseudoStreamMode = false;
            public static bool Enable_RenamingSession = false;
        }
        public static class CItem
        {
            public static bool Enable = false;
            public static bool DisplayQuestItems = false; 
            public static bool DisplayLootCommon = false;
            public static bool DisplayLootRare = false;
            public static bool DisplayLootSrare = false;
            public static bool DisplayLootCustom1 = false;
            public static bool DisplayLootCustom2 = false;
            public static bool DisplayLootCustom3 = false;
            public static float CustomDistance1 = 200f;
            public static float CustomDistance2 = 500f;
            public static float CustomDistance3 = 1000f;
            public static float BaseDistance = 300f;
            public static int ScreenLimitItems = 300;
            public static Color DrawBaseColor = new Color(1f, 1f, 1f, .8f);
            public static Color DrawQuest = new Color(.5f, .5f, .9f, .8f);
            public static Color DrawSRare = new Color(1f, 0f, .5647f, .8f);
            public static Color DrawCustom1 = new Color(.68f, .37f, .26f, .8f);
            public static Color DrawCustom2 = new Color(1f, 0.6f, 0f, .8f);
            public static Color DrawCustom3 = new Color(0.43f, 1f, 0.36f, .8f);
            public static string SearchFor = "";
        }
        public static class CPlayer
        {
            public static bool Enable = false;
            public static bool DrawSnapLines = false;
            public static bool DrawBones = false;
            public static bool ReplaceToPlayerSide = false;
            public static float BonesDistance = 200f;
            public static float BaseDistance = 1000f;
        }
        public static class CThrowable
        {
            public static bool Enable = false;
            public static float Distance = 100f;
            public static Color DrawColor = new Color(1f, 0f, 0f, 1f);
        }
        public static class CLocalPlayer
        {
        }
        [ObfuscationAttribute(Exclude = true)]
        public static class variables
        {
            [ObfuscationAttribute(Exclude = true)]
            public static string SpoofedName = "~Protected~By~Maoci~";
            public static float m_snicSpeed = 1f;
            public static bool OldMenuStyle = true;
        }
        [ObfuscationAttribute(Exclude = true)]
        public static class switches {
            [ObfuscationAttribute(Exclude = true)]
            public static bool core_NameSpoof = true;
            public static bool m_rapegun = false;
            public static bool m_thermal = false;
            public static bool m_visor = false;
            public static bool m_nvg = false;
            public static bool m_snicPenel = false;
            public static bool m_nRecoil = false;
            public static bool m_LegitRecoil = false;
            public static bool m_fullBright = false;
            public static bool m_infStam = false;
            public static bool m_moveWcloser = false;
            public static bool m_AimFovRange = false;
            public static bool sb_searchForItems = false;
            public static bool sb_searchForContainers = false;
            public static bool btn_5 = false;
            public static bool btn_6 = false;
            public static bool vb_Menu = false;
            public static bool vb_lpValue = false;
            public static bool vb_lpWeapon = false;
            public static bool vb_ciriMode = false;
            public static bool vb_HelpMenu = false;
        }
    }
}
