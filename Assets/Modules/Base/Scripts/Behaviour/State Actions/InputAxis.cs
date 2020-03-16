using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Inputs/Axis")]
    public class InputAxis : StateActions
    {
        public float value;
        public InputAxisType targetAxis;

        private PlayerStateManager states;

        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            switch (targetAxis)
            {
                case InputAxisType.X:
                    value = states.mouseX; 
                    break;
                case InputAxisType.Y:
                    value = states.mouseY;
                    break;
                default:
                    break;
            }
        }

        public enum InputAxisType
        { 
            X,
            Y
        }
    }
}
