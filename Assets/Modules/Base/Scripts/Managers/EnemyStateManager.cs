using Grimsite.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class EnemyStateManager : CharacterStateManager
    {
        public float delta;
        public float fixedDelta;

        [HideInInspector]
        public DamageCollider hitCollider;

        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            hitCollider = GetComponent<DamageCollider>();
            hitCollider.onHit += AwardExperience;
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


        private void AwardExperience(CharacterStateManager charStates)
        {
            PlayerStateManager playerStates = charStates as PlayerStateManager;
            PlayerStats playerStats = playerStates.runtimeStats as PlayerStats;
            EnemyStats thisEnemyStats = this.runtimeStats as EnemyStats;

            playerStats.experience.targetStat.Add(((FloatVariable)thisEnemyStats.experienceReward.targetStat.value).value);
            hitCollider.onHit -= AwardExperience;
        }
    }
}