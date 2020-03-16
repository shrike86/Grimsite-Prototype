using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Enable Ragdoll")]
    public class EnableRagdoll : StateActions
    {
        public override void Execute(CharacterStateManager states)
        {
            for (int i = 0; i < states.ragdollRigids.Count; i++)
            {
                states.ragdollRigids[i].isKinematic = false;

                Collider col = states.ragdollColliders[i].GetComponent<Collider>();

                col.enabled = true;
                col.isTrigger = false;
            }

            Collider characterCollider = states.rigidbody.gameObject.GetComponent<Collider>();
            characterCollider.enabled = false;
            states.rigidbody.isKinematic = true;
        }
    }
}