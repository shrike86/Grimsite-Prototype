using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.Items
{
    public class WeaponHook : MonoBehaviour
    {
        DamageCollider[] damageColliders;

        private CharacterStateManager attackingChar;

        public void Init(CharacterStateManager cs)
        {
            attackingChar = cs;
            damageColliders = transform.GetComponentsInChildren<DamageCollider>(true);

            for (int i = 0; i < damageColliders.Length; i++)
            {
                damageColliders[i].GetComponent<Collider>().isTrigger = true;
                damageColliders[i].onHit += DealDamage;
                damageColliders[i].onHit += DisableLockonIfTargetDead;
            }
        }

        public void OpenDamageColliders()
        {
            for (int i = 0; i < damageColliders.Length; i++)
            {
                damageColliders[i].gameObject.SetActive(true);
            }
        }

        public void CloseDamageColliders()
        {
            for (int i = 0; i < damageColliders.Length; i++)
            {
                damageColliders[i].gameObject.SetActive(false);
            }
        }

        private void DealDamage(CharacterStateManager hitCharacter)
        {
            if (attackingChar.isPlayer)
            {
                PlayerStateManager st = attackingChar as PlayerStateManager;
                hitCharacter.TakeDamage(hitCharacter, attackingChar, st.rightHandItem.damageAmounts[st.rightHandItem.comboIndex].targetStat.ToInt());
            }
        }

        private void DisableLockonIfTargetDead(CharacterStateManager hitCharacter)
        {
            if (attackingChar.isPlayer)
            {
                PlayerStateManager st = attackingChar as PlayerStateManager;

                if (hitCharacter.isDead)
                {
                    st.isLockedOn = false;
                    st.currentLockonTarget = null;
                }
            }
        }
    }
}