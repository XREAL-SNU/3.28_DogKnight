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
        /// ���� �� �ǰ� �ִϸ��̼� ���� �� �� ������ AttackButton ��Ȱ��ȭ �ϴ� �ڵ�
        /// ������ �ʿ� ���� + �ǵ帮�� �� ��
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