using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;
    private float _randomHeal;

    public bool isHeal;
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


    public override void Attack()
    {

        if (_myName == _whoseTurn && !_isFinished)
        {
            AttackMotion();
            _player.GetHit(_myDamage); //이 부분에서 헷갈려가지고 애먹은...ㅠㅠㅠ
            _myDamage += 3;
        }

        if (_gameRound >= 10) _myDamage = _player._myHp;
    }

 
    public override void GetHit(float damage)
    {
        base.GetHit(damage);
        _randomHeal = Random.Range(0, 10);
        if (_randomHeal % 3 == 0 && !_isFinished)
        {
            isHeal = true;
            StartCoroutine(GetHeal()); 
        }
  

    }
    IEnumerator GetHeal()
    {
        yield return new WaitForSeconds(2.3f);
        _myHp += 10;
        yield return new WaitForSeconds(0.6f);
        isHeal = false;
    }
}
