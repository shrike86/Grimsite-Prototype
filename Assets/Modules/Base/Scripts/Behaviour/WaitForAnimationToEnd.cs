using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Wait For Animation To End")]
    public class WaitForAnimationToEnd : Condition
    {
        public string targetBool = "isInteracting";

        public override bool CheckCondition(PlayerStateManager states)
        {
            bool returnValue = !states.anim.GetBool(targetBool);
            return returnValue;
        }
    }
}