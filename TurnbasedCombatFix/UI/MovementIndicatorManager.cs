using HarmonyLib;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.AbilityTarget;
using ModMaker.Utility;
using System.Reflection;
using TurnBased.Controllers;
using UnityEngine;
using static TurnbasedCombatFix.Main;
using static TurnbasedCombatFix.Utility.SettingsWrapper;
/*
namespace TurnbasedCombatFix.UI
{
    public class MovementIndicatorManager : MonoBehaviour
    {
        private RangeIndicatorManager _rangeInner;
        private RangeIndicatorManager _rangeOuter;
        AccessTools.FieldRef<TurnController, UnitEntityData> m_HighlightedObjectRef;
        public static MovementIndicatorManager CreateObject()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            GameObject abilityTargetSelect = Game.Instance.IsControllerMouse ?
                Game.Instance.UI.Common?.transform.Find("CombatCursorPC")?.gameObject :
                Game.Instance.UI.Common?.transform.Find("Console_TargetSelect")?.gameObject;
            GameObject aoeRange = abilityTargetSelect?.GetComponent<AbilityAoERange>().Range;
            if (aoeRange == null) Mod.Error("AoeRange is null");
            if (!aoeRange)
                return null;

            GameObject movementIndicator = new GameObject("TurnBasedMovementIndicator");
            movementIndicator.transform.SetParent(abilityTargetSelect.transform, true);

            MovementIndicatorManager movementIndicatorManager = movementIndicator.AddComponent<MovementIndicatorManager>();

            movementIndicatorManager._rangeInner = RangeIndicatorManager.CreateObject(aoeRange, "MovementRangeInner");
            movementIndicatorManager._rangeInner.VisibleColor = Color.white;
            DontDestroyOnLoad(movementIndicatorManager._rangeInner.gameObject);

            movementIndicatorManager._rangeOuter = RangeIndicatorManager.CreateObject(aoeRange, "MovementRangeOuter");
            movementIndicatorManager._rangeOuter.VisibleColor = Color.white;
            DontDestroyOnLoad(movementIndicatorManager._rangeOuter.gameObject);

            movementIndicatorManager.m_HighlightedObjectRef = 
                AccessTools.FieldRefAccess<TurnController, UnitEntityData>("m_HighlightedUnit");
            return movementIndicatorManager;
        }

        void OnEnable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            HotkeyHelper.Bind(HOTKEY_FOR_TOGGLE_MOVEMENT_INDICATOR, HandleToggleMovementIndicator);
        }

        void OnDisable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            HotkeyHelper.Unbind(HOTKEY_FOR_TOGGLE_MOVEMENT_INDICATOR, HandleToggleMovementIndicator);
        }

        void OnDestroy()
        {
            _rangeInner.SafeDestroy();
            _rangeOuter.SafeDestroy();
        }

        void Update()
        {
            if (CombatController.IsInTurnBasedCombat())
            {
                UnitEntityData unit = null;
                float radiusInner = 0f;
                float radiusOuter = 0f;

                var controller = Game.Instance.TurnBasedCombatController;
                var currentTurn = controller.CurrentTurn;
                if (currentTurn == null) return;

                if (ShowMovementIndicatorOnHoverUI && (unit = m_HighlightedObjectRef(currentTurn)) != null)
                {
                    radiusInner = unit.CurrentSpeedMps * TIME_MOVE_ACTION;
                    radiusOuter = radiusInner * 2f;
                }
                else
                {
                    if (ShowMovementIndicatorOfCurrentUnit && (unit = CombatController.CurrentUnit) != null &&
                        (unit.IsDirectlyControllable ? ShowMovementIndicatorForPlayer : ShowMovementIndicatorForNonPlayer))
                    {

                        radiusInner = currentTurn.GetRemainingMovementRange(true, false);
                        radiusOuter = currentTurn.GetRemainingMovementRange(true, true);
                    }
                }

                if (unit != null && radiusOuter > 0 && (!DoNotMarkInvisibleUnit || unit.IsVisibleForPlayer))
                {
                    _rangeOuter.SetPosition(unit);
                    _rangeOuter.SetRadius(radiusOuter);
                    _rangeOuter.SetVisible(true);

                    if (radiusOuter != radiusInner)
                    {
                        _rangeInner.SetPosition(unit);
                        _rangeInner.SetRadius(radiusInner);
                        _rangeInner.SetVisible(true);
                    }
                    else
                    {
                        _rangeInner.SetVisible(false);
                    }

                    return;
                }
            }

            _rangeInner.SetVisible(false);
            _rangeOuter.SetVisible(false);
        }

        private void HandleToggleMovementIndicator()
        {
            ShowMovementIndicatorOfCurrentUnit = !ShowMovementIndicatorOfCurrentUnit;
        }
    }
}*/