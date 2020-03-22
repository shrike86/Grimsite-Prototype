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
        Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

        public void Init()
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                if (!itemDict.ContainsKey(allItems[i].name))
                {
                    itemDict.Add(allItems[i].id, allItems[i]);
                }
                else
                {
                    Debug.Log("No item found");
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
      
    }
}