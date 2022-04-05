namespace MainSystem.Managers.UIManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    public partial class UI : MonoBehaviour//Data
    {
        protected UIManager uiManager;
        protected UnityEvent CallUIManager = new UnityEvent();
    }
    public partial class UI : MonoBehaviour//Main
    {
        private void Start()
        {
            MainSystem.Instance.UIManager.SignupUI(this);
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
    public partial class UI : MonoBehaviour//Extand
    {
        protected virtual void ExtendAllocate()
        {

        }
        protected virtual void ExtendInitialize()
        {

        }
    }
    public partial class UI : MonoBehaviour//Signup
    {
        public virtual void SignupUIManager(UIManager uiManagerPra)
        {
            uiManager = uiManagerPra;

        }
    }
}