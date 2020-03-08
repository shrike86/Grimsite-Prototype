using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Items;

namespace Grimsite.Base
{
    public class PlayerStateManager : CharacterStateManager
    {
        [Header("Player State Bools")]
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


        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            isUnarmed = true;
            base.Init();
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