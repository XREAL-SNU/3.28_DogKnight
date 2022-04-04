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
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Player"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        GameManager.Instance().AddCharacter(this, true);
    }
    /// <summary>
    /// 1) _enemy�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _enemy �Ҵ�
    /// </summary>
    private void Start()
    {
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
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
