using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Content.Controller
{
    public class Controller_SceneLoader : MonoBehaviour
    {
        public delegate void InitializeScene();
        public static event InitializeScene Event_OnInitializeScene;
        
        [SerializeField]private Animator Animator;
        [SerializeField]private TextMeshProUGUI LoadingText;
        [SerializeField]private AnimationClip FadeInAnimation;

        const string FADE_IN_NAME = "FadeIn";
        const string FADE_OUT_NAME = "FadeOut";
        
        void OnEnable()
        {
            GlobalData.Data.SceneLoaderController = this;
            DontDestroyOnLoad(gameObject);
            
        }

        void OnDisable()
        {
            if (GlobalData.Data.SceneLoaderController == this) 
                GlobalData.Data.SceneLoaderController = null;
        }
        
        public void LoadScene(int sceneIndex)
        {
            LoadingText.text = string.Empty;
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }
        
        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            Animator.CrossFade(FADE_IN_NAME, 0.1f);
            yield return new WaitForSeconds(FadeInAnimation.length);

            var asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone) yield return null;

            if (Event_OnInitializeScene != null) Event_OnInitializeScene.Invoke();

            Animator.CrossFade(FADE_OUT_NAME, 0.1f);
            yield return new WaitForSeconds(FadeInAnimation.length);
        }
    }
}