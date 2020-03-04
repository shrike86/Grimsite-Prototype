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

        public override void Execute(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

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

        public override void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }

        public enum InputAxisType
        { 
            X,
            Y
        }
    }
}
