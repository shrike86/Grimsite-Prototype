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

        public StateManagerVariable characterStates;

        public override void Execute()
        {
            mouseX.Execute(characterStates.value);
            mouseY.Execute(characterStates.value);
        }
    }
}