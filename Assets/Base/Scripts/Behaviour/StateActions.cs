using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public abstract class StateActions : ScriptableObject
    {
        public abstract void Execute(CharacterStateManager states);
    }
}
