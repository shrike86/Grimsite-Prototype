using UnityEngine;
using Grimsite.Base;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Movement/Handle Normal Movement Animations")]
    public class HandleNormalMovementAnimations : StateActions
    {
        public string floatName;
        public StateActions[] animActions;

        private PlayerStateManager states;

        public override void Execute(PlayerStateManager states)
        {
            if (animActions.Length > 0)
            {
                for (int i = 0; i < animActions.Length; i++)
                {
                    animActions[i].Execute(states);
                }
            }

            if (states.anim.GetFloat("sideways") != 0)
            {
                states.anim.SetFloat("sideways", 0);
            }

            states.anim.SetFloat(floatName, states.moveAmount, .2f, states.delta);
            
        }
    }
}
