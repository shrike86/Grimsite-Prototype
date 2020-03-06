using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Camera/Rotate Camera Around Lock on Target")]
    public class RotateCameraAroundLockonTarget : Action
    {
        public StateManagerVariable characterStates;
        public FloatVariable delta;
        public float speed = 8;
        public TransformVariable cameraYTransform;
        public TransformVariable pivotTransform;
        public FloatVariable yAngle;
        public FloatVariable xAngle;

        public float minClamp = -35;
        public float maxClamp = 35;

        private PlayerStateManager states;

        public override void Execute()
        {
            if (states == null)
                Init(characterStates.value);

            if (states.currentLockonTarget == null)
                return;

            Vector3 direction = states.currentLockonTarget.position - cameraYTransform.value.position;
            Vector3 pivotDirection = direction;
            direction.y = 0;
            

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            cameraYTransform.value.rotation = Quaternion.Slerp(cameraYTransform.value.rotation, targetRotation, delta.value * speed);

            // Ensure the pivot will look at the lock on target. Helps when rolling.
            float distance = Vector3.Distance(states.mTransform.position, states.currentLockonTarget.position);
            distance /= 2;
            Vector3 lookPosition = pivotDirection * distance;
            lookPosition += cameraYTransform.value.position;
            pivotTransform.value.LookAt(lookPosition);

            Vector3 localEulers = pivotTransform.value.localEulerAngles;
            localEulers.y = 0;
            localEulers.z = 0;
            localEulers.x = 0;
            pivotTransform.value.localEulerAngles = localEulers;

            yAngle.value = cameraYTransform.value.localEulerAngles.y;
            xAngle.value = localEulers.x;
        }

        private void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }
    }
}