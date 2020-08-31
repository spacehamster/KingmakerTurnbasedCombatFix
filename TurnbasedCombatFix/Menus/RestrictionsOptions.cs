using ModMaker;
using ModMaker.Utility;
using UnityEngine;
using UnityModManagerNet;
using static ModMaker.Utility.RichTextExtensions;
using static TurnbasedCombatFix.Main;
using static TurnbasedCombatFix.Utility.SettingsWrapper;

namespace TurnbasedCombatFix.Menus
{
    public class RestrictionsOptions : IMenuSelectablePage
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
            SkipPrepareForCombatPrompt =
                GUIHelper.ToggleButton(SkipPrepareForCombatPrompt,
                "Skip Prepare For Combat Prompt", _buttonStyle, GUILayout.ExpandWidth(false));
        }
    }
}
