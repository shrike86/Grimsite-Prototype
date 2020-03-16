using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Base/Delta Time Manager")]
    public class DeltaTimeManager : Action
    {
        public FloatVariable targetVariable;

        public override void Execute()
        {
            targetVariable.value = Time.deltaTime;
        }
    }
}