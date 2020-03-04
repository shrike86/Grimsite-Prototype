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

            states.anim.SetFloat("sideways", states.horizontal);
            states.anim.SetFloat("forward", states.vertical);
        }

        public override void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }
    }
}