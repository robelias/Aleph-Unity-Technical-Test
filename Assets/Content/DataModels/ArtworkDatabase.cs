using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Gallery.Content.DataModels
{

    [CreateAssetMenu(fileName = "Artwork Database", menuName = "Gallery/Create Artwork Database")]
    public class ArtworkDatabase : ScriptableObject
    {
        public List<Artwork> Artworks = new List<Artwork>();
        public List<Artwork> Favorites = new List<Artwork>();

        public const string MAIN_FILE_PATH = "artworks-db";
        public const string FAVORITES_FILE_PATH = "favorites-db";

        
        public Artwork GetArtwork(string artworkID)
        {
            foreach (var artwork in Artworks)
            {
                if (artwork.ID == artworkID)
                {
                    return artwork;
                }
            }

            Debug.LogWarning($"Artwork with ID: {artworkID} not found");
            return null;
        }
        
        public void AddFavorite(Artwork newPiece)
        {
            var exist = Favorites.Any(a => a.ID == newPiece.ID);
            if (exist) return;

            Favorites.Add(newPiece);
            SaveFavoriteArtworks();
        }


        #region Export


        public void SaveFavoriteArtworks() => SaveDatabaseAsync(Favorites, FAVORITES_FILE_PATH);

        private void SaveDatabaseAsync(List<Artwork> artworkList, string path)
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

            //await using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
            //await writer.WriteAsync(json);
            
            PlayerPrefs.SetString(path, json);
        }

        #endregion

        #region Import

        public void LoadFavoriteArtworks()
        {
            var result = LoadDatabaseAsync(FAVORITES_FILE_PATH);
            if (result == null) return;

            Favorites = result;
        }

        private  List<Artwork> LoadDatabaseAsync(string filePath)
        {
            try
            {
                if (PlayerPrefs.HasKey(filePath))
                {
                    //using var reader = new StreamReader(filePath, Encoding.UTF8);
                    //string json =  await reader.ReadToEndAsync();
                    string json = PlayerPrefs.GetString(filePath);
                    
                    List<Artwork> loadedArtworks = JsonUtility.FromJson<ArtworkListWrapper>(json).Artworks;

                    if (loadedArtworks == null)
                    {
                        Debug.LogError("Error: Failed to deserialize artwork database.");
                        return null;
                    }

                    foreach (var artwork in loadedArtworks)
                        artwork.LoadTextureFromBytes();

                    return loadedArtworks;
                }

                Debug.LogWarning("The artwork database was not found.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error loading database: {ex.Message}");
            }

            return null;
        }

        #endregion

        #region Utils

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
            if(PlayerPrefs.HasKey(FAVORITES_FILE_PATH)) PlayerPrefs.DeleteKey(FAVORITES_FILE_PATH);
            
            Artworks.Clear();
            Favorites.Clear();
        }
        
        #endregion

        #region Download json from Github

        public async Task LoadArtworksFromJsonAsync()
        {
            try
            {
                List<Artwork> loadedArtworks = await DownloadJsonAsync();

                if (loadedArtworks != null)
                {
                    foreach (var artwork in loadedArtworks)
                        artwork.LoadTextureFromBytes();
                    
                    Artworks = loadedArtworks;
                }
                else
                    Debug.LogError("The JSON could not be downloaded or an error occurred.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error downloading JSON: {ex.Message}");
            }
        }
        
        public async Task<List<Artwork>> DownloadJsonAsync()
        {
            string jsonUrl = "https://raw.githubusercontent.com/robelias/gallery-data/refs/heads/Master/artworks-db.json";
            
            using (UnityWebRequest request = UnityWebRequest.Get(jsonUrl))
            {
                var asyncOperation = request.SendWebRequest();

                while (!asyncOperation.isDone) await Task.Yield();

                if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Error downloading JSON: {request.error}");
                    return null;
                }

                string jsonResponse = request.downloadHandler.text;
                ArtworkListWrapper artworkList = JsonUtility.FromJson<ArtworkListWrapper>(jsonResponse);

                if (artworkList != null) return artworkList.Artworks;

                Debug.LogError("Error deserializing the JSON.");
                return null;
            }
        }

        #endregion      

        
    }
}