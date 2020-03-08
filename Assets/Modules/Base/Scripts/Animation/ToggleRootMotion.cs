using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Animations/Toggle Root Motion")]
    public class ToggleRootMotion : StateActions
    {
		public bool status;

		public override void Execute(CharacterStateManager characterStates)
		{
			characterStates.anim.applyRootMotion = status;
		}

		public override void Init(CharacterStateManager characterStates)
		{
		}
	}
}