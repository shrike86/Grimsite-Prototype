using Grimsite.Base;
using Grimsite.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Grimsite.Inventory
{
    [CreateAssetMenu(menuName = "Behaviour/Mono Actions/Inventory/Handle Equip By Drag")]
    public class HandleEquipByDrag : Action
    {
        public InventoryPanel inventoryPanel;
        public EquipmentPanel leftEquipmentPanel;
        public EquipmentPanel rightEquipmentPanel;
        public EquipmentPanel bottomEquipmentPanel;
        public TransformVariable draggableItemTransform;

        private ItemSlot draggedSlot;
        private Image draggableItem;

        private Color normalColour = Color.white;
        private Color disabledColour = new Color(1, 1, 1, 0);

        public override void Execute()
        {
            draggableItem = draggableItemTransform.value.GetComponent<Image>();

            InitDragEvents();
        }

        private void InitDragEvents()
        {
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
    }
}