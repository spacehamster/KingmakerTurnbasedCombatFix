using Kingmaker.PubSubSystem;
using ModMaker;
using ModMaker.Utility;
using System.Reflection;
using TurnbasedCombatFix.UI;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix.Controllers
{
    public class UIController :
        IModEventHandler,
        ISceneHandler
    {
        public DebugUIManager DebugUI { get; private set; }

    //public AttackIndicatorManager AttackIndicator { get; private set; }

    //public MovementIndicatorManager MovementIndicator { get; private set; }

    public int Priority => 800;

        public void Attach()
        {
#if DEBUG
            if (!DebugUI)
            {
                DebugUI = DebugUIManager.CreateObject();
            }
#endif
            /*if (!AttackIndicator)
            {
                AttackIndicator = AttackIndicatorManager.CreateObject();
            }

            if (!MovementIndicator)
            {
                MovementIndicator = MovementIndicatorManager.CreateObject();
            }*/
        }

        public void Detach()
        {
            DebugUI.SafeDestroy();
            DebugUI = null;

            /*
            AttackIndicator.SafeDestroy();
            AttackIndicator = null;

            MovementIndicator.SafeDestroy();
            MovementIndicator = null;*/
        }

#if DEBUG
        /*public void Clear()
        {
            Transform attackIndicator;
            while (attackIndicator = Game.Instance.UI.Common.transform.Find("AbilityTargetSelect/TurnBasedAttackIndicator"))
            {
                attackIndicator.gameObject.SafeDestroy();
            }
            AttackIndicator = null;

            Transform movementIndicator;
            while (movementIndicator = Game.Instance.UI.Common.transform.Find("AbilityTargetSelect/TurnBasedMovementIndicator"))
            {
                movementIndicator.SafeDestroy();
            }
            MovementIndicator = null;
        }*/
#endif

        public void Update()
        {
            Detach();
            Attach();
        }

        #region Event Handlers

        public void HandleModEnable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            Mod.Core.UI = this;
            Attach();

            EventBus.Subscribe(this);
        }

        public void HandleModDisable()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            EventBus.Unsubscribe(this);

            Detach();
            Mod.Core.UI = null;
        }

        public void OnAreaBeginUnloading() { }

        public void OnAreaDidLoad()
        {
            Mod.Debug(MethodBase.GetCurrentMethod());

            Attach();
        }

        #endregion
    }
}