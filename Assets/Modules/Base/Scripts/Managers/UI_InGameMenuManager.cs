using UnityEngine;

namespace Grimsite.Base
{
    public class UI_InGameMenuManager : MonoBehaviour
    {
        public StateManagerVariable states;

        public PlayerStateManager playerStates;
        public InventoryManager inventoryManager;

        public StateActions toggleInventory;

        private void Start()
        {
            toggleInventory.Execute(states.value);
            inventoryManager = states.value.GetComponent<InventoryManager>();
            playerStates = states.value as PlayerStateManager;
        }

        public void QuitGame()
        {
            playerStates.profile.SaveProfile(playerStates);

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}