using UnityEngine;
using My;
using static Define;
//IEnumerator : 코루틴만의 반환형 인터페이스 = 가져오기 위해 밑에 5,6번 줄 추가
using System.Collections;
using System.Collections.Generic;

public class MonsterMove : MonoBehaviour
{
    /** 물리적으로 따라갈 타겟 */
    public Rigidbody2D Target;

    /** 몬스터 생존여부 */
    bool bIsLive = true;

    /** 이 클래스를 가진 오브젝트의 Rigid에 접근을 위한 선언 */
    Rigidbody2D rigid;
    /** 이 클래스를 가진 오브젝트 SpritrRenderer접근을 위한 선언 */
    SpriteRenderer sprite;

    public EMonsterType CurrentMonsterType;

    public GameObject PlayerFind;

    /** TODO ## MonsterMove.cs 몬스터 체력 설정 재설정 필요 */
    public float CurrentMonsterHp = 20f;
    float MonsterMaxHp = 20f;
    float MonsterSpeed;

    Animator anim;
    WaitForFixedUpdate wait;
    Collider2D coll;
    
    void Awake()
    {
        wait = new WaitForFixedUpdate();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    void Start()
    {
        GameManager.GMInstance.MonsterMoveRef = this;
        #region PlayerFind
        //if (playerfind == null)
        //{
        //    /** playercharacter태그를 가진 플레이어를 찾는다. */
        //    playerfind = gameobject.findgameobjectwithtag("playercharacter");

        //    if (playerfind != null)
        //    {
        //        /** 찾은 player의 rigidbody2d컴포넌트를 가져온다. */
        //        target = playerfind.getcomponent<rigidbody2d>();
        //    }
        //}
        #endregion // 안쓰는거 주석

        MonsterSpeed = GameManager.GMInstance.MonsterSpeed;

        bIsLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);

        /** TODO ## MonsterMove.cs 몬스터 타입 정하기 */
        #region MonsterType
        if (gameObject.name == "Monster_A(Clone)")
        {
            CurrentMonsterType = EMonsterType.MonsterTypeA;
        }
        else if (gameObject.name == "Monster_B(Clone)")
        {
            CurrentMonsterType = EMonsterType.MonsterTypeB;
        }
        else if (gameObject.name == "Monster_C(Clone)")
        {
            CurrentMonsterType = EMonsterType.MonsterTypeC;
        }
        else if (gameObject.name == "Monster_D(Clone)")
        {
            CurrentMonsterType = EMonsterType.MonsterTypeD;
        }
        #endregion
    }

    void Update()
    {
        if (!bIsLive && PlayerFind == null)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        if (!GameManager.GMInstance.bIsLive)
        {
            return;
        }

        if (!bIsLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        /** TODO ## MonsterMove.cs 몬스터 이동 구현 */
        /** 몬스터와 플레이어의 위치 차이(방향값이 나옴) */
        Vector2 DirVec = Target.position - rigid.position;

        /**
        앞으로 가야할 다음 위치 계산 normalized를 사용하여 DirVec를 1로 정규화 해준다.
        피타고라스 정의에 의해 대각선 이동시 크기가 일정하지 핞기 때문에 일정하게 이동할수 있게 해준다.
        */
        Vector2 NextVec = DirVec.normalized * MonsterSpeed * Time.deltaTime;

        /** 이 클래스의 물리적 이동 구현 */
        rigid.MovePosition(rigid.position + NextVec);

        /** 물리적 속력 0으로 */
        rigid.velocity = Vector2.zero;

        /** 몬스터 방향 전환 */
        sprite.flipX = Target.position.x < rigid.position.x;
    }

    
    void OnEnable()
    {
        Target = GameManager.GMInstance.Player.GetComponent<Rigidbody2D>();

        // 재활용하기 위해
        bIsLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);
        CurrentMonsterHp = MonsterMaxHp;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /** 몬스터가 살아있지 않다면 */
        if (!bIsLive)
        {
            return;
        }

        /** TODO ## MonsterMove.cs 태그가 Bullet이면 */
        if (collision.CompareTag("Bullet"))
        {
            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);

            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** TODO ## MonsterMove.cs 크리티컬 적용 공식 */
            /** 파이어볼 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float FireBallCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetFireBallBaseDamage()) + collision.GetComponent<Bullet>().m_Damage);
            /** 파이어볼 일반 공격 데미지 계산식*/
            float FireBallNormalDamage = (GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetFireBallBaseDamage()) + collision.GetComponent<Bullet>().m_Damage;


            /** TODO ## MonsterMove.cs 크리티컬 적용 공식 */
            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                /** 크리티컬 데미지 피해 */
                GetDamage(FireBallCriticalDamage);
                // Debug.Log("파이어볼 크리티컬 데미지 : " + FireBallCriticalDamage);
            }
            else
            {
                /** 몬스터의 체력은 Bullet 태그를 가진 오브젝트에 닿으면 m_Damage만큼 빼준다. */
                GetDamage(FireBallNormalDamage);
                // Debug.Log("파이어볼 일반공격 데미지 : " + FireBallNormalDamage);
            }

