using ModMaker;
using ModMaker.Utility;
using UnityEngine;
using UnityModManagerNet;
using static ModMaker.Utility.RichTextExtensions;
using static TurnbasedCombatFix.Main;
using static TurnbasedCombatFix.Utility.SettingsWrapper;

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
                DoNotMarkInvisibleUnit =
                    GUIHelper.ToggleButton(DoNotMarkInvisibleUnit,
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
                ShowAttackIndicatorOfCurrentUnit =
                    GUIHelper.ToggleButton(ShowAttackIndicatorOfCurrentUnit,
                    "Show Attack Indicator Of Current Unit", _buttonStyle, GUILayout.ExpandWidth(false));

                ShowAttackIndicatorForPlayer =
                    GUIHelper.ToggleButton(ShowAttackIndicatorForPlayer,
                    "... For Player", _buttonStyle, GUILayout.ExpandWidth(false));

                ShowAttackIndicatorForNonPlayer =
                    GUIHelper.ToggleButton(ShowAttackIndicatorForNonPlayer,
                    "... For Non-Player", _buttonStyle, GUILayout.ExpandWidth(false));
            }

            ShowAttackIndicatorOnHoverUI =
                GUIHelper.ToggleButton(ShowAttackIndicatorOnHoverUI,
                "Show Attack Indicator On Hover", _buttonStyle, GUILayout.ExpandWidth(false));

            ShowAutoCastAbilityRange =
                GUIHelper.ToggleButton(ShowAutoCastAbilityRange,
                "Show Ability Range Instead Of Attack Range When Using Auto-Cast", _buttonStyle, GUILayout.ExpandWidth(false));

        }

        private void OnGUIMovementIndicator()
        {
            using (new GUILayout.HorizontalScope())
            {
                ShowMovementIndicatorOfCurrentUnit =
                    GUIHelper.ToggleButton(ShowMovementIndicatorOfCurrentUnit,
                    "Show Movement Indicator Of Current Unit", _buttonStyle, GUILayout.ExpandWidth(false));

                ShowMovementIndicatorForPlayer =
                    GUIHelper.ToggleButton(ShowMovementIndicatorForPlayer,
                    "... For Player", _buttonStyle, GUILayout.ExpandWidth(false));

                ShowMovementIndicatorForNonPlayer =
                    GUIHelper.ToggleButton(ShowMovementIndicatorForNonPlayer,
                    "... For Non-Player", _buttonStyle, GUILayout.ExpandWidth(false));
            }

            ShowMovementIndicatorOnHoverUI =
                GUIHelper.ToggleButton(ShowMovementIndicatorOnHoverUI,
                "Show Movement Indicator On Hover", _buttonStyle, GUILayout.ExpandWidth(false));
        }
    }
}
