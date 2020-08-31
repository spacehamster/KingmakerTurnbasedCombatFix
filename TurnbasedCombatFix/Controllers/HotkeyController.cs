using Kingmaker.PubSubSystem;
using Kingmaker.UI;
using Kingmaker.UI.SettingsUI;
using ModMaker;
using ModMaker.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static TurnbasedCombatFix.Main;
using static TurnbasedCombatFix.Utility.SettingsWrapper;

namespace TurnbasedCombatFix.Controllers
{
    public class HotkeyController :
        IModEventHandler,
        ISceneHandler
    {
        public IDictionary<string, BindingKeysData> Hotkeys => Mod.Settings.hotkeys;

        public int Priority => 0;

        private void Initialize()
        {
            Dictionary<string, BindingKeysData> hotkeys = new Dictionary<string, BindingKeysData>()
            {
                {HOTKEY_FOR_TOGGLE_ATTACK_INDICATOR, new BindingKeysData() { IsAltDown = true, Key = KeyCode.R }},
                {HOTKEY_FOR_TOGGLE_MOVEMENT_INDICATOR, new BindingKeysData() { IsAltDown = true, Key = KeyCode.R }},
                {HOTKEY_FOR_PAUSE, new BindingKeysData() { IsAltDown = true, Key = KeyCode.Q }},
                {HOTKEY_FOR_DEBUG_UI, new BindingKeysData() { IsAltDown = true, Key = KeyCode.D }},
           };

            // remove invalid keys from the settings
            foreach (string name in Hotkeys.Keys.ToList())
                if (!hotkeys.ContainsKey(name))
                    Hotkeys.Remove(name);

            // add missing keys to the settings
            foreach (KeyValuePair<string, BindingKeysData> item in hotkeys)
                if (!Hotkeys.ContainsKey(item.Key))
                    Hotkeys.Add(item.Key, item.Value);
        }

        public void SetHotkey(string name, BindingKeysData value)
        {
            Hotkeys[name] = value;
            TryRegisterHotkey(name, value);
        }

        private void TryRegisterHotkey(string name, BindingKeysData value)
        {
            Mod.Debug(MethodBase.GetCurrentMethod(), name, HotkeyHelper.GetKeyText(value));

            if (value != null)
                HotkeyHelper.RegisterKey(name, value, KeyboardAccess.GameModesGroup.World);
            else
                HotkeyHelper.UnregisterKey(name);
        }

        public void Update(bool initialize, bool register)
        {
            Mod.Debug(MethodBase.GetCurrentMethod(), initialize, register);

            if (initialize)
                Initialize();

            if (register)
                foreach (KeyValuePair<string, BindingKeysData> item in Hotkeys)
                    TryRegisterHotkey(item.Key, item.Value);
            else
                foreach (string name in Hotkeys.Keys)
                    TryRegisterHotkey(name, null);
        }

        public void HandleModEnable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            Mod.Core.Hotkeys = this;
            Update(true, true);

            EventBus.Subscribe(this);
        }

        public void HandleModDisable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            EventBus.Unsubscribe(this);

            Update(false, false);
            Mod.Core.Hotkeys = null;
        }

        public void OnAreaBeginUnloading() { }

        public void OnAreaDidLoad()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            Update(false, true);
        }
    }
}