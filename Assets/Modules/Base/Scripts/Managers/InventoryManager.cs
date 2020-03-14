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
        public StateManagerVariable states;
        public TransformVariable inventory;

        public InventoryPanel inventoryPanel;
        public EquipmentPanel leftEquipmentPanel;
        public EquipmentPanel rightEquipmentPanel;
        public EquipmentPanel bottomEquipmentPanel;

        [Header("Player Items")]
        public List<Item> playerInventoryItems;
        public List<Item> playerEquippedItems;

        [Header("Actions")]
        public StateActions weaponModelHandler;
        public StateActions monitorInventoryToggle;
        public Action handleEquipByDrag;
        public Action handleEquipByClick;


        private void Awake()
        {
            handleEquipByDrag.Execute();
            handleEquipByClick.Execute();
        }

        private void Start()
        {
            inventoryPanel.Init();
            leftEquipmentPanel.Init();
            rightEquipmentPanel.Init();
            bottomEquipmentPanel.Init();

            weaponModelHandler.Execute(states.value);
        }

        private void Update()
        {
            monitorInventoryToggle.Execute(states.value);
        }
    }
}