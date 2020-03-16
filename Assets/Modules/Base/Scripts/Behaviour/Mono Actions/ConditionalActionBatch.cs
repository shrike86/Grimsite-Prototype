using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Base/ConditionalActionBatch")]
    public class ConditionalActionBatch : Action
    {
        public bool isTrue;
        public BoolVariable boolVariable;
        public Action[] actions;

        public override void Execute()
        {
            if (boolVariable.value)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }   
            }
            else if(!isTrue && !boolVariable.value)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }
            }
        }
    }
}