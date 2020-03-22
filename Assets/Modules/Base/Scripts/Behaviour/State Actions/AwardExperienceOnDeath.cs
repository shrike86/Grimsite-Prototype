using Grimsite.AI;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Award Experience On Death")]
    public class AwardExperienceOnDeath : StateActions
    {
        private EnemyStateManager thisEnemy;

        public override void Execute(CharacterStateManager characterStates)
        {
            thisEnemy = characterStates as EnemyStateManager;
            AwardExperience(thisEnemy.lastHitByChar);
        }

        private void AwardExperience(CharacterStateManager attackingChar)
        {
            PlayerStateManager playerStates = attackingChar as PlayerStateManager;
            PlayerStats playerStats = playerStates.runtimeStats as PlayerStats;
            EnemyStats thisEnemyStats = thisEnemy.runtimeStats as EnemyStats;

            playerStats.experience.targetStat.Add(thisEnemyStats.experienceReward);
        }
    }
}