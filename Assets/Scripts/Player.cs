using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float _randomAttack;

    /// <summary>
    /// 1. Init: ??? ??
    /// 1) Subject? Observer? ??
    /// 2) _myName, _myHp, _myDamage ???
    /// 3) _myName? ??? "Player"? ? ?
    /// 4) _myHp, _myDamage? 100, 20?? ?? ??? (?? ??)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Player";
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 20;
        GameManager.Instance().AddCharacter(this.GetComponent<Player>());
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Attack:
    /// 1) Player? 30%? ??? ???? ? ?? ??? ?? ?
    /// 2) _randomAttack = Random.Range(0,10); ?? ?? ?? ??
    ///   -> 0~9 ??? ?? ? ??? ???? ????.
    /// 3) _randomAttack ???? 30% ??? ?? ????? 10 ?? ?? ??
    /// 4) ??? AttackMotion() ?? SpecialAttackMotion() ??? ?
    ///    + Debug.Log($"{_myName} Special Attack!"); ??
    /// 5) 70% ??? ?? ?? ??? Character? ??? ??? ??
    /// </summary>
    public override void Attack()
    {
        if (!_isFinished && _myName == _whoseTurn)
        {
            _randomAttack = Random.Range(0, 10);

            if (_randomAttack < 3)
            {
                SpecialAttackMotion();
                Debug.Log($"{_myName} Special Attack!");
                GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage + 10);
            }
            else
            {
                AttackMotion();
                GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage);
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}
