using ModMaker;
using ModMaker.Utility;
using TurnbasedCombatFix.Controllers;
using UnityEngine;
using UnityModManagerNet;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix.Menus
{
    public class GameplayOptions : IMenuSelectablePage
    {
        GUIStyle _buttonStyle;
        GUIStyle _labelStyle;

        public string Name => "GamePlay";

        public int Priority => 0;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled)
                return;

            if (_buttonStyle == null)
            {
                _buttonStyle = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft };
                _labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, padding = _buttonStyle.padding };
            }

            using (new GUISubScope())
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Reset Settings", _buttonStyle, GUILayout.ExpandWidth(false)))
                    {
                        Mod.Core.ResetSettings();
                    }
                }
            }


            using (new GUISubScope("Automation"))
                OnGUIAutomation();
        }

        private void OnGUIAutomation()
        {
            GUIHelper.ToggleButton(ref Main.Settings.skipPrepareForCombat,
                "Skip Prepare For Combat Prompt", _buttonStyle, GUILayout.ExpandWidth(false));

            GUIHelper.ToggleButton(ref Main.Settings.enablePausing,
                "Enable Pause During Turn Based Combat", PauseController.TogglePatches, PauseController.TogglePatches, _buttonStyle, GUILayout.ExpandWidth(false));

            GUIHelper.ToggleButton(ref Main.Settings.removeAnimationSpeedLimit,
                "Remove Animation Speed Limit", TimeController_Tick_Patch.TogglePatch, TimeController_Tick_Patch.TogglePatch, _buttonStyle, GUILayout.ExpandWidth(false));

        }
    }
}
