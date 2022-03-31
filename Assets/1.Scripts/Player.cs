namespace MainSystem.Managers.GameManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public partial class Player : Character//Main
    {
        protected override void ExtendAllocate()
        {
            MainSystem.Instance.GameManager.SignupPlayer(this);

        }
        protected override void ExtendInitialize()
        {

        }
    }
    public partial class Player : Character//Prop
    {
        protected override void ReceiveAttackcommand()
        {
            if (Random.Range(1, 100) < 100)
            {
                Debug.Log($"Object Number{myNum} Special Attack!");
                SpecialAttack();
            }
            else
            {
                NormalAttack();
            }
        }
        private void SpecialAttack()
        {
            GiveDamage(myDamage + 10);
            SpecialAttackMotion();
        }
    }


}