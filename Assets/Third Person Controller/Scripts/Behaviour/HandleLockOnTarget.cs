﻿using System.Collections;
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

        public StateManagerVariable playerStates;
        public FloatVariable delta;
        public BoolVariable isLockedOn;
        public TransformVariable cameraTransform;

        private PlayerStateManager states;

        public override void Execute()
        {
            if (states == null)
                Init(playerStates.value);

            if (states.inputStates.isPressed_T)
            {
                if (states.isLockedOn)
                {
                    states.isLockedOn = false;
                    states.currentLockonTarget = null;
                }
                else
                {
                    states.isLockedOn = true;
                    FindLockableTargets();
                }
            }

            if (states.currentLockonTarget != null)
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
                states.isLockedOn = false;
            }

            isLockedOn.value = states.isLockedOn;
        }

        private void FindLockableTargets()
        {
            Collider[] colliders = Physics.OverlapSphere(states.mTransform.position, 10);

            Vector3 cameraDirection = cameraTransform.value.forward;
            cameraDirection.y = 0;

            for (int i = 0; i < colliders.Length; i++)
            {
                ILockable lockableTarget = colliders[i].GetComponentInParent<ILockable>();

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
                float tempDistance = Vector3.Distance(states.mTransform.position, targets[i].position);

                if (tempDistance < minDistance && targets[i] != states.currentLockonTarget)
                {
                    minDistance = tempDistance;
                    states.currentLockonTarget = targets[i];
                }
            }
        }

        private void ValidateTargets()
        {
            float distance = Vector3.Distance(states.currentLockonTarget.position, playerStates.value.mTransform.position);

            if (distance > 30)
            {
                states.isLockedOn = false;
            }
        }

        private void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }
    }
}