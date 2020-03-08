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
        public bool isLeftHand;
        public Weapon defaultWeapon;

        private PlayerStateManager states;

        public override void Execute(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

            if (states.isUnarmed)
            {
                states.animHook.PlayAnimation(defaultWeapon.attackAnimName);
            }
            else if (states.isTwoHanded)
            {
                states.animHook.PlayAnimation(states.leftHandItem.attackAnimName);
            }

        }

        public override void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }
    }
}