using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using Grimsite.Items;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Attack/Attack Action")]
    public class AttackAction : StateActions
    {
        public Weapon defaultWeapon;

        public override void Execute(PlayerStateManager states)
        {
            if (states.isUnarmed)
            {
                states.animHook.PlayAnimation(defaultWeapon.attackAnimations[defaultWeapon.comboIndex]);
            }
            else if (states.isTwoHanded)
            {
                states.animHook.PlayAnimation(states.leftHandItem.attackAnimations[states.leftHandItem.comboIndex]);
            }

        }
    }

    public enum ComboAttackPhase
    { 
        NotAttacking,
        First,
        Second,
        Third
    }
}