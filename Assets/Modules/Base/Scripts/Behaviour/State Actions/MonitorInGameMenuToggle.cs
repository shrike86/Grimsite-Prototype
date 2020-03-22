using Grimsite.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/State Actions/Base/Monitor In Game Menu Toggle")]
public class MonitorInGameMenuToggle : StateActions
{
    public TransformVariable inGameMenuTransform;

    private PlayerControls input;
    private PlayerStateManager states;
    private CanvasGroup canvasGroup;

    public override void Execute(CharacterStateManager charStates)
    {
        states = charStates as PlayerStateManager;

        Init();
    }

    public void Init()
    {
        canvasGroup = inGameMenuTransform.value.GetComponent<CanvasGroup>();
        input = new PlayerControls();
        input.Enable();
        input.Player.EscapeKey.performed += ToggleInGameMenu;
    }

    private void ToggleInGameMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        float currentAlpha = canvasGroup.alpha;

        if (currentAlpha == 0)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            states.isUserInterfaceActive = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            states.isUserInterfaceActive = false;
        }

    }
}
