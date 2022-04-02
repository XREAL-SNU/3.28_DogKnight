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
    // 1. Hp UI bar ������ ���� HpMax �� �߰�
    public float _myHpMax;
    public float _myDamage;

    protected int _gameRound;
    protected string _whoseTurn;
    protected bool _isFinished;

    public void TurnUpdate(int round, string turn)
    {
        this._gameRound = round;
        this._whoseTurn = turn;
    }

    public void FinishUpdate(bool isFinish)
    {
        this._isFinished = isFinish;
    }

    
    public virtual void Attack()
    {
        if(!_isFinished && _myName == _whoseTurn)
        {
            AttackMotion();
          
        }
    }

    public virtual void GetHit(float damage)
    {
        _myHp -= damage;
        if(_myHp <= 0)
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
}
