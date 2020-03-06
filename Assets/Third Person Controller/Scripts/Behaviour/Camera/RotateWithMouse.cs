using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/RotateWithMouse")]
    public class RotateWithMouse : Base.Action
    {
        public InputAxis targetInput;
        public TransformVariable targetTransform;
        public FloatVariable delta;
        public FloatVariable angle;
        public StateManagerVariable characterStates;

        public RotateAxis targetAxis;
        public float speed;
        public float minClamp = -35;
        public float maxClamp = 35;
        public bool clamp;
        public bool negative;
        public bool canTurn;

        private PlayerStateManager states;

        public override void Execute()
        {
            if (states == null)
                Init();

            Debug.Log(angle.value);
            float t = delta.value * speed;

            if (!characterStates.value.isLockedOn)
            {
                if (!negative)
                    angle.value = Mathf.LerpAngle(angle.value, angle.value + targetInput.value * t, t);
                else
                    angle.value = Mathf.LerpAngle(angle.value, angle.value - targetInput.value * t, t);

                if (clamp)
                {
                    angle.value = Mathf.Clamp(angle.value, minClamp, maxClamp);
                }

                switch (targetAxis)
                {
                    case RotateAxis.X:
                        targetTransform.value.localRotation = Quaternion.Euler(angle.value, 0, 0);
                        break;
                    case RotateAxis.Y:
                        targetTransform.value.localRotation = Quaternion.Euler(0, angle.value, 0);
                        break;
                    case RotateAxis.Z:
                        targetTransform.value.localRotation = Quaternion.Euler(0, 0, angle.value);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Init()
        {
            states = characterStates.value as PlayerStateManager;
        }


        public enum RotateAxis
        {
            X,
            Y,
            Z
        }
    }
}