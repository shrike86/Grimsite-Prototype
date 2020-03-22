using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/TPC/Reset Combo Coooldown")]
    public class ResetComboCooldown : StateActions
    {
        public float maxComboTimeInBetween = 0.7f;

        private PlayerStateManager states;
        private float timer;

        public override void Execute(CharacterStateManager characterStates)
        {
            if (states == null)
                states = characterStates as PlayerStateManager;

            if (states.comboCooldownDone)
            {
                timer += states.delta;
            }

            if (timer > maxComboTimeInBetween)
            {
                states.comboCooldownDone = false;
                timer = 0;
            }
        }
    }
}