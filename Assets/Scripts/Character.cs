using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ????? ??? ?? ????? ?? (??? ?? ??)
public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

public class Character : MonoBehaviour, Observer
{
    public string _myName;
    // 1. Hp UI bar 구현을 위해 HpMax 값 추가
    public float _myHpMax;
    public float _myHp;
    public float _myDamage;

    protected int _gameRound;
    protected string _whoseTurn;
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

    /// <summary>
    /// 3. Attack: ??? ??? ?? ? Player? Enemy ???? ??? ?? ??
    /// ?? ? class?? ??????? ??
    /// 1) ??? ??? ??? ??? _myName? _whoseTurn? ?????,
    /// 2) AttackMotion() ???? ????? ??
    /// 3) ???? GetHit()? ??? _myDamage ??? ??
    /// </summary>
    public virtual void Attack() {}

    /// <summary>
    /// 4. GetHit: ??? ??? ?? 3?? ???? ???? ?? ??
    /// ?? ? class?? ??????? ??
    /// 1) ?? ?? damage?? _myHp ??
    /// 2) ?? _myHp? 0?? ??? ???, DeadMotion() ???? ????? ??
    ///    + Subject? EndNotify() ??
    /// 3) ?? ?????, GetHitMotion() ???? ????? ??
    ///    + Debug.Log($"{_myName} HP: {_myHp}"); ??
    /// </summary>
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

    /// <summary>
    /// ? ???? animation ?? code, ??? ?? ?? (??? ???? ? ?)
    /// ??? ???? ?? ???? ?? ??? ??? ??? ?? ???? ???
    /// ??? ??? ?? 4?? ???? ?????.
    /// ?? Attack, GetHit ??????, ??? ???? ???? animation ???
    /// 1. AttackMotion()
    /// 2. SpecialAttackMotion()
    /// 3. DeadMotion()
    /// 4. GetHitMotion()
    /// </summary>
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
