using UnityEngine;

namespace Content.UIView
{
    public class UIView_Favorites : MonoBehaviour
    {
        [SerializeField]private GameObject Window;
        [SerializeField]private GameObject Prefab;
        [SerializeField]private Transform Container;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GlobalData.Data.RenderingController.OpenWindow(Window);
                PopulateFavorites();
            }
        }
        private void PopulateFavorites()
        {
            var favorites = GlobalData.Data.Database.Favorites;
            if(Container.childCount == favorites.Count) return;
                
            for (int i = 0; i < Container.childCount; i++)
            {
                Destroy(Container.GetChild(i).gameObject);
            }

            foreach (var favorite in favorites)
            {
                var view = Instantiate(Prefab, Container).GetComponent<UIView_FavoriteArtworkSlot>();
                view.Initialize(favorite.Canvas, favorite.Title, favorite.Artist);
            }
        }
    }
}