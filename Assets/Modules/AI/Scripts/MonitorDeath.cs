using Grimsite.Base;
using UnityEngine;

namespace Grimsite.AI
{
    [CreateAssetMenu(menuName = "Behaviour/Conditions/Monitor Death")]
    public class MonitorDeath : Condition
    {
        public override bool CheckCondition(CharacterStateManager states)
        {
            if (states.IsDead())
            {
                states.isDead = true;            
                return true;
            }
            else
                return false;
        }
    }
}