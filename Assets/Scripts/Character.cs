using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이팅 트리거 이름 열거형으로 저장 (이해할 필요 없음)
public enum AnimatorParameters
{
    IsAttack, IsSpecialAttack, GetHit, IsDead
}

public class Character : MonoBehaviour, Observer
{
    public string _myName;
    public float _myHp, maxHp;
    public float _myDamage;

    protected int _gameRound;
    protected string _whoseTurn;
    protected bool myturn = false;
    protected bool _isFinished;

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
    /// 3. Attack: 공격시 실행될 내용 중 Player와 Enemy 공통으로 실행될 기능 작성
    /// 이후 각 class에서 오버라이딩해서 작성
    /// 1) 게임이 끝나지 않았고 자신의 _myName와 _whoseTurn이 일치한다면,
    /// 2) AttackMotion() 호출해서 애니메이션 실행
    /// 3) 상대방의 GetHit()에 자신의 _myDamage 넘겨서 호출
    /// </summary>
    public virtual void Attack()
    {
        AttackMotion();
    }

    /// <summary>
    /// 4. GetHit: 피격시 실행될 내용 3번과 동일하게 공통되는 기능 작성
    /// 이후 각 class에서 오버라이딩해서 작성
    /// 1) 넘겨 받은 damage만큼 _myHp 감소
    /// 2) 만약 _myHp가 0보다 작거나 같다면, DeadMotion() 호출해서 애니메이션 실행
    ///    + Subject의 EndNotify() 호출
    /// 3) 아직 살아있다면, GetHitMotion() 호출해서 애니메이션 실행
    ///    + Debug.Log($"{_myName} HP: {_myHp}"); 추가
    /// </summary>
    public virtual void GetHit(float damage)
    {
        _myHp -= damage;
        if(_myHp <= 0) {
            DeadMotion();
            GameManager.Instance().EndNotify();
        }
        else {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }

    public virtual void Heal(float hp) {
        _myHp += hp;
        if(_myHp > maxHp) _myHp = maxHp;
    }

    public virtual Character Target() {
        List<Character> enemies = GameManager.Instance().TeamMember(!GameManager.Instance().PlayerTurn());

        return enemies[Random.Range(0, enemies.Count)];
    }

    /// <summary>
    /// 이 밑으로는 animation 관련 code, 이해할 필요 없음 (다음주 세션에서 할 것)
    /// 원래는 아래처럼 여러 메소드를 만들 필요도 없지만 배우지 않은 내용이기 때문에
    /// 사용의 편의를 위해 4가지 메소드를 작성하였음.
    /// 위의 Attack, GetHit 오버라이딩시, 아래의 메소드만 호출하면 animation 실행됨
    /// 1. AttackMotion()
    /// 2. SpecialAttackMotion()
    /// 3. DeadMotion()
    /// 4. GetHitMotion()
    /// </summary>
    protected Animator _animator;

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        _myHp = maxHp;
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
        StartCoroutine(DeadCoroutine());
    }

    protected void GetHitMotion()
    {
        StartCoroutine(GetHitCoroutine());
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
