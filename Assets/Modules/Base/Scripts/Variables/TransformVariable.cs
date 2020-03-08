using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/Transform")]
    public class TransformVariable : ScriptableObject
    {
        public Transform value;
    }
}