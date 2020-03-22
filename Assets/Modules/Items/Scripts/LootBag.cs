using Grimsite.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Items
{
    public class LootBag : MonoBehaviour
    {
        public List<Item> lootItems = new List<Item>();
        public CurrencyReward currencyReward;
    }
}