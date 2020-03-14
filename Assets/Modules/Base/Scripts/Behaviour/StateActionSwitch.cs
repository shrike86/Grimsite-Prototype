using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/State Action Switch")]
    public class StateActionSwitch : StateActions
    {
        public BoolVariable boolVariable;
        public StateActions trueAction;
        public StateActions falseAction;

        public override void Execute(PlayerStateManager states)
        {
            if (boolVariable.value)
            {
                trueAction.Execute(states);
            }
            else
            {
                falseAction.Execute(states);
            }
        }
    }
}