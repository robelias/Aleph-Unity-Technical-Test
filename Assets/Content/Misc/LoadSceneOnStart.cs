using UnityEngine;

namespace Content.Misc
{
    public class LoadSceneOnStart : MonoBehaviour
    {
        [SerializeField]private int BuildSceneIndex;
        [SerializeField]private float Delay;
    
        private async void Start()
        {
            GlobalData.Data.Database.ClearCache();
            await GlobalData.Data.Database.LoadAllArtworks();
            await GlobalData.Data.Database.LoadFavoriteArtworks();
            
            if(Delay>0)
                Invoke(nameof(Load),Delay);
            else
                Load();
        }

        void Load()
        {
            GlobalData.Data.SceneLoaderController.LoadScene(BuildSceneIndex);
        }
    }
}