using Content.Models;
using UnityEngine;

namespace Content.Handler
{
    public class Handle_VisionRange : MonoBehaviour
    {
        [SerializeField] private bool ActiveRaycast;
        private ArtPiece _lastInspectedArtPiece;
        
        void FixedUpdate()
        {
            if(!ActiveRaycast) return;

            var main_camera = Camera.main.transform;
            
            Ray ray = new Ray(main_camera.position, main_camera.forward);

            if (Physics.Raycast(ray, out var hit, 5f))
            {
                if(!hit.collider.CompareTag("Art")) return;
                
                Handler_ArtInfo artPiece = hit.transform.parent.GetComponent<Handler_ArtInfo>();
                
                if (artPiece != null)
                {
                    GlobalData.Data.RenderingController.ArtInfo.ShowArtDetails(artPiece.Art, artPiece.GetInfoPosition_WorldSpace());
                    GlobalData.Data.RenderingController.SetCurrentInspectedArtwork(artPiece.Art);
                }
                else
                {
                    GlobalData.Data.RenderingController.ArtInfo.HideArtDetails();
                }
            }
            else
            {
                GlobalData.Data.RenderingController.ArtInfo.HideArtDetails();
            }
            
        }

        public void ActivateFieldOfView(bool value)
        {
            ActiveRaycast = value;
        }
    }
}
