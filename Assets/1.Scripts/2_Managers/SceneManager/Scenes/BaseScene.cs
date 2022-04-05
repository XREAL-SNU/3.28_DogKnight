namespace MainSystem.Managers.SceneManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class BaseScene : MonoBehaviour //Data Field
    {

    }
    public partial class BaseScene : MonoBehaviour //Main
    {
        private void Start()
        {
            MainSystem.Instance.SceneManager.SignupBaseScene(this);
        }
        private void Allocate()
        {
            ExtendAllocate();
        }
        public void Initialize()
        {
            Allocate();

            ExtendInitialize();
        }
    }
    public partial class BaseScene : MonoBehaviour //Extend
    {
        protected virtual void ExtendAllocate()
        {

        }
        protected virtual void ExtendInitialize()
        {

        }
    }
    public partial class BaseScene : MonoBehaviour //Property
    {
        public void SceneLoadStandard(string sceneName)
        {
            MainSystem.Instance.SceneManager.SceneLoadStandard(sceneName);
        }
        public void SceneLoadAsync(string sceneName)
        {
            MainSystem.Instance.SceneManager.SceneLoadAsync(sceneName);
        }
        public void WideCallExit()
        {
            MainSystem.Instance.Exit();
        }
    }
}