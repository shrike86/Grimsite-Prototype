using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Monitor Roll Input")]
    public class MonitorRollInput : Condition
    {
        private PlayerStateManager states;
        private float rollTimer;

        public override bool CheckCondition(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

            bool returnValue = false;
            states.isRolling = false;

            if (states.inputStates.isPressed_space)
            {
                rollTimer += Time.deltaTime;
            }
            else
            {
                if (rollTimer > 0)
                {
                    returnValue = true;
                    states.generalTime = 0;

                    if (states.moveAmount > 0)
                    {
                        //states.anim.SetFloat("vertical", 1);
                        PlayRollAnimation();
                        states.isRolling = true;

                        if (states.isLockedOn)
                        {
                            states.rollDirection = states.movementDirection;
                        }
                        else
                        {
                            states.rollDirection = states.mTransform.forward;
                        }
                    }

                }

                rollTimer = 0;
            }

            return returnValue;
        }

        private void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }

        private void PlayRollAnimation()
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
    }
}