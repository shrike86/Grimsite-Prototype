using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public abstract class Condition : ScriptableObject
    {
		public string description;

        public abstract bool CheckCondition(CharacterStateManager characterStates);

    }
}
