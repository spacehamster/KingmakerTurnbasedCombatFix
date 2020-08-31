using HarmonyLib;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.AbilityTarget;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using ModMaker.Utility;
using System.Reflection;
using TurnBased.Controllers;
using TurnbasedCombatFix.Utility;
using UnityEngine;
using static TurnbasedCombatFix.Main;
using static TurnbasedCombatFix.Utility.SettingsWrapper;
/*
namespace TurnbasedCombatFix.UI
{
    public class AttackIndicatorManager :
        MonoBehaviour,
        IAbilityTargetHoverUIHandler,
        IAbilityTargetSelectionUIHandler,
        IShowAoEAffectedUIHandler
    {
        private bool _isAbilityHovered;
        private bool _isAbilitySelected;
        private bool _isHandlingAOEMove;
        private RangeIndicatorManager _range;
        AccessTools.FieldRef<TurnController, UnitEntityData> m_HighlightedObjectRef;

        public UnitEntityData Unit { get; private set; }

        public static AttackIndicatorManager CreateObject()
        {
            GameObject abilityTargetSelect = Game.Instance.IsControllerMouse ?
                Game.Instance.UI.Common?.transform.Find("CombatCursorPC")?.gameObject :
                Game.Instance.UI.Common?.transform.Find("Console_TargetSelect")?.gameObject;
            GameObject aoeRange = abilityTargetSelect?.GetComponent<AbilityAoERange>().Range;

            if (!aoeRange)
                return null;

            GameObject attackIndicator = new GameObject("TurnBasedAttackIndicator");
            attackIndicator.transform.SetParent(abilityTargetSelect.transform, true);

            AttackIndicatorManager attackIndicatorManager = attackIndicator.AddComponent<AttackIndicatorManager>();

            attackIndicatorManager._range = RangeIndicatorManager.CreateObject(aoeRange, "AttackRange", false);
            DontDestroyOnLoad(attackIndicatorManager._range.gameObject);
            attackIndicatorManager.m_HighlightedObjectRef =
                AccessTools.FieldRefAccess<TurnController, UnitEntityData>("m_HighlightedUnit");

            return attackIndicatorManager;
        }

        void OnEnable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            HotkeyHelper.Bind(HOTKEY_FOR_TOGGLE_ATTACK_INDICATOR, HandleToggleAttackIndicator);
            EventBus.Subscribe(this);
        }

        void OnDisable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            EventBus.Unsubscribe(this);
            HotkeyHelper.Unbind(HOTKEY_FOR_TOGGLE_ATTACK_INDICATOR, HandleToggleAttackIndicator);
        }

        void OnDestroy()
        {
            _range.SafeDestroy();
        }

        void Update()
        {
            if (CombatController.IsInTurnBasedCombat() && !_isAbilityHovered && !_isAbilitySelected)
            {
                UnitEntityData unit = null;
                float radius = 0f;
                bool canTargetEnemies = true;
                bool canTargetFriends = false;

                var controller = Game.Instance.TurnBasedCombatController;
                var currentTurn = controller.CurrentTurn;
                if (currentTurn == null) return;


                if (ShowAttackIndicatorOnHoverUI && (unit = m_HighlightedObjectRef(currentTurn)) != null)
                {
                    GetRadius();
                }
                else
                {
                    if (ShowAttackIndicatorOfCurrentUnit && (unit = CombatController.CurrentUnit) != null &&
                        (unit.IsDirectlyControllable ? ShowAttackIndicatorForPlayer : ShowAttackIndicatorForNonPlayer))
                    {
                        GetRadius();

                        if (radius > 0f && currentTurn.EnabledFiveFootStep)
                        {
                            radius += currentTurn.GetRemainingMovementRange(true, false);
                        }
                    }
                }

                if (unit != null && radius > 0 && (!DoNotMarkInvisibleUnit || unit.IsVisibleForPlayer))
                {
                    _range.SetPosition(unit);
                    _range.SetRadius(radius);
                    _range.SetVisible(true);

                    Unit = unit;
                    EventBus.RaiseEvent<IShowAoEAffectedUIHandler>
                        (h => h.HandleAoEMove(new Vector3(radius, canTargetEnemies ? 1f : 0f, canTargetFriends ? 1f : 0f), null));

                    return;
                }

                void GetRadius()
                {
                    AbilityData ability = ShowAutoCastAbilityRange ? unit.GetAvailableAutoUseAbility() : null;
                    if (ability != null)
                    {
                        radius = ability.GetAbilityRadius();
                        canTargetEnemies = ability.Blueprint.CanTargetEnemies;
                        canTargetFriends = ability.Blueprint.CanTargetFriends;
                        _range.VisibleColor = ability.TargetAnchor == AbilityTargetAnchor.Owner ? Color.green : Color.yellow;
                    }
                    else
                    {
                        radius = unit.GetAttackRadius();
                        _range.VisibleColor = Color.red;
                    }
                }
            }

            if (Unit != null)
            {
                _range.SetVisible(false);

                Unit = null;
                if (_isHandlingAOEMove)
                    EventBus.RaiseEvent<IShowAoEAffectedUIHandler>(h => h.HandleAoECancel());
            }
        }

        private void HandleToggleAttackIndicator()
        {
            ShowAttackIndicatorOfCurrentUnit = !ShowAttackIndicatorOfCurrentUnit;
        }

        public void HandleAbilityTargetHover(AbilityData ability, bool hover)
        {
            _isAbilityHovered = hover;
        }

        public void HandleAbilityTargetSelectionStart(AbilityData ability)
        {
            _isAbilitySelected = true;
        }

        public void HandleAbilityTargetSelectionEnd(AbilityData ability)
        {
            _isAbilitySelected = false;
        }

        public void HandleAoEMove(Vector3 pos, AbilityData ability)
        {
            _isHandlingAOEMove = ability == null;
        }

        public void HandleAoECancel()
        {
            _isHandlingAOEMove = false;
        }
    }
}*/