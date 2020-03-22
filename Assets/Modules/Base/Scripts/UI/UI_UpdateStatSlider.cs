using Grimsite.Base;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Grimsite.Base
{
    public class UI_UpdateStatSlider : MonoBehaviour
    {
        public StatType statType;
        public StateManagerVariable charStates;
        public Slider slider;
        public Stat targetStat;
        [Range(0, 10)]
        public float updateSpeed;
        public bool isEnemyCanvas;

        EnemyStateManager enemyStates;

        private void Awake()
        {
            slider = GetComponent<Slider>();

            AssignStat();
        }

        private void Start()
        {
            targetStat.statChangeEvent += UpdateSlider;
            enemyStates = charStates.value as EnemyStateManager;
        }

        private void LateUpdate()
        {
            if (!isEnemyCanvas)
                return;

            transform.LookAt(Camera.main.transform);

            if (enemyStates.isDead)
            {
                Disable();
            }
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
                default:
                    break;
            }
        }

        private void UpdateSlider()
        {
            if (targetStat != null)
                StartCoroutine(ChangeToPercent(targetStat.percent));
        }

        private IEnumerator ChangeToPercent(float percent)
        {
            float currentPercentage = slider.value;
            float elapsed = 0f;

            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                slider.value = Mathf.Lerp(currentPercentage, percent, elapsed / updateSpeed);
                yield return null;
            }

            slider.value = percent;
        }

        private void Disable()
        {
            gameObject.SetActive(false);

            if (targetStat != null)
                targetStat.statChangeEvent -= UpdateSlider;
        }
    }

    public enum StatType
    { 
        Health,
        Stamina,
        Experience,
        Currency
    }
}