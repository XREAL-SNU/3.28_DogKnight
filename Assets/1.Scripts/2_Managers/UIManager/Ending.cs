namespace MainSystem.Managers.UIManager
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    public partial class Ending : UI//Data
    {
        public Text objectName;
        private UnityEvent<GameObject> signupEnding = new UnityEvent<GameObject>();
    }
    public partial class Ending : UI//Main
    {
        protected override void ExtendAllocate()
        {

        }
        protected override void ExtendInitialize()
        {

        }
    }
    public partial class Ending : UI//Prop
    {
        public void ReceiveObjectName(int mynum)
        {
            objectName.text = "" + mynum + "is Winner";
        }
        public override void SignupUIManager(UIManager uiManagerPra)
        {
            base.SignupUIManager(uiManagerPra);
            MainSystem.Instance.UIManager.SignupEnding(gameObject);
        }
    }
    public partial class Ending : UI//
    {

    }
}