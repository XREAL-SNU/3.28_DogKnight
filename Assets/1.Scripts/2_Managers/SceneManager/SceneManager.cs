namespace MainSystem.Managers.SceneManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class SceneManager : MonoBehaviour //Data Field
    {
        private bool isSceneLoading = false;
        private LoadingScreen loadingScreen = null;
        public BaseScene activeScene { get; private set; }
        private AsyncOperation asyncOperation = default;
        private ThreadPriority threadPriority = ThreadPriority.Normal;
    }
    public partial class SceneManager : MonoBehaviour //Main
    {
        private void Allocate()
        {
            Debug.Log("SM Allocate");
        }
        public void Initialize()
        {
            Allocate();
            Debug.Log("SM Init");
        }
    }
    public partial class SceneManager : MonoBehaviour //Sign up
    {
        public void SignupBaseScene(BaseScene baseScene)
        {
            SceneLoadingDone();



            activeScene = baseScene;
            activeScene.Initialize();
        }
    }
    public partial class SceneManager : MonoBehaviour //Property
    {
        public void SceneLoadStandard(string sceneName)
        {
            if (isSceneLoading == false)
            {
                isSceneLoading = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
        public void SceneLoadAsync(string sceneName)
        {
            if (isSceneLoading == false)
            {
                isSceneLoading = true;
                ActiveSceneDestroy();
                LoadingScreenSetup();
                StartCoroutine(SceneLoadingAsyncEnumerator(sceneName));
            }
        }
        private void ActiveSceneDestroy()
        {
            if (activeScene != null)
            {
                Destroy(activeScene.gameObject);
            }
        }
        private void LoadingScreenSetup()
        {
            loadingScreen = Instantiate<GameObject>(Resources.Load<GameObject>("LoadingScreen")).GetComponent<LoadingScreen>();
            loadingScreen.Initialize();
        }
        public void SceneLoadingDone()
        {
            isSceneLoading = false;

            if (asyncOperation != null)
                asyncOperation.allowSceneActivation = true;
        }
        private IEnumerator SceneLoadingAsyncEnumerator(string sceneName)
        {
            StartOperation(sceneName);

            while (CheckAsyncLoadingDone() == false)
            {
                yield return null;
            }

            loadingScreen.FinishSceneLoading();
            yield return null;
        }
        private void StartOperation(string sceneName)
        {
            Application.backgroundLoadingPriority = threadPriority;
            asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
        }
        private bool CheckAsyncLoadingDone()
        {
            return asyncOperation.progress >= 0.9f;
        }
    }
    
}