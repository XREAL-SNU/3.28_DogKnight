namespace MainSystem.Managers.SceneManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class LogoScene : BaseScene//Data
    {
        private bool logoFinish = false;
        private void Update()
        {
            if (logoFinish)
                SceneLoadAsync("MainScene");
        }
    }
    public partial class LogoScene : BaseScene//Main
    {
        protected override void ExtendAllocate()
        {
            logoFinish = true;
            Debug.Log("LogoSceneAllocate");
        }
        protected override void ExtendInitialize()
        {

        }
    }
    public partial class LogoScene : BaseScene
    {

    }
}
