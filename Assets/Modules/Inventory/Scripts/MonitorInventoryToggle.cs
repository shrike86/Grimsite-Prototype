using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Inventory/Monitor Inventory Toggle")]
    public class MonitorInventoryToggle : StateActions
    {
        public TransformVariable inventoryTransform;

        private PlayerStateManager states;
        private float inputTimer;
        private CanvasGroup invCanvasGroup;

        public override void Execute(CharacterStateManager charStates)
        {
            if (states == null)
                states = charStates as PlayerStateManager;

            if (invCanvasGroup == null)
                Init();

            if (states.inputStates.isPressed_I)
            {
                inputTimer += states.delta;
            }
            else
            {
                if (inputTimer > 0)
                {
                    float currentAlpha = invCanvasGroup.alpha;

                    if (currentAlpha == 0)
                    {
                        invCanvasGroup.alpha = 1;
                        invCanvasGroup.interactable = true;
                        invCanvasGroup.blocksRaycasts = true;
                        states.isUserInterfaceActive = true;
                    }
                    else
                    {
                        invCanvasGroup.alpha = 0;
                        invCanvasGroup.interactable = false;
                        invCanvasGroup.blocksRaycasts = false;
                        states.isUserInterfaceActive = false;
                    }

                    inputTimer = 0;
                }
            }
        }

        public void Init()
        {
            invCanvasGroup = inventoryTransform.value.GetComponent<CanvasGroup>();
        }
    }
}