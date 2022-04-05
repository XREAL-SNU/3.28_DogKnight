using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    /*private Player _player;
    private float _randomHeal;

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
        GameManager.Instance().AddCharacter(this);
        this._myName = "Enemy";
        this._myHp = 100;
        this._myDamage = 20;
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _player가 할당이 안됐다면,
    /// 2) GameObject.FindWithTag 이용해서 _player 할당
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
    /// 1) _gameRound가 지날때마다 데미지 3씩 증가
    /// 2) _gameRound가 10이 되면 무조건 Player를 죽이도록 데미지 증가
    /// </summary>
    public override void Attack()
    {
        if (_myName == _whoseTurn && !_isFinished)
        {
            AttackMotion();
            _player.GetHit(_myDamage);
            _myDamage += 3;
        }

        if (_gameRound >= 10)
        {
            _myDamage = _player._myHp;
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
        _randomHeal = Random.Range(0, 10);
        if (_randomHeal < 3 && !_isFinished)
        {
            _myHp += 10;
            Debug.Log($"{_myName} Heal!");
        }
    }*/

    // 1. _player 변수 삭제 -> GetCharacter로 접근할 거임
    //private Player _player;
    private float _randomHeal;

    protected override void Init()
    {
        base.Init();
        _myName = "Enemy";
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 15;
        GameManager.Instance().AddCharacter(this.GetComponent<Enemy>());
    }

    private void Awake()
    {
        Init();
    }

    public override void Attack()
    {
        if (_myName.Equals(_whoseTurn) && !_isFinished)
        {
            _myDamage += 3;
            // 1. GetCharacter로 Player 접근
            if (_gameRound >= 10) _myDamage = GameManager.Instance().GetCharacter("Player")._myHp;
            AttackMotion();
            GameManager.Instance().GetCharacter("Player").GetHit(_myDamage);
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        if (_myHp > 0)
        {
            _randomHeal = Random.Range(0, 10);
            if (_randomHeal < 5) // 50% 확률로 Heal
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

