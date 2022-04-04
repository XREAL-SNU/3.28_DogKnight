using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
    private float _randomAttack;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Player"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����)
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
    /// 1) _enemy�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _enemy �Ҵ�
    /// </summary>
    private void Start()
    {
        //���� �ʿ��Ѱ� Enemy�� GetHit�̹Ƿ�, getComponent�ؼ� Enemy���� �����ؾ� ��
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    /// <summary>
    /// Attack:
    /// 1) Player�� 30%�� Ȯ���� ���ݷ��� �� ���� ������ ���� ��
    /// 2) _randomAttack = Random.Range(0,10); ���� ���� ���� ����
    ///   -> 0~9 ������ ���� �� �ϳ��� �������� �Ҵ����.
    /// 3) _randomAttack �̿��ؼ� 30% Ȯ���� ���� ���ݷº��� 10 ���� ���� ����
    /// 4) �̶��� AttackMotion() ���� SpecialAttackMotion() ȣ���� ��
    ///    + Debug.Log($"{_myName} Special Attack!"); �߰�
    /// 5) 70% Ȯ���� �ϴ� �Ϲ� ������ Character�� ���ִ� �ּ��� ����
    /// </summary>
    public override void Attack()
    {
        

        if (_whoseTurn == _myName && _isFinished == false)
        {
            _randomAttack = Random.Range(0, 10);
            if (_randomAttack < 3)
            {
                _myDamage += 10;
                Debug.Log($"{_myName} Special Attack!");
                _enemy.GetHit(_myDamage);
                SpecialAttackMotion();
                

            }

            else
            {
                _enemy.GetHit(_myDamage);
                AttackMotion();
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);

        StartCoroutine(HpBarDelay());
        
        
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
