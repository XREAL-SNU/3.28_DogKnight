using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MainSystem.Managers.GameManager;
public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

namespace MainSystem.Managers.GameManager
{
    public partial class Character : MonoBehaviour//Data
    {
        [SerializeField] private UnityEvent<float> giveDamageEvent;
        protected int gameRound;
        protected int whoseTurn;
        protected bool isFinished;
        [SerializeField] protected float myHp = 100f;
        [SerializeField] protected float myDamage = 20f;
        [SerializeField] protected UnityEvent DeadSignal;
        [SerializeField] protected int myNum;
        private GameManager gameManager;
    }
    public partial class Character : MonoBehaviour //Main
    {
        private void Start()
        {
            Initialize();
        }
        private void Allocate()
        {
            _animator = GetComponent<Animator>();

            ExtendAllocate();
        }
        public void Initialize()
        {
            Allocate();

            ExtendInitialize();
        }
    }
    public partial class Character : MonoBehaviour//Extand
    {
        protected virtual void ExtendAllocate()
        {

        }
        protected virtual void ExtendInitialize()
        {

        }
    }
    public partial class Character : MonoBehaviour
    {
        public void SignupGameManager(GameManager gameManagerp)
        {
            gameManager = gameManagerp;
            DeadSignal.AddListener(gameManager.ReceiveDeadSignal);
        }
        public void ReceiveTureOrder(int whosTurnPra, int gameRoundPra)
        {
            GameInfoUpdate(whosTurnPra, gameRoundPra);
            if (whosTurnPra == myNum)
            {
                ReceiveAttackcommand();
            }
        }
        private void GameInfoUpdate(int whosTurnPra, int gameRoundPra)
        {
            whoseTurn = whosTurnPra;
            gameRound = gameRoundPra;
        }
        protected virtual void ReceiveAttackcommand()
        {

        }
        protected void NormalAttack()
        {
            AttackMotion();
            GiveDamage(myDamage);
        }
        public virtual void ReceiveDamagecommand(float damage)
        {
            GetHit(damage);
        }
        protected void GiveDamage(float damage)
        {
            giveDamageEvent.Invoke(damage);
        }
        protected virtual void GetHit(float damage)
        {
            myHp -= damage;
            Debug.Log($"Object Number {myNum} HP: {myHp}");
            if (myHp <= 0)
            {
                Death();
            }
            GetHitMotion();
        }
        protected void Death()
        {
            DeadMotion();
            DeadSignal.Invoke();
        }
        protected Animator _animator;

        protected void AttackMotion()
        {
            _animator.SetTrigger(AnimatorParameters.IsAttack.ToString());
        }
        protected void SpecialAttackMotion()
        {
            _animator.SetTrigger(AnimatorParameters.IsSpecialAttack.ToString());
        }

        protected void DeadMotion()
        {
            StartCoroutine(DeadCoroutine());
        }

        protected void GetHitMotion()
        {
            StartCoroutine(GetHitCoroutine());
        }

        IEnumerator GetHitCoroutine()
        {
            yield return new WaitForSeconds(1f);
            _animator.SetTrigger(AnimatorParameters.GetHit.ToString());
        }

        IEnumerator DeadCoroutine()
        {
            yield return new WaitForSeconds(1f);
            _animator.SetTrigger(AnimatorParameters.IsDead.ToString());
        }
    }

}