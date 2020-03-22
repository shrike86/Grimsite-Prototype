using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using static UnityEngine.InputSystem.InputAction;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/TPC/Monitor Attack Input")]
    public class MonitorAttackInput : Condition
    {
        public AttackAction attackAction;

        private PlayerStateManager states;
        private PlayerControls inputs;


        public override bool CheckCondition(CharacterStateManager charStates)
        {
            states = charStates as PlayerStateManager;

            if (inputs == null)
                Init(states);

            return states.isAttacking;
        }

        private void Init(PlayerStateManager states)
        {
            this.states = states;
            states.comboCooldownDone = false;
            inputs = new PlayerControls();
            inputs.Enable();
            inputs.Player.LeftMouse.performed += InitAttack;
        }


        private void InitAttack(CallbackContext context)
        {
            if (states.isUserInterfaceActive)
                return;

            attackAction.Execute(states);

        }
    }
}