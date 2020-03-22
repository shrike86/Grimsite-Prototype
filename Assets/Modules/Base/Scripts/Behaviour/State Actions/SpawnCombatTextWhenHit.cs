using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Spawn Combat Text When Hit")]
    public class SpawnCombatTextWhenHit : StateActions
    {
        public GameObject combatTextPrefab;

        private DamageCollider charCollider;
        private UI_CombatText combatText;
        private CharacterStateManager thisCharStates;
        private CharacterStateManager attackingCharStates;
        private bool hasInit;
        private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);
        private Vector3 yOffset = new Vector3(0, 2.2f, 0);

        private void OnEnable()
        {
            hasInit = false;
        }

        private void OnDisable()
        {
            hasInit = false;
        }

        public override void Execute(CharacterStateManager characterStates)
        {
            if (!hasInit)
                Init(characterStates);

            if (attackingCharStates == null)
                return;

            thisCharStates = characterStates as CharacterStateManager;
            SpawnCombatText(attackingCharStates);
        }

        private void UpdateAttackingCharacter(CharacterStateManager attackingChar)
        {
            attackingCharStates = attackingChar;
        }

        private void Init(CharacterStateManager characterStates)
        {
            Debug.Log(characterStates);
            hasInit = true;
            charCollider = characterStates.GetComponent<DamageCollider>();
            charCollider.onHit += UpdateAttackingCharacter;
        }

        private void SpawnCombatText(CharacterStateManager attackingChar)
        {
            var damage = attackingChar.rightHandItem.damageAmounts[attackingChar.rightHandItem.comboIndex].targetStat.Value;

            Debug.Log(thisCharStates.transform.position);
            GameObject go = Instantiate(combatTextPrefab, thisCharStates.transform.position + yOffset, Quaternion.identity, thisCharStates.transform);
            combatText = go.GetComponentInChildren<UI_CombatText>();

            go.transform.position += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x), 0, 0); 

            combatText.SpawnCombatText(damage.ToString(), Color.yellow);
        }
    }
}