using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;
    private float _randomHeal;
    private float _turnDamage;

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
        GameManager.Instance().AddCharacter(this);
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 10;
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _player? ??? ????,
    /// 2) GameObject.FindWithTag ???? _player ??
    /// </summary>
    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound? ????? ??? 3? ??
    /// 2) _gameRound? 10? ?? ??? Player? ???? ??? ??
    /// </summary>
    public override void Attack()
    {
        if (!_isFinished && _myName == _whoseTurn)
        {
            _turnDamage = _myDamage + 3 * _gameRound;
            if (_gameRound == 10)
            {
                _turnDamage = float.MaxValue;
            }

            AttackMotion();

            _player.GetHit(_turnDamage);
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
                _myHp += 10;
                Debug.Log($"{_myName} Heal!");
            }

            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }
}

