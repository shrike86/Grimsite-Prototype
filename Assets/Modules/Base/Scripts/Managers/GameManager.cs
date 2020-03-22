using UnityEngine;

namespace Grimsite.Base
{
    public static class GameManager
    {
        static ResourcesManager resourcesManager;

        public static ResourcesManager GetResourcesManager()
        {
            if (resourcesManager == null)
            {
                resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
                resourcesManager.Init();
            }

            return resourcesManager;
        }
    }
}