using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.Base
{
    public class AnimatorHook : MonoBehaviour
    {
        public StateActions[] ikActions;
        public StateActions[] fixedActions;
        public StateActions[] updatedActions;

        PlayerStateManager playerStates;
        CharacterStateManager charStates;
        Animator anim;

        public void Init(CharacterStateManager st)
        {
            anim = st.anim;
            charStates = st;
            playerStates = st as PlayerStateManager;
        }

        private void FixedUpdate()
        {
            if (playerStates == null)
                return;

            for (int i = 0; i < fixedActions.Length; i++)
            {
                fixedActions[i].Execute(playerStates);
            }
        }

        private void Update()
        {
            if (playerStates == null)
                return;

            for (int i = 0; i < updatedActions.Length; i++)
            {
                updatedActions[i].Execute(playerStates);
            }
        }


        private void OnAnimatorIK(int layerIndex)
        {
            if (playerStates == null)
                return;

            for (int i = 0; i < ikActions.Length; i++)
            {
                ikActions[i].Execute(playerStates);
            }
        }

        private void OnAnimatorMove()
        {
            transform.localPosition = Vector3.zero;
        }

        public void PlayAnimation(string targetAnim)
        {
            anim.CrossFade(targetAnim, 0.2f);
            anim.SetBool("isInteracting", true);
        }

        public void OpenComboPhase()
        {
            playerStates.canCombo = true;
        }

        public void CloseComboPhase()
        {
            playerStates.canCombo = false;
        }

        public void SetEndAttack()
        {
            playerStates.isAttacking = false;
        }

        public void OpenDamageColliders()
        {
            if (playerStates == null)
            {

            }
            else
            {
                if (playerStates.leftHandItem.isDefault && playerStates.rightHandItem.isDefault)
                {
                    playerStates.rightHandItem.runtimeWeapon.weaponHook.OpenDamageColliders();
                    playerStates.leftHandItem.runtimeWeapon.weaponHook.OpenDamageColliders();
                }
                else
                    playerStates.rightHandItem.runtimeWeapon.weaponHook.OpenDamageColliders();
            }
        }

        public void CloseDamageColliders()
        {
            if (playerStates == null)
            {
                // TODO enemy logic.
            }
            else
            {
                if (playerStates.leftHandItem.isDefault && playerStates.rightHandItem.isDefault)
                {
                    playerStates.rightHandItem.runtimeWeapon.weaponHook.CloseDamageColliders();
                    playerStates.leftHandItem.runtimeWeapon.weaponHook.CloseDamageColliders();
                }
                else
                    playerStates.rightHandItem.runtimeWeapon.weaponHook.CloseDamageColliders();
            }
        }
    }
}