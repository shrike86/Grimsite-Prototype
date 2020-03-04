using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool value;
    }
}