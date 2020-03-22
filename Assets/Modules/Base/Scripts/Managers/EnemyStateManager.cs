using Grimsite.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class EnemyStateManager : CharacterStateManager
    {
        public float delta;
        public float fixedDelta;

        private ResourcesManager resourcesManager;

        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            resourcesManager = GameManager.GetResourcesManager();
            runtimeStats = resourcesManager.GetEnemyStats(characterId);

            base.Init();

            rigidbody.mass = 100;
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;

            if (currentState != null)
            {
                currentState.FixedTick(this);
            }
        }

        private void Update()
        {
            delta = Time.deltaTime;

            if (currentState != null)
            {
                currentState.Tick(this);
            }
        }

        //// For testing, later this will occur when the corpse is looted.
        //private void AwardGold(CharacterStateManager charStates)
        //{
        //    PlayerStateManager playerStates = charStates as PlayerStateManager;
        //    PlayerStats playerStats = playerStates.runtimeStats as PlayerStats;
        //    EnemyStats thisEnemyStats = this.runtimeStats as EnemyStats;

        //    GoldReward enemyGoldReward = thisEnemyStats.enemyGoldReward;

        //    playerStats.playerCurrency.gold += enemyGoldReward.gold;
        //    playerStats.playerCurrency.silver += enemyGoldReward.silver;
        //    playerStats.playerCurrency.copper += enemyGoldReward.copper;

        //    hitCollider.onHit -= AwardGold;
        //}
    }
}