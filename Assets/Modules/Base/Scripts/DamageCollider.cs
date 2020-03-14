using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class DamageCollider : MonoBehaviour
    {
        public event System.Action<CharacterStateManager> onHit;

        private void Start()
        {
            this.gameObject.layer = 10;
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterStateManager states = other.transform.GetComponentInParent<CharacterStateManager>();

            if (states != null)
            {
                onHit?.Invoke(states);
            }
        }
    }
}