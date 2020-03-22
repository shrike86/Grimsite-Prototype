using Grimsite.AI;
using Grimsite.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Managers/Resources Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public List<Item> allItems = new List<Item>();
        public List<EnemyStats> allEnemyStats = new List<EnemyStats>();

        private Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
        private Dictionary<string, EnemyStats> enemyStatDict = new Dictionary<string, EnemyStats>();

        public void Init()
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                if (!itemDict.ContainsKey(allItems[i].id))
                {
                    itemDict.Add(allItems[i].id, allItems[i]);
                }
                else
                {
                    Debug.Log("No item found");
                }
            }

            for (int i = 0; i < allEnemyStats.Count; i++)
            {
                if (!itemDict.ContainsKey(allEnemyStats[i].characterId))
                {
                    enemyStatDict.Add(allEnemyStats[i].characterId, allEnemyStats[i]);
                }
                else
                {
                    Debug.Log("No enemy stat found");
                }
            }
        }

        public Item GetItemInstance(string targetID)
        {
            if (string.IsNullOrEmpty(targetID))
                return null;

            Item defaultItem = GetItem(targetID);
            Item newItem = Instantiate(defaultItem);
            newItem.name = defaultItem.name;

            return newItem;
        }

        public Item GetItem(string targetID)
        {
            Item returnValue = null;
            itemDict.TryGetValue(targetID, out returnValue);

            return returnValue;
        }

        public EnemyStats GetEnemyStats(string targetID)
        {
            EnemyStats returnValue = null;
            enemyStatDict.TryGetValue(targetID, out returnValue);

            return returnValue;
        }
    }
}