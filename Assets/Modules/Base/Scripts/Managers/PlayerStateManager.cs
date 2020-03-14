using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Items;
using Grimsite.ThirdPersonController;

namespace Grimsite.Base
{
    public class PlayerStateManager : CharacterStateManager
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
        public bool canCombo;
        public bool isAttacking;
        public ComboAttackPhase currentAttackPhase;


        public State currentState;
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

        public Weapon rightHandItem;
        public Weapon leftHandItem;


        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            // This will be removed when unarmed is no longer the default and the last equipped weapons will be equippen on init.
            leftHandItem.Init(this);
            rightHandItem.Init(this);
            isUnarmed = true;

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
        }

        private void Update()
        {
            delta = Time.deltaTime;

            if (currentState != null)
            {
                currentState.Tick(this);
            }
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