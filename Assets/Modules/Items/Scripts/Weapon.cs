using Grimsite.Base;
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
        public float timeBetweenComboAttack;
        public bool isTwoHanded;
        public bool isDefault;
        [Header("Stats")]
        public StatContainer[] damageAmounts;
        public StatContainer staminaCost;
        
        [HideInInspector]
        public int comboIndex;

        public void Init(CharacterStateManager charStates)
        {
            runtimeWeapon = new RuntimeWeapon();

            InitWeaponStats();

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

            w.runtimeWeapon.modelInstance.transform.localScale = Vector3.one / 100;
            w.runtimeWeapon.modelInstance.SetActive(true);
        }

        private void InitWeaponStats()
        {
            for (int i = 0; i < damageAmounts.Length; i++)
            {
                damageAmounts[i].targetStat.Init(damageAmounts[i].startingValue);
            }

            staminaCost.targetStat.Init(staminaCost.startingValue);
        }
    }
}