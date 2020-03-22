using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class CombatTextHook : MonoBehaviour
    {
        public GameObject combatTextPrefab;

        private UI_CombatText combatText;
        private CharacterStateManager charStates;
        private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);
        private Vector3 yOffset = new Vector3(0, 2.2f, 0);

        public void SpawnCombatText(CharacterStateManager attackingChar)
        {
            if (charStates == null)
                charStates = GetComponentInParent<CharacterStateManager>();

            var damage = attackingChar.rightHandItem.damageAmounts[attackingChar.rightHandItem.comboIndex].targetStat.Value;

            GameObject go = Instantiate(combatTextPrefab, charStates.gameObject.transform.position + yOffset, Quaternion.identity, transform);
            go.transform.position += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x), 0, 0);

            combatText = go.GetComponentInChildren<UI_CombatText>();
            combatText.SpawnCombatText(damage.ToString(), Color.yellow);
        }
    }
}