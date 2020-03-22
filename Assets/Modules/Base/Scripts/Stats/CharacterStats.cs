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
            ((FloatVariable)health.targetStat.value).Set(health.startingValue);
            ((FloatVariable)stamina.targetStat.value).Set(stamina.startingValue);
        }
    }
}