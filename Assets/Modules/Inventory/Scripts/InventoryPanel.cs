using Grimsite.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Items;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Inventory/UI/Inventory Panel")]
    public class InventoryPanel : ScriptableObject
    {
        public TransformVariable parentTransform;
        public StateManagerVariable characterStates;
        [Space]
        public ItemSlot[] itemSlots;

        public event Action<ItemSlot> OnRightClickEvent;
        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;
        public event Action<ItemSlot> OnDropEvent;

        private InventoryManager playerInvManager;

        public void Init()
        {
            playerInvManager = characterStates.value.GetComponent<InventoryManager>();
            itemSlots = parentTransform.value.GetComponentsInChildren<ItemSlot>();

            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnRightClickEvent;
                itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                itemSlots[i].OnDragEvent += OnDragEvent;
                itemSlots[i].OnEndDragEvent += OnEndDragEvent;
                itemSlots[i].OnDropEvent += OnDropEvent;
            }
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    itemSlots[i].Item = item;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == item)
                {
                    itemSlots[i].Item = null;
                    return true;
                }
            }

            return false;
        }
        public bool IsFull()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    return false;
                }
            }

            return true;
        }

        public void SetStartingItems(List<Item> startingItems)
        {
            int i = 0;

            for (; i < startingItems.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = startingItems[i];
            }

            for (; i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = null;
            }
        }
    }
}