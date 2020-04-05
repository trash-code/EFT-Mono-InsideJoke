using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using EFT;
using Exception6.Functions;
using System.Reflection;
using Exception5.Functions;
using System.Collections;
using AimBodyPart = Exception6.Functions.PlayerBones.AimBody;

namespace Exception6.Instances
{
    public class CAimH : MonoBehaviour
    {
        public static int aimAtBodyPart = 133;
        public static Vector3 out_AimingAt = Vector3.zero;

        public class vars
        {
            public static List<Player> l_Player;
            public static List<Player> tPlayers;
            public static List<Player>.Enumerator list_tmp;
            public static Player PlayerAimedAt;
            public static Vector3 Place3dW_AimedAt = Vector3.zero;
            public static AimBodyPart BodyPart_AimedAt = AimBodyPart.Null;
            public static Player tmpItem;
            public static Rect box = new Rect(10f, 40f, 100f, 20f);
            public static Rect box2 = new Rect(10f, 10f, 100f, 20f);
            public static Camera camera = Camera.main;
            public static GUIStyle drawTextStyle = new GUIStyle() { fontSize = 12 };
            
            public static Vector3 tmpHeadPos = Vector3.zero;
            public static float travelTime = 1f;
            public static Vector2 prioritizeTarget = Vector2.zero;
            public static Color colorToTarget = new Color(1f, 0f, 0.94f, 0.6f);
            public static Vector2 centerOfScreen = new Vector2(MaociScreen.width_half, MaociScreen.height_half);
            #region OnGUI
            public static Vector3 vector;
            public static float distanceToTarget;
            #endregion
            public static int countObj = 0;
        }
        [ObfuscationAttribute(Exclude = true)]
        void Awake() {
            vars.drawTextStyle.normal.textColor = Color.white;
        }
        [ObfuscationAttribute(Exclude = true)]
        public void Update()
        {
            if (!Settings.CAimH.Enable) return; // enable disable aimbot keys
            if (LocalPlayer.Base == null) return; // make sure gameworld is active
            aKeys();
            isOnUpdate();
        }
        [ObfuscationAttribute(Exclude = true)]
        public void OnGUI()
        {
            if (Settings.CAimH.Enable) // enable disable aimbot keys
                isOnGui();
        }

