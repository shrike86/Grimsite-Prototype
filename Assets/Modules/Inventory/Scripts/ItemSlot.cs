using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Grimsite.Items;
using UnityEngine.InputSystem;

namespace Grimsite.Inventory
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
    {
        public Image image;

        private Item item;

        public Item Item
        {
            get { return item; }
            set
            {
                item = value;

                if (OnItemChanged != null)
                    OnItemChanged(item, this);

                if (item == null)
                    image.color = disabledColour;
                else
                {
                    image.sprite = item.icon;
                    image.color = normalColour;
                }
            }
        }

        public event Action<ItemSlot> OnRightClickEvent;
        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;
        public event Action<ItemSlot> OnDropEvent;
        public event Action<Item, ItemSlot> OnItemChanged;

        private Color normalColour = Color.white;
        private Color disabledColour = new Color(1, 1, 1, 0);

        private void Awake()
        {
            if (image == null)
                image = GetComponent<Image>();
        }

        private void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClickEvent?.Invoke(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragEvent?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnDropEvent?.Invoke(this);
        }

        public virtual bool CanReceiveItem(Item item)
        {
            return true;
        }
    }
}
