using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Items
{
    public abstract class Item : ScriptableObject
    {
        public string uiName;
        public string id;
        public Sprite icon;
        public EquipmentType equipmentType;
    }

    public enum EquipmentType
    { 
        Helmet,
        Shoulders,
        Chest,
        Waist,
        Bracers,
        Gloves,
        Legs,
        Boots,
        Weapon,
        Shield
    }
}