        private void aKeys()
        {
            if (Input.GetKeyDown(Keys.AimKey_1))
            { // if mouse is pressed tick change
                Settings.CAimH.AimkeyFS = true;
            }
            if (Input.GetKeyUp(Keys.AimKey_1))
            { // if mouse is released tick change
                Settings.CAimH.AimkeyFS = false;
            }
            if (Input.GetKeyDown(Keys.AimKey_2))
            { // if mouse is pressed tick change
                Settings.CAimH.AimkeyFOV = true;
            }
            if (Input.GetKeyUp(Keys.AimKey_2))
            { // if mouse is released tick change
                Settings.CAimH.AimkeyFOV = false;
            }
            if (Input.GetKeyDown(Keys.SwitchBodyPart))
            {
                func.AimAtSwitch();
            }
            if (Input.GetKeyDown(Keys.SwitchAimToHead))
            {
                Settings.CAimH.ForceAimAtBodyPart = "head";
            }
            if (Input.GetKeyDown(Keys.SwitchAimToThorax))
            {
                Settings.CAimH.ForceAimAtBodyPart = "thorax";
            }
            if (Input.GetKeyDown(Keys.SwitchAimToLegs))
            {
                Settings.CAimH.ForceAimAtBodyPart = "legs";
            }
            if (Input.GetKeyDown(Keys.SwitchKillaMode))
            {
                Settings.CAimH.KillaMode = !Settings.CAimH.KillaMode;
            }
        }
        private void isOnUpdate()
        {
            if (LocalPlayer.Base == null)
                return;
            try
            {
                // creater list of players only on screen less ressource hungry
                // it can also be done using FOV * 2 to add to list
                vars.tPlayers = new List<Player>();
                vars.list_tmp = CGameWorld.Base.RegisteredPlayers.GetEnumerator();
                vars.countObj = 0;
                while (vars.list_tmp.MoveNext())
                {
                    vars.tmpItem = vars.list_tmp.Current;
                    if (vars.tmpItem == null) continue;
                    vars.tmpHeadPos = vars.tmpItem.PlayerBones.Head.position;
                    if (MaociScreen.onScreenStrict(Camera.main.WorldToScreenPoint(vars.tmpHeadPos)))
                    { // if players head is on screen then .Add
                        if (Settings.CAimH.Distance > Vector3.Distance(Camera.main.transform.position, vars.tmpHeadPos))
                        {// if players distance is in range
                            if (!CPlayer.Function.inGroup(vars.tmpItem))
                            {// exclude players in group
                                vars.tPlayers.Add(vars.tmpItem);
                                vars.countObj++;
                            }
                        }
                    }
                }
                vars.tPlayers = Prioritize.SortData(vars.tPlayers);
                vars.l_Player = vars.tPlayers;
            }
            catch { }
            if (vars.countObj == 0)
                return;
            vars.PlayerAimedAt = func.ChooseTarget(Settings.CAimH.ForceAim);
        }
        private static string onWhatImGonnaAimAt() {
            if (Settings.CAimH.ForceAim)
                return Settings.CAimH.ForceAimAtBodyPart;
            return "AutoSelect";
        }
        private void isOnGui()
        {
            if (LocalPlayer.Base == null)
                return;

            if (Settings.CAimH.KillaMode)
            {
                Print.Special.DrawText("KillaMode", vars.box2, vars.drawTextStyle);
            }
            else
            {
                Print.Special.DrawText(onWhatImGonnaAimAt(), vars.box2, vars.drawTextStyle);
            }
            if (Settings.CAimH.AimkeyFS && !Settings.CAimH.AimkeyFOV)
            {
                Print.Special.DrawText("Aim FS " + vars.vector.ToString() + " - " + vars.BodyPart_AimedAt.ToString(), vars.box, vars.drawTextStyle);
            }
            if (Settings.CAimH.AimkeyFOV && !Settings.CAimH.AimkeyFS)
            {
                Print.Special.DrawText("Aim FOV", vars.box, vars.drawTextStyle);
            }
            if (!Settings.CAimH.AimkeyFOV && !Settings.CAimH.AimkeyFS)
                return;
            vars.camera = Camera.main;
            if (!vars.camera) return;
            if (vars.countObj == 0)
                return; // return if no object found
            if (vars.PlayerAimedAt == null)
                return; // oh somehow you get a default player object ... LOL

            if (vars.BodyPart_AimedAt != AimBodyPart.Null)
            {
                vars.vector = Functions.PlayerBones.FinalVector(vars.PlayerAimedAt.PlayerBody.SkeletonRootJoint, (int)vars.BodyPart_AimedAt);// vars.PlayerAimedAt.PlayerBones.Head.position;// Functions.PlayerBones.FinalVector(vars.PlayerAimedAt.PlayerBody.SkeletonRootJoint, aimAtBodyPart);
            }
            else 
            {
                return;
            }
            if (Settings.CAimH.DrawSnapLine)
            {
                #region DrawLineToTarget
                if (MaociScreen.onScreenStrict(vars.camera.WorldToScreenPoint(vars.PlayerAimedAt.Transform.position)))
                {
                    vars.prioritizeTarget = vars.camera.WorldToScreenPoint(vars.vector);
                    vars.prioritizeTarget.y = MaociScreen.height - vars.prioritizeTarget.y;
                    Print.Line.Draw(vars.centerOfScreen, vars.prioritizeTarget, vars.colorToTarget, 2f);
                }
                #endregion
            }

            if (vars.vector != Vector3.zero)
            {
                if (!Settings.CAimH.AimkeyFS)
                    if (Vector2.Distance(vars.centerOfScreen, vars.camera.WorldToScreenPoint(vars.vector)) > Settings.CAimH.FOV) return; // if not in FOV and FS is not pressed return
                vars.distanceToTarget = Vector3.Distance(Camera.main.transform.position, vars.vector);
                vars.travelTime = vars.distanceToTarget / LocalPlayer.Base.Weapon.CurrentAmmoTemplate.InitialSpeed;
                vars.vector += vars.PlayerAimedAt.Velocity * vars.travelTime; // aim prediction to target move point depends on player movement
                vars.vector -= LocalPlayer.Base.Velocity * Time.deltaTime;
                // should increase aiming by countind bullet drop
                if (vars.distanceToTarget > 100f)
                {
                    vars.vector = vars.vector + Vector3.up * func.BulletDrop(LocalPlayer.Base.Fireport.position, vars.vector, LocalPlayer.Base.Weapon.CurrentAmmoTemplate.InitialSpeed);
                }
                // predict own player movement and compensage aiming
                // strangely changes position of esp ... //Camera.main.transform.LookAt(vars.vector);
                //LocalPlayer.Base.Transform.LookAt(, Camera.main.transform.up);
                func.AimAtPos(vars.vector);
                if (NextMouseClick < Time.time && Settings.CAimH.AutoShoot && Raycast.isBodyPartVisible(vars.PlayerAimedAt, (int)vars.BodyPart_AimedAt))
                {// no need to viss check here cause we already know target is visible - making less cykles is better
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp | MouseOperations.MouseEventFlags.LeftDown);
                    NextMouseClick = Time.time + 0.064f;
                }
                
            }
        }
        private static float NextMouseClick = 0f;
        private class func
        {
            private static class Target
            {
                public static Player choosed = null;
                public static Player first = null;
            }
            public static Player ChooseTarget(bool forceAimAt = false)
            {
                /*
                 * It should select first player from list (skipping looping through list casue we sort players in order of distence from center of screen)
                 * if first target is not visible then loop through all players on screen to check if they are visible and return them if they are
                 * if non of player was selected then check if button AIM ON EVERYONE is enabled if it is it ends there and no futher players are returned
                 * otherwise returned is first player (closest to crosshair)
                 */
                Target.first = vars.l_Player.FirstOrDefault();
                if (Target.first == default(Player))
                    return null; // no player on list
                                 // make sure noone is visible if visible snap on target
                if (Settings.CAimH.AimThroughWalls)
                {
                    vars.BodyPart_AimedAt = AimBodyPart.Head;
                    return Target.first;
                }
                vars.BodyPart_AimedAt = ChooseBodyPartToAim(Target.first, forceAimAt);
                if (vars.BodyPart_AimedAt != AimBodyPart.Null)
                {
                    return Target.first;
                }

                for (int i = 1; i < vars.l_Player.Count; i++)
                {
                    if (vars.l_Player[i] != null) {
                        vars.BodyPart_AimedAt = ChooseBodyPartToAim(vars.l_Player[i], forceAimAt);
                        if (vars.BodyPart_AimedAt != AimBodyPart.Null)
                        {
                            return vars.l_Player[i];
                        }
                    }
                }
                //if (vars.vb_mouseControl)
                return null;
               // return Target.first;
                #region Not Used - Other checks aka distance for all objects (its useless but will stay here
                /*
                 * additional checks for distance prioritising
                for (int i = 1; i < vars.l_Player.Count; i++)
                {
                    if (i == 5) break;
                    if (vars.l_Player[i] != null)
                        if (CompareDistances(Target.choosed, vars.l_Player[i]))
                            Target.choosed = vars.l_Player[i];
                }
                return Target.choosed;*/
                #endregion
            }
            private static AimBodyPart[] aimHierarhy = new AimBodyPart[20] {
                AimBodyPart.Head,
                AimBodyPart.Neck,
                AimBodyPart.Ribcage,
                AimBodyPart.Pelvis,
                AimBodyPart.LCalf,
                AimBodyPart.RCalf,
                AimBodyPart.LThigh1,
                AimBodyPart.RThigh1,
                AimBodyPart.LThigh2,
                AimBodyPart.RThigh2,
                AimBodyPart.LFoot,
                AimBodyPart.RFoot,
                AimBodyPart.LUpperarm,
                AimBodyPart.RUpperarm,
                AimBodyPart.LForearm1,
                AimBodyPart.RForearm1,
                AimBodyPart.LForearm2,
                AimBodyPart.RForearm2,
                AimBodyPart.LForearm3,
                AimBodyPart.RForearm3
            };
            private static AimBodyPart[] aimHierarhy_Killa = new AimBodyPart[14] {
                //AimBodyPart.LThigh1,
                AimBodyPart.LCalf,
                AimBodyPart.RCalf,
                AimBodyPart.LThigh2,
                AimBodyPart.RThigh2,
                AimBodyPart.LFoot,
                //AimBodyPart.RThigh1,
                AimBodyPart.RFoot,
                AimBodyPart.LUpperarm,
                AimBodyPart.RUpperarm,
                AimBodyPart.LForearm1,
                AimBodyPart.RForearm1,
                AimBodyPart.LForearm2,
                AimBodyPart.RForearm2,
                AimBodyPart.LForearm3,
                AimBodyPart.RForearm3
            };
            public static AimBodyPart ChooseBodyPartToAim(Player player, bool forceAimAt = false) {
                #region Description
                /*
                 * If forceAimAt is true then aim will return current selected aimed body part
                 * if its false then it will proceede with visual check per body part and will select body part in order
                 * if nothing was found will return Maoci_BodyPart.Null which means no bodypart was found
                 */
                #endregion
                AimBodyPart AimAtPart = AimBodyPart.Head;
                if (Settings.CAimH.KillaMode) 
                {
                    for (int i = 0; i < aimHierarhy_Killa.Length; i++)
                    {
                        if (Raycast.isBodyPartVisible(player, (int)aimHierarhy_Killa[i]))
                            return aimHierarhy_Killa[i];
                    }
                    return AimBodyPart.Null;
                }
                if (forceAimAt) 
                { // force to aim at specific body part
                    if (Settings.CAimH.ForceAimAtBodyPart == "head")
                    {
                        return AimAtPart; 
                    }
                    else if (Settings.CAimH.ForceAimAtBodyPart == "thorax")
                    {
                        return AimBodyPart.Pelvis;
                    }
                    else // if legs then go head
                    {
                        if (Raycast.isBodyPartVisible(player, (int)AimBodyPart.LCalf))
                        {
                            return AimBodyPart.LCalf;
                        }
                        if (Raycast.isBodyPartVisible(player, (int)AimBodyPart.RCalf))
                        {
                            return AimBodyPart.RCalf;
                        }
                    }
                    return AimBodyPart.Null;
                }
                // artificial aim at visible part of body
                for (int i = 0; i < aimHierarhy.Length; i++) {
                    if (Raycast.isBodyPartVisible(player, (int)aimHierarhy[i]))
                        return aimHierarhy[i];
                }
                return AimBodyPart.Null;
            }

