namespace MainSystem.Managers.UIManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Managers.GameManager;
    public partial class UIManager : MonoBehaviour//Data
    {
        private List<UI> uiList = new List<UI>();
        private GameObject ending;
        private UnityEvent<int> sendWinner = new UnityEvent<int>();
    }
    public partial class UIManager : MonoBehaviour//Main
    {
        private void Allocate()
        {
            
        }
        public void Initialize()
        {
            Allocate();

        }
    }
    public partial class UIManager : MonoBehaviour//Signup
    {
        public void SignupUI(UI ui)
        {
            uiList.Add(ui);
            ui.Initialize();
            ui.SignupUIManager(this);
        }
        public void SignupEnding(GameObject gameObject)
        {
            ending = gameObject;
        }
    }
    public partial class UIManager : MonoBehaviour//:Prop
    {
        public void CallRoundProgress()
        {
            MainSystem.Instance.GameManager.RoundProgress();
        }
        public void CallInventory()
        {
            MainSystem.Instance.GameManager.CallInventory();
        }
        public void ActiveEnding(int winnersName)
        {
            ending.SetActive(true);
        }
    }
}