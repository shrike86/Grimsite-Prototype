using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Grimsite.Base
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 inputDirection;
        public Vector2 cameraDirection;

        public StateManagerVariable stateManagerVariable;
        public TransformVariable cameraTransform;

        Vector3 movementDirection;

        PlayerStateManager states;
        PlayerControls input;

        private void Start()
        {
            states = stateManagerVariable.value as PlayerStateManager;

            input = new PlayerControls();
            input.Player.Movement.performed += i => inputDirection = i.ReadValue<Vector2>();
            input.Player.Camera.performed += i => cameraDirection = i.ReadValue<Vector2>();
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void Update()
        {
            GetMovementDirection();
            UpdateInputState();

        }

        private void FixedUpdate()
        {
 
        }

        private void GetMovementDirection()
        {
            if (cameraTransform != null)
            {
                movementDirection = cameraTransform.value.right * inputDirection.x;
                movementDirection += cameraTransform.value.forward * inputDirection.y;
            }
        }

        private void UpdateInputState()
        {
            states.vertical = inputDirection.y;
            states.horizontal = inputDirection.x;
            states.mouseY = cameraDirection.y;
            states.mouseX = cameraDirection.x;
            states.moveAmount = Mathf.Clamp01(Mathf.Abs(inputDirection.x) + Mathf.Abs(inputDirection.y));
            states.movementDirection = movementDirection;

            states.inputStates.isPressed_space = GetButtonStatus(input.Player.Roll.phase);
            states.inputStates.isPressed_T = GetButtonStatus(input.Player.LockOn.phase);
            states.inputStates.isPressed_I = GetButtonStatus(input.Player.ToggleInventory.phase);
            states.inputStates.isPressed_leftMouse = GetButtonStatus(input.Player.LeftMouse.phase);
            states.inputStates.isPressed_rightMouse = GetButtonStatus(input.Player.RightMouse.phase);
        }

        bool GetButtonStatus(UnityEngine.InputSystem.InputActionPhase phase)
        {
            return phase == UnityEngine.InputSystem.InputActionPhase.Started;
        }

    }
}