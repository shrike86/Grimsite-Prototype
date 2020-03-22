using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Roll Action")]
    public class RollAction : StateActions
    {
        private PlayerStateManager states;
        private bool hasInit;

        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            if (states.HasStamina(Mathf.RoundToInt(BaseStats.BASE_ROLL_STAMINA_COST)))
            {
                SubtractStamina(states);
                PlayRollAnimation(states);
                states.isRolling = true;
                states.isAttacking = false;

                if (states.isLockedOn)
                {
                    states.rollDirection = states.movementDirection;
                }
                else
                {
                    states.rollDirection = states.mTransform.forward;
                }
            }
            else
            {
                Debug.Log("You don't have enough stamina to roll.");
            }
        }

        private void PlayRollAnimation(PlayerStateManager states)
        {
            if (states.isUnarmed)
            {
                states.animHook.PlayAnimation("Unarmed Roll");
            }
            else if (states.isTwoHanded)
            {
                states.animHook.PlayAnimation("Two Handed Roll");
            }
        }

        private void SubtractStamina(CharacterStateManager states)
        {
            states.runtimeStats.stamina.targetStat.Remove(BaseStats.BASE_ROLL_STAMINA_COST);
        }
    }
}