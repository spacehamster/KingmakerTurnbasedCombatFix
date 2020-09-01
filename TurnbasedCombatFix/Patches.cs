using HarmonyLib;
using Kingmaker.Controllers;
using Kingmaker.UI._ConsoleUI.CombatStartScreen;
using Kingmaker.UI.TurnBasedMode;
using ModMaker.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace TurnbasedCombatFix
{
    //Disable Prepare For Combat Prompt
    [HarmonyPatch(typeof(TurnBasedModeUIController), "ShowCombatStartWindow")]
    static class TurnBasedModeUIController_ShowCombatStartWindow_Patch
    {
        static bool Prefix(ref CombatStartWindowVM ___m_CombatStartWindowVM)
        {
            try
            {
                if (!Main.Settings.skipPrepareForCombat)
                    return true;
                if (___m_CombatStartWindowVM != null)
                {
                    ___m_CombatStartWindowVM.Dispose();
                    ___m_CombatStartWindowVM = null;
                }
                return false;
            } catch(Exception ex)
            {
                Main.Error(ex);
            }
            return true;
        }
    }
    //Unlock speed limit
    [HarmonyPatch(typeof(TimeController), "Tick")]
    public static class TimeController_Tick_Patch
    {
        public static void TogglePatch()
        {
            Util.TogglePatch(Main.harmony, typeof(TimeController_Tick_Patch), Main.Settings.removeAnimationSpeedLimit);
        }
        //Only patch if enabled
        public static bool Prepare(MethodBase original)
        {
            return Main.Settings.removeAnimationSpeedLimit;
        }
        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original, IEnumerable<CodeInstruction> codes, ILGenerator il)
        {
            if (!Main.Settings.removeAnimationSpeedLimit)
            {
                return codes;
            }
            /*Remove
            if (this.PlayerTimeScale > 3f)
            {
                this.PlayerTimeScale = 3f;
            }*/
            var findingCodes = new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call,
                    AccessTools.Property(typeof(TimeController), "PlayerTimeScale").GetMethod),
                new CodeInstruction(OpCodes.Ldc_R4, 3f),
                new CodeInstruction(OpCodes.Ble_Un),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_R4, 3f),
                new CodeInstruction(OpCodes.Call,
                    AccessTools.Property(typeof(TimeController), "PlayerTimeScale").SetMethod),
            };
            int startIndex = codes.FindCodes(findingCodes);

            if (startIndex >= 0)
            {
                var newCodes = new CodeInstruction[] { };
                return codes.ReplaceAll(findingCodes, newCodes);
            }
            else
            {
                Core.FailedToPatch(MethodBase.GetCurrentMethod());
                return codes;
            }
        }
    }
}
