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
    // 1. Hp UI bar 구현을 위해 HpMax 값 추가
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
            ///뭐지뭐지? 아직 캐릭터클래스 내에서는 상대방을 잡을 수 없는데
            //어케하지? 뭐지? 그렇다고 gameobject is enemy || gamebobject is player이런걸로 다운 캐스팅으로 알 수 있지도 않는데...
            //역시 난 코드아메바다 그냥 아주 미개하게 짜는가보다. 
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