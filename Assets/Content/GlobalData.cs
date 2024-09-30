using Content.Controller;
using UnityEngine;

namespace Content
{
    public class GlobalData: MonoBehaviour
    {
        public static GlobalData Data { get; private set; }
        
        //Controllers
        public Controller_Rendering RenderingController { get; set; }
        
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