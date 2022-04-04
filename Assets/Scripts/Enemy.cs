using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float _randomHeal;

    /// <summary>
    /// 1. Init: ??? ??
    /// 1) Subject? Observer? ??
    /// 2) _myName, _myHp, _myDamage ???
    /// 3) _myName? ??? "Enemy"? ? ?
    /// 4) _myHp, _myDamage? 100, 10?? ?? ??? (?? ??)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Enemy";
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 10;
        GameManager.Instance().AddCharacter(this.GetComponent<Player>());

    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound? ????? ??? 3? ??
    /// 2) _gameRound? 10? ?? ??? Player? ???? ??? ??
    /// </summary>
    public override void Attack()
    {
        if (!_isFinished && _myName.Equals(_whoseTurn))
        {
            AttackMotion();

            if (_gameRound >= 10)
            {
                GameManager.Instance().GetCharacter("Player").GetHit(float.MaxValue);
            }
            else
            {
                GameManager.Instance().GetCharacter("Player").GetHit(_myDamage + 3 * _gameRound);
            }
        }
    }

    /// <summary>
    /// GetHit:
    /// 1) Player? _randomAttack? ??? ??
    /// 2) 30%? ??? ??? 10 ?? ??
    ///   + Debug.Log($"{_myName} Heal!"); ??
    /// </summary>
    public override void GetHit(float damage)
    {
        base.GetHit(damage);

        if (_myHp > 0)
        {
            _randomHeal = Random.Range(0, 10);

            if (_randomHeal < 3)
            {
                StartCoroutine(HealCoroutine());
            }
        }
    }

    /// <summary>
    /// HealCoroutine: 
    /// 1) Player가 Enemy 공격 -> Hp 깎임 -> UI 반영
    /// 2) Enemy 확률적으로 회복 -> Hp 참 -> UI 반영
    /// 3) 중간에 yield return 하지 않으면 한번에 처리돼서 피격 하고 Heal 하는 UI 반영이 제대로 이루어지지 않음.
    /// </summary>
    /// <returns></returns>
    IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        _myHp += 10;
        Debug.Log($"{_myName} Heal!");
    }
}

