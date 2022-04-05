using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // 1. _player ���� ���� -> GetCharacter�� ������ ����
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
            // 1. GetCharacter�� Player ����
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
            if (_randomHeal < 5) // 50% Ȯ���� Heal
            {
                StartCoroutine(HealCoroutine());
            }
        }
    }

    /// <summary>
    /// HealCoroutine: 
    /// 1) Player�� Enemy ���� -> Hp ���� -> UI �ݿ�
    /// 2) Enemy Ȯ�������� ȸ�� -> Hp �� -> UI �ݿ�
    /// 3) �߰��� yield return ���� ������ �ѹ��� ó���ż� �ǰ� �ϰ� Heal �ϴ� UI �ݿ��� ����� �̷������ ����. //UI �ݿ��� ���� ���� �ʾ���.
    /// </summary>
    /// <returns></returns>
    IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        _myHp += 10;
        Debug.Log($"{_myName} Heal!");
        yield return new WaitForSeconds(1.3f);

    }
}