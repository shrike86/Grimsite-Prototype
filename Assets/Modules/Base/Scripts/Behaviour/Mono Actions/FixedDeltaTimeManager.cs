using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Base/FixedDeltaTimeManager")]
    public class FixedDeltaTimeManager : Action
    {
        public FloatVariable targetVariable;

        public override void Execute()
        {
            targetVariable.value = Time.fixedDeltaTime;
        }
    }
}