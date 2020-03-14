using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class EnemyStateManager : CharacterStateManager
    {
        public State currentState;
        public float delta;
        public float fixedDelta;

        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

        }

        private void FixedUpdate()
        {

        }

        private void Update()
        {
            if (IsDead())
            {
                isDead = true;
                EnableRagdoll();
            }
        }
    }
}