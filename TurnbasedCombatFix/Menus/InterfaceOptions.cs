﻿using ModMaker;
using ModMaker.Utility;
using UnityEngine;
using UnityModManagerNet;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix.Menus
{
    public class InterfaceOptions : IMenuSelectablePage
    {
        GUIStyle _buttonStyle;
        GUIStyle _labelStyle;

        public string Name => "Interface";

        public int Priority => 200;

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
                    GUIHelper.ToggleButton(ref Main.Settings.toggleDoNotMarkInvisibleUnit,
                    "Don't mark invisible Units", _buttonStyle, GUILayout.ExpandWidth(false));
            }

            using (new GUISubScope("Attack Indicator"))
                OnGUIAttackIndicator();

            using (new GUISubScope("Movement Indicator"))
                OnGUIMovementIndicator();
        }

        private void OnGUIAttackIndicator()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUIHelper.ToggleButton(ref Main.Settings.showAttackIndicatorOfCurrentUnit,
                    "Show Attack Indicator Of Current Unit", _buttonStyle, GUILayout.ExpandWidth(false));

                GUIHelper.ToggleButton(ref Main.Settings.toggleShowAttackIndicatorForPlayer,
                    "... For Player", _buttonStyle, GUILayout.ExpandWidth(false));

                GUIHelper.ToggleButton(ref Main.Settings.toggleShowAttackIndicatorForNonPlayer,
                    "... For Non-Player", _buttonStyle, GUILayout.ExpandWidth(false));
            }

            GUIHelper.ToggleButton(ref Main.Settings.toggleShowAttackIndicatorOnHoverUI,
                "Show Attack Indicator On Hover", _buttonStyle, GUILayout.ExpandWidth(false));

            GUIHelper.ToggleButton(ref Main.Settings.toggleShowAutoCastAbilityRange,
                "Show Ability Range Instead Of Attack Range When Using Auto-Cast", _buttonStyle, GUILayout.ExpandWidth(false));

        }

        private void OnGUIMovementIndicator()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUIHelper.ToggleButton(ref Main.Settings.showMovementIndicatorOfCurrentUnit,
                    "Show Movement Indicator Of Current Unit", _buttonStyle, GUILayout.ExpandWidth(false));

                GUIHelper.ToggleButton(ref Main.Settings.showMovementIndicatorForPlayer,
                    "... For Player", _buttonStyle, GUILayout.ExpandWidth(false));

                GUIHelper.ToggleButton(ref Main.Settings.showMovementIndicatorForNonPlayer,
                    "... For Non-Player", _buttonStyle, GUILayout.ExpandWidth(false));
            }

            GUIHelper.ToggleButton(ref Main.Settings.toggleShowMovementIndicatorOnHoverUI,
                "Show Movement Indicator On Hover", _buttonStyle, GUILayout.ExpandWidth(false));
        }
    }
}
