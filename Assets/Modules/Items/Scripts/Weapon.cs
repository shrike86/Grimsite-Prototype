using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Items
{
    [CreateAssetMenu(menuName = "Inventory/Items/Weapon")]
    public class Weapon : EquippableItem
    {
        public bool isLeft;
        public bool isTwoHanded;
        public string attackAnimName;
        public int comboIndex;
    }
}