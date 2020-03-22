using Grimsite.Inventory;
using Grimsite.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Grimsite.Base
{
    public class InventoryManager : MonoBehaviour
    {
        [Header("References")]
        public StateManagerVariable charStates;
        public TransformVariable inventory;

        public InventoryPanel inventoryPanel;
        public EquipmentPanel leftEquipmentPanel;
        public EquipmentPanel rightEquipmentPanel;
        public EquipmentPanel bottomEquipmentPanel;

        [Header("Player Items")]
        public List<Item> inventoryItems;
        public List<Item> equippedItems;

        [Header("Actions")]
        public StateActions weaponModelHandler;
        public StateActions monitorInventoryToggle;
        public StateActions initInventory;
        public Action handleEquipByDrag;
        public Action handleEquipByClick;

        private PlayerStateManager states;

        private void Awake()
        {
            states = charStates.value as PlayerStateManager;
            handleEquipByDrag.Execute();
            handleEquipByClick.Execute();
        }

        private void Start()
        {
            inventoryPanel.Init();
            leftEquipmentPanel.Init();
            rightEquipmentPanel.Init();
            bottomEquipmentPanel.Init();

            weaponModelHandler.Execute(states);
            initInventory.Execute(states);
        }

        private void Update()
        {
            monitorInventoryToggle.Execute(states);
        }
    }
}