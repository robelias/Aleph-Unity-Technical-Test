using System;
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
        private ArtPiece _currentPiece;
        
        void Start()
        {
            ArtInfoWidget.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && !ArtInfoPanel.activeInHierarchy)
            {
                var last_artwork = GlobalData.Data.RenderingController.GetLastInspectedArtwork();
                if(last_artwork == null) return;
                
                _currentPiece = last_artwork;
                ShowArtExtendedDetails();
            }
            
            if (Input.GetKeyDown(KeyCode.Escape) && ArtInfoPanel.activeInHierarchy)
            {
                ArtInfoPanel.SetActive(false);
                if(_isViewActive) ArtInfoWidget.gameObject.SetActive(true);
            }
            
            if(!_isViewActive) return;
            
            if (Input.GetKeyDown(KeyCode.E) && !ArtInfoPanel.activeInHierarchy)
            {
                ArtInfoWidget.gameObject.SetActive(false);
                ShowArtExtendedDetails();
                ArtInfoPanel.SetActive(true);
            }
        }

        void ShowArtExtendedDetails()
        {
            ArtInfoWidget.gameObject.SetActive(false);
            ArtInfoPanel.SetActive(true);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Title: <b><color=orange>{_currentPiece.Title}</color></b>");
            sb.AppendLine($"Artist: <b>{_currentPiece.Artist}</b>");
            sb.AppendLine($"Description: {_currentPiece.ExtendedDescription}");
            
            ExtendedInfo_Text.text = sb.ToString();
        }
        
        public void ShowArtDetails(ArtPiece artPiece, Vector3 boundPositionWs)
        {
            if(ArtInfoPanel.activeInHierarchy) return;
            
            _currentPiece = artPiece;
            ArtInfoWidget.gameObject.SetActive(true);
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<b><color=orange>Title: {artPiece.Title}</color></b>");
            sb.AppendLine($"Artist: <b>{artPiece.Artist}</b>");
            sb.AppendLine($"Description: {artPiece.Description}");
            
            DefaultInfo_Text.text = sb.ToString();
            _isViewActive = true;
            
            LocateWidgetPos(boundPositionWs);
        }
        
        public void HideArtDetails()
        {
            ArtInfoWidget.gameObject.SetActive(false);
            _isViewActive = false;
        }
        
        void LocateWidgetPos(Vector3 boundPositionWs)
        {
            Vector2 viewport_position = Camera.main.WorldToViewportPoint(boundPositionWs);
                
            Vector2 size_delta = CanvasPanel.sizeDelta;
                
            var canvas_position = new Vector2(
                ((viewport_position.x * size_delta.x) - (size_delta.x * 0.5f)),
                ((viewport_position.y * size_delta.y) - (size_delta.y * 0.5f))
            );

            ArtInfoWidget.anchoredPosition = canvas_position;
        }
    }
}
