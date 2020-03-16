using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Handle Lock on Movement Animations")]
    public class HandleLockonMovementAnimations : StateActions
    {
        private PlayerStateManager states;


        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            SetAnimStates(states);

            states.anim.SetFloat("forward", states.vertical, .2f, states.delta);
            states.anim.SetFloat("sideways", states.horizontal, .2f, states.delta);

        }

        private void SetAnimStates(CharacterStateManager states)
        {
            states.anim.SetBool("isUnarmed", states.isUnarmed);
            states.anim.SetBool("isTwoHanded", states.isTwoHanded);
        }
    }
}