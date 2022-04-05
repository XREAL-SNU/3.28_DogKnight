namespace MainSystem.Managers.GameManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    public partial class Player : Character//Data
    {
        [SerializeField]public int healItem { get; private set; } = 2;
        [SerializeField]public int damageItem { get; private set; } = 2;
        [SerializeField]private GameObject inventoryFiled;
        [SerializeField]private UnityEvent<int> updateHealQuantity;
        [SerializeField] private UnityEvent<int> updateDamageQuantity;
    }
    public partial class Player : Character//Main
    {
        protected override void Start()
        {
            base.Start();
            MainSystem.Instance.GameManager.SignupPlayer(this);
        }
        protected override void ExtendAllocate()
        {

        }
        protected override void ExtendInitialize()
        {
            updateDamageQuantity.Invoke(damageItem);
            updateHealQuantity.Invoke(healItem);
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
        public void UseDamageItem()
        {
            if (damageItem <= 0)
            {
                return;
            }
            else
            {
                damageItem--;
                updateDamageQuantity.Invoke(damageItem);
                GiveDamage(5);
            }
        }
        public void UseHealItem()
        {
            if (healItem <= 0)
            {
                return;
            }
            else
            {
                healItem--;
                updateHealQuantity.Invoke(healItem);
                Healing(5);
            }
        }
        public void Inventory()
        {
            if (inventoryFiled.activeSelf==false)
            {
                inventoryFiled.SetActive(true);
            }
            else if (inventoryFiled.activeSelf==true)
            {
                inventoryFiled.SetActive(false);
            }
        }
    }


}