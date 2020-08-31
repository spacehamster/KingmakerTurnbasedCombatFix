using HarmonyLib;
using Kingmaker;
using Kingmaker.Controllers;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using ModMaker;
using ModMaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TurnBased.Controllers;
using static TurnbasedCombatFix.Utility.SettingsWrapper;

namespace TurnbasedCombatFix.Controllers
{
    public class PauseController :
        IModEventHandler
    {
        public int Priority => 100;

        public void HandleModEnable()
        {
            Main.Mod.Debug(MethodBase.GetCurrentMethod());
            HotkeyHelper.Bind(HOTKEY_FOR_PAUSE, HandlePause);
            EventBus.Subscribe(this);
        }

        public void HandleModDisable()
        {
            Main.Mod.Debug(MethodBase.GetCurrentMethod());
            HotkeyHelper.Unbind(HOTKEY_FOR_PAUSE, HandlePause);
            EventBus.Unsubscribe(this);
        }

        private void HandlePause()
        {
            Main.Mod.Debug("Pause Requested");
            if(Game.Instance.CurrentMode != GameModeType.None && CombatController.IsInTurnBasedCombat())
            {
                Main.Mod.Debug("Pausing game");
                Game.Instance.IsPaused = !Game.Instance.IsPaused;
            }
        }
        [HarmonyPatch(typeof(TbmPauseController), "Tick")]
        public static class TbmPauseController_Tick_Patch
        {
            public static bool Prefix()
            {
                return false;
            }
        }
        [HarmonyPatch(typeof(Game), "DoStartMode")]
        public static class Game_DoStartMode_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(MethodBase original, IEnumerable<CodeInstruction> codes, ILGenerator il)
            {
                /*Remove
                if (CombatController.IsInTurnBasedCombat())
                {
                    return;
                }*/
                var findingCodes = new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Call,
                        AccessTools.Method(typeof(CombatController), "IsInTurnBasedCombat")),
                    new CodeInstruction(OpCodes.Brfalse_S),
                    new CodeInstruction(OpCodes.Ret)
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
}
