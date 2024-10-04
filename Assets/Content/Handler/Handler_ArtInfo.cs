using Gallery.Content.Builders;
using Gallery.Content.Controller;
using Gallery.Content.DataModels;
using UnityEngine;

namespace Gallery.Content.Handler
{
    public class Handler_ArtInfo : MonoBehaviour
    {
        public string ArtworkID;
        public Artwork Artwork;
        
        [SerializeField] Transform InfoTarget;
        
        [Space]
        [Space]
        [SerializeField] Material CanvasMaterial;
        [SerializeField] Material FrameMaterial;
        [SerializeField] GameObject ArtParent;
        
        private Transform _artCanvas;
        private readonly float _baseHeight = 1.25f;

        void OnEnable()
        {
            Controller_SceneLoader.Event_OnInitializeScene += LoadData;
        }

        void OnDisable()
        {
            Controller_SceneLoader.Event_OnInitializeScene -= LoadData;
        }

        private void LoadData()
        {
            Artwork = GlobalData.Data.Database.GetArtwork(ArtworkID);
            InitializeArtwork();
        }

        private void InitializeArtwork()
        {
            Builder_FrameGenerator.GenerateFrameAndCanvas(Artwork.Size.x, Artwork.Size.y, ArtParent, FrameMaterial, InfoTarget, out Transform artCanvas);
            
            _artCanvas = artCanvas;
            var mat = Instantiate(CanvasMaterial);
            mat.mainTexture = Artwork.Canvas;
            _artCanvas.GetComponent<Renderer>().material = mat;
            
            AlignArtwork();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            other.GetComponent<Handle_VisionRange>().ActivateFieldOfView(true);
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            other.GetComponent<Handle_VisionRange>().ActivateFieldOfView(false);
        }

        public Vector3 GetInfoPosition_WorldSpace() => InfoTarget.position;

        private void AlignArtwork()
        {
            var artworkRenderer = _artCanvas.GetComponent<Renderer>();
            if (artworkRenderer == null)
            {
                Debug.LogError("The object does not have a Renderer component.");
                return;
            }

            var artworkSize = artworkRenderer.bounds.size;
            var totalHeight = artworkSize.y;
            var currentPosition = _artCanvas.transform.position;

            var newYPosition = _baseHeight + (totalHeight / 2);
            ArtParent.transform.position = new Vector3(currentPosition.x, newYPosition, currentPosition.z);
        }
        
        
        [ContextMenu("Rename GameObject by Selected Art")]
        private void RenameGameObject()
        {
            var database = Resources.Load<ArtworkDatabase>("Artwork-Initialization");
            
            var artworkTemp = database.GetArtwork(ArtworkID);
            gameObject.name = $"Art Piece - {artworkTemp.Title}";
        }
    }
}