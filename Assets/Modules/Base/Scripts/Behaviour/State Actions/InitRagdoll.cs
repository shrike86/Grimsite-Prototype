using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Init Ragdoll")]
    public class InitRagdoll : StateActions
    {
        public override void Execute(CharacterStateManager states)
        {
            Rigidbody[] rigs = states.GetComponentsInChildren<Rigidbody>();

            for (int i = 0; i < rigs.Length; i++)
            {
                if (rigs[i] == states.rigidbody)
                    continue;

                states.ragdollRigids.Add(rigs[i]);
                rigs[i].isKinematic = true;

                Collider col = rigs[i].gameObject.GetComponent<Collider>();
                col.isTrigger = true;
                states.ragdollColliders.Add(col);

                col.enabled = false;
            }
        }
    }
}
