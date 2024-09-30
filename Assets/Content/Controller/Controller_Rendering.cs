using Content.Models;
using Content.UIView;
using UnityEngine;

namespace Content.Controller
{
    public class Controller_Rendering : MonoBehaviour
    {
        [Header("Display Art")]
        public UIView_ArtInfo ArtInfo;
        private ArtPiece _currentArtworkDetails;
        private ArtPiece _lastArtworkDetails;
        
        void OnEnable()
        {
            GlobalData.Data.RenderingController = this;
        }

        void OnDisable()
        {
            if (GlobalData.Data.RenderingController == this) GlobalData.Data.RenderingController = null;
        }
        
        public void SetCurrentInspectedArtwork(ArtPiece details)
        {
            _lastArtworkDetails = _currentArtworkDetails;
            _currentArtworkDetails = details;
        }

        public ArtPiece GetLastInspectedArtwork()
        {
            return _lastArtworkDetails;
        }
    }
}