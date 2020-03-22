using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using Grimsite.Items;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/UI/Equipment Panel")]
    public class EquipmentPanel : ScriptableObject
    {
        public TransformVariable playerTrans;
        [HideInInspector]
        public TransformVariable parentTransform;
        [Space]
        public EquipmentSlot[] equipmentSlots;

        public event Action<ItemSlot> OnRightClickEvent;
        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;
        public event Action<ItemSlot> OnDropEvent;

        private InventoryManager playerInvManager;

        public void Init()
        {
            playerInvManager = playerTrans.value.GetComponent<InventoryManager>();
            equipmentSlots = parentTransform.value.GetComponentsInChildren<EquipmentSlot>();

            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
                equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                equipmentSlots[i].OnDragEvent += OnDragEvent;
                equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
                equipmentSlots[i].OnDropEvent += OnDropEvent;
            }
        }

        public bool AddItem(EquippableItem item, out EquippableItem previousItem)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].equipmentType == item.equipmentType || equipmentSlots[i].secondaryEquipmentType == item.equipmentType)
                {
                    previousItem = (EquippableItem)equipmentSlots[i].Item;
                    equipmentSlots[i].Item = item;
                    playerInvManager.equippedItems.Add(item);
                    playerInvManager.inventoryItems.Remove(item);
                    return true;
                }
            }

            previousItem = null;

            return false;
        }

        public bool RemoveItem(EquippableItem item)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].Item == item)
                {
                    equipmentSlots[i].Item = null;
                    playerInvManager.equippedItems.Remove(item);
                    playerInvManager.inventoryItems.Add(item);
                    return true;
                }
            }

            return false;
        }

        public void SetStartingItems(List<Item> startingItems)
        {
            int i = 0;

            for (; i < startingItems.Count && i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].equipmentType == startingItems[i].equipmentType || equipmentSlots[i].secondaryEquipmentType == startingItems[i].equipmentType)
                {
                    equipmentSlots[i].Item = startingItems[i];
                    return;
                }
            }

            for (; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].Item = null;
            }
        }
    }
}