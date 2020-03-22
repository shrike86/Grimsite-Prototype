using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Stats/Player Stats")]
    public class PlayerStats : CharacterStats
    {
        public StatContainer experience;
        public PlayerCurrency playerCurrency;

    }

    [Serializable]
    public class PlayerCurrency
    {
        public int gold;
        public int silver;
        public int copper;
    }
}