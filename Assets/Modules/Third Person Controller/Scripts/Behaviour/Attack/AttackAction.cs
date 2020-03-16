using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using Grimsite.Items;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Attack/Attack Action")]
    public class AttackAction : StateActions
    {
        public TransformVariable gameManager;
        public Weapon defaultWeapon;

        private ScriptableHelpers sh;

        public override void Execute(CharacterStateManager states)
        {
            if (sh == null)
                Init();

            int staminaCost = ((IntVariable)states.rightHandItem.staminaCost.targetStat.value).value;

            if (states.HasStamina(staminaCost))
            {
                if (HandleCombo(states))
                {
                    SubtractStamina(states);
                    PlayAttackAnimation(states);
                }
            }
            else
            {
                Debug.Log("You don't have enough stamina to attack.");
            }
        }

        private void Init()
        {
            sh = gameManager.value.GetComponent<ScriptableHelpers>();
        }

        private bool HandleCombo(CharacterStateManager states)
        {
            if (states.currentAttackPhase == ComboAttackPhase.NotAttacking && !states.comboCooldownDone)
            {
                states.currentAttackPhase = ComboAttackPhase.First;
                states.isAttacking = true;
                states.comboCooldownDone = false;

                sh.WaitForTime(states.rightHandItem.timeBetweenComboAttack, x =>
                {
                    states.comboCooldownDone = x;
                });

                return true;
            }

            if (states.currentAttackPhase == ComboAttackPhase.First && states.canCombo && states.comboCooldownDone)
            {
                states.canCombo = false;
                states.currentAttackPhase = ComboAttackPhase.Second;
                states.rightHandItem.comboIndex = 1;
                states.isAttacking = true;
                states.comboCooldownDone = false;

                sh.WaitForTime(states.rightHandItem.timeBetweenComboAttack, x =>
                {
                    states.comboCooldownDone = x;
                });

                return true;
            }

            if (states.currentAttackPhase == ComboAttackPhase.Second && states.canCombo && states.comboCooldownDone)
            {
                states.canCombo = false;
                states.currentAttackPhase = ComboAttackPhase.Third;
                states.rightHandItem.comboIndex = 2;
                states.isAttacking = true;
                states.comboCooldownDone = false;

                sh.WaitForTime(states.rightHandItem.timeBetweenComboAttack, x =>
                {
                    states.comboCooldownDone = x;
                });

                return true;
            }

            return false;
        }

        private void PlayAttackAnimation(CharacterStateManager states)
        {
            if (states.isUnarmed)
            {
                states.animHook.PlayAnimation(defaultWeapon.attackAnimations[defaultWeapon.comboIndex]);
            }
            else if (states.isTwoHanded)
            {
                states.animHook.PlayAnimation(states.leftHandItem.attackAnimations[states.leftHandItem.comboIndex]);
            }
        }

        private void SubtractStamina(CharacterStateManager states)
        {
            int weaponStam = ((IntVariable)states.rightHandItem.staminaCost.targetStat.value).value;
            ((FloatVariable)states.runtimeStats.stamina.targetStat.value).Remove(weaponStam);
        }
    }

    public enum ComboAttackPhase
    {
        NotAttacking,
        First,
        Second,
        Third
    }
}