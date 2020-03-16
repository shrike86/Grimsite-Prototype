﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Leave Attack State")]
    public class LeaveAttackState : Condition
    {
        public string targetBool = "isInteracting";

        private PlayerStateManager states;

        public override bool CheckCondition(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            bool isAttacking = states.anim.GetBool(targetBool);

            if (!isAttacking)
            {
                states.comboCooldownDone = false;
                states.leftHandItem.comboIndex = 0;
                states.rightHandItem.comboIndex = 0;
                states.currentAttackPhase = ComboAttackPhase.NotAttacking;
                return true;
            }

            return false;
        }
    }
}