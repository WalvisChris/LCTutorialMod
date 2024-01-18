using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTutorialMod.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void UpdatePatch(ref float ___sprintMeter, ref float ___movementSpeed)
        {
            if (TutorialModBase.Instance.ConfigManager.CustomSprint)
            {
                ___movementSpeed = TutorialModBase.Instance.ConfigManager.PlayerSpeed;
                ___sprintMeter = 1f; 
            }
        }

        [HarmonyPatch(nameof(PlayerControllerB.AllowPlayerDeath))]
        [HarmonyPostfix]
        static void PatchDeath(ref bool __result)
        {
            if (TutorialModBase.Instance.ConfigManager.GodMode) { __result = false; }
        }

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void StartPatch(PlayerControllerB __instance)
        {
            TutorialModBase.Instance.Player = __instance;
        }
    }
}
