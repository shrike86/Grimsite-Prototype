using Grimsite.Base;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Stats/Stat")]
    public class Stat : ScriptableObject
    {
        public NumberVariable value;

        public void Set(float v)
        {
            if (value is FloatVariable)
            {
                (value as FloatVariable).Set(v);
            }
            else if(value is IntVariable)
            {
                (value as IntVariable).Set((int)v);
            }
        }

        public void Add(float v)
        {
            if (value is FloatVariable)
            {
                (value as FloatVariable).Add(v);
            }
            else if (value is IntVariable)
            {
                (value as IntVariable).Add((int)v);
            }
        }

        public void Remove(float v)
        {
            if (value is FloatVariable)
            {
                (value as FloatVariable).Remove(v);
            }
            else if (value is IntVariable)
            {
                (value as IntVariable).Remove((int)v);
            }
        }
    }
}