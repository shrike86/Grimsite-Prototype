using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/States Manager")]
    public class StateManagerVariable : ScriptableObject
    {
        public CharacterStateManager value;
    }
}