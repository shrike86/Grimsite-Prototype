using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using static UnityEngine.InputSystem.InputAction;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Monitor Attack Input")]
    public class MonitorAttackInput : Condition
    {
        public AttackAction attackAction;

        private PlayerStateManager states;
        private float leftInputTimer;
        private float rightInputTimer;

        private bool isAttacking;

        PlayerControls inputs;

        public override bool CheckCondition(PlayerStateManager states)
        {
            if (inputs == null)
                Init(states);

            return states.isAttacking;
        }

        private void Init(PlayerStateManager states)
        {
            this.states = states;
            inputs = new PlayerControls();
            inputs.Enable();
            inputs.Player.LeftMouse.performed += Attack;
        }


        private void Attack(CallbackContext context)
        {
            if (states.isUserInterfaceActive)
                return;

            if (states.currentAttackPhase == ComboAttackPhase.NotAttacking)
            {
                states.currentAttackPhase = ComboAttackPhase.First;
                attackAction.Execute(states);
                states.isAttacking = true;
                return;
            }

            if (states.currentAttackPhase == ComboAttackPhase.First && states.canCombo)
            {
                states.canCombo = false;
                states.currentAttackPhase = ComboAttackPhase.Second;
                states.rightHandItem.comboIndex = 1;
                attackAction.Execute(states);
                states.isAttacking = true;
                return;
            }

            if (states.currentAttackPhase == ComboAttackPhase.Second && states.canCombo)
            {
                states.canCombo = false;
                states.currentAttackPhase = ComboAttackPhase.Third;
                states.rightHandItem.comboIndex = 2;
                attackAction.Execute(states);
                states.isAttacking = true;
                return;
            }
        }
    }
}