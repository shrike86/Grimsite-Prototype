using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Animations/Update Anim States")]
    public class UpdateAnimStates : StateActions
    {
        private PlayerStateManager states;

        public override void Execute(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

            states.anim.SetBool("isUnarmed", states.isUnarmed);
            states.anim.SetBool("isTwoHanded", states.isTwoHanded);
            states.anim.SetBool("isLockedOn", states.isLockedOn);

        }

        public override void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }
    }
}