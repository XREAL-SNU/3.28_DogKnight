namespace MainSystem.Managers.GameManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class Enemy : Character//Main
    {
        protected override void ExtendAllocate()
        {

        }
        protected override void ExtendInitialize()
        {

        }
    }
    public partial class Enemy : Character///Prop
    {
        protected override void ReceiveAttackcommand()
        {
            if (gameRound >= 10)
                {
                    Ultimate();
                }
                else
                {
                    GiveDamage(myDamage + (gameRound - 1) * 3);
                    AttackMotion();
                }
        }
        private void Ultimate()
        {
            GiveDamage(myDamage + 200);
            AttackMotion();
        }
        protected override void GetHit(float damage)
        {
            base.GetHit(damage);
            if (Random.Range(1, 100) < 30)
            {
                myHp += 10;
                Debug.Log($"Object Number {myNum} Heal!");
            }
        }
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound가 지날때마다 데미지 3씩 증가
    /// 2) _gameRound가 10이 되면 무조건 Player를 죽이도록 데미지 증가
    /// </summary>

}