using Grimsite.Base;
using System;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Camera/Assign Mouse Input")]
    public class AssignMouseInput : Base.Action
    {
        public InputAxis mouseX;
        public InputAxis mouseY;

        public StateManagerVariable charStates;

        private PlayerStateManager states;

        public override void Execute()
        {
            states = charStates.value as PlayerStateManager;

            mouseX.Execute(states);
            mouseY.Execute(states);
        }
    }
}