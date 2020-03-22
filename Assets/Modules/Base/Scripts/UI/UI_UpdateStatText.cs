using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Grimsite.Base
{
    public class UI_UpdateStatText : MonoBehaviour
    {
        public StatType statType;
        public StateManagerVariable charStates;
        public Stat targetStat;

        private PlayerStateManager playerStates;
        private PlayerStats playerStats;
        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            playerStates = charStates.value as PlayerStateManager;
            playerStats = playerStates.runtimeStats as PlayerStats;
            
            AssignStat();
        }

        private void Start()
        {
            targetStat.statChangeEvent += UpdateText;
        }

        private void AssignStat()
        {
            switch (statType)
            {
                case StatType.Health:
                    targetStat = charStates.value.runtimeStats.health.targetStat;
                    break;
                case StatType.Stamina:
                    targetStat = charStates.value.runtimeStats.stamina.targetStat;
                    break;
                case StatType.Experience:
                    targetStat = playerStats.experience.targetStat;
                    break;
                default:
                    break;
            }
        }

        private void UpdateText()
        {
            int experienceAsInt = targetStat.ToInt();

            text.text = string.Format("Experience: {0}", experienceAsInt.ToString());
        }
    }
}