using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Event")]
    public class GameEvent : ScriptableObject
    {
        List<GameEventListener> listeners = new List<GameEventListener>();

        public void Register(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnRegister(GameEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public void Raise()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                 listeners[i].Raise();
            }
        }
    }
}