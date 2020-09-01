using HarmonyLib;
using Kingmaker;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Commands;
using ModMaker.Utility;
using Pathfinding;
using System.Collections.Generic;
using System.Reflection;
using TurnBased.Controllers;
using UnityEngine;
using static TurnbasedCombatFix.Utility.SettingsWrapper;

namespace TurnbasedCombatFix.UI
{
    public class DebugUIManager : MonoBehaviour
    {
        public static DebugUIManager Instance;
        public static Rect rect;
        static AccessTools.FieldRef<CombatController, List<CombatController.TBUnitInfo>> m_UnitsRef;
        public bool enabled;
        public static DebugUIManager CreateObject()
        {
            var go = new GameObject("TurnbasedDebugUI");
            Instance = go.AddComponent<DebugUIManager>();
            DontDestroyOnLoad(go);
            rect = new Rect(10, 10, 1000, 1000);
            m_UnitsRef = AccessTools.FieldRefAccess<CombatController, List<CombatController.TBUnitInfo>>("m_Units");

            return Instance;
        }
        public static void SafeDestroy() {
            if(Instance != null) Destroy(Instance);
        }

        void OnEnable()
        {
            Main.Mod.Debug(MethodBase.GetCurrentMethod());

            HotkeyHelper.Bind(HOTKEY_FOR_DEBUG_UI, Toggle);
            EventBus.Subscribe(this);
        }

        void OnDisable()
        {
            Main.Mod.Debug(MethodBase.GetCurrentMethod());

            EventBus.Unsubscribe(this);
            HotkeyHelper.Unbind(HOTKEY_FOR_DEBUG_UI, Toggle);
        }
        void Toggle()
        {
            enabled = !enabled;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Popcron.Gizmos.Enabled = !Popcron.Gizmos.Enabled;
            }
            if (!Popcron.Gizmos.Enabled) return;
            for (int j = 0; j < AstarPath.active.graphs.Length; j++)
            {
                if (AstarPath.active.graphs[j] != null)
                {
                    Color? color = null;
                    if(j == 0)
                    {
                        color = Color.red;
                    }
                    if(j == 1)
                    {
                        color = Color.cyan;
                    }
                    DrawGraph(AstarPath.active.graphs[j], color);
                }
            }
        }
        static void DrawGraph(NavGraph graph, Color? color = null)
        {
            PathHandler data = AstarPath.active.debugPathData;
            GraphNode node = null;
            GraphNodeDelegate drawConnection = delegate (GraphNode otherNode)
            {
                Popcron.Gizmos.Line((Vector3)node.position, (Vector3)otherNode.position, color);
            };
            graph.GetNodes(delegate (GraphNode _node)
            {
                node = _node;
                Gizmos.color = graph.NodeColor(node, AstarPath.active.debugPathData);
                if (AstarPath.active.showSearchTree && !NavGraph.InSearchTree(node, AstarPath.active.debugPath))
                {
                    return true;
                }
                PathNode pathNode = (data != null) ? data.GetPathNode(node) : null;
                if (AstarPath.active.showSearchTree && pathNode != null && pathNode.parent != null)
                {
                    Popcron.Gizmos.Line((Vector3)node.position, (Vector3)pathNode.parent.node.position, color);
                }
                else
                {
                    node.GetConnections(drawConnection);
                }
                return true;
            });
        }
        void OnGUI()
        {
            if (!enabled) return;
            try
            {
                GUILayout.BeginArea(rect);
                if (Game.Instance == null) 
                { 
                    GUILayout.Label("Game is null");
                    return;
                }
                GUILayout.Label($"GameMode: {Game.Instance.CurrentMode}");
                if (Game.Instance.CurrentMode == GameModeType.None) return;
                var controller = Game.Instance.TurnBasedCombatController;
                var isInTurnbasedCombat = CombatController.IsInTurnBasedCombat();
                GUILayout.Label($"IsInTunBasedCombat: {isInTurnbasedCombat}");
                GUILayout.Label($"IsPaused: {Game.Instance.IsPaused}");
                if (!isInTurnbasedCombat) return;
                var currentUnit = CombatController.CurrentUnit;
                if (currentUnit != null)
                {
                    GUILayout.Label($"CurrentUnit: {currentUnit.CharacterName}");
                    var autoUseAbility = currentUnit.GetAvailableAutoUseAbility();
                    if(autoUseAbility != null)
                    {
                        GUILayout.Label($"AutoUseAbility: {autoUseAbility.Name}");
                    } else
                    {
                        GUILayout.Label($"AutoUseAbility: Null");
                    }
                } else
                {
                    GUILayout.Label($"CurrentUnit: NULL");
                }
                GUILayout.Label($"TimeScale: {Time.timeScale}");
                GUILayout.Label($"PlayerTimeScale: {Game.Instance.TimeController.PlayerTimeScale}");
                GUILayout.Label($"TimeScaleInPlayerTurn: {controller.TimeScaleInPlayerTurn}");
                GUILayout.Label($"TimeScaleInNonPlayerTurn: {controller.TimeScaleInNonPlayerTurn}");
                GUILayout.Label($"TimeScaleBetweenTurns: {controller.TimeScaleBetweenTurns}");
                GUILayout.Label($"TimeScaleWhenIdle: {controller.TimeScaleWhenIdle}");
                GUILayout.Label($"RoundNumber: {controller.RoundNumber}");
                GUILayout.Label($"WaitingForUI: {controller.WaitingForUI.Value}");
                GUILayout.Label($"IsPassing: {CombatController.IsPassing()}");
                var turn = controller.CurrentTurn;
                if (turn != null)
                {
                    GUILayout.Label($"Status: {turn.Status}");
                    GUILayout.Label($"EnabledFiveFootStep: {turn.EnabledFiveFootStep}");
                    GUILayout.Label($"EnabledFullAttack: {turn.EnabledFullAttack}");
                    GUILayout.Label($"EnabledSingleActionMove: {turn.EnabledSingleActionMove}");
                    GUILayout.Label($"CurrentAbility: {turn.CurrentAbility?.Name ?? "NULL"}");
                    if (turn.CurrentAbility != null && 
                        turn.CurrentAbility.Blueprint.AssetGuid == "c78506dd0e14f7c45a599990e4e65038")
                    {

                    }
                } else
                {
                    GUILayout.Label($"Turn: Null");
                }
                DebugAttackIndicator();
                GUILayout.Label($"Units");
                var units = m_UnitsRef(controller);
                foreach (var unit in units)
                { 
                    GUILayout.Label($"  {unit.Unit.CharacterName} Surprising {unit.Surprising} Surprised {unit.Surprised} HasFullRoundAction {unit.Unit.HasFullRoundAction()} EstimatedAttacks {UnitAttack.EstimateFullAttacks(unit.Unit)}");
                }
            }
            finally
            {
                GUILayout.EndArea();
            }
        }
        static void DebugAttackIndicator()
        {
            /*
            var attackUnit = Main.Mod.Core.UI.AttackIndicator.Unit;
            GUILayout.Label($"AttackIndicatorUnit: {Main.Mod.Core.UI.AttackIndicator.Unit?.CharacterName ?? "NULL"}");
            if (Main.Mod.Core.UI.AttackIndicator.Unit != null)
            {
                AbilityData ability = ShowAutoCastAbilityRange ? attackUnit.GetAvailableAutoUseAbility() : null;
                if (ability != null)
                {
                    GUILayout.Label($"AttackType: {ability.Name}");
                }
                else
                {
                    GUILayout.Label($"AttackType: Weapon");
                }
            }*/
        }
    }
}
