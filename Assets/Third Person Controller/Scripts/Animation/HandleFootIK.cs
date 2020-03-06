using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Animations/Handle Foot IK")]
    public class HandleFootIK : StateActions
    {
        public AvatarIKGoal ikGoal;
        public string targetCurve;
        public float originOffset = 0.2f;
        public float hitOffset = 0.1f;

        public override void Execute(CharacterStateManager characterStates)
        {
            Transform originTransform = characterStates.animData.rightFoot;

            if (ikGoal == AvatarIKGoal.LeftFoot)
                originTransform = characterStates.animData.leftFoot;

            float weight = characterStates.anim.GetFloat(targetCurve);

            RaycastHit hit;
            Vector3 origin = originTransform.position;
            origin.y += originOffset;
            Vector3 dir = -Vector3.up;

            Vector3 targetPosition = origin;

            if (Physics.Raycast(origin, dir, out hit))
            {
                //targetPosition = hit.point + (Vector3.up * hitOffset);
                targetPosition = hit.point;
            }

            characterStates.anim.SetIKPositionWeight(ikGoal, weight);
            characterStates.anim.SetIKPosition(ikGoal, targetPosition);
        }

        public override void Init(CharacterStateManager characterStates)
        {
            throw new System.NotImplementedException();
        }
    }
}