using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Handle Lock on Targets")]
    public class HandleLockOnTarget : Action
    {
        [System.NonSerialized]
        public List<Transform> targets = new List<Transform>();
        [System.NonSerialized]
        public float validateTargetTimer;

        public StateManagerVariable states;
        public FloatVariable delta;
        public BoolVariable isLockedOn;
        public TransformVariable cameraTransform;

        private float inputTimer;

        public override void Execute()
        {
            if (states.value.inputStates.isPressed_T)
            {
                inputTimer += delta.value;
            }
            else
            {
                if (inputTimer > 0)
                {
                    if (states.value.isLockedOn)
                    {
                        states.value.isLockedOn = false;
                        states.value.currentLockonTarget = null;
                    }
                    else
                    {
                        states.value.isLockedOn = true;
                        FindLockableTargets();
                    }

                    inputTimer = 0;
                }
            }

            if (states.value.currentLockonTarget != null)
            {
                validateTargetTimer += delta.value;

                if (validateTargetTimer > 2)
                {
                    ValidateTargets();
                    validateTargetTimer = 0;
                }
            }
            else
            {
                states.value.isLockedOn = false;
            }

            isLockedOn.value = states.value.isLockedOn;
        }

        private void FindLockableTargets()
        {
            Collider[] colliders = Physics.OverlapSphere(states.value.mTransform.position, 10);

            Vector3 cameraDirection = cameraTransform.value.forward;
            cameraDirection.y = 0;

            for (int i = 0; i < colliders.Length; i++)
            {
                ILockable lockableTarget = colliders[i].GetComponent<ILockable>();

                if (lockableTarget == null)
                    continue;

                float dot = Vector3.Dot(cameraDirection, colliders[i].transform.position - cameraTransform.value.position);

                if (dot > 0)
                {
                    Transform t = lockableTarget.LockOn();

                    if (t != null)
                    {
                        if (!targets.Contains(t))
                            targets.Add(t);
                    }

                }
            }

            float minDistance = 100;

            for (int i = 0; i < targets.Count; i++)
            {
                float tempDistance = Vector3.Distance(states.value.mTransform.position, targets[i].position);

                if (tempDistance < minDistance && targets[i] != states.value.currentLockonTarget)
                {
                    minDistance = tempDistance;
                    states.value.currentLockonTarget = targets[i];
                }
            }
        }

        private void ValidateTargets()
        {
            float distance = Vector3.Distance(states.value.currentLockonTarget.position, states.value.mTransform.position);

            if (distance > 30)
            {
                states.value.isLockedOn = false;
            }
        }
    }
}