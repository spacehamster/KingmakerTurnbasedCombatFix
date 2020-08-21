using HarmonyLib;
using Kingmaker.UI._ConsoleUI.CombatStartScreen;
using Kingmaker.UI.TurnBasedMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnbasedCombatFix
{
    class Patches
    {
        [HarmonyPatch(typeof(TurnBasedModeUIController), "ShowCombatStartWindow")]
        static class TurnBasedModeUIController_ShowCombatStartWindow_Patch
        {
            static bool Prefix(ref CombatStartWindowVM ___m_CombatStartWindowVM)
            {
                if (___m_CombatStartWindowVM != null)
                {
                    Main.Log("Skipping show combat window");
                    ___m_CombatStartWindowVM.Dispose();
                    ___m_CombatStartWindowVM = null;
                }
                else
                {
                    Main.Log("CombatStartWindow is null");
                }
                return false;
            }
        }
    }
}
