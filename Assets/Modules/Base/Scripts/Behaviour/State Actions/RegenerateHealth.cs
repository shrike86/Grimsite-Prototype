using System;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Regenerate Health")]
    public class RegenerateHealth : StateActions
    {
        public StatContainer baseHealthRegen;

        private bool hasInit;

        private void OnEnable()
        {
            hasInit = false;
        }

        public override void Execute(CharacterStateManager states)
        {
            if (!hasInit)
                InitStat();

            float currentHealth = ((FloatVariable)states.runtimeStats.health.targetStat.value).value;
            float maxRegen = (float)Convert.ToDecimal(states.runtimeStats.health.startingValue);
            float regenAmount = ((FloatVariable)baseHealthRegen.targetStat.value).value;

            if (currentHealth < maxRegen)
            {
                states.runtimeStats.health.targetStat.Add(regenAmount);
            }
        }

        private void InitStat()
        {
            hasInit = true;
            ((FloatVariable)baseHealthRegen.targetStat.value).Set(baseHealthRegen.startingValue);
        }
    }
}