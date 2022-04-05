namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class MainSystemTrigger : MonoBehaviour //Data
    {
    }
    public partial class MainSystemTrigger : MonoBehaviour //Main
    {
        private void Awake()
        {
            MainSystem.Instance.MainSystemStart(ReceiveFinishMainSystemStart);
        }
    }
    public partial class MainSystemTrigger : MonoBehaviour //Prop
    {
        public void ReceiveFinishMainSystemStart()
        {
            MainSystem.Instance.SceneManager.SceneLoadStandard("LogoScene");
        }
    }
}