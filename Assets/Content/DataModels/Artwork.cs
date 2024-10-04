using System;
using UnityEngine;

namespace Gallery.Content.DataModels
{
    [Serializable]
    public class Artwork
    {
        public string ID;
        public Vector2 Size;
        public string Title;
        public string Artist;
        public string Description;
        public string ExtendedDescription;
        public byte[] CanvasData;

        public Texture2D Canvas;

        public void LoadTextureFromBytes()
        {
            if (CanvasData != null && CanvasData.Length > 0)
            {
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(CanvasData);
                Canvas = tex;
            }
        }

        public void ConvertTextureToBytes()
        {
            if (Canvas != null)
            {
                CanvasData = Canvas.EncodeToJPG(30);
            }
        }
    }
}