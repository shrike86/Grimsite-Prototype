using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Animations/Update Anim States")]
    public class UpdateAnimStates : StateActions
    {
        public override void Execute(CharacterStateManager states)
        {
            states.anim.SetBool("isUnarmed", states.isUnarmed);
            states.anim.SetBool("isTwoHanded", states.isTwoHanded);
            states.anim.SetBool("isLockedOn", states.isLockedOn);

        }
    }
}