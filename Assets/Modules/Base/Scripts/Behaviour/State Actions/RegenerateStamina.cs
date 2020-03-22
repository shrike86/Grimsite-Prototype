using System;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Regenerate Stamina")]
    public class RegenerateStamina : StateActions
    {
        public override void Execute(CharacterStateManager states)
        {
            float currentStam = states.runtimeStats.stamina.targetStat.Value;
            float maxStamina = (float)Convert.ToDecimal(states.runtimeStats.stamina.startingValue);

            // Eventually this will be plus skill modifier amount.
            float regenAmount = BaseStats.BASE_STAMINA_REGEN;

            if (currentStam < maxStamina)
            {
                states.runtimeStats.stamina.targetStat.Add(regenAmount);
            }
        }
    }
}