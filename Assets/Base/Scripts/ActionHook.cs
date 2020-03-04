using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class ActionHook : MonoBehaviour
    {
        public Action[] fixedActions;
        public Action[] updateActions;
        
        void Update()
        {
            for (int i = 0; i < updateActions.Length; i++)
            {
                updateActions[i].Execute();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < fixedActions.Length; i++)
            {
                fixedActions[i].Execute();
            }
        }
    }
}
