using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public abstract class CharacterStateManager : MonoBehaviour
    {
        [Header("References")]
        public Animator anim;

        public State currentState;

        public bool stopActions;
    }
}