using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public abstract class CharacterStats : ScriptableObject
    {
        public StatContainer health;
        public StatContainer stamina;

        public virtual void InitStats()
        {
            health.targetStat.Init(health.startingValue);
            stamina.targetStat.Init(stamina.startingValue);
        }
    }
}