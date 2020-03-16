using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/Float")]
    public class FloatVariable : NumberVariable
    {
        public event System.Action gameEvent;
        public float value;

        public void Set(float v)
        {
            value = v;
            gameEvent?.Invoke();
        }

        public void Set(NumberVariable v)
        {
            if (v is FloatVariable)
            {
                FloatVariable f = (FloatVariable)v;
                value = f.value;
            }

            if (v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
                value = i.value;
            }

            gameEvent?.Invoke();
        }

        public void Add(float v)
        {
            value += v;
            gameEvent?.Invoke();
        }

        public void Remove(float v)
        {
            value -= v;
            gameEvent?.Invoke();
        }

        public void Add(NumberVariable v)
        {
            if (v is FloatVariable)
            {
                FloatVariable f = (FloatVariable)v;
                value += f.value;
            }

            if (v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
                value += i.value;
            }

            gameEvent?.Invoke();
        }

        public void Remove(NumberVariable v)
        {
            if (v is FloatVariable)
            {
                FloatVariable f = (FloatVariable)v;
                value -= f.value;
            }

            if (v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
                value -= i.value;
            }

            gameEvent?.Invoke();
        }
    }
}
