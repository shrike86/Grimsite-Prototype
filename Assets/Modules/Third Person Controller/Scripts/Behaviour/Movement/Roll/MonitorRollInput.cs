using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Monitor Roll Input")]
    public class MonitorRollInput : Condition
    {
        public StateActions rollAction;

        private PlayerStateManager states;
        private float rollTimer;

        public override bool CheckCondition(CharacterStateManager charStates)
        {
            states = charStates as PlayerStateManager;

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
                        rollAction.Execute(states);
                    }
                }

                rollTimer = 0;
            }

            return returnValue;
        }
    }
}