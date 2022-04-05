    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class CheckQuantity : MonoBehaviour//Data
    {
        [SerializeField] private Text quantity;
        [SerializeField] private UseItem useItem;
}
    public partial class CheckQuantity : MonoBehaviour//Main
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();

        }
    }
    public partial class CheckQuantity : MonoBehaviour//Prop
    {
        public void UpdateQuantity(int quantitypra)
        {
            quantity.text = "" + quantitypra;
        if (quantitypra <= 0)
        {
            useItem.Disable();
        }
        }
    }