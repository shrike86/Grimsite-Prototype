using System.Collections;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Init Death")]
    public class InitDeath : StateActions
    {
        public TransformVariable gameManager;
        public StateActions awardExperienceAction;
        public StateActions dropLootAction;

        public override void Execute(CharacterStateManager states)
        {
            ScriptableHelpers sh = gameManager.value.GetComponent<ScriptableHelpers>();
            sh.DisableAnimatorWrapper(states);
            awardExperienceAction.Execute(states);
            sh.DestroyObjectAfterPeriodWrapper(states.gameObject, 3);
            dropLootAction.Execute(states);
        }
    }
}