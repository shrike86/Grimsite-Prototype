using Grimsite.Base;
using Grimsite.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Inventory/Init Inventory")]
    public class InitInventory : StateActions
    {
        private PlayerStateManager states;
        private InventoryManager inventoryManager;
        private ResourcesManager resourcesManager;

        public override void Execute(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
            inventoryManager = states.GetComponent<InventoryManager>();
            resourcesManager = GameManager.GetResourcesManager();

            LoadPlayerItems();
        }

        private void LoadPlayerItems()
        {
            List<Item> startingInvItems = new List<Item>();
            List<Item> startingEquippedItems = new List<Item>();

            for (int i = 0; i < states.profile.inventoryItems.Count; i++)
            {
                Item invItem = resourcesManager.GetItem(states.profile.inventoryItems[i]);
                startingInvItems.Add(invItem);
            }

            for (int i = 0; i < states.profile.equippedItems.Count; i++)
            {
                Item equippedItem = resourcesManager.GetItem(states.profile.equippedItems[i]);
                startingEquippedItems.Add(equippedItem);
            }

            LoadInventoryItems(startingInvItems);
            LoadEquippedItems(startingEquippedItems);
        }

        private void EquipItems()
        {
        }

        private void LoadInventoryItems(List<Item> startingInvItems)
        {
            inventoryManager.inventoryPanel.SetStartingItems(startingInvItems);
        }

        private void LoadEquippedItems(List<Item> startingInvItems)
        {
            inventoryManager.leftEquipmentPanel.SetStartingItems(startingInvItems);
            inventoryManager.rightEquipmentPanel.SetStartingItems(startingInvItems);
            inventoryManager.bottomEquipmentPanel.SetStartingItems(startingInvItems);
        }
    }
}