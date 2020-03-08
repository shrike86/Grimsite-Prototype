using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/Integer")]
    public class IntVariable : NumberVariable
    {
		public GameEvent gameEvent;

		private int value;

        public int Value
		{
			get { return value; }
		}

		public void Set(int v)
		{
			value = v;

			if (gameEvent != null)
			{
				gameEvent.Raise();
			}
				
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

			if (gameEvent != null)
			{
				gameEvent.Raise();
			}
		}

        public void Add(int v)
        {
			value += v;

			if (gameEvent != null)
			{
				gameEvent.Raise();
			}
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

			if (gameEvent != null)
			{
				gameEvent.Raise();
			}
		}
    }
}
