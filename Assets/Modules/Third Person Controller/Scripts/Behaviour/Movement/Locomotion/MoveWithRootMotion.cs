using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Move With Root Motion")]
    public class MoveWithRootMotion : StateActions
    {
        public override void Execute(CharacterStateManager states)
        {
            states.rigidbody.isKinematic = false;
            Vector3 velocity = states.rigidbody.velocity;
            Vector3 targetVelocity = states.anim.deltaPosition;
            targetVelocity *= 60;
            targetVelocity.y = velocity.y;
            states.rigidbody.velocity = targetVelocity;
        }
    }
}