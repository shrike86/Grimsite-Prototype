using System;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Regenerate Health")]
    public class RegenerateHealth : StateActions
    {
        public override void Execute(CharacterStateManager states)
        {
            float currentHealth = states.runtimeStats.health.targetStat.Value;
            float maxHealth = (float)Convert.ToDecimal(states.runtimeStats.health.targetStat.maxValue);
            float regenAmount = BaseStats.BASE_HEALTH_REGEN;

            if (currentHealth < maxHealth)
            {
                states.runtimeStats.health.targetStat.Add(regenAmount);
            }
        }
    }
}