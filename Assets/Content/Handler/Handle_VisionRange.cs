using UnityEngine;

namespace Content.Handler
{
    public class Handle_VisionRange : MonoBehaviour
    {
        [SerializeField] private bool ActiveRaycast;
        
        void FixedUpdate()
        {
            //if(!ActiveRaycast) return;

            var mainCamera = Camera.main.transform;
            Ray ray = new Ray(mainCamera.position, mainCamera.forward);
            var controller = GlobalData.Data.RenderingController;
            
            if (Physics.Raycast(ray, out var hit, 5f))
            {
                if (!hit.collider.CompareTag("Art"))
                {
                    controller.ArtInfo.HideArtDetails();;
                    return;
                }
                
                Handler_ArtInfo artPiece = hit.transform.parent.GetComponent<Handler_ArtInfo>();
                
                if (artPiece != null)
                {
                    controller.ArtInfo.ShowArtDetails(artPiece.Artwork, artPiece.GetInfoPosition_WorldSpace());
                    controller.SetCurrentInspectedArtwork(artPiece.Artwork);
                }
                else
                {
                    controller.ArtInfo.HideArtDetails();
                }
            }
            else
            {
                controller.ArtInfo.HideArtDetails();
            }
            
        }

        public void ActivateFieldOfView(bool value)
        {
            ActiveRaycast = value;
            //if(!value) GlobalData.Data.RenderingController.ArtInfo.HideArtDetails();
        }
    }
}
