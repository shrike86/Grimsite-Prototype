using System.Collections;
using UnityEngine;

namespace Grimsite.Base
{
    public class ScriptableHelpers : MonoBehaviour
    {
        public void PerformDeathCleanupWrapper(CharacterStateManager states)
        {
            StartCoroutine(PerformDeathCleanup(states));
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

        private IEnumerator PerformDeathCleanup(CharacterStateManager states)
        {
            yield return new WaitForEndOfFrame();
            states.anim.enabled = false;
            states.animHook.enabled = false;
            states.enabled = false;
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