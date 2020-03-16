using System;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Regenerate Stamina")]
    public class RegenerateStamina : StateActions
    {
        public StatContainer baseStaminaRegen;

        private bool hasInit;

        private void OnEnable()
        {
            hasInit = false;
        }

        public override void Execute(CharacterStateManager states)
        {
            if (!hasInit)
                InitStat();

            float currentStam = ((FloatVariable)states.runtimeStats.stamina.targetStat.value).value;
            float maxRegen = (float)Convert.ToDecimal(states.runtimeStats.stamina.startingValue);
            float regenAmount = ((FloatVariable)baseStaminaRegen.targetStat.value).value;

            if (currentStam < maxRegen)
            {
                states.runtimeStats.stamina.targetStat.Add(regenAmount);
            }
        }

        private void InitStat()
        {
            hasInit = true;
            ((FloatVariable)baseStaminaRegen.targetStat.value).Set(baseStaminaRegen.startingValue);
        }
    }
}