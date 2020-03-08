#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Grimsite.Utilities
{
    [ExecuteInEditMode]
    public class IconMaker : MonoBehaviour
    {
        public RenderTexture render;
        public Camera bakeCamera;
        public string spriteName;
        [Space]
        public bool create;


        private void Update()
        {
            if (create)
            {
                create = false;
                CreateIcon();
            }
        }

        private void CreateIcon()
        {
            if (string.IsNullOrEmpty(spriteName))
            {
                spriteName = "icon";
            }

            string path = GetSaveLocation();
            path += spriteName;

            bakeCamera.targetTexture = render;  


            RenderTexture currentTexture = RenderTexture.active;
            bakeCamera.targetTexture.Release();
            RenderTexture.active = bakeCamera.targetTexture;
            bakeCamera.Render();

            Texture2D imgPng = new Texture2D(bakeCamera.targetTexture.width, bakeCamera.targetTexture.height, TextureFormat.ARGB32, false);

            imgPng.ReadPixels(new Rect(0, 0, bakeCamera.targetTexture.width, bakeCamera.targetTexture.height), 0, 0);
            imgPng.Apply();
            RenderTexture.active = currentTexture;
            byte[] bytesPng = imgPng.EncodeToPNG();
            System.IO.File.WriteAllBytes(path + ".png", bytesPng);
            Debug.Log("Successfully created " + spriteName + " icon");
        }

        private string GetSaveLocation()
        {
            string saveLocation = Application.streamingAssetsPath + "/Icons/";

            if (!Directory.Exists(saveLocation))
            {
                Directory.CreateDirectory(saveLocation);
            }

            return saveLocation;
        }
    }
}

#endif