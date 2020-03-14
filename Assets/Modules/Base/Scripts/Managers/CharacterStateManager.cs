using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class CharacterStateManager : MonoBehaviour, ILockable
    {
        [Header("References")]
        public Animator anim;
        public AnimatorHook animHook;
        public AnimData animData;
        public new Rigidbody rigidbody;
        public GameObject activeModel;
        public Transform mTransform;
        public bool isPlayer;
        public bool isDead;

        [HideInInspector]
        public LayerMask ignoreLayers;
        [HideInInspector]
        public LayerMask ignoreForGroundCheck;

        public IntVariable health;

        private List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        private List<Collider> ragdollColliders = new List<Collider>();

        public Transform LockOn()
        {
            if (!isPlayer)
                return mTransform;
            else
                return null;
        }

        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            mTransform = this.transform;
            SetupAnimator();

            rigidbody = GetComponent<Rigidbody>();
            rigidbody.angularDrag = 999;
            rigidbody.drag = 4;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 8;
            ignoreForGroundCheck = ~(1 << 10);

            animHook = GetComponentInChildren<AnimatorHook>();
            animHook.Init(this);
            mTransform = transform;

            health.Set(100);
            InitRagdoll();

        }

        public bool IsDead()
        {
            if (health.Value <= 0)
                return true;

            return false;
        }

        public void TakeDamage(CharacterStateManager st, int amount)
        {
            if (st == this)
            {
                health.Remove(amount);
                Debug.Log(health.Value);
            }
        }

        public void EnableRagdoll()
        {
            for (int i = 0; i < ragdollRigids.Count; i++)
            {
                ragdollRigids[i].isKinematic = false;

                Collider col = ragdollColliders[i].GetComponent<Collider>();

                col.enabled = true;
                col.isTrigger = false;
            }

            Collider characterCollider = rigidbody.gameObject.GetComponent<Collider>();
            characterCollider.enabled = false;
            rigidbody.isKinematic = true;

            StartCoroutine("CloseAnimator");
        }

        private IEnumerator CloseAnimator()
        { 
            yield return new WaitForEndOfFrame();
            anim.enabled = false;
            enabled = false;
        }

        private void SetupAnimator()
        {
            if (anim == null)
                anim = GetComponentInChildren<Animator>();

            if (activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                activeModel = anim.gameObject;
            }

            anim.applyRootMotion = false;
        }

        private void InitRagdoll()
        {
            Rigidbody[] rigs = GetComponentsInChildren<Rigidbody>();

            for (int i = 0; i < rigs.Length; i++)
            {
                if (rigs[i] == rigidbody)
                    continue;

                ragdollRigids.Add(rigs[i]);
                rigs[i].isKinematic = true;

                Collider col = rigs[i].gameObject.GetComponent<Collider>();
                col.isTrigger = true;
                ragdollColliders.Add(col);

                col.enabled = false;
            }
        }
    }
}