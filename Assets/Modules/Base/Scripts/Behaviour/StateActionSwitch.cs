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

        public override void Execute(CharacterStateManager characterStates)
        {
            if (boolVariable.value)
            {
                trueAction.Execute(characterStates);
            }
            else
            {
                falseAction.Execute(characterStates);
            }
        }

        public override void Init(CharacterStateManager characterStates)
        {

        }
    }
}