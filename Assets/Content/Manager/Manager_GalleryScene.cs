using Gallery.Content.Controller;
using UnityEngine;

namespace Gallery.Content.Manager
{
    public class Manager_GalleryScene : MonoBehaviour
    {
        void OnEnable()
        {
            Controller_SceneLoader.Event_OnInitializeScene += LoadData;
            GlobalData.Data.RenderingController.DisplayUI(true);
        }

        void OnDisable()
        {
            Controller_SceneLoader.Event_OnInitializeScene -= LoadData;
        }

        private void LoadData()
        {
            GlobalData.Data.RenderingController.DisplayUI(true);
        }
        
        
    }
}