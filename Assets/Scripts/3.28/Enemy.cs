using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;

    private int DAMAGE_INCREASE = 3;
    private int FINAL_ROUND = 10;
    private int PLAYER_HP = 100;
    private int SPECIAL_HEAL_MAX_VALUE = 3;
    private int HEAL_INCREASE = 10;

    /// <summary>
    /// 1. Init: 초기화 기능
    /// 1) Subject에 Observer로 등록
    /// 2) _myName, _myHp, _myDamage 초기화
    /// 3) _myName은 무조건 "Enemy"로 할 것
    /// 4) _myHp, _myDamage는 100, 10으로 각각 초기화 (권장 사항)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 10;
    }

    /// <summary>
    /// 1) _player가 할당이 안됐다면,
    /// 2) GameObject.FindWithTag 이용해서 _player 할당
    /// </summary>
    private void Awake()
    {
        Init();
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    
 
    /// <summary>
    /// Attack:
    /// 1) _gameRound가 지날때마다 데미지 3씩 증가
    /// 2) _gameRound가 10이 되면 무조건 Player를 죽이도록 데미지 증가
    /// </summary>
    public override IEnumerator Attack()
    {
        if (!_isFinished && _myName == _whoseTurn)
        {
            AttackMotion();
            if (_gameRound == FINAL_ROUND)
            {
                _myDamage = PLAYER_HP;
            } else
            {
                _myDamage = _myDamage + DAMAGE_INCREASE;
            }

            yield return null;

            _player.GetHit(_myDamage);
        }

        
    }

    /// <summary>
    /// GetHit:
    /// 1) Player의 _randomAttack과 동일한 기능
    /// 2) 30%의 확률로 피격시 10 체력 증가
    ///   + Debug.Log($"{_myName} Heal!"); 추가
    /// </summary>
    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        SetHpOfChracters.updateEnemyHp(_myHp);

        int _randomHealOrNot = Random.Range(0, 10);

        if(0 <= _randomHealOrNot && _randomHealOrNot < SPECIAL_HEAL_MAX_VALUE)
        {
            PLAYER_HP += HEAL_INCREASE;
           
        }
    }
}

