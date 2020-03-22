using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Move Around Lock on Target")]
    public class MoveAroundLockonTarget : StateActions
    {
        public float movementSpeed = 2;

        private PlayerStateManager states;

        public override void Execute(CharacterStateManager charStates)
        {
            states = charStates as PlayerStateManager;

            if (states.isGrounded)
            {

                if (states.moveAmount > 0.1f)
                {
                    states.rigidbody.drag = 0;
                    states.rigidbody.isKinematic = false;
                }
                else
                {
                    states.rigidbody.drag = 4;
                    states.rigidbody.isKinematic = true;
                }

            }
            else
            {
                states.rigidbody.drag = 0;
            }


            Vector3 velocity = Vector3.zero;
            velocity = states.movementDirection * states.moveAmount * movementSpeed;


            if (states.isGrounded)
            {
                RaycastHit hitInfo;

                if (Physics.Raycast(states.transform.position + Vector3.up, Vector3.down, out hitInfo, 2))
                {
                    velocity = Vector3.ProjectOnPlane(velocity, hitInfo.normal);
                }
                states.rigidbody.velocity = velocity;
            }

            states.transform.position = Vector3.Lerp(states.transform.position, states.mTransform.position, states.delta / 0.1f);
        }
    }
}