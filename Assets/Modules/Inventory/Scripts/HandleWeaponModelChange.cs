using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using Grimsite.Items;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Inventory/Handle Weapon Model Change")]
    public class HandleWeaponModelChange : StateActions
    {
        public EquipmentPanel equipmentPanel;

        public Weapon previousLeftHandItem;
        public Weapon previousRightHandItem;
        public Weapon defaultWeapon;

        private PlayerStateManager states;

        public override void Execute(PlayerStateManager states)
        {
            this.states = states;

            for (int i = 0; i < equipmentPanel.equipmentSlots.Length; i++)
            {
                equipmentPanel.equipmentSlots[i].OnItemChanged += HandleWeaponChange;
            }
        }

        private void HandleWeaponChange(Item item, ItemSlot itemSlot)
        {
            previousLeftHandItem = states.leftHandItem;
            previousRightHandItem = states.rightHandItem;

            UpdateWeaponStates(item, itemSlot);
            EquipWeaponModel(item, itemSlot);
        }

        private void UpdateWeaponStates(Item item, ItemSlot itemSlot)
        {
            EquipmentSlot equipmentSlot = itemSlot as EquipmentSlot;
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
            else if (equipmentSlot.isLeftHandWeaponSlot)
            {
                states.leftHandItem = newWeapon;
                states.isUnarmed = false;
                states.isTwoHanded = false;
            }
            else
            {
                states.rightHandItem = newWeapon;
                states.isUnarmed = false;
                states.isTwoHanded = false;
            }
        }

        private void EquipWeaponModel(Item item, ItemSlot itemSlot)
        {
            EquipmentSlot equipmentSlot = itemSlot as EquipmentSlot;
            Weapon newWeapon = item as Weapon;

            if (newWeapon != null && newWeapon.runtimeWeapon == null)
            {
                newWeapon.Init(states);
            }
            else
            {
                newWeapon.runtimeWeapon.modelInstance.SetActive(true);
            }

            if (newWeapon == null)
            {
                EquipDefaultWeapon();
                return;
            }

            newWeapon.ParentWeaponUnderHand(newWeapon, equipmentSlot.isLeftHandWeaponSlot);
        }

        private void EquipDefaultWeapon()
        {
            previousRightHandItem.runtimeWeapon.modelInstance.SetActive(false);
            previousLeftHandItem = defaultWeapon;
            previousRightHandItem = defaultWeapon;
        }
    }
}