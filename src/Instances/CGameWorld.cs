using EFT;
using EFT.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exception6.Instances
{
    public class CGameWorld : MonoBehaviour
    {
        public static GameWorld Base = null;
       //public static PreloaderUI preloaderUI = null;
        public static EFT.UI.PreloaderUI PreloaderUI;
        public static string SceneName = "";
        public static bool LoadedIntoMap = false;
        public class DisableFrustrumCulling : MonoBehaviour
        {
            private Camera cam;

            void Start()
            {
                
            }



            void OnDisable()
            {
                cam.ResetCullingMatrix();
            }
        }
        public static bool IsSpawnedInWorld() {
            return functions.IsSpawnedInWorld();
        }
        private static class functions {
            public static void FindActiveGameWorld()
            {
                #region setGameworld
                try
                {
                    if (GameWorldActive())
                        Base = Comfort.Common.Singleton<GameWorld>.Instance;
                    else
                        Base = null;

                }
                catch { Base = null; }
                #endregion
            }
            public static string GetActiveSceneName()
            {
                #region setNameOfScene
                try
                {
                    return SceneManager.GetActiveScene().name;
                }
                catch
                {
                    return "";
                }
                #endregion
            }
            public static bool IsSpawnedInWorld()
            {
                if ((SceneIsLoaded() && GameWorldActive() && PlayerIsInMatch() && BlackScreenNotActive()))
                {
                    return true;
                }
                return false;
            }
            public static void FindActivePreloaderUI()
            {
                try
                {
                    if (MonoBehaviourSingleton<EFT.UI.PreloaderUI>.Instantiated)
                        PreloaderUI = MonoBehaviourSingleton<EFT.UI.PreloaderUI>.Instance;
                    else
                        PreloaderUI = null;
                }
                catch { PreloaderUI = null; }
            }
            public static void BetaVersionRenamer()
            {
                if (Settings.CGameWorld.Enable_RenamingSession)
                {
                    PreloaderUI.SetSessionId(Settings.CGameWorld.ReplaceSessionIDto);
                }
                else
                {
                    if (Settings.CGameWorld.Enable_PseudoStreamMode)
                    {
                        PreloaderUI.SetStreamMode(true);
                    }
                }
            }
            //-- PRIVATE --
            private static bool BlackScreenNotActive() {
                return !PreloaderUI.Instance.IsBackgroundBlackActive;
            }
            private static bool AwaitingDeploy() {
                return false;//GClass1790.CheckCurrentScreen(EFT.UI.Screens.EScreenType.FinalCountdown) || GClass1790.CheckCurrentScreen(EFT.UI.Screens.EScreenType.TimeHasCome);
            }
            private static bool PlayerIsInMatch()
            {
                #region player in match ? true
                return SceneName != "EnvironmentUIScene" &&
                       SceneName != "MenuUIScene" &&
                       SceneName != "CommonUIScene" &&
                       SceneName != "MainScene" &&
                       SceneName != "";
                #endregion
            }
            private static bool SceneIsLoaded()
            {
                #region Scene is loaded ? true
                try
                {
                    return SceneManager.GetActiveScene().isLoaded;
                }
                catch
                {
                    return false;
                }
                #endregion
            }
            private static bool GameWorldActive()
            {
                return Comfort.Common.Singleton<GameWorld>.Instantiated;
            }
        }
        [ObfuscationAttribute(Exclude = true)]
        void Awake()
        {
            LoadedIntoMap = false;
            functions.FindActiveGameWorld(); // try to grab that data at awake of this function but it will not work at all
        }
        [ObfuscationAttribute(Exclude = true)]
        void OnPreCull()
        {
            if (Camera.main != null)
                Camera.main.cullingMatrix = Matrix4x4.Ortho(-99999, 99999, -99999, 99999, 0.001f, 99999) *
                                    Matrix4x4.Translate(Vector3.forward * -99999 / 2f) *
                                    Camera.main.worldToCameraMatrix;
        }
        public static void onUpdate() {
            SceneName = functions.GetActiveSceneName();
            if (Base == null)
                functions.FindActiveGameWorld();
            if (!PreloaderUI || PreloaderUI == null)
            {
                functions.FindActivePreloaderUI();
            }
            else
            {
                functions.BetaVersionRenamer();
            }
        }
        [ObfuscationAttribute(Exclude = true)]
        void Update()
        {
            onUpdate();
        }
    }
}
