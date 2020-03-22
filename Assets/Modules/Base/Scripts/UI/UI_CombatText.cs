using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Grimsite.Base
{
    public class UI_CombatText : MonoBehaviour
    {
        private TextMeshPro text;

        public void SpawnCombatText(string textValue, Color textColour)
        {
            text = GetComponent<TextMeshPro>();

            text.text = textValue;
            text.color = textColour;

            Destroy(transform.parent.gameObject, 1.5f);
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}