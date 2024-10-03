using Content.Controller;
using Content.Misc;
using Content.Models;
using UnityEngine;

namespace Content.Handler
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
                Debug.LogError("El objeto de la obra de arte no tiene un componente Renderer.");
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
            gameObject.name = $"Art Piece - {Artwork.Title}";
        }
    }
}