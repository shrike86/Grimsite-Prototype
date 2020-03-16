using System.Collections;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Init Death")]
    public class InitDeath : StateActions
    {
        public TransformVariable gameManager;

        public override void Execute(CharacterStateManager states)
        {
            ScriptableHelpers sh = gameManager.value.GetComponent<ScriptableHelpers>();
            sh.PerformDeathCleanupWrapper(states);
        }
    }
}