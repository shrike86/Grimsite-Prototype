using Grimsite.Base;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Grimsite.Base
{
    public class UI_UpdateStatSlider : MonoBehaviour
    {
        public StateManagerVariable charStates;
        public Slider slider;
        public IntVariable targetInt;
        public FloatVariable targetFloat;
        [Range(0, 10)]
        public float updateSpeed;
        public bool isEnemyCanvas;

        EnemyStateManager enemyStates;

        private void Awake()
        {
            slider = GetComponent<Slider>();

            if (targetInt != null)
                targetInt.gameEvent += UpdateSlider;
            else
                targetFloat.gameEvent += UpdateSlider;
        }

        private void Start()
        {
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

        private void UpdateSlider()
        {
            if (targetInt != null)
                StartCoroutine(ChangeToPercent(targetInt.value));
            else
                StartCoroutine(ChangeToPercent(targetFloat.value))
;
        }

        private IEnumerator ChangeToPercent(int percent)
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

            if (targetInt != null)
                targetInt.gameEvent -= UpdateSlider;
            else
                targetFloat.gameEvent -= UpdateSlider;
        }
    }
}