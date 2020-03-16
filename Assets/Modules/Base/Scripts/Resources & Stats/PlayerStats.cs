using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Stats/Player Stats")]
    public class PlayerStats : CharacterStats
    {
        public StatContainer experience;
        public StatContainer gold;
        public StatContainer silver;
        public StatContainer copper;

        public override void InitStats()
        {
            base.InitStats();
        }
    }
}