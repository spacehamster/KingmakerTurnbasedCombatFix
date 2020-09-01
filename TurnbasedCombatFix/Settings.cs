using Kingmaker.UI.SettingsUI;
using ModMaker.Utility;
using UnityModManagerNet;

namespace TurnbasedCombatFix
{
    public class Settings : UnityModManager.ModSettings
    {
        // gameplay
        public bool skipPrepareForCombat = true;
        public bool enablePausing = true;
        public bool removeAnimationSpeedLimit = true;
        // interface
        public bool toggleDoNotMarkInvisibleUnit = true;
        public bool showAttackIndicatorOfCurrentUnit = true;
        public bool toggleShowAttackIndicatorForPlayer = true;
        public bool toggleShowAttackIndicatorForNonPlayer;
        public bool toggleShowAttackIndicatorOnHoverUI = true;
        public bool toggleShowAutoCastAbilityRange = true;
        public bool toggleCheckForObstaclesOnTargeting = true;

        public bool showMovementIndicatorOfCurrentUnit = true;
        public bool showMovementIndicatorForPlayer = true;
        public bool showMovementIndicatorForNonPlayer;
        public bool toggleShowMovementIndicatorOnHoverUI;

        // hotkeys
        public SerializableDictionary<string, BindingKeysData> hotkeys = new SerializableDictionary<string, BindingKeysData>();
    }
}
