using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Roll Movement")]
    public class RollMovement : StateActions
    {
        public AnimationCurve speedCurve;
        public float speed = 3;
        public float backstepSpeed = 2;

        private PlayerStateManager states;

        public override void Execute(PlayerStateManager states)
        {
            states.rigidbody.drag = 0;
            states.generalTime += states.delta;
            Vector3 velocity = states.rigidbody.velocity;
            Vector3 targetVelocity = states.rollDirection * speed;
            targetVelocity.y = 0;

            targetVelocity *= speedCurve.Evaluate(states.generalTime);
            targetVelocity.y = velocity.y;
            states.rigidbody.velocity = targetVelocity;

            //// look in the direction that the player is rolling.
            //if (states.isLockedOn && states.vertical == 0 && states.horizontal > 0)
            //{
            //    Vector3 targetDirection = states.rollDirection;
            //    targetDirection.y = 0;
            //    Quaternion targetRot = Quaternion.LookRotation(targetDirection);
            //    states.mTransform.rotation = Quaternion.Slerp(states.mTransform.rotation, targetRot, states.delta * 15);
            //}
        }
    }
}