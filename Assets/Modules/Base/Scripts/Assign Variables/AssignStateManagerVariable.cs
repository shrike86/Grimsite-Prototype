using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class AssignStateManagerVariable : MonoBehaviour
    {
        public StateManagerVariable target;

        private void OnEnable()
        {
            target.value = GetComponent<PlayerStateManager>();
        }
    }
}