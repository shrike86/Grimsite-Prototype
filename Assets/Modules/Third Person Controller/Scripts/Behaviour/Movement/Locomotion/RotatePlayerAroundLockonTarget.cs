using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Rotate Player Around Lock on Target")]
    public class RotatePlayerAroundLockonTarget : StateActions
    {
        public float speed = 8;

        private PlayerStateManager states;

        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            if (states.currentLockonTarget == null)
                return;

            Vector3 targetDir = states.currentLockonTarget.position - states.mTransform.position;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = states.mTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(states.mTransform.rotation, tr, states.delta * speed);

            states.mTransform.rotation = targetRotation;
        }
    }
}