using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/Integer")]
    public class IntVariable : NumberVariable
    {
		public event System.Action gameEvent;

		private int value;

        public int Value
		{
			get { return value; }
		}

		public void Set(int v)
		{
			value = v;

			gameEvent?.Invoke();

		}

        public void Set(NumberVariable v)
        {
            if(v is FloatVariable)
            {
                FloatVariable f = (FloatVariable)v;
				value = Mathf.RoundToInt(f.value);
            }

            if(v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
				value = i.value;
            }

            gameEvent?.Invoke();
        }

        public void Add(int v)
        {
			value += v;

            gameEvent?.Invoke();
        }

        public void Remove(int v)
        {
            value -= v;

            gameEvent?.Invoke();
        }

        public void Add(NumberVariable v)
        {
            if (v is FloatVariable)
            {
                FloatVariable f = (FloatVariable)v;
				value += Mathf.RoundToInt(f.value);
            }

            if (v is IntVariable)
            {
                IntVariable i = (IntVariable)v;
				value += i.value;
            }

            gameEvent?.Invoke();
        }
    }
}
