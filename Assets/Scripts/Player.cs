using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
    private float _randomAttack;
    private float _turnDamage;

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
        GameManager.Instance().AddCharacter(this);
        _myName = "Player";
        _myHp = 100;
        _myDamage = 20;
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _enemy? ??? ????,
    /// 2) GameObject.FindWithTag ???? _enemy ??
    /// </summary>
    private void Start()
    {
        if (_enemy == null)
        {
            _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        }
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
            _turnDamage = _myDamage;

            if (_randomAttack < 3)
            {
                SpecialAttackMotion();
                Debug.Log($"{_myName} Special Attack!");
                _turnDamage += 10;
            }
            else
            {
                AttackMotion();
            }

            _enemy.GetHit(_turnDamage);
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);

        if (_myHp > 0)
        {
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }
}
