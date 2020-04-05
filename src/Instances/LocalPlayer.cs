using EFT;
using EFT.InventoryLogic;
using Exception6.Instances;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception6.Instances
{
    public class LocalPlayer : MonoBehaviour
    {
        private static List<Player> l_Player = new List<Player>();
        public static Player Base = null; // LOCALPLAYER
                                          // also Base == null equals not spawned on map (it changes after gameworld is found / scene of map is loaded / blackscreen is turned off
        public static Vector3 RealPlayerPosition;
        public static bool enableCiri = false;
        public static bool isLocalPlayerLoaded { get { return Base != null; } }
        private static Item tempIcase = null;
        private void isOnUpdate()
        {
            if (!CGameWorld.IsSpawnedInWorld())
            {
                Base = null;
                return;
            }
            if (isLocalPlayerLoaded) {

                if (Input.GetKeyDown(KeyCode.Keypad3)) 
                {
                    Features.GetICaseForChange();
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Features.ReverseICaseChange();
                }

                if (!Settings.switches.vb_ciriMode || RealPlayerPosition == Vector3.zero)
                {
                    RealPlayerPosition = Base.Transform.position;
                }
                else
                {
                    RealPlayerPosition += Base.Velocity * Time.deltaTime; // should be ok :)
                }
            } else {
                //we dont need to call it all the time its a waste of power processing
                l_Player = CGameWorld.Base.RegisteredPlayers;
                var tmp_P = l_Player.GetEnumerator();
                while (tmp_P.MoveNext())
                {
                    var p = tmp_P.Current;
                    if (p.PointOfView == EPointOfView.FirstPerson)
                    {
                        Base = p;
                    }
                    //create groups here also
                }
            }

        }
        [ObfuscationAttribute(Exclude = true)]
        public void Update()
        {
            isOnUpdate();
        }
        public class Features {
            public static string CalculateEQValue()
            {
                if (LocalPlayer.Base == null)
                    return "Not Loaded";
                int value = 0;
                try
                {
                    listToExclude = new List<Item>();
                    equipItemList = LocalPlayer.Base.Profile.Inventory.Equipment.GetAllItems().GetEnumerator();
                    while (equipItemList.MoveNext())
                    {
                        TempItem = equipItemList.Current;
                        value += TempItem.Template.CreditsPrice;
                        if(TempItem.Template._parent == "5448bf274bdc2dfc2f8b456a")
                        {
                            var x = TempItem.GetAllItems().GetEnumerator();
                            while (x.MoveNext())
                            {
                                value -= x.Current.Template.CreditsPrice;
                            }
                        }
                    }
                    return "Value: " + (value / 1000).ToString() + "|k";
                }
                catch { return "Value: 0"; }
            }
            public static Item GetLocalPlayerKeys() {
                IEnumerable<Item> ItemsList_t = LocalPlayer.Base.Profile.Inventory.GetAllItemByTemplate("5780cfa52459777dfb276eb1");
                // key 5c99f98d86f7745c314214b3
                return ItemsList_t.FirstOrDefault();
                /*
                allItemsList = LocalPlayer.Base.Profile.Inventory.Equipment.GetAllItems().GetEnumerator();
                while (equipItemList.MoveNext())
                {
                    if (equipItemList.Current.Template._id == "5780cfa52459777dfb276eb1")
                        return equipItemList.Current;
                }
                return null;*/
            }
            public static void GetICaseForChange() {
                /*
                IEnumerable<Item> ItemsList_t = LocalPlayer.Base.Profile.Inventory.GetAllItemByTemplate("59fb042886f7746c5005a7b2");
                tempItem = ItemsList_t.FirstOrDefault();
                TemporalICaseData = tempItem.Template._parent;
                tempItem.Template._parent = "5448bf274bdc2dfc2f8b456a";
                //return tempItem;*/
            }
            public static void ReverseICaseChange() {
                //tempItem.Template._parent = TemporalICaseData;
            }
            public static string ActualWeaponStats() { 
                if(LocalPlayer.Base == null)
                    return "Not Loaded";
                // iPlayer.player.HandsController.Item.ShortName.Localized()
                string Vreturn = "";// LocalPlayer.Base.HandsController.Item.ShortName.Localized() + "\n";
                try
                {
                    Vreturn += (LocalPlayer.Base.Weapon.GetCurrentMagazineCount() + LocalPlayer.Base.Weapon.ChamberAmmoCount).ToString() + " / " + LocalPlayer.Base.Weapon.GetMaxMagazineCount().ToString();
                } catch { }
                return Vreturn;

            }
            public static string ActualWeaponBulletInChamber() {
                if (LocalPlayer.Base == null)
                    return "Not Loaded";
                try
                {
                    return LocalPlayer.Base.Weapon.Chambers[0].Items.FirstOrDefault().Name.Localized();
                } catch { return ""; }
            }
            public static void InfStramina(bool set) {
                if (Base != null)
                    if (set)
                    {
                        if (Base.Physical.HandsStamina.Current < NextStamina_hands)
                        {
                            Base.Physical.HandsStamina.Current += (float)rnd.Next(15, 35);
                            NextStamina_hands = (float)rnd.Next(-25, 25) + 40f;
                        }
                        if (Base.Physical.Stamina.Current < NextStamina_legs)
                        {
                            Base.Physical.Stamina.Current += (float)rnd.Next(15, 35);
                            NextStamina_legs = (float)rnd.Next(-25, 25) + 40f;
                        }
                    }
            }
            #region TELEPORT FUNCTIONS
            public static void TeleportTo(Vector3 pos)
            {
                if (Base != null)
                    Base.Transform.position = pos;
            }
            public static void TeleportDown(float distance = 1f)
            {
                if (Base != null)
                    Base.Teleport(Base.Transform.position - Camera.main.transform.up * distance);
            }
            public static void TeleportDownDev(float distance = 1f)
            {
                if (Base != null)
                    Base.Teleport(Base.Transform.position - Camera.main.transform.up * distance, true);
            }
            public static void TeleportFront(float distance = 1f)
            {
                if (Base != null)
                    Base.Transform.position = Base.Transform.position + Camera.main.transform.forward * distance;
            }
            public static void TeleportBack(float distance = 1f)
            {
                if (Base != null)
                    Base.Transform.position = Base.Transform.position - Camera.main.transform.forward * distance;
            }
            public static void TeleportLeft(float distance = 1f)
            {
                if (Base != null)
                    Base.Transform.position = Base.Transform.position - Camera.main.transform.right * distance;
            }
            public static void TeleportRight(float distance = 1f)
            {
                if (Base != null)
                    Base.Transform.position = Base.Transform.position + Camera.main.transform.right * distance;
            }
            #endregion
            #region FULL BRIGHT
            private static Vector3 tempPosition = Vector3.zero;
            public static void FullBright_UpdateObject(bool set)
            {
                if (Base != null)
                {
                    FullBright.Enabled = set;
                    if (set)
                    {

                        if (FullBright.FullBrightLight == null) return;
                        tempPosition = Base.Transform.position;
                        tempPosition.y += .2f;
                        FullBright.lightGameObject.transform.position = tempPosition;
                    }
                    else
                    {
                        if (FullBright.FullBrightLight != null)
                            GameObject.Destroy(FullBright.FullBrightLight);
                        FullBright.lightCalled = false;
                    }
                }
            }
            public static void FullBright_SpawnObject()
            {
                if (Base != null)
                    if (!FullBright.lightCalled && FullBright.Enabled)
                    {
                        FullBright.lightGameObject = new GameObject("Fullbright");
                        FullBright.FullBrightLight = FullBright.lightGameObject.AddComponent<Light>();
                        FullBright.FullBrightLight.color = new Color(1f, 0.839f, 0.66f, 1f);
                        FullBright.FullBrightLight.range = 2000f;
                        FullBright.FullBrightLight.intensity = 0.6f;
                        FullBright.lightCalled = true;
                    }

            }
            #endregion

            public static void SonicTeachMeThis(float set) {
                if (Base != null)
                    Base.MovementContext.SonicSpeedValue = set;
            }
            public static void NoMovementPenelty(bool set) {
                if (Base != null)
                    Base.MovementContext.Speed_O_Sonic = set;
            }
            #region WEAPON MANIPULATION
            public static void SetRapeWeapon(bool set) {
                if (Base != null)
                    if (set)
                    {
                        try
                        {
                            if (Base.Weapon != null)
                            {
                                //Base.Weapon.FireMode.FireMode = EFT.InventoryLogic.Weapon.EFireMode.fullauto;
                                Base.Weapon.Template.bFirerate = 1000;
                                Base.Weapon.Template.weapFireType = maociFiremodes;
                                Base.Weapon.Template.Ergonomics = 100;
                                Base.Weapon.Template.bHearDist = 1;
                            }
                        }
                        catch { return; }
                    }
            }

            public static void MoveWeaponCloser(bool set) {
                if (Base != null)
                   
                    if (set)
                        try
                        {
                            Base.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.ForceReaction;// _shouldMoveWeaponCloser = !set;
                        } catch { return; }
                    }
            public static void NoRecoil(bool set) {
                if (Base != null)
                {
                    Base.ProceduralWeaponAnimation.WalkEffectorEnabled = set;
                    Base.ProceduralWeaponAnimation.Shootingg.Stiffness = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.ForceReact.Intensity = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.Walk.Intensity = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.Shootingg.Intensity = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.Breath.Intensity = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.MotionReact.Intensity = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.MotionReact.SwayFactors.x = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.MotionReact.SwayFactors.y = (set) ? 0f : 1f;
                    Base.ProceduralWeaponAnimation.MotionReact.SwayFactors.z = (set) ? 0f : 1f;
                    if (set)
                    {
                        Base.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = Zero2d;
                        Base.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = Zero2d;
                        Base.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.x = 0f;
                        Base.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.y = 0f;
                        Base.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.z = 0f;
                        Base.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.x = 0f;
                        Base.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.y = 0f;
                        Base.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.z = 0f;
                        Base.ProceduralWeaponAnimation.Breath.HipPenalty = 0f;
                        Base.ProceduralWeaponAnimation.MotionReact.Velocity.x = 0f;
                        Base.ProceduralWeaponAnimation.MotionReact.Velocity.y = 0f;
                        Base.ProceduralWeaponAnimation.MotionReact.Velocity.z = 0f;
                    }
                }
            }
            public static void LegitRecoil(bool set) {
                Base.ProceduralWeaponAnimation.ForceReact.Intensity = (set) ? .5f : 1f;
                Base.ProceduralWeaponAnimation.Walk.Intensity = (set) ? .5f : 1f;
                Base.ProceduralWeaponAnimation.Shootingg.Intensity = (set) ? .5f : 1f;
                Base.ProceduralWeaponAnimation.Breath.Intensity = (set) ? .5f : 1f;
                Base.ProceduralWeaponAnimation.MotionReact.Intensity = (set) ? .5f : 1f;
            }
            #endregion
            #region VISION FUNCTIONS
            public static void Visor(bool set) {
                if (Base != null)
                    if (Camera.main != null)
                    {
                        if (set)
                        {
                            Camera.main.GetComponent<VisorEffect>().Intensity = 0f;
                            Camera.main.GetComponent<VisorEffect>().enabled = true;
                        }
                        else if (!set)
                        {
                            Camera.main.GetComponent<VisorEffect>().Intensity = 1f;
                            Camera.main.GetComponent<VisorEffect>().enabled = true;
                        }
                    }
            }
            public static void ThermalEffect(bool set) {
                if (Base != null)
                    if (set && ThermalComponent == null)
                    {
                        ThermalComponent = Camera.main.GetComponent<ThermalVision>();
                        ThermalComponent.On = true;
                        ThermalComponent.IsFpsStuck = false;
                        ThermalComponent.IsGlitch = false;
                        ThermalComponent.IsMotionBlurred = false;
                        ThermalComponent.IsNoisy = false;
                        ThermalComponent.IsPixelated = false;
                    }
                    else if (!set && ThermalComponent != null)
                    {
                        ThermalComponent.On = false;
                        ThermalComponent = null;
                    }
            }
            public static void NightVisionEffect(bool set) {
                if (Base != null)
                    if (set && NVGComponent == null)
                    {
                        //Camera.main.GetComponent<BSG.CameraEffects.NightVision>().ApplySettings();
                        NVGComponent = Camera.main.GetComponent<BSG.CameraEffects.NightVision>();
                        NVGComponent.StartSwitch(true);
                        NVGComponent.DiffuseIntensity = 0f;
                        NVGComponent.TextureMask.Color = new Color(0f, 0f, 0f, 0f);
                        NVGComponent.TextureMask.Stretch = false;
                        NVGComponent.TextureMask.Size = 0f;
                        NVGComponent.Intensity = 0f;
                    }
                    else if(!set && NVGComponent != null)
                    {
                        NVGComponent.StartSwitch(false);
                        NVGComponent = null;
                    }
            }
            #endregion
            public static void StartCiriMode(bool enable)
            {
                if (!revertedCiriMode)
                    RealPlayerPosition = Base.Transform.position;
                if (enable)
                {
                    if (nextTime < Time.time)
                    {
                        Base.Transform.position = CalculateCiriNextPosition(RealPlayerPosition, 2f);
                        nextTime = Time.time + 0.1f;
                    }
                    if (!revertedCiriMode)
                        revertedCiriMode = true;
                }
                else
                {
                    if (revertedCiriMode)
                    {
                        revertedCiriMode = false;
                        Base.Transform.position = RealPlayerPosition;
                    }
                }
            }
            #region private variables
            private static IEnumerator<EFT.InventoryLogic.Item> equipItemList;
            private static List<Item> listToExclude;
            private static Item TempItem = null;
            private static string TemporalICaseData;
            private static Item tempItem;
            private static Vector3 NewPose = Vector3.zero;
            private static Vector3 CalculateCiriNextPosition(Vector3 CenterPosition, float num = 1f) {
                NextRandom = rnd.Next(0, (int)(1000 * num)) / 1000f;
                tempR = radius * (float)Math.Sqrt(NextRandom);
                theta = NextRandom * 2 * 3.14f;
                //Base.Transform.forward
                    NewPose.x = CenterPosition.x + tempR * (float)Math.Cos(theta);
                    NewPose.z = CenterPosition.z + tempR * (float)Math.Sin(theta);
                    NewPose.y = CenterPosition.y;
                return NewPose;
            }
            private static float radius = 1f;
            private static float NextRandom = 0f;
            private static float tempR = 0f;
            private static float theta = 0f;
            private static float nextTime = 0f;
            private static bool revertedCiriMode = false;
            private static float NextStamina_hands = 50f;
            private static float NextStamina_legs = 50f;
            private static BSG.CameraEffects.NightVision NVGComponent = null;
            private static Vector3 Zero2d = new Vector2(0, 0);
            private static EFT.InventoryLogic.Weapon.EFireMode[] maociFiremodes = new EFT.InventoryLogic.Weapon.EFireMode[3] { 
                EFT.InventoryLogic.Weapon.EFireMode.single, 
                //EFT.InventoryLogic.Weapon.EFireMode.doublet, 
                EFT.InventoryLogic.Weapon.EFireMode.burst, 
                EFT.InventoryLogic.Weapon.EFireMode.fullauto 
            };
            private static ThermalVision ThermalComponent = null;
            private static System.Random rnd = new System.Random();
            private class FullBright
            {
                public static bool Enabled = false;
                public static GameObject lightGameObject;
                public static Light FullBrightLight;
                public static bool _LightEnabled = true;
                //public static bool _LightCreated;
                public static bool lightCalled;
            }
            #endregion
        }
    }
}
