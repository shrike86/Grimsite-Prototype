using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Stats/Player Profile")]
    public class PlayerProfile : ScriptableObject
    {
        public string profileName;
        public string rightHandWeaponId;
        public string leftHandWeaponId;
        [Space]
        public List<string> inventoryItems = new List<string>();
        public List<string> equippedItems = new List<string>();
        [Space]
        public PlayerStats stats;


        public void SaveProfile(PlayerStateManager states)
        {
            InventoryManager inventoryManager = states.GetComponent<InventoryManager>();

            inventoryItems.Clear();
            equippedItems.Clear();

            for (int i = 0; i < inventoryManager.inventoryItems.Count; i++)
            {
                inventoryItems.Add(inventoryManager.inventoryItems[i].id);
            }

            for (int i = 0; i < inventoryManager.equippedItems.Count; i++)
            {
                equippedItems.Add(inventoryManager.equippedItems[i].id);
            }
        }
    }
}