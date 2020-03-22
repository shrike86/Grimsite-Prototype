using System.Collections;
using UnityEngine;

namespace Grimsite.Base
{
    public class ScriptableHelpers : MonoBehaviour
    {
        public void DisableAnimatorWrapper(CharacterStateManager states)
        {
            StartCoroutine(DisableAnimator(states));
        }

        public void DestroyObjectAfterPeriodWrapper(GameObject obj, int period)
        {
            StartCoroutine(DestroyObjectAfterPeriod(obj, period));
        }

        public void WaitForTime(float time, System.Action<bool> callback)
        {
            bool tempBool = false;

            StartCoroutine(WaitForTimeImpl(time, x => 
            {
                tempBool = x;
                callback(tempBool);
            }));
        }

        private IEnumerator DisableAnimator(CharacterStateManager states)
        {
            yield return new WaitForEndOfFrame();
            states.anim.enabled = false;
            states.animHook.enabled = false;
        }

        private IEnumerator DestroyObjectAfterPeriod(GameObject obj, int period)
        {
            yield return new WaitForSeconds(period);
            Destroy(obj);
        }

        private IEnumerator WaitForTimeImpl(float time, System.Action<bool> callback)
        {
            bool tempBool = false;

            yield return new WaitForSeconds(time);
            tempBool = true;

            callback(tempBool);
        }

    }
}