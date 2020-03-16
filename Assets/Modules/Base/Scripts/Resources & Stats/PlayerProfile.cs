using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Stats/Player Profile")]
    public class PlayerProfile : ScriptableObject
    {
        public string profileName;
        public string rightHandWeaponId;
        public string leftHandWeaponId;

        public List<string> allItems = new List<string>();

        public PlayerStats stats;
    }
}