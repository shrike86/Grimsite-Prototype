using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class CharacterStateManager : MonoBehaviour, ILockable
    {
        [Header("Character State Bools")]
        public bool isGrounded;
        public bool isInteracting;
        public bool isLockedOn;
        public bool isRolling;
        public bool isOneHandedLeft;
        public bool isOneHandedRight;
        public bool isUnarmed;
        public bool isTwoHanded;
        public bool isDualWield;
        public bool isPlayer;


        [Header("References")]
        public Animator anim;
        public AnimatorHook animHook;
        public AnimData animData;
        public new Rigidbody rigidbody;
        public Transform mTransform;
        public GameObject activeModel;

        [HideInInspector]
        public LayerMask ignoreLayers;
        [HideInInspector]
        public LayerMask ignoreForGroundCheck;

        public State currentState;
        public float generalTime;
        public float delta;
        public float fixedDelta;

        public bool stopActions;

        private void Start()
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

            animData = new AnimData(anim);
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

        public Transform LockOn()
        {
            if (!isPlayer)
                return mTransform;
            else
                return null;
        }
    }
}