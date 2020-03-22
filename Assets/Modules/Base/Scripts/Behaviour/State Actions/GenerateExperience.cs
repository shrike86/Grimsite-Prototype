using System;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Generate Experience")]
    public class GenerateExperience : StateActions
    {
        public StatContainer experienceGain;

        private PlayerStateManager states;
        private PlayerStats playerStats;

        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                Init(charStates);

            playerStats.experience.targetStat.Add(experienceGain.targetStat.Value);
        }

        private void Init(CharacterStateManager charStates)
        {
            states = charStates as PlayerStateManager;
            playerStats = states.runtimeStats as PlayerStats;
            experienceGain.targetStat.Init(experienceGain.startingValue);
        }
    }
}