using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Monitor Attack Input")]
    public class MonitorAttackInput : Condition
    {
        public AttackAction attackAction;

        private PlayerStateManager states;
        private float leftInputTimer;
        private float rightInputTimer;

        public override bool CheckCondition(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

            if (states.isUserInterfaceActive)
                return false;

            if (states.inputStates.isPressed_leftMouse)
            {
                leftInputTimer += states.delta;
            }
            else
            {
                if (leftInputTimer > 0)
                {
                    leftInputTimer += states.delta;
                    attackAction.isLeftHand = true;
                    attackAction.Execute(characterStates);
                    leftInputTimer = 0;
                    return true;
                }
            }

            if (states.inputStates.isPressed_rightMouse)
            {
                rightInputTimer += states.delta;
            }
            else
            {
                if (rightInputTimer > 0)
                {
                    attackAction.isLeftHand = false;
                    attackAction.Execute(characterStates);
                    rightInputTimer = 0;
                    return true;
                }
            }

            return false;
        }

        private void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }
    }
}