namespace MainSystem.Managers.UIManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    public class AttackButton : UI
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
            CallUIManager.AddListener(uiManager.CallRoundProgress);

        }
        public void OnClick()
        {
            CallUIManager.Invoke();
            StartCoroutine(ButtonDisableCoroutine());
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }
        IEnumerator ButtonDisableCoroutine()
        {
            Button attackButton = this.GetComponent<Button>();
            attackButton.interactable = false;
            yield return new WaitForSeconds(2.5f);
            attackButton.interactable = true;
        }
    }
}