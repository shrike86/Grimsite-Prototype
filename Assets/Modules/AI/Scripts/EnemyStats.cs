using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using System;

namespace Grimsite.AI
{
    [CreateAssetMenu(menuName = "Stats/Enemy Stats")]
    public class EnemyStats : CharacterStats
    {
        public float experienceReward;
        public float lootChance = 80;
        public int maxGold = 4;
        public int maxSilver = 100;
        public int maxCopper = 100;

        public CurrencyReward enemyCurrencyReward;

        public override void InitStats()
        {
            base.InitStats();

            GenerateGoldReward();
        }

        private void GenerateGoldReward()
        {
            enemyCurrencyReward.gold = UnityEngine.Random.Range(0, maxGold);
            enemyCurrencyReward.silver = UnityEngine.Random.Range(0, maxSilver);
            enemyCurrencyReward.copper = UnityEngine.Random.Range(0, maxCopper);
        }
    }
    
    [Serializable]
    public class CurrencyReward
    {
        public int gold;
        public int silver;
        public int copper;
    }
}