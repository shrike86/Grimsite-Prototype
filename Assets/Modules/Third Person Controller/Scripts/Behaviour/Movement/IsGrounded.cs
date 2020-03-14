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

        public override void Execute(PlayerStateManager states)
        {
            Vector3 origin = states.mTransform.position;
            origin.y += .7f;
            Vector3 direction = -Vector3.up;
            float distance = groundedDistance;

            if (!states.isGrounded)
                distance = inAirDistance;

            RaycastHit hit;

            Debug.DrawRay(origin, direction * distance);

            if (Physics.SphereCast(origin, .3f, direction, out hit, distance, states.ignoreForGroundCheck))
            {
                states.isGrounded = true;
            }
            else
            {
                states.isGrounded = false;
            }

            states.anim.SetBool("onGround", states.isGrounded);

            //if (states.isGrounded)
            //{
            //    Vector3 targetPosition = states.mTransform.position;
            //    targetPosition.y = hit.point.y;
            //    states.mTransform.position = targetPosition;
            //}
        }
    }
}
