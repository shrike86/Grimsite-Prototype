using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}
