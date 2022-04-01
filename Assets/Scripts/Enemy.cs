using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject gamemanager;
    private Player _player;
    private int playeris = 100;
    private float _randomHeal;

    /// <summary>
    /// 1. Init: 초기화 기능
    /// 
    /// 1) Subject에 Observer로 등록
    /// 
    /// 2) _myName, _myHp, _myDamage 초기화
    /// 
    /// 3) _myName은 무조건 "Enemy"로 할 것
    /// 
    /// 4) _myHp, _myDamage는 100, 10으로 각각 초기화 (권장 사항)
    /// 
    /// </summary>
    /// 
    protected override void Init()
    {
        base.Init();/// 1. Init: 초기화 기능
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 10;           /// 2) _myName, _myHp, _myDamage 초기화
                                  /// 4) _myHp, _myDamage는 100, 10으로 각각 초기화 (권장 사항)
                                  ///  /// 3) _myName은 무조건 "Enemy"로 할 것
        gamemanager.GetComponent<GameManager>().AddCharacter(this);/// 1) Subject에 Observer로 등록

    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _player가 할당이 안됐다면,
    /// 
    /// 2) GameObject.FindWithTag 이용해서 _player 할당
    /// 
    /// </summary>
    /// 
    private void Start()
    {
        if (_player == null)  /// 1) _player가 할당이 안됐다면,
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            /// 2) GameObject.FindWithTag 이용해서 _player 할당
        }
    }


    /// <summary>
    /// Attack:
    /// 
    /// 1) _gameRound가 지날때마다 데미지 3씩 증가
    /// 
    /// 2) _gameRound가 10이 되면 무조건 Player를 죽이도록 데미지 증가
    /// 
    /// </summary>
    /// 
    public override void Attack()
    {

        if (!_isFinished && _myName == _whoseTurn)
        {
            AttackMotion();

           

          if (_gameRound == 10)
            {
                _myDamage = playeris;
            }
            else
            {
                _myDamage = _myDamage + 3;
            }


            _player.GetHit(_myDamage);

        }


    }

    /// <summary>
    /// 
    /// GetHit:
    /// 
    /// 1) Player의 _randomAttack과 동일한 기능
    /// 
    /// 2) 30%의 확률로 피격시 10 체력 증가
    /// 
    ///   + Debug.Log($"{_myName} Heal!"); 추가
    ///   
    /// </summary>
    /// 
    public override void GetHit(float damage)
    {

        base.GetHit(damage);
        int _random = Random.Range(0, 10);
        if (0 <= _random && _random < 3)
        {
            playeris += 10;
            Debug.Log($"{_myName} Heal!");
        }


    }


}