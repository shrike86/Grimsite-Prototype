using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/TPC/Rotate When Attacking")]
    public class RotateWhenAttacking : StateActions
    {
        public StateActions rotateAction;

        public override void Execute(CharacterStateManager characterStates)
        {
            if (characterStates.canRotate)
            {
                rotateAction.Execute(characterStates);
            }
        }
    }
}