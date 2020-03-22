using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/TPC/Monitor Movement During Animation")]
    public class MonitorMovementDuringAnimations : Condition
    {
        private PlayerStateManager states;

        public override bool CheckCondition(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;

            if (states.canMove)
            {
                if (states.moveAmount > 0 && !states.canRotate)
                {
                    states.anim.Play("Empty");
                    return true;
                }
            }

            return false;
        }
    }
}