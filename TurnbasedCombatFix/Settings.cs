using Kingmaker.UI.SettingsUI;
using ModMaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;

namespace TurnbasedCombatFix
{
    public class Settings : UnityModManager.ModSettings
    {
        // gameplay
        public bool toggleSkipPrepareForCombat = true;
        public bool removeAnimationSpeedLimit = true;
        // interface
        public bool toggleDoNotMarkInvisibleUnit = true;
        public bool toggleShowAttackIndicatorOfCurrentUnit = true;
        public bool toggleShowAttackIndicatorForPlayer = true;
        public bool toggleShowAttackIndicatorForNonPlayer;
        public bool toggleShowAttackIndicatorOnHoverUI = true;
        public bool toggleShowAutoCastAbilityRange = true;
        public bool toggleCheckForObstaclesOnTargeting = true;

        public bool toggleShowMovementIndicatorOfCurrentUnit = true;
        public bool toggleShowMovementIndicatorForPlayer = true;
        public bool toggleShowMovementIndicatorForNonPlayer;
        public bool toggleShowMovementIndicatorOnHoverUI;

        // hotkeys
        public SerializableDictionary<string, BindingKeysData> hotkeys = new SerializableDictionary<string, BindingKeysData>();
    }
}
