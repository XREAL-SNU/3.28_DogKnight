namespace MainSystem.Managers.SceneManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class LoadingScreen : MonoBehaviour //Data Field
    {
        private bool isFinishSceneLoading = false;
    }
    public partial class LoadingScreen : MonoBehaviour //Main Field
    {
        private void Update()
        {
            if (isFinishSceneLoading)
            {
                isFinishSceneLoading = false;
                MainSystem.Instance.SceneManager.SceneLoadingDone();
            }
        }
    }
    public partial class LoadingScreen : MonoBehaviour //Property
    {
        public void Initialize()
        {

        }
    }
    public partial class LoadingScreen : MonoBehaviour //Property
    {
        public void FinishSceneLoading()
        {
            isFinishSceneLoading = true;

        }
    }
}
