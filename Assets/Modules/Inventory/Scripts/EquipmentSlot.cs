using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Items;

namespace Grimsite.Inventory
{
    public class EquipmentSlot : ItemSlot
    {
        public EquipmentType equipmentType;
        public EquipmentType secondaryEquipmentType;
        public bool isLeftHandWeaponSlot;

        private void Awake()
        {
            gameObject.name = equipmentType.ToString() + " Slot";
        }

        public override bool CanReceiveItem(Item item)
        {
            if (item == null)
                return true;

            EquippableItem equippableItem = item as EquippableItem;
            return equippableItem != null && equippableItem.equipmentType == equipmentType;
        }
    }
}