using Grimsite.Base;
using UnityEngine;

namespace Grimsite.Base
{
    [System.Serializable]
    public class Stat 
    {
        public float maxValue;
        public float percent;

        public event System.Action statChangeEvent;

        private float value;

        public float Value
        {
            get { return this.value; }
            set 
            {
                this.value = value;
                percent = Mathf.Clamp((value / maxValue) * 100, 0, 100);
                statChangeEvent?.Invoke();
            }
        }

        public void Init(float maxValue)
        {
            this.maxValue = maxValue;
            Value = maxValue;
        }

        public void Add(float v)
        {
            Value += v;
        }

        public void Remove(float v)
        {
            Value -= v;
        }

        public int ToInt()
        {
            return Mathf.RoundToInt(Value);
        }
    }
}