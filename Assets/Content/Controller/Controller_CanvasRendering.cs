using Gallery.Content.DataModels;
using Gallery.Content.UIView;
using UnityEngine;

namespace Gallery.Content.Controller
{
    public class Controller_CanvasRendering : MonoBehaviour
    {
        [Header("Display Art")]
        public UIView_ArtInfo ArtInfo;
        public CanvasGroup InteractableUI;
        public GameObject LastInspectedShortcutView;


        [Header("UI View")]
        public GameObject CurrentWindow;
        public GameObject MainUI;
        
        private Artwork _currentArtworkDetails;
        private Artwork _lastArtworkDetails;
        private bool _isWindowOpen;
        
        void OnEnable()
        {
            GlobalData.Data.RenderingController = this;
        }

        void OnDisable()
        {
            if (GlobalData.Data.RenderingController == this) GlobalData.Data.RenderingController = null;
        }
        
        void Update()
        {
            LastInspectedShortcutView.SetActive(_lastArtworkDetails != null);
            
            if (_isWindowOpen && Input.GetKeyDown(KeyCode.Escape)) CloseWindow();
        }
        
        public void SetCurrentInspectedArtwork(Artwork details)
        {
            _lastArtworkDetails = _currentArtworkDetails;
            _currentArtworkDetails = details;
        }

        public Artwork GetLastInspectedArtwork()
        {
            return _lastArtworkDetails;
        }

        public void DisplayUI(bool value)
        {
            InteractableUI.alpha = value ? 1 : 0;
            InteractableUI.interactable = value;
            InteractableUI.blocksRaycasts = value;
        }

        public void OpenWindow(GameObject newWindow)
        {
            if (CurrentWindow != null) CurrentWindow.SetActive(false);
            CurrentWindow = newWindow;
            MainUI.SetActive(false);
            CurrentWindow.SetActive(true);
            _isWindowOpen = true;
        }
        
        public void CloseWindow()
        {
            if (CurrentWindow != null)
            {
                CurrentWindow.SetActive(false);
                MainUI.SetActive(true);
                CurrentWindow = null;
                _isWindowOpen = false;
            }
        }
    }
}