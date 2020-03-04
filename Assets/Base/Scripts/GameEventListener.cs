using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Grimsite.Base
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent targetEvent;
        public UnityEvent response;

        private void OnEnable()
        {
            //targetEvent.UnRegister(this);
            targetEvent.Register(this);
        }
        public virtual void Raise()
        {
            response.Invoke();
        }
    }

}