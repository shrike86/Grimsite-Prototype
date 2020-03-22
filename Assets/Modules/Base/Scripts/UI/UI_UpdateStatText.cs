using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Grimsite.Base
{
    public class UI_UpdateStatText : MonoBehaviour
    {
        public FloatVariable targetFloat;

        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            targetFloat.gameEvent += UpdateText;
        }

        private void UpdateText()
        {
            int experienceAsInt = Mathf.RoundToInt(targetFloat.value);

            text.text = string.Format("Experience: {0}", experienceAsInt.ToString());
        }
    }
}