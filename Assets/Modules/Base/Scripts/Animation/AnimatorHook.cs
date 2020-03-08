using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.Base
{
    public class AnimatorHook : MonoBehaviour
    {
        public StateActions[] ikActions;
        public StateActions[] fixedActions;
        public StateActions[] updatedActions;

        CharacterStateManager states;
        Animator anim;

        public void Init(CharacterStateManager st)
        {
            states = st;
            anim = states.anim;
        }

        private void FixedUpdate()
        {
            if (states.anim == null)
                return;

            for (int i = 0; i < fixedActions.Length; i++)
            {
                fixedActions[i].Execute(states);
            }
        }

        private void Update()
        {
            if (states.anim == null)
                return;

            for (int i = 0; i < updatedActions.Length; i++)
            {
                updatedActions[i].Execute(states);
            }
        }


        private void OnAnimatorIK(int layerIndex)
        {
            if (states.anim == null)
                return;

            for (int i = 0; i < ikActions.Length; i++)
            {
                ikActions[i].Execute(states);
            }
        }

        private void OnAnimatorMove()
        {
            transform.localPosition = Vector3.zero;
        }

        public void PlayAnimation(string targetAnim)
        {
            anim.CrossFade(targetAnim, 0.2f);
            anim.SetBool("isInteracting", true);
        }
    }
}