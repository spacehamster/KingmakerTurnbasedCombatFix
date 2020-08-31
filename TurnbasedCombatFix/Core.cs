using Kingmaker.Controllers;
using Kingmaker.PubSubSystem;
using ModMaker;
using ModMaker.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using TurnBased.Controllers;
using TurnbasedCombatFix.Controllers;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix
{
    public class Core :
        IModEventHandler,
        ISceneHandler
    {
        public HotkeyController Hotkeys { get; internal set; }

        public int Priority => 200;

        public UIController UI { get; internal set; }

        public bool Enabled
        {
            get => true;
            set
            {
                throw new NotImplementedException();
            }
        }

        public static void FailedToPatch(MethodBase patch)
        {
            Type type = patch.DeclaringType;
            Mod.Warning($"Failed to patch '{type.DeclaringType?.Name}.{type.Name}.{patch.Name}'");
        }

        public void ResetSettings()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            Mod.ResetSettings();
            Hotkeys?.Update(true, true);
        }

        private void HandleToggleTurnBasedMode()
        {
            Enabled = !Enabled;
        }

        public void HandleModEnable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            EventBus.Subscribe(this);
        }

        public void HandleModDisable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            EventBus.Unsubscribe(this);
        }

        public void OnAreaBeginUnloading() { }

        public void OnAreaDidLoad()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());
        }
    }
}