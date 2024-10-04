using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gallery.Content.UIView
{
    public class UIView_GalleriesSelection: MonoBehaviour
    {
        [SerializeField] private  GameObject MainPanel;
        private bool _isWindowOpen;
        
        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GlobalData.Data.RenderingController.OpenWindow(MainPanel);
                _isWindowOpen = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Escape)) _isWindowOpen = false;

            if (_isWindowOpen && Input.GetKeyDown(KeyCode.Alpha1) && SceneManager.GetActiveScene().buildIndex != 1)
            {
                GlobalData.Data.RenderingController.CloseWindow();
                GlobalData.Data.SceneLoaderController.LoadScene(1);
            }
            
            if (_isWindowOpen && Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().buildIndex != 2)
            {
                GlobalData.Data.RenderingController.CloseWindow();
                GlobalData.Data.SceneLoaderController.LoadScene(2);
            }
        }
    }
}