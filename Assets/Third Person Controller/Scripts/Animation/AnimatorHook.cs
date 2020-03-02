using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    public class AnimatorHook : MonoBehaviour
    {
        public StateActions[] animActions;

        CharacterStateManager states;
        Animator anim;

        public void Init(CharacterStateManager st)
        {
            states = st;
            anim = states.anim;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            for (int i = 0; i < animActions.Length; i++)
            {
                animActions[i].Execute(states);
            }
        }
    }
}