using Grimsite.ThirdPersonController;
using UnityEngine;

namespace Grimsite.Base
{
    public class PlayerStateManager : CharacterStateManager
    {
        [Header("Player State")]
        public State playerConstant;

        public float generalTime;
        public float delta;
        public float fixedDelta;

        public bool stopActions;
        public bool isUserInterfaceActive;

        [Header("Movement States")]
        public float horizontal;
        public float vertical;
        public float mouseX;
        public float mouseY;
        public float moveAmount;
        public Vector3 rollDirection;
        public Vector3 movementDirection;

        [Header("Input States")]
        public InputStates inputStates;

        public Transform currentLockonTarget;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            runtimeStats.InitStats();

            // This will be removed when unarmed is no longer the default and the last equipped weapons will be equipped on init.
            leftHandItem.Init(this);
            rightHandItem.Init(this);
            isUnarmed = true;
        }

        public override void Init()
        {
            base.Init();

            animData = new AnimData(anim);
            currentAttackPhase = ComboAttackPhase.NotAttacking;
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;

            if (currentState != null)
            {
                currentState.FixedTick(this);
            }

            playerConstant.FixedTick(this);
        }

        private void Update()
        {
            delta = Time.deltaTime;

            if (currentState != null)
            {
                currentState.Tick(this);
            }

            playerConstant.Tick(this);
        }
    }

    [System.Serializable]
    public class InputStates
    {
        public bool isPressed_space;
        public bool isPressed_T;
        public bool isPressed_I;
        public bool isPressed_leftMouse;
        public bool isPressed_rightMouse;
    }

}