using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static float mp = 0f, maxMp = 250f;

    public Skill[] skills;
    public int meditation = 0;
    private bool meditated = false;

    public Color meditateColor = Color.white;
    public GameObject meditateFx;

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
        GameManager.Instance().AddCharacter(this, true);
    }
    /// <summary>
    /// 1) _enemy가 할당이 안됐다면,
    /// 2) GameObject.FindWithTag 이용해서 _enemy 할당
    /// </summary>
    private void Start()
    {
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
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
    public override void Attack(Character target)
    {
        base.Attack(target);
    }

    public override void EndAttack() {
        if(!meditated) meditation = 0;
        meditated = false;
        base.EndAttack();
    }

    public void Meditate() {
        meditated = true;
        StartCoroutine(MeditateEnum(Mathf.Min(maxMp - mp, 10 * (1 << meditation))));
        meditation++;
        if (meditation > 3) meditation = 3;
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }

    private IEnumerator MeditateEnum(float amount) {
        if(meditateFx != null) Instantiate(meditateFx, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        mp += amount;
        if(mp > maxMp) mp = maxMp;
        if (amount >= 0.5f) UI.Damage(transform, transform.position, Mathf.RoundToInt(amount), meditateColor, DamageIndicator.fin);
        EndAttack();
    }
}
