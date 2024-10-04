using Gallery.Content.Controller;
using Gallery.Content.DataModels;
using UnityEngine;

namespace Gallery.Content
{
    public class GlobalData: MonoBehaviour
    {
        public static GlobalData Data { get; private set; }
        
        //Controllers
        public ArtworkDatabase Database;
        public Controller_CanvasRendering RenderingController { get; set; }
        public Controller_SceneLoader SceneLoaderController { get; set; }

        void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        private void Awake()
        {
            if (Data != null && Data != this)
            {
                Destroy(this);
                return;
            }

            Data = this;
        }
    }
}