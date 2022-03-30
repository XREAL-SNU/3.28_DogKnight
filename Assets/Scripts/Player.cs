using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
    private float _randomAttack;
    /// <summary>
    /// 1. Init: 초기화 기능
    /// 1) Subject에 Observer로 등록
    /// 2) _myName, _myHp, _myDamage 초기화
    /// 3) _myName은 무조건 "Player"로 할 것
    /// 4) _myHp, _myDamage는 100, 20으로 각각 초기화 (권장 사항)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        GameManager.Instance.AddCharacter(this);
        _myHp = 100;
        _myDamage = 20;
        _myName = "Player";

    }


    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _enemy가 할당이 안됐다면,
    /// 2) GameObject.FindWithTag 이용해서 _enemy 할당
    /// </summary>
    private void Start()
    {
        if (_enemy == null) 
        { 
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>(); // John-Lemon문법 참고
        }
    }

    /// <summary>
    /// Attack:
    /// 1) Player는 30%의 확률로 공격력이 더 높은 공격을 가할 것
    /// 2) _randomAttack = Random.Range(0,10); 으로 랜덤 변수 생성
    ///   -> 0~9 까지의 정수 중 하나를 랜덤으로 할당받음.
    /// 3) _randomAttack 이용해서 30% 확률로 기존 공격력보다 10 높은 공격 실행
    /// 4) 이때는 AttackMotion() 말고 SpecialAttackMotion() 호출할 것
    ///    + Debug.Log($"{_myName} Special Attack!"); 추가
    /// 5) 70% 확률로 하는 일반 공격은 Character에 써있는 주석과 동일
    /// </summary>
    public override void Attack()
    {
        bool revisFinished = !_isFinished;
        if (_myName == _whoseTurn && revisFinished)
        {
            int _randomAttack = Random.Range(0, 10);
            if (_randomAttack >= 7)
            {
                SpecialAttackMotion();
                float _damage = _myDamage + 10;
                Debug.Log($"{_myName} Special Attack!");
                _enemy.GetHit(_damage);
            }
            else
            {
                AttackMotion();
                _enemy.GetHit(_myDamage);
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}
