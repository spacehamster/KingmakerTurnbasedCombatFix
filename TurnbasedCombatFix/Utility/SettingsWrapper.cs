using Kingmaker.Utility;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix.Utility
{
    public static class SettingsWrapper
    {
        // hotkeys
        public const string HOTKEY_PREFIX = "Hotkey_";
        public const string HOTKEY_FOR_TOGGLE_ATTACK_INDICATOR = HOTKEY_PREFIX + "Toggle_AttackIndicator";
        public const string HOTKEY_FOR_TOGGLE_MOVEMENT_INDICATOR = HOTKEY_PREFIX + "Toggle_MovementIndicator";
        public const string HOTKEY_FOR_PAUSE = HOTKEY_PREFIX + "Toggle_Pause";
        public const string HOTKEY_FOR_DEBUG_UI = HOTKEY_PREFIX + "Toggle_DebugUI";

        public static bool SkipPrepareForCombatPrompt
        {
            get => Mod.Settings.toggleSkipPrepareForCombat;
            set => Mod.Settings.toggleSkipPrepareForCombat = value;
        }

        public static bool RemoveAnimationSpeedLimit
        {
            get => Mod.Settings.removeAnimationSpeedLimit;
            set => Mod.Settings.removeAnimationSpeedLimit = value;
        }

        public static bool DoNotMarkInvisibleUnit
        {
            get => Mod.Settings.toggleDoNotMarkInvisibleUnit;
            set => Mod.Settings.toggleDoNotMarkInvisibleUnit = value;
        }

        public static bool ShowAttackIndicatorOfCurrentUnit
        {
            get => Mod.Settings.toggleShowAttackIndicatorOfCurrentUnit;
            set => Mod.Settings.toggleShowAttackIndicatorOfCurrentUnit = value;
        }

        public static bool ShowAttackIndicatorForPlayer
        {
            get => Mod.Settings.toggleShowAttackIndicatorForPlayer;
            set => Mod.Settings.toggleShowAttackIndicatorForPlayer = value;
        }

        public static bool ShowAttackIndicatorForNonPlayer
        {
            get => Mod.Settings.toggleShowAttackIndicatorForNonPlayer;
            set => Mod.Settings.toggleShowAttackIndicatorForNonPlayer = value;
        }

        public static bool ShowAttackIndicatorOnHoverUI
        {
            get => Mod.Settings.toggleShowAttackIndicatorOnHoverUI;
            set => Mod.Settings.toggleShowAttackIndicatorOnHoverUI = value;
        }

        public static bool ShowAutoCastAbilityRange
        {
            get => Mod.Settings.toggleShowAutoCastAbilityRange;
            set => Mod.Settings.toggleShowAutoCastAbilityRange = value;
        }


        public static bool ShowMovementIndicatorOfCurrentUnit
        {
            get => Mod.Settings.toggleShowMovementIndicatorOfCurrentUnit;
            set => Mod.Settings.toggleShowMovementIndicatorOfCurrentUnit = value;
        }

        public static bool ShowMovementIndicatorForPlayer
        {
            get => Mod.Settings.toggleShowMovementIndicatorForPlayer;
            set => Mod.Settings.toggleShowMovementIndicatorForPlayer = value;
        }

        public static bool ShowMovementIndicatorForNonPlayer
        {
            get => Mod.Settings.toggleShowMovementIndicatorForNonPlayer;
            set => Mod.Settings.toggleShowMovementIndicatorForNonPlayer = value;
        }

        public static bool ShowMovementIndicatorOnHoverUI
        {
            get => Mod.Settings.toggleShowMovementIndicatorOnHoverUI;
            set => Mod.Settings.toggleShowMovementIndicatorOnHoverUI = value;
        }
    }
}
