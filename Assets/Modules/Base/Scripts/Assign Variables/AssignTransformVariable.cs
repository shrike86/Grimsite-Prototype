using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class AssignTransformVariable : MonoBehaviour
    {
        public TransformVariable targetVariable;

        private void Awake()
        {
            targetVariable.value = transform;
        }
    }
}