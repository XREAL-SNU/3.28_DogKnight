using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이팅 트리거 이름 열거형으로 저장 (이해할 필요 없음)
public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

public class Character : MonoBehaviour, Observer
{
    public string _myName;
    public float _myHp;
    public float _myDamage;
    protected int _gameRound;
    protected string _whoseTurn; //int -> string
    protected bool _isFinished;
    // 1. TurnUpdate: _gameRound, _whoseTurn update
    public void TurnUpdate(int round, string turn)
    {
        _gameRound = round;
        _whoseTurn = turn;
    }

    // 2. FinishUpdate: _isFinished update
    public void FinishUpdate(bool isFinish)
    {
        _isFinished = isFinish;
    }


    public virtual void Attack()
    {
        ///공통 코드
        //공통 코드가 뭐가있나..?
    }


    public virtual void GetHit(float damage)
    {
        _myHp -= damage;
        if (_myHp <= 0)
        {
            DeadMotion();
            GameManager.Instance().EndNotify();
        }
        else
        {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }

    #region Summary

    protected Animator _animator;

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
    }
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
    #endregion
}