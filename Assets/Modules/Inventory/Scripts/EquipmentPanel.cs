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
        [HideInInspector]
        public PlayerStateManager states;
        [HideInInspector]
        public StateManagerVariable characterStates;
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
            playerInvManager = characterStates.value.GetComponent<InventoryManager>();
            states = characterStates.value as PlayerStateManager;
            equipmentSlots = parentTransform.value.GetComponentsInChildren<EquipmentSlot>();

            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
                equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
                equipmentSlots[i].OnDragEvent += OnDragEvent;
                equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
                equipmentSlots[i].OnDropEvent += OnDropEvent;

                if (equipmentSlots[i].equipmentType == EquipmentType.Weapon)
                {
                    equipmentSlots[i].OnItemChanged += UpdateWeaponStates;
                }
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
                    playerInvManager.playerEquippedItems.Add(item);
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
                    playerInvManager.playerEquippedItems.Remove(item);
                    return true;
                }
            }

            return false;
        }

        private void UpdateWeaponStates(Item item)
        {
            Weapon newWeapon = item as Weapon;

            if (item == null)
            {
                states.isUnarmed = true;
                states.isTwoHanded = false;
                states.leftHandItem = null;
                states.rightHandItem = null;
                return;
            }

            if (newWeapon.isTwoHanded)
            {
                states.leftHandItem = newWeapon;
                states.rightHandItem = newWeapon;
                states.isUnarmed = false;
                states.isTwoHanded = true;
            }
            else if (newWeapon.isLeft)
            {
                states.leftHandItem = newWeapon;
                states.rightHandItem = null;
                states.isUnarmed = false;
                states.isTwoHanded = true;
            }
            else
            {
                states.rightHandItem = newWeapon;
                states.leftHandItem = null;
                states.isUnarmed = false;
                states.isTwoHanded = true;
            }


        }
    }
}