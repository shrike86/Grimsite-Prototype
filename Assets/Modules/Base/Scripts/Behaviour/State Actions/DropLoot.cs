using Grimsite.AI;
using Grimsite.Items;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Drop Loot")]
    public class DropLoot : StateActions
    {
        public GameObject lootPrefab;

        private EnemyStateManager thisEnemy;
        private LootBag loot;

        public override void Execute(CharacterStateManager characterStates)
        {
            thisEnemy = characterStates as EnemyStateManager;
            loot = lootPrefab.GetComponent<LootBag>();

            EnemyStats enemyStats = thisEnemy.runtimeStats as EnemyStats;
            InitLoot(enemyStats);

            SpawnLoot();
        }

        private void InitLoot(EnemyStats enemyStats)
        {
            loot.currencyReward.gold = enemyStats.enemyCurrencyReward.gold;
            loot.currencyReward.silver = enemyStats.enemyCurrencyReward.silver;
            loot.currencyReward.copper = enemyStats.enemyCurrencyReward.copper;
        }

        private void SpawnLoot()
        {
            Instantiate(lootPrefab, thisEnemy.transform.position, Quaternion.identity);
        }
    }
}