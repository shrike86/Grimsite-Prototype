using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Items
{
    [CreateAssetMenu(menuName = "Inventory/Items/Weapon")]
    public class Weapon : EquippableItem
    {
        [Header("Rerferences")]
        public TransformVariable leftHandWeaponParent;
        public TransformVariable rightHandWeaponParent;
        public GameObject modelPrefab;
        public RuntimeWeapon runtimeWeapon;
        [Header("State")]
        public string[] attackAnimations;
        public bool isTwoHanded;
        public bool isDefault;
        public int comboIndex;

        // This will be replaced with stats class
        public int damageAmount;

        public void Init(CharacterStateManager charStates)
        {
            runtimeWeapon = new RuntimeWeapon();

            runtimeWeapon.modelInstance = Instantiate(modelPrefab) as GameObject;
            runtimeWeapon.modelInstance.SetActive(false);
            runtimeWeapon.weaponHook = runtimeWeapon.modelInstance.GetComponent<WeaponHook>();
            runtimeWeapon.weaponHook.Init(charStates);
            ParentWeaponUnderHand(this, false);
        }

        public void ParentWeaponUnderHand(Weapon w, bool isLeft = false)
        {
            if (isLeft)
            {
                w.runtimeWeapon.modelInstance.transform.parent = leftHandWeaponParent.value;
                w.runtimeWeapon.modelInstance.transform.localPosition = Vector3.zero;
                w.runtimeWeapon.modelInstance.transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                w.runtimeWeapon.modelInstance.transform.parent = rightHandWeaponParent.value;
                w.runtimeWeapon.modelInstance.transform.localPosition = Vector3.zero;
                w.runtimeWeapon.modelInstance.transform.localEulerAngles = Vector3.zero;
            }

            // TODO Look into this, not sure why this is now happening after remaking the prefab but dividing by 100 works.
            w.runtimeWeapon.modelInstance.transform.localScale = Vector3.one / 100;
            w.runtimeWeapon.modelInstance.SetActive(true);
        }
    }
}