using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using Grimsite.Items;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Inventory/Handle Equip By Click")]
    public class HandleEquipByClick : Action
    {
        public InventoryPanel inventoryPanel;
        public EquipmentPanel leftEquipmentPanel;
        public EquipmentPanel rightEquipmentPanel;
        public EquipmentPanel bottomEquipmentPanel;

        public override void Execute()
        {
            InitClickEvents();
        }

        private void InitClickEvents()
        {
            inventoryPanel.OnRightClickEvent += EquipFromInventory;
            leftEquipmentPanel.OnRightClickEvent += UnequipFromEquipmentPanel;
            rightEquipmentPanel.OnRightClickEvent += UnequipFromEquipmentPanel;
            bottomEquipmentPanel.OnRightClickEvent += UnequipFromEquipmentPanel;
        }

        private void EquipFromInventory(ItemSlot itemSlot)
        {
            EquippableItem equippableItem = itemSlot.Item as EquippableItem;

            if (equippableItem != null)
            {
                Equip(equippableItem);
            }
        }

        private void UnequipFromEquipmentPanel(ItemSlot itemSlot)
        {
            EquippableItem equippableItem = itemSlot.Item as EquippableItem;

            if (equippableItem != null)
            {
                Unequip(equippableItem);
            }
        }

        private void Equip(EquippableItem item)
        {
            if (inventoryPanel.RemoveItem(item))
            {
                EquippableItem previousItem;
                if (leftEquipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventoryPanel.AddItem(previousItem);
                    }
                }
                else if (rightEquipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventoryPanel.AddItem(previousItem);
                    }
                }
                else if (bottomEquipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventoryPanel.AddItem(previousItem);
                    }
                }
                else
                {
                    inventoryPanel.AddItem(item);
                }
            }
        }

        private void Unequip(EquippableItem item)
        {
            if (!inventoryPanel.IsFull() && leftEquipmentPanel.RemoveItem(item))
            {
                inventoryPanel.AddItem(item);
            }
            else if (!inventoryPanel.IsFull() && rightEquipmentPanel.RemoveItem(item))
            {
                inventoryPanel.AddItem(item);
            }
            else if (!inventoryPanel.IsFull() && bottomEquipmentPanel.RemoveItem(item))
            {
                inventoryPanel.AddItem(item);
            }
        }
    }
}