using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LCTutorialMod.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTutorialMod
{
    [BepInPlugin(modGUID, modName, modVersion)]

    public class TutorialModBase : BaseUnityPlugin
    {
        private const string modGUID = "Chris.LCTutorialMod";
        private const string modName = "Chris LC Tutorial Mod";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        internal static TutorialModBase Instance;

        internal static ManualLogSource mls;

        #region InstanceVars
        internal ConfigurationController ConfigManager;
        internal ModMenu Menu;

        // stores all enemy AI reference
        internal List<SpawnableEnemyWithRarity> IndoorEnemyList;
        internal List<SpawnableEnemyWithRarity> OutdoorEnemyList;

        // current level always exists while in a match
        internal RoundManager CurrentRound;
        // will only be set if the player is host
        internal SelectableLevel CurrentLevel;
        internal PlayerControllerB Player;

        internal bool EnemySpawnsFixed = false;

        #endregion

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo(modName + " loaded successfully");

            //harmony.PatchAll(typeof(PlayerControllerBPatch));
            //harmony.PatchAll(typeof(StartOfRoundPatch));
            harmony.PatchAll(typeof(QuickMenuManagerPatch));
            harmony.PatchAll(typeof(RoundManagerPatch));

            mls = Logger;

            ConfigManager = new ConfigurationController(Config);

            Task.Delay(2000).ContinueWith(t => { CreateMenu(); });
        }

        private void CreateMenu()
        {
            var gameObject = new UnityEngine.GameObject("ModMenu");
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = UnityEngine.HideFlags.HideAndDontSave;
            gameObject.AddComponent<ModMenu>();
            Menu = (ModMenu)gameObject.GetComponent("ModMenu");
        }
    }
}