using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Rotate Based On Camera")]
    public class RotateBasedOnCamera : StateActions
    {
        public TransformVariable cameraTransform;
        public float speed = 8;

        private PlayerStateManager states;


        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            if (cameraTransform.value == null)
                return;

            Vector3 targetDir = states.movementDirection;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = states.mTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(states.mTransform.rotation, tr, states.delta * states.moveAmount * speed);

            states.mTransform.rotation = targetRotation;
        }
    }
}
