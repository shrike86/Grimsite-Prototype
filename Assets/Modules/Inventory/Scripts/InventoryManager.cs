using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grimsite.Base;
using Grimsite.Items;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Grimsite.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public TransformVariable inventory;
        public TransformVariable draggableItemTransform;

        public InventoryPanel inventoryPanel;
        public EquipmentPanel leftEquipmentPanel;
        public EquipmentPanel rightEquipmentPanel;
        public EquipmentPanel bottomEquipmentPanel;

        [Header("Player Items")]
        public List<Item> playerInventoryItems;
        public List<Item> playerEquippedItems;

        public PlayerStateManager states;

        private CanvasGroup invCanvasGroup;
        private float inputTimer;
        private ItemSlot draggedSlot;
        private Image draggableItem;

        private Color normalColour = Color.white;
        private Color disabledColour = new Color(1, 1, 1, 0);

        private void Awake()
        {
            inventoryPanel.OnRightClickEvent += EquipFromInventory;
            leftEquipmentPanel.OnRightClickEvent += UnequipFromEquipmentPanel;
            rightEquipmentPanel.OnRightClickEvent += UnequipFromEquipmentPanel;
            bottomEquipmentPanel.OnRightClickEvent += UnequipFromEquipmentPanel;

            inventoryPanel.OnBeginDragEvent += BeginDrag;
            leftEquipmentPanel.OnBeginDragEvent += BeginDrag;
            rightEquipmentPanel.OnBeginDragEvent += BeginDrag;
            bottomEquipmentPanel.OnBeginDragEvent += BeginDrag;

            inventoryPanel.OnEndDragEvent += EndDrag;
            leftEquipmentPanel.OnEndDragEvent += EndDrag;
            rightEquipmentPanel.OnEndDragEvent += EndDrag;
            bottomEquipmentPanel.OnEndDragEvent += EndDrag;

            inventoryPanel.OnDragEvent += Drag;
            leftEquipmentPanel.OnDragEvent += Drag;
            rightEquipmentPanel.OnDragEvent += Drag;
            bottomEquipmentPanel.OnDragEvent += Drag;

            inventoryPanel.OnDropEvent += Drop;
            leftEquipmentPanel.OnDropEvent += Drop;
            rightEquipmentPanel.OnDropEvent += Drop;
            bottomEquipmentPanel.OnDropEvent += Drop;

            inventoryPanel.Init();
            leftEquipmentPanel.Init();
            rightEquipmentPanel.Init();
            bottomEquipmentPanel.Init();
        }
        private void Start()
        {
            states = GetComponent<PlayerStateManager>();
            invCanvasGroup = inventory.value.GetComponent<CanvasGroup>();
            draggableItem = draggableItemTransform.value.GetComponent<Image>();
        }

        private void Drop(ItemSlot dropItemSlot)
        {
            if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
            {
                Item draggedItem = draggedSlot.Item;
                draggedSlot.Item = dropItemSlot.Item;
                dropItemSlot.Item = draggedItem;
            }
        }

        private void Drag(ItemSlot itemSlot)
        {
            if (draggableItem.color == disabledColour)
                return;

            draggableItem.transform.position = Mouse.current.position.ReadValue();
        }

        private void EndDrag(ItemSlot itemSlot)
        {
            draggedSlot = null;
            draggableItem.color = disabledColour;
        }

        private void BeginDrag(ItemSlot itemSlot)
        {
            if (itemSlot != null)
            {
                draggedSlot = itemSlot;
                draggableItem.color = normalColour;
                draggableItem.sprite = itemSlot.Item.icon;
                draggableItem.transform.position = Mouse.current.position.ReadValue();
            }
        }


        private void Update()
        {
            ToggleInventoryOnInput();
        }

        private void EquipFromInventory(ItemSlot itemSlot)
        {
            EquippableItem equippableItem = itemSlot.Item as EquippableItem;

            if (equippableItem != null)
            {
                Equip(equippableItem);
            }
        }

        private void UnequipFromEquipmentPanel(ItemSlot itemSlot)
        {
            EquippableItem equippableItem = itemSlot.Item as EquippableItem;

            if (equippableItem != null)
            {
                Unequip(equippableItem);
            }
        }

        private void Equip(EquippableItem item)
        {
            if (inventoryPanel.RemoveItem(item))
            {
                EquippableItem previousItem;
                if (leftEquipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventoryPanel.AddItem(previousItem);
                    }
                }
                else if (rightEquipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventoryPanel.AddItem(previousItem);
                    }
                }
                else if (bottomEquipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventoryPanel.AddItem(previousItem);
                    }
                }
                else
                {
                    inventoryPanel.AddItem(item);
                }
            }
        }

        private void Unequip(EquippableItem item)
        {
            if (!inventoryPanel.IsFull() && leftEquipmentPanel.RemoveItem(item))
            {
                inventoryPanel.AddItem(item);
            }
            else if (!inventoryPanel.IsFull() && rightEquipmentPanel.RemoveItem(item))
            {
                inventoryPanel.AddItem(item);
            }
            else if (!inventoryPanel.IsFull() && bottomEquipmentPanel.RemoveItem(item))
            {
                inventoryPanel.AddItem(item);
            }
        }

        private void ToggleInventoryOnInput()
        {
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
    }
}