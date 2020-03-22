using System;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Generate Experience")]
    public class GenerateExperience : StateActions
    {
        private PlayerStateManager states;
        private PlayerStats playerStats;

        public override void Execute(CharacterStateManager charStates)
        {
            states = charStates as PlayerStateManager;
            playerStats = states.runtimeStats as PlayerStats;
            playerStats.experience.targetStat.Add(BaseStats.BASE_EXPERIENCE_GAIN);
        }
    }
}