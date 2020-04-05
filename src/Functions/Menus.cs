using Exception5.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception6.Functions
{
    public static class Menus
    {
        private static GUIContent tempGuiContent = new GUIContent();
        private static Rect MenuRect = Rect.zero;
        private static CMenu Smenu_0 = new CMenu("menu");
        private static CMenu Smenu_1 = new CMenu("switches");
        private static CMenu Smenu_2 = new CMenu("items");
        private static CMenu Smenu_3 = new CMenu("miscs");
        private static CMenu SkeyCspoof = new CMenu("keyCspoof");
        private static CMenu SkeyMspoof = new CMenu("keyMspoof");
        private static CMenu LPlayer_Value = new CMenu("LP_EQValue");
        private static CMenu LPlayer_Weapon = new CMenu("LP_WeaponStats");
        private static CMenu helpMenu = new CMenu("HelpMenu");
        private class CMenu
        {
            public CMenu(string menuType)
            {
                switch (menuType)
                {
                    #region Main menu
                    case "menu":
                        x = 5f;
                        y = 40f;
                        width = 200f;
                        height = 200f;
                        break;
                    case "switches":
                        x = 156f;
                        y = 40f;
                        width = 200f;
                        height = 200f;
                        break;
                    case "items":
                        x = 307f;
                        y = 40f;
                        width = 200f;
                        height = 200f;
                        break;
                    case "miscs":
                        x = 478f;
                        y = 40f;
                        width = 200f;
                        height = 200f;
                        break;
                    #endregion
                    #region additional menus
                    case "configs":
                        x = 0f;
                        y = 0f;
                        width = 0f;
                        height = 0f;
                        break;
                    case "masks":
                        x = 0f;
                        y = 0f;
                        width = 0f;
                        height = 0f;
                        break;
                    #endregion
                    #region keySpoofers
                    case "keyCspoof":
                        x = 10f;
                        y = 35f;
                        width = 200f;
                        height = 20f;
                        break;
                    case "keyMspoof":
                        x = 10f;
                        y = 60f;
                        width = 200f;
                        height = 20f;
                        break;
                    #endregion
                    #region LocalPlayer Movable data
                    case "LP_EQValue":
                        x = MaociScreen.width - (200f);
                        y = 200f;
                        width = 200f;
                        height = 50f;
                        break;
                    case "LP_WeaponStats":
                        x = MaociScreen.width - (400f);
                        y = MaociScreen.height - (300f);
                        width = 200f;
                        height = 100f;
                        break;
                    #endregion
                    case "HelpMenu":
                        width = 300f;
                        height = 100f;
                        x = MaociScreen.width / 2 - width;
                        y = MaociScreen.height / 2 - (height + 100);
                        break;
                    default: break;
                }
            }
            public float x;
            public float y;
            public float width;
            public float height;
        }
        public static Rect Menu_0
        {
            get
            {
                MenuRect.x = Smenu_0.x;
                MenuRect.y = Smenu_0.y;
                MenuRect.width = Smenu_0.width;
                MenuRect.height = Smenu_0.height;
                return MenuRect;
            }
            set
            {
                Smenu_0.x = value.x;
                Smenu_0.y = value.y;
                Smenu_0.width = value.width;
                Smenu_0.height = value.height;
            }
        }
        public static Rect Menu_1
        {
            get
            {
                MenuRect.x = Smenu_1.x;
                MenuRect.y = Smenu_1.y;
                MenuRect.width = Smenu_1.width;
                MenuRect.height = Smenu_1.height;
                return MenuRect;
            }
            set
            {
                Smenu_1.x = value.x;
                Smenu_1.y = value.y;
                Smenu_1.width = value.width;
                Smenu_1.height = value.height;
            }
        }
        public static Rect Menu_2
        {
            get
            {
                MenuRect.x = Smenu_2.x;
                MenuRect.y = Smenu_2.y;
                MenuRect.width = Smenu_2.width;
                MenuRect.height = Smenu_2.height;
                return MenuRect;
            }
            set
            {
                Smenu_2.x = value.x;
                Smenu_2.y = value.y;
                Smenu_2.width = value.width;
                Smenu_2.height = value.height;
            }
        }
        public static Rect Menu_3
        {
            get
            {
                MenuRect.x = Smenu_3.x;
                MenuRect.y = Smenu_3.y;
                MenuRect.width = Smenu_3.width;
                MenuRect.height = Smenu_3.height;
                return MenuRect;
            }
            set
            {
                Smenu_3.x = value.x;
                Smenu_3.y = value.y;
                Smenu_3.width = value.width;
                Smenu_3.height = value.height;
            }
        }
        public static Rect Menu_KeyC_Spoofed
        {
            get
            {
                MenuRect.x = SkeyCspoof.x;
                MenuRect.y = SkeyCspoof.y;
                MenuRect.width = SkeyCspoof.width;
                MenuRect.height = SkeyCspoof.height;
                return MenuRect;
            }
        }
        public static Rect Menu_KeyM_Spoofed
        {
            get
            {
                MenuRect.x = SkeyMspoof.x;
                MenuRect.y = SkeyMspoof.y;
                MenuRect.width = SkeyMspoof.width;
                MenuRect.height = SkeyMspoof.height;
                return MenuRect;
            }
        }
        public static Rect Menu_LP_EQValue
        {
            get
            {
                MenuRect.x = LPlayer_Value.x;
                MenuRect.y = LPlayer_Value.y;
                MenuRect.width = LPlayer_Value.width;
                MenuRect.height = LPlayer_Value.height;
                return MenuRect;
            }
            set
            {
                LPlayer_Value.x = value.x;
                LPlayer_Value.y = value.y;
                LPlayer_Value.width = value.width;
                LPlayer_Value.height = value.height;
            }

        }
        public static Rect Menu_LP_Weapon
        {
            get
            {
                MenuRect.x = LPlayer_Weapon.x;
                MenuRect.y = LPlayer_Weapon.y;
                MenuRect.width = LPlayer_Weapon.width;
                MenuRect.height = LPlayer_Weapon.height;
                return MenuRect;
            }
            set
            {
                LPlayer_Weapon.x = value.x;
                LPlayer_Weapon.y = value.y;
                LPlayer_Weapon.width = value.width;
                LPlayer_Weapon.height = value.height;
            }

        }
        public static Rect Menu_HelpInfo
        {
            get
            {
                MenuRect.x = helpMenu.x;
                MenuRect.y = helpMenu.y;
                MenuRect.width = helpMenu.width;
                MenuRect.height = helpMenu.height;
                return MenuRect;
            }
            set
            {
                helpMenu.x = value.x;
                helpMenu.y = value.y;
                helpMenu.width = value.width;
                helpMenu.height = value.height;
            }

        }
        public static GUIContent SetGuiContent(string text) {
            tempGuiContent.text = text;
            return tempGuiContent;
        }
    }

}
