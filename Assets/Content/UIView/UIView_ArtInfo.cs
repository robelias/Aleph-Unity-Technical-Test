using System.Text;
using Content.Models;
using TMPro;
using UnityEngine;

namespace Content.UIView
{
    public class UIView_ArtInfo : MonoBehaviour
    {
        [SerializeField] private RectTransform  CanvasPanel;
        [SerializeField] private  RectTransform ArtInfoWidget;
        [SerializeField] private  GameObject ArtInfoPanel;
        
        [SerializeField] private  TextMeshProUGUI  DefaultInfo_Text;
        [SerializeField] private  TextMeshProUGUI  ExtendedInfo_Text;

        private bool _isViewActive;
        private Artwork _currentPiece;

        void Start()
        {
            ArtInfoWidget.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && !ArtInfoPanel.activeInHierarchy)
            {
                var lastArtwork = GlobalData.Data.RenderingController.GetLastInspectedArtwork();
                if(lastArtwork == null) return;
                
                _currentPiece = lastArtwork;
                ShowArtExtendedDetails();
            }
            
            if(!_isViewActive) return;
            
            if (Input.GetKeyDown(KeyCode.E) && !ArtInfoPanel.activeInHierarchy)
            {
                ArtInfoWidget.gameObject.SetActive(false);
                ShowArtExtendedDetails();
                ArtInfoPanel.SetActive(true);
            }
            
            if (Input.GetKeyDown(KeyCode.F) && !ArtInfoPanel.activeInHierarchy && _currentPiece != null)
            {
                GlobalData.Data.Database.AddFavorite(_currentPiece);
            }
        }

        public void ShowArtDetails(Artwork artPiece, Vector3 boundPositionWs)
        {
            if(ArtInfoPanel.activeInHierarchy) return;
            
            _currentPiece = artPiece;
            ArtInfoWidget.gameObject.SetActive(true);
            
            DefaultInfo_Text.text = CreateDetail(artPiece);
            _isViewActive = true;
            
            LocateWidgetPos(boundPositionWs);
        }
        
        public void HideArtDetails()
        {
            ArtInfoWidget.gameObject.SetActive(false);
            _isViewActive = false;
        }
        
        void ShowArtExtendedDetails()
        {
            ArtInfoWidget.gameObject.SetActive(false);
            GlobalData.Data.RenderingController.OpenWindow(ArtInfoPanel);
            ExtendedInfo_Text.text = CreateDetail(_currentPiece, true);
        }
        
        void LocateWidgetPos(Vector3 boundPositionWs)
        {
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(boundPositionWs);
                
            Vector2 sizeDelta = CanvasPanel.sizeDelta;
                
            var canvasPosition = new Vector2(
                ((viewportPosition.x * sizeDelta.x) - (sizeDelta.x * 0.5f)),
                ((viewportPosition.y * sizeDelta.y) - (sizeDelta.y * 0.5f))
            );

            ArtInfoWidget.anchoredPosition = canvasPosition;
        }

        string CreateDetail(Artwork piece, bool isExtended = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<b>Title: <color=green>{piece.Title}</color></b>");
            sb.AppendLine($"<b>Artist: {piece.Artist}</b>");
            sb.AppendLine($"<b>Description:</b> {(isExtended ? piece.ExtendedDescription : piece.Description)}");
            
            return sb.ToString();
        }
    }
}
