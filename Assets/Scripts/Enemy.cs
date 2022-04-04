using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    // 1. _player 변수 삭제 -> GetCharacter로 접근할 거임
    //private Player _player;
    private float _randomHeal;
    public GameObject _healUI;

    protected override void Init()
    {
        base.Init();
        _myName = "Enemy";
<<<<<<< HEAD
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 15;
        GameManager.Instance().AddCharacter(this.GetComponent<Enemy>());
=======
        _myHp = 100;
        _myDamage = 20;
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }

    private void Awake()
    {
        Init();
    }

    public override void Attack()
    {
<<<<<<< HEAD
        if (_myName.Equals(_whoseTurn) && !_isFinished)
        {
            _myDamage += 3;
            // 1. GetCharacter로 Player 접근
            if (_gameRound >= 10) _myDamage = GameManager.Instance().GetCharacter("Player")._myHp;
            AttackMotion();
            GameManager.Instance().GetCharacter("Player").GetHit(_myDamage);
=======
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player").AddComponent<Player>();
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
        }
    }

    public override void GetHit(float damage)
    {
<<<<<<< HEAD
        base.GetHit(damage);
        if (_myHp > 0)
        {
            _randomHeal = Random.Range(0, 10);
            if (_randomHeal < 5) // 50% 확률로 Heal
            {
                StartCoroutine(HealCoroutine());
            }
        }
=======

        _myDamage = _gameRound < 10 ? 10 + _gameRound * 3 : 10000;
        _player.GetHit(_myDamage);
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
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
<<<<<<< HEAD
        yield return new WaitForSeconds(2.0f);
        _myHp += 10;
        Debug.Log($"{_myName} Heal!");
        _healUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _healUI.SetActive(false);
=======
        _myHp -= damage;
        _randomHeal = Random.Range(0, 10);
        if(_randomHeal < 3.0f)
        {
            _myHp += 10;
            Debug.Log($"{_myName} Heal!");
        }

        if (_myHp < 0) DeadMotion();
        else
        {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }
}