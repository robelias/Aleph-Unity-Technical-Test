using UnityEngine;

namespace Content.Models
{
    [CreateAssetMenu(fileName = "Artwork -", menuName = "Gallery/Create Artwork")]
    public class ArtPiece : ScriptableObject
    {
        public string Title;
        public string Artist;
        public string Description;
        public string ExtendedDescription;
    }
}
