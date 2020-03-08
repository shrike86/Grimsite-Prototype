using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    [CreateAssetMenu(menuName = "Variables/GameObject Variable")]
    public class GameObjectVariable : ScriptableObject
    {
        public GameObject value;

        public void Set(GameObject go)
        {
            this.value = go;
        }
    }
}