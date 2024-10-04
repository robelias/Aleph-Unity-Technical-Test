using UnityEngine;

namespace Gallery.Content.Misc
{
    public class LoadSceneOnStart : MonoBehaviour
    {
        [SerializeField]private int BuildSceneIndex;
        [SerializeField]private float Delay;

        private async void Start()
        {
            GlobalData.Data.Database.ClearCache();
            GlobalData.Data.Database.LoadFavoriteArtworks();
            await GlobalData.Data.Database.LoadArtworksFromJsonAsync();

            if(Delay > 0) Invoke(nameof(Load),Delay);
            else Load();
        }

        void Load()
        {
            GlobalData.Data.SceneLoaderController.LoadScene(BuildSceneIndex);
        }
    }
}