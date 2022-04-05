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
    public GameObject gameManager;

    protected int _gameRound;
    protected string _whoseTurn;
    protected bool _isFinished = false;

    public void TurnUpdate(int round, string turn)
    {
        _gameRound = round;
        _whoseTurn = turn;

        StartCoroutine("Attack");
    }

    public void FinishUpdate(bool isFinish)
    {
        _isFinished = isFinish;
    }

    
    public virtual IEnumerator Attack()
    {
        yield return null;
    }

    public virtual void GetHit(float damage)
    {
        _myHp -= damage;
        if(_myHp <= 0)
        {
            DeadMotion();
            gameManager.GetComponent<GameManager>().EndNotify();

        } else
        {
            GetHitMotion();
            
        }
    }

    protected Animator _animator;

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        gameManager.GetComponent<GameManager>().AddCharacter(this);
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
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger(AnimatorParameters.IsDead.ToString());
    }
}
