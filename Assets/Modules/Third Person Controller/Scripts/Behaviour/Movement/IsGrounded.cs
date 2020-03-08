using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Is Grounded")]
    public class IsGrounded : StateActions
    {
        public float groundedDistance = 1.4f;
        public float inAirDistance = 0.8f;

        public override void Execute(CharacterStateManager characterStates)
        {
            Vector3 origin = characterStates.mTransform.position;
            origin.y += .7f;
            Vector3 direction = -Vector3.up;
            float distance = groundedDistance;

            if (!characterStates.isGrounded)
                distance = inAirDistance;

            RaycastHit hit;

            Debug.DrawRay(origin, direction * distance);

            if (Physics.SphereCast(origin, .3f, direction, out hit, distance, characterStates.ignoreForGroundCheck))
            {
                characterStates.isGrounded = true;
            }
            else
            {
                characterStates.isGrounded = false;
            }

            characterStates.anim.SetBool("onGround", characterStates.isGrounded);

            //if (states.isGrounded)
            //{
            //    Vector3 targetPosition = states.mTransform.position;
            //    targetPosition.y = hit.point.y;
            //    states.mTransform.position = targetPosition;
            //}
        }

        public override void Init(CharacterStateManager characterStates)
        {
            throw new NotImplementedException();
        }
    }
}
