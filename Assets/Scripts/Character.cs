using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִϸ����� Ʈ���� �̸� ���������� ���� (������ �ʿ� ����)
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
<<<<<<< HEAD
        this._gameRound = round;
        this._whoseTurn = turn;
=======
        _gameRound = round;
        _whoseTurn = turn;
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }

    public void FinishUpdate(bool isFinish)
    {
<<<<<<< HEAD
        this._isFinished = isFinish;
=======
        _isFinished = isFinish;
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }


    public virtual void Attack()
    {
        if(_isFinished == false)
        {
            if(_myName == _whoseTurn)
            {
                AttackMotion();
            }
            ////// I don't know how to search another charcter
            GetHit(_myDamage);
            ///��������? ���� ĳ����Ŭ���� �������� ������ ���� �� ���µ�
            //��������? ����? �׷��ٰ� gameobject is enemy || gamebobject is player�̷��ɷ� �ٿ� ĳ�������� �� �� ������ �ʴµ�...
            //���� �� �ڵ�Ƹ޹ٴ� �׳� ���� �̰��ϰ� ¥�°�����. 
        }
    }

    public virtual void GetHit(float damage)
    {
        _myHp -= damage;
<<<<<<< HEAD
        if (_myHp <= 0)
        {
            DeadMotion();
            GameManager.Instance().EndNotify();
        }
=======
        if (_myHp < 0) DeadMotion();
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
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