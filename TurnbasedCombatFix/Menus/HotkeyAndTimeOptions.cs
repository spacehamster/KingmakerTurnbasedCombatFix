using Kingmaker.UI.SettingsUI;
using ModMaker;
using ModMaker.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityModManagerNet;
using static ModMaker.Utility.RichTextExtensions;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix.Menus
{
    public class HotkeyAndTimeOptions : IMenuSelectablePage
    {
        private string _waitingHotkeyName;

        GUIStyle _buttonStyle;
        GUIStyle _downButtonStyle;
        GUIStyle _labelStyle;

        public string Name => "Hotkey & Time";

        public int Priority => 400;

        public void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Mod == null || !Mod.Enabled)
                return;

            if (_buttonStyle == null)
            {
                _buttonStyle = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft };
                _downButtonStyle = new GUIStyle(_buttonStyle)
                {
                    focused = _buttonStyle.active,
                    normal = _buttonStyle.active,
                    hover = _buttonStyle.active
                };
                _downButtonStyle.active.textColor = Color.gray;
                _labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, padding = _buttonStyle.padding };
            }

            using (new GUISubScope("Hotkey"))
                OnGUIHotkey();

            using (new GUISubScope("Time"))
                OnGUITime();

            using (new GUISubScope("Pause"))
                OnGUIPause();
        }

        private void OnGUIHotkey()
        {
            if (!string.IsNullOrEmpty(_waitingHotkeyName) && HotkeyHelper.ReadKey(out BindingKeysData newKey))
            {
                Mod.Core.Hotkeys.SetHotkey(_waitingHotkeyName, newKey);
                _waitingHotkeyName = null;
            }

            IDictionary<string, BindingKeysData> hotkeys = Mod.Core.Hotkeys.Hotkeys;

            using (new GUILayout.HorizontalScope())
            {
                using (new GUILayout.VerticalScope())
                {
                    foreach (KeyValuePair<string, BindingKeysData> item in hotkeys)
                    {
                        GUIHelper.ToggleButton(item.Value != null, item.Key, _labelStyle, GUILayout.ExpandWidth(false));
                    }
                }

                GUILayout.Space(10f);

                using (new GUILayout.VerticalScope())
                {
                    foreach (BindingKeysData key in hotkeys.Values)
                    {
                        GUILayout.Label(HotkeyHelper.GetKeyText(key));
                    }
                }

                GUILayout.Space(10f);

                using (new GUILayout.VerticalScope())
                {
                    foreach (string name in hotkeys.Keys)
                    {
                        bool waitingThisHotkey = _waitingHotkeyName == name;
                        if (GUILayout.Button("Set", waitingThisHotkey ? _downButtonStyle : _buttonStyle))
                        {
                            if (waitingThisHotkey)
                                _waitingHotkeyName = null;
                            else
                                _waitingHotkeyName = name;
                        }
                    }
                }

                using (new GUILayout.VerticalScope())
                {
                    string hotkeyToClear = default;
                    foreach (string name in hotkeys.Keys)
                    {
                        if (GUILayout.Button("Clear", _buttonStyle))
                        {
                            hotkeyToClear = name;

                            if (_waitingHotkeyName == name)
                                _waitingHotkeyName = null;
                        }
                    }
                    if (!string.IsNullOrEmpty(hotkeyToClear))
                        Mod.Core.Hotkeys.SetHotkey(hotkeyToClear, null);
                }

                using (new GUILayout.VerticalScope())
                {
                    foreach (KeyValuePair<string, BindingKeysData> item in hotkeys)
                    {
                        if (item.Value != null && !HotkeyHelper.CanBeRegistered(item.Key, item.Value))
                        {
                            GUILayout.Label("Duplicated!!".Color(RGBA.yellow));
                        }
                        else
                        {
                            GUILayout.Label(string.Empty);
                        }
                    }
                }

                GUILayout.FlexibleSpace();
            }

        }

        private void OnGUITime()
        {
   
        }

        private void OnGUIPause()
        {
        }
    }
}
