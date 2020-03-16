using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.AI
{
    [CreateAssetMenu(menuName = "Stats/Enemy Stats")]
    public class EnemyStats : CharacterStats
    {
        public StatContainer experienceReward;

        public override void InitStats()
        {
            base.InitStats();

            ((FloatVariable)experienceReward.targetStat.value).Set(experienceReward.startingValue);
        }
    }
}