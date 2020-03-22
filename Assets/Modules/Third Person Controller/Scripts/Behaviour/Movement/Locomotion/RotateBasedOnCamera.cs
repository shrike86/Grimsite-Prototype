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
        public float attackRotationSpeed = 3;

        private PlayerStateManager states;


        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            if (cameraTransform.value == null)
                return;

            float rotateSpeed;

            if (charStates.canRotate)
                rotateSpeed = attackRotationSpeed;
            else
                rotateSpeed = speed;

            Vector3 targetDir = states.movementDirection;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = states.mTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(states.mTransform.rotation, tr, states.delta * states.moveAmount * rotateSpeed);

            states.mTransform.rotation = targetRotation;
        }
    }
}
