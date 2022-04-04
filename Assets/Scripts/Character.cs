using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִϸ����� Ʈ���� �̸� ���������� ���� (������ �ʿ� ����)
public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

public class Character : MonoBehaviour, Observer
{
    public string _myName;
    public float hp, maxHp;

    public Attack baseAttack;
    public float strength = 1f;
    public bool dead = false;

    protected int _gameRound;
    protected string _whoseTurn;
    protected bool myturn = false;
    protected bool _isFinished;

    void Awake() {
        Init();
    }

    // 1. TurnUpdate: _gameRound, _whoseTurn update
    public void TurnUpdate(int round, string turn, Character character)
    {
        _gameRound = round;
        _whoseTurn = turn;
        myturn = character == this;
    }

    // 2. FinishUpdate: _isFinished update
    public void FinishUpdate(bool isFinish)
    {
        _isFinished = isFinish;
    }

    /// <summary>
    /// 3. Attack: ���ݽ� ����� ���� �� Player�� Enemy �������� ����� ��� �ۼ�
    /// ���� �� class���� �������̵��ؼ� �ۼ�
    /// 1) ������ ������ �ʾҰ� �ڽ��� _myName�� _whoseTurn�� ��ġ�Ѵٸ�,
    /// 2) AttackMotion() ȣ���ؼ� �ִϸ��̼� ����
    /// 3) ������ GetHit()�� �ڽ��� _myDamage �Ѱܼ� ȣ��
    /// </summary>
    public virtual void Attack(Character target) {
        baseAttack.At(this, target == null ? Target() : target);
    }

    public virtual void EndAttack() {
        GameManager.Instance().TurnEnd();
    }

    /// <summary>
    /// 4. GetHit: �ǰݽ� ����� ���� 3���� �����ϰ� ����Ǵ� ��� �ۼ�
    /// ���� �� class���� �������̵��ؼ� �ۼ�
    /// 1) �Ѱ� ���� damage��ŭ _myHp ����
    /// 2) ���� _myHp�� 0���� �۰ų� ���ٸ�, DeadMotion() ȣ���ؼ� �ִϸ��̼� ����
    ///    + Subject�� EndNotify() ȣ��
    /// 3) ���� ����ִٸ�, GetHitMotion() ȣ���ؼ� �ִϸ��̼� ����
    ///    + Debug.Log($"{_myName} HP: {_myHp}"); �߰�
    /// </summary>
    public virtual void GetHit(float damage)
    {
        hp -= damage;
        UI.Damage(transform, Mathf.CeilToInt(damage));
        if(hp <= 0) {
            dead = true;
            DeadMotion();
            GameManager.Instance().DeadNotify(this);
            GameManager.Instance().EndNotify();
        }
        else {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {hp}");
        }
    }

    public virtual void Heal(float hp) {
        hp = Mathf.Min(hp, maxHp - this.hp);
        this.hp += hp;
        UI.Damage(transform, -Mathf.CeilToInt(hp));
    }

    public virtual Character Target() {
        List<Character> enemies = GameManager.Instance().TeamMember(!GameManager.Instance().PlayerTurn());

        return enemies[Random.Range(0, enemies.Count)];
    }

    /// <summary>
    /// �� �����δ� animation ���� code, ������ �ʿ� ���� (������ ���ǿ��� �� ��)
    /// ������ �Ʒ�ó�� ���� �޼ҵ带 ���� �ʿ䵵 ������ ����� ���� �����̱� ������
    /// ����� ���Ǹ� ���� 4���� �޼ҵ带 �ۼ��Ͽ���.
    /// ���� Attack, GetHit �������̵���, �Ʒ��� �޼ҵ常 ȣ���ϸ� animation �����
    /// 1. AttackMotion()
    /// 2. SpecialAttackMotion()
    /// 3. DeadMotion()
    /// 4. GetHitMotion()
    /// </summary>
    protected Animator _animator;

    public void Animate(string animation) {
        _animator.SetTrigger(animation);
    }

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        hp = maxHp;
    }
    protected void AttackMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsAttack.ToString());
    }
    protected void SpecialAttackMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsSpecialAttack.ToString());
    }

    protected void DeadMotion()
    {
        _animator.SetTrigger(AnimatorParameters.IsDead.ToString());
    }

    protected void GetHitMotion()
    {
        _animator.SetTrigger(AnimatorParameters.GetHit.ToString());
    }

    IEnumerator GetHitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger(AnimatorParameters.GetHit.ToString());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetTrigger(AnimatorParameters.IsDead.ToString());
    }
}
