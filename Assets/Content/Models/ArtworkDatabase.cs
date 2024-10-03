using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Content.Models
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
                CanvasData = Canvas.EncodeToJPG();
            }
        }
    }

    [Serializable]
    public class ArtworkListWrapper
    {
        public List<Artwork> Artworks;
    }

    [CreateAssetMenu(fileName = "Artwork Database", menuName = "Gallery/Create Artwork Database")]
    public class ArtworkDatabase : ScriptableObject
    {
        public List<Artwork> Artworks = new List<Artwork>();
        public List<Artwork> Favorites = new List<Artwork>();

        private string _mainFilePath;
        private string _favoritesFilePath;

        public Artwork GetArtwork(string artworkID)
        {
            foreach (var artwork in Artworks)
            {
                if (artwork.ID == artworkID)
                {
                    return artwork;
                }
            }

            Debug.LogWarning($"No se encontró la obra de arte con el ID: {artworkID}");
            return null;
        }

        void OnEnable()
        {
            _mainFilePath = Path.Combine(Application.persistentDataPath, "artworks-db.json");
            _favoritesFilePath = Path.Combine(Application.persistentDataPath, "favorites-db.json");
        }


        #region Export

        public async void SaveAllArtworks() => await SaveDatabaseAsync(Artworks, _mainFilePath);

        public async void SaveFavoriteArtworks() => await SaveDatabaseAsync(Favorites, _favoritesFilePath);

        private async Task SaveDatabaseAsync(List<Artwork> artworkList, string path)
        {
            foreach (var artwork in artworkList)
            {
                artwork.ConvertTextureToBytes();
            }

            ArtworkListWrapper wrapper = new ArtworkListWrapper
            {
                Artworks = artworkList
            };

            string json = JsonUtility.ToJson(wrapper, true);

            await WriteToFileAsync(path, json);
        }

        private async Task WriteToFileAsync(string path, string content)
        {
            await using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
            await writer.WriteAsync(content);
        }

        #endregion

        #region Import

        public async Task LoadAllArtworks()
        {
            var result = await LoadDatabaseAsync(_mainFilePath);
            if (result == null) return;

            Artworks = result;
        }

        public async Task LoadFavoriteArtworks()
        {
            var result = await LoadDatabaseAsync(_favoritesFilePath);
            if (result == null) return;

            Favorites = result;
        }

        private async Task<List<Artwork>> LoadDatabaseAsync(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = await ReadFromFileAsync(filePath);
                    List<Artwork> loadedArtworks = JsonUtility.FromJson<ArtworkListWrapper>(json).Artworks;

                    if (loadedArtworks == null)
                    {
                        Debug.LogError("Error: No se pudo deserializar la base de datos de obras de arte.");
                        return null;
                    }

                    foreach (var artwork in loadedArtworks)
                    {
                        try
                        {
                            artwork.LoadTextureFromBytes();
                        }
                        catch (Exception texEx)
                        {
                            Debug.LogError($"Error al cargar la textura para la obra con ID {artwork.ID}: {texEx.Message}");
                            // Puedes decidir continuar o detener el proceso, según el caso
                            // En este ejemplo, continuamos con las siguientes obras
                        }
                    }

                    return loadedArtworks;
                }
                else
                {
                    Debug.LogWarning("No se encontró la base de datos de obras de arte.");
                }
            }
            catch (IOException ioEx)
            {
                Debug.LogError($"Error de E/S al leer la base de datos: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error inesperado al cargar la base de datos: {ex.Message}");
            }

            return null;
        }

        private async Task<string> ReadFromFileAsync(string path)
        {
            using var reader = new StreamReader(path, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }

        #endregion

        #region Utils

        [ContextMenu("Save initial data")]
        public void InitializeData()
        {
            Init();
            async void Init() => await SaveDatabaseAsync(Artworks, _mainFilePath);
        }

        [ContextMenu("Generate Guid")]
        public void GenerateGuid()
        {
            foreach (var artwork in Artworks)
            {
                Guid guid = Guid.NewGuid();
                artwork.ID = guid.ToString();
            }
        }
        
        public void ClearCache()
        {
            Artworks.Clear();
            Favorites.Clear();
        }
        
        #endregion

        public void AddFavorite(Artwork newPiece)
        {
            var exist = Favorites.Any(a => a.ID == newPiece.ID);
            if (exist) return;

            Favorites.Add(newPiece);
            SaveFavoriteArtworks();
        }

        
    }
}