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

        public override void Execute(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

            SetAnimStates();

            states.anim.SetFloat("forward", states.vertical, .2f, states.delta);
            states.anim.SetFloat("sideways", states.horizontal, .2f, states.delta);

        }

        public override void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }

        private void SetAnimStates()
        {
            states.anim.SetBool("isUnarmed", states.isUnarmed);
            states.anim.SetBool("isTwoHanded", states.isTwoHanded);
        }
    }
}