            private static class BulletParams {
                public static float Distance;
                public static float Traveltime;
            }
            public static float BulletDrop(Vector3 startVector, Vector3 endVector, float BulletSpeed)
            {
                BulletParams.Distance = Vector3.Distance(startVector, endVector);
                if (BulletParams.Distance >= 50f)
                {
                    BulletParams.Traveltime = BulletParams.Distance / BulletSpeed;
                    return (float)(4.905 * BulletParams.Traveltime * BulletParams.Traveltime);
                }
                return 0f;
            }
            private static class AimingParams {
                public static Vector3 HandsPosition;
                public static Vector2 newRotation = Vector2.zero;
                public static Vector3 eulerAngles;
                //public static Vector3 ForwardDirection;
            }
            public static void AimAtPos(Vector3 target)
            {
                //LocalPlayer.Base.Transform.LookAt(target, Camera.main.transform.up); // could also works but not sure
                #region Maoci AimAtPos
                AimingParams.HandsPosition = GetFireportShootDirection(); // OK
                if (AimingParams.HandsPosition == Vector3.zero) return;
                    #region moving target back by 2 meters to skip snaping in close range (not working idea)
                    //AimingParams.ForwardDirection = (AimingParams.HandsPosition - target).normalized; // it should be a forward direction
                    //target = target + AimingParams.ForwardDirection * 1f; // this should move target back from you but it will not propably
                    #endregion
                    AimingParams.eulerAngles = Quaternion.LookRotation((target - AimingParams.HandsPosition).normalized).eulerAngles;
                if (AimingParams.eulerAngles.x > 180f)
                {
                    AimingParams.eulerAngles.x -= 360f;
                }
                AimingParams.newRotation.x = AimingParams.eulerAngles.y; // Mathf.DeltaAngle(-AimingParams.eulerAngles.y, AimingParams.eulerAngles.y);//Mathf.Clamp(AimingParams.eulerAngles.y, 0, 90);
                AimingParams.newRotation.y = Mathf.DeltaAngle(0, AimingParams.eulerAngles.x); //Mathf.DeltaAngle(0, AimingParams.eulerAngles.x); // delta angle
                LocalPlayer.Base.MovementContext.Rotation = AimingParams.newRotation;
                #endregion
                #region original Brayden's
                /*Vector3 b = GetFireportShootDirection();
                Vector3 eulerAngles = Quaternion.LookRotation((target - b).normalized).eulerAngles;
                if (eulerAngles.x > 180f)
                {
                    eulerAngles.x -= 360f;
                }
                LocalPlayer.Base.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x); */
                #endregion

            }
            public static Vector3 GetFireportPosition() {
                if (LocalPlayer.Base == null)
                    return Vector3.zero;
                Player.FirearmController firearmController = LocalPlayer.Base.HandsController as Player.FirearmController;
                if (firearmController == null)
                    return Vector3.zero;
                return firearmController.Fireport.position;
            }
            public static Vector3 GetFireportShootDirection()
            {
                return (GetFireportPosition() - Camera.main.transform.forward * 1.55f) + Camera.main.transform.forward * 1f;// - firearmController.Fireport.up * 1f); //fireport 
            }
            public static void AimAtSwitch()
            {
                if (Settings.CAimH.ForceAimAtBodyPart == "head")
                {
                    Settings.CAimH.ForceAimAtBodyPart = "thorax";
                }
                else if (Settings.CAimH.ForceAimAtBodyPart == "thorax")
                {
                    Settings.CAimH.ForceAimAtBodyPart = "legs";
                }
                else // if legs then go head
                {
                    Settings.CAimH.ForceAimAtBodyPart = "head";
                }
            }
#if ReleaseMaoci
            /*public static float CalcInFov(Vector3 Position)
            {
                Vector3 position = Camera.main.transform.position;
                Vector3 forward = Camera.main.transform.forward;
                Vector3 normalized = (Position - position).normalized;
                return Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, normalized), -1f, 1f)) * 57.29578f;
            }*/
#endif
        }
    }
}
