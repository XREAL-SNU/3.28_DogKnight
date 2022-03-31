namespace MainSystem.Managers.UIManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using Managers.GameManager;
    public class AttackButton : MonoBehaviour
    {
        private UnityEvent attackCommend = new UnityEvent();
        /// <summary>
        /// 공격 및 피격 애니메이션 실행 다 될 때까지 AttackButton 비활성화 하는 코드
        /// 이해할 필요 없음 + 건드리지 말 것
        /// </summary>
        private void Start()
        {
            Debug.Log("btnstart");
            MainSystem.Instance.GameManager.SignupATB(this);
            Initialize();
        }
        private void Allocate()
        {
        
        }
        private void Initialize()
        {
        Allocate();

        }
    public void Signup(GameManager gameManager)
        {
            attackCommend.AddListener(gameManager.RoundProgress);
            Debug.Log("btkSiu");
        }
    public void Active()
        {
            Debug.Log("click");
            attackCommend.Invoke();
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