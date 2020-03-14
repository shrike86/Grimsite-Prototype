using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Behaviour/State")]
    public class State : ScriptableObject
    {
    	public StateActions[] onFixed;
        public StateActions[] onUpdate;
        public StateActions[] onEnter;
        public StateActions[] onExit;

        public int idCount;
		[SerializeField]
        public List<Transition> transitions = new List<Transition>();

        public void OnEnter(PlayerStateManager states)
        {
            ExecuteActions(states, onEnter);
        }
	
		public void FixedTick(PlayerStateManager states)
		{
			ExecuteActions(states,onFixed);
            CheckTransitions(states);
        }

        public void Tick(PlayerStateManager states)
        {
            ExecuteActions(states, onUpdate);
            CheckTransitions(states);
        }

        public void OnExit(PlayerStateManager states)
        {
            ExecuteActions(states, onExit);
        }

        public void CheckTransitions(PlayerStateManager states)
        {
            if (states.stopActions)
                return;

            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].disable)
                    continue;

                if(transitions[i].condition.CheckCondition(states))
                {
                    if (transitions[i].targetState != null)
                    {
                        OnExit(states);
                        states.currentState = transitions[i].targetState;
                        states.currentState.OnEnter(states);
                    }
                    return;
                }
            }
        }
        
        public void ExecuteActions(PlayerStateManager states, StateActions[] l)
        {
            states.stopActions = false;

            for (int i = 0; i < l.Length; i++)
            {
                if (states.stopActions)
                    return;

                if (l[i] != null)
                    l[i].Execute(states);
            }
        }

        public Transition AddTransition()
        {
            Transition retVal = new Transition();
            transitions.Add(retVal);
            retVal.id = idCount;
            idCount++;
            return retVal;
        }

        public Transition GetTransition(int id)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].id == id)
                    return transitions[i];
            }

            return null;
        }

		public void RemoveTransition(int id)
		{
			Transition t = GetTransition(id);
			if (t != null)
				transitions.Remove(t);
		}

    }
}
