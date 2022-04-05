using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    /*private Player _player;
    private float _randomHeal;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Enemy"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 10���� ���� �ʱ�ȭ (���� ����)
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
    /// 1) _player�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _player �Ҵ�
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
    /// 1) _gameRound�� ���������� ������ 3�� ����
    /// 2) _gameRound�� 10�� �Ǹ� ������ Player�� ���̵��� ������ ����
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
    /// 1) Player�� _randomAttack�� ������ ���
    /// 2) 30%�� Ȯ���� �ǰݽ� 10 ü�� ����
    ///   + Debug.Log($"{_myName} Heal!"); �߰�
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
    /// 3) �߰��� yield return ���� ������ �ѹ��� ó���ż� �ǰ� �ϰ� Heal �ϴ� UI �ݿ��� ����� �̷������ ����.
    /// </summary>
    /// <returns></returns>
    IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        _myHp += 10;
        Debug.Log($"{_myName} Heal!");
    }
}

