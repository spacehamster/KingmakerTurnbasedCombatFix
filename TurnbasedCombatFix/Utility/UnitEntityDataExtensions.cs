using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using static TurnbasedCombatFix.Main;

namespace TurnbasedCombatFix.Utility
{
    public static class UnitEntityDataExtensions
    {
        public static float GetAttackRadius(this UnitEntityData unit)
        {
            ItemEntityWeapon weapon = unit.GetFirstWeapon();
            return unit.View.Corpulence + (weapon != null ? weapon.AttackRange.Meters : 0f);
        }
    }
}