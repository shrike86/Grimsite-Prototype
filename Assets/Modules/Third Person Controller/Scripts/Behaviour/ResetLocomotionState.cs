using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/TPC/Reset Locomotion State")]
    public class ResetLocomotionState : StateActions
    {
        PlayerStateManager playerStates;

        public override void Execute(CharacterStateManager characterStates)
        {
            if (playerStates == null)
                playerStates = characterStates as PlayerStateManager;

            if (playerStates == null)
                return;

            playerStates.canCombo = false;
            playerStates.canMove = false;
            playerStates.currentAttackPhase = ComboAttackPhase.NotAttacking;
            playerStates.isAttacking = false;
            playerStates.comboCooldownDone = false;

            if (playerStates.leftHandItem != null)
                playerStates.leftHandItem.comboIndex = 0;

            if (playerStates.rightHandItem != null)
                playerStates.rightHandItem.comboIndex = 0;
        }
    }
}