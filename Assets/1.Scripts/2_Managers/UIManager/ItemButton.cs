namespace MainSystem.Managers.UIManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Managers.GameManager;
    public class ItemButton : UI
    {
        protected override void ExtendAllocate()
        {

        }
        protected override void ExtendInitialize()
        {

        }
        public override void SignupUIManager(UIManager uiManagerPra)
        {
            base.SignupUIManager(uiManagerPra);
            CallUIManager.AddListener(uiManager.CallInventory);

        }
        public void OnClick()
        {
            CallUIManager.Invoke();
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}