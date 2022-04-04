using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;
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
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 10;
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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound�� ���������� ������ 3�� ����
    /// 2) _gameRound�� 10�� �Ǹ� ������ Player�� ���̵��� ������ ����
    /// </summary>
    public override void Attack()
    {
       

        if (_whoseTurn == _myName && _isFinished == false)
        {
            if (_gameRound == 10)
            {
                _myDamage = 1000;
            }

            else
            {
                _myDamage += 3;
            }

            _player.GetHit(_myDamage);
            AttackMotion();

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
        

        if (_isFinished == false)
        {
            _randomHeal = Random.Range(0, 10);
            if (_randomHeal < 3)
            {
                _myHp += 10;
                Debug.Log($"{_myName} Heal!");
                Debug.Log($"{_myName} HP: {_myHp}");

                StartCoroutine(HpBarDelay());
                


            }

            else
            {
                base.GetHit(damage);
                StartCoroutine(HpBarDelay());
                

            }
        }

    }




    //HP bar UI

    public delegate void HpHandler(float hp);
    HpHandler _hpHandler;


    public void AddObserver(HpInterface characterHpBar)
    {
        _hpHandler += characterHpBar.HpOnNotify;
    }

    public void HpNotify(float hp)
    {
        _hpHandler(hp);
    }

    IEnumerator HpBarDelay()
    {
        yield return new WaitForSeconds(2f);
        HpNotify(_myHp);

    }




}