            StartCoroutine(KnockBack());

            /** health 0보다 크면 몬스터가 살아있다면 */
            if (CurrentMonsterHp > 0)
            {
                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }

        /** TODO ## MonsterMove.cs 태그가 IceArrow이면 */
        if (collision.CompareTag("IceArrow"))
        {
            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** 아이스에로우 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float IceArrowCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetIceArrowBaseDamage()) + collision.GetComponent<Bullet>().m_Damage);
            /** 아이스에로우 일반 공격 데미지 계산식*/
            float IceArrowNormalDamage = (GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetIceArrowBaseDamage()) + collision.GetComponent<Bullet>().m_Damage;

            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                GetDamage(IceArrowCriticalDamage);
                // Debug.Log("아이스에로우 크리티컬 데미지 : " + IceArrowCriticalDamage);
            }
            else
            {
                /** 몬스터의 체력은 Bullet 태그를 가진 오브젝트에 닿으면 m_Damage만큼 빼준다. */
                GetDamage(IceArrowNormalDamage);
                // Debug.Log("아이스에로우 일반공격 데미지 : " + IceArrowNormalDamage);
            }

            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);

            StartCoroutine(KnockBack());

            /** health 0보다 크면 몬스터가 살아있다면 */
            if (CurrentMonsterHp > 0)
            {
                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }

        /** TODO ## MonsterMove.cs 태그가 ElectricBall이면 */
        if (collision.CompareTag("ElectricBall"))
        {
            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);

            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** 전기구체 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float ElectricBallCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetElectricBallBaseDamage()) + collision.GetComponent<Bullet>().m_Damage);
            /** 전기구체 일반 공격 데미지 계산식*/
            float ElectricBallNormalDamage = (GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetElectricBallBaseDamage()) + collision.GetComponent<Bullet>().m_Damage;

            /** TODO ## MonsterMove.cs 크리티컬 적용 공식 */
            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                
                GetDamage(ElectricBallCriticalDamage);
                // Debug.Log("뇌구 크리티컬 데미지 : " + ElectricBallCriticalDamage);
            }
            else
            {
                /** 몬스터의 체력은 Bullet 태그를 가진 오브젝트에 닿으면 m_Damage만큼 빼준다. */
                GetDamage(ElectricBallNormalDamage);
                // Debug.Log("뇌구 일반공격 데미지 : " + ElectricBallNormalDamage);
                // Debug.Log(GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetElectricBallBaseDamage());
            }

            StartCoroutine(KnockBack());



            /** health 0보다 크면 몬스터가 살아있다면 */
            if (CurrentMonsterHp > 0)
            {
                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }

        /** TODO ## MonsterMove.cs 몬스터가 스킬에 닿았다면 */
        if (collision.CompareTag("Mateo"))
        {
            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);

            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** 메테오 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float MateoCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetMateoBaseDamage()) + GameManager.GMInstance.SkillManagerRef.MateoDamage);
            /** 메테오 일반 공격 데미지 계산식*/
            float MateoNormalDamage = GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetMateoBaseDamage() + GameManager.GMInstance.SkillManagerRef.MateoDamage;

            /** TODO ## MonsterMove.cs 크리티컬 적용 공식 */
            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                
                GetDamage(MateoCriticalDamage);
                // Debug.Log("메테오 크리티컬 데미지 : " + MateoCriticalDamage);
            }
            else
            {
                /** 몬스터의 체력은 Bullet 태그를 가진 오브젝트에 닿으면 m_Damage만큼 빼준다. */
                GetDamage(MateoNormalDamage);
                // Debug.Log("메테오 일반공격 데미지 : " + MateoNormalDamage);

            }


            if (CurrentMonsterHp > 0)
            {
                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }

        /** 라이트닝에 닿았다면 */
        if (collision.gameObject.CompareTag("Lightning"))
        {
            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);

            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** 낙뢰 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float LightningCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetLightningBaseDamage()) + GameManager.GMInstance.SkillManagerRef.LightningDamage);
            /** 낙뢰 일반 공격 데미지 계산식*/
            float LightningNormalDamage = GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetLightningBaseDamage() + GameManager.GMInstance.SkillManagerRef.LightningDamage;

            
            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                /** 낙뢰 크리티컬 데미지 */
                GetDamage(LightningCriticalDamage);
                // Debug.Log("낙뢰 크리티컬 데미지 : " + LightningCriticalDamage);
            }
            else
            {
                /** 낙뢰 일반 데미지 */
                GetDamage(LightningNormalDamage);
                // Debug.Log("낙뢰 일반공격 데미지 : " + LightningNormalDamage);

            }


            if (CurrentMonsterHp > 0)
            {        
                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }

        if (collision.gameObject.CompareTag("Tornado"))
        {
            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);


            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** 토네이토 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float TornadoCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetTornadoBaseDamage()) + GameManager.GMInstance.SkillManagerRef.TornadoDamage);
            /** 토네이도 일반 공격 데미지 계산식*/
            float TornadoNormalDamage = GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetTornadoBaseDamage() + GameManager.GMInstance.SkillManagerRef.TornadoDamage;

            
            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                /** 토네이도 크리티컬 데미지 */
                GetDamage(TornadoCriticalDamage);
                // Debug.Log("토네이도 크리티컬 데미지 : " + TornadoCriticalDamage);
            }
            else
            {
                /** 토네이도 일반 데미지 */
                GetDamage(TornadoNormalDamage);
                // Debug.Log("토네이도 일반공격 데미지 : " + TornadoNormalDamage);
            }


            if (CurrentMonsterHp > 0)
            {

                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }

        /** 아이스에이지에 닿았다면 */
        if (collision.gameObject.CompareTag("IceAge"))
        {
            /** 피격 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);

            float Crtical = 100.0f * Random.Range(0.0f, 1.0f);
            // Debug.Log(Crtical);

            /** 아이스에이지 크리티컬 데미지 계산식 (크리티컬 데미지 * (능력치 증가로 인한 데미지 증가 + 기존 데미지) */
            float IceAgeCriticalDamage = GameManager.GMInstance.GetCriticalDamage() * ((GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetIceAgeBaseDamage()) + GameManager.GMInstance.SkillManagerRef.IceAgeDamage);
            /** 아이스에이지 일반 공격 데미지 계산식*/
            float IceAgeNormalDamage = GameManager.GMInstance.GetSkillDamageUp() * GameManager.GMInstance.GetIceAgeBaseDamage() + GameManager.GMInstance.SkillManagerRef.IceAgeDamage;


            /** 크리티커 확률 적용 되었다면 */
            if (Crtical <= 100.0f * GameManager.GMInstance.GetCriticalPercent())
            {
                /** 아이스에이지 크리티컬 데미지 */
                GetDamage(IceAgeCriticalDamage);
                Debug.Log("아이스에이지 크리티컬 데미지 : " + IceAgeCriticalDamage);
            }
            else
            {
                /** 아이스에이지 일반 데미지 */
                GetDamage(IceAgeNormalDamage);
                Debug.Log("아이스에이지 일반공격 데미지 : " + IceAgeNormalDamage);
            }


            if (CurrentMonsterHp > 0)
            {

                //피격 부분에 애니메이터를 호출하여 상태 변경
                anim.SetTrigger("Hit");
            }
            /** 몬스터가 죽으면 */
            else
            {
                // 몬스터가 죽었으므로 비활성화
                bIsLive = false;
                /** 콜라이더 비활성화 */
                coll.enabled = false;
                /** RigidBody2D 비활성화 */
                rigid.simulated = false;
                /** sprite 레이어 1 */
                sprite.sortingOrder = 1;
                // 죽는 애니메이션
                anim.SetBool("Dead", true);
                /** 함수 호출 */
                GameManager.GMInstance.killcount++;
                // 몬스터 사망 시 킬수 증가 함수 호출
                GameManager.GMInstance.GetExp();
                // 몬스터 사망 시 경험치 함수 호출
                // Dead();
            }
        }
    }

    /** TODO ## MonsterMove.cs 아이스 에이지에 닿았을 때 데미지 */
    void OnTriggerStay2D(Collider2D collision)
    {
        ///** 아이스에이지에 닿았다면 */
        //if (collision.gameObject.CompareTag("IceAge"))
        //{
        //    CurrentMonsterHp -= Time.deltaTime * GameManager.GMInstance.SkillManagerRef.IceAgeDamage;
        //    // Debug.Log(GameManager.GMInstance.SkillManagerRef.IceAgeDamage);

        //    /** 피격 효과음 재생 */
        //    GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Hit);


        //    if (CurrentMonsterHp > 0)
        //    {

        //        //피격 부분에 애니메이터를 호출하여 상태 변경
        //        anim.SetTrigger("Hit");
        //    }
        //    /** 몬스터가 죽으면 */
        //    else
        //    {
        //        // 몬스터가 죽었으므로 비활성화
        //        bIsLive = false;
        //        /** 콜라이더 비활성화 */
        //        coll.enabled = false;
        //        /** RigidBody2D 비활성화 */
        //        rigid.simulated = false;
        //        /** sprite 레이어 1 */
        //        sprite.sortingOrder = 1;
        //        // 죽는 애니메이션
        //        anim.SetBool("Dead", true);
        //        /** 함수 호출 */
        //        GameManager.GMInstance.killcount++;
        //        // 몬스터 사망 시 킬수 증가 함수 호출
        //        GameManager.GMInstance.GetExp();
        //        // 몬스터 사망 시 경험치 함수 호출
        //        // Dead();
        //    }
        //}
    }

    public void Init(GameManager Data)
    {
        MonsterMaxHp = Data.MonsterMaxHp;
        MonsterSpeed = Data.MonsterCurrentSpeed;
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerpos = GameManager.GMInstance.Player.transform.position;
        Vector3 dirVec = transform.position - playerpos;
        rigid.AddForce(dirVec.normalized * 1.5f, ForceMode2D.Impulse);
    }

    /** 죽는 함수 */
    void Dead()
    {
        /** 게임 오브젝트 비활성화 */
        gameObject.SetActive(false);
    }

    /** 데이미지를 받음 */
    public void GetDamage(float _damage)
    {
        CurrentMonsterHp -= _damage; 
    }
}