using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.UIView
{
    public class UIView_FavoriteArtworkSlot : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI ArtworkName;
        [SerializeField]private TextMeshProUGUI Artist;
        [SerializeField]private RawImage Image;

        public void Initialize(Texture2D artwork, string artworkName, string artist)
        {
            ArtworkName.text = artworkName;
            Artist.text = artist;
            Image.texture = artwork;
        }
    }
}