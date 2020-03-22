using Grimsite.Items;
using Grimsite.ThirdPersonController;
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
        public Weapon rightHandItem;
        public Weapon leftHandItem;
        public CharacterStateManager lastHitByChar;

        [Header("Character State Bools")]
        public State currentState;
        public ComboAttackPhase currentAttackPhase;
        [Space]
        public bool isGrounded;
        public bool isInteracting;
        public bool isLockedOn;
        public bool isRolling;
        public bool isOneHandedLeft;
        public bool isOneHandedRight;
        public bool isAttacking;
        public bool isPlayer;
        public bool isDead;
        public bool isUnarmed;
        public bool isTwoHanded;
        public bool isDualWield;
        public bool canCombo;
        public bool comboCooldownDone;
        public bool canMove;
        public bool canRotate;
        [Header("Stats")]
        public CharacterStats runtimeStats;

        [HideInInspector]
        public LayerMask ignoreLayers;
        [HideInInspector]
        public LayerMask ignoreForGroundCheck;
        [HideInInspector]
        public List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        [HideInInspector]
        public List<Collider> ragdollColliders = new List<Collider>();

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

            currentState.OnEnter(this);
            runtimeStats.InitStats();
        }

        public bool IsDead()
        {
            if (((FloatVariable)runtimeStats.health.targetStat.value).value <= 0)
                return true;

            return false;
        }

        public bool HasStamina(int amount)
        {
            if (((FloatVariable)runtimeStats.stamina.targetStat.value).value >= amount)
                return true;

            return false;
        }

        public void TakeDamage(CharacterStateManager hitChar, CharacterStateManager attackingChar, int amount)
        {
            if (hitChar == this)
            {
                lastHitByChar = attackingChar;
                ((FloatVariable)runtimeStats.health.targetStat.value).Remove(amount);
            }
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
    }
}