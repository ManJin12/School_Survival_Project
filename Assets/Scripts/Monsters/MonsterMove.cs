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

    public float health = 20;

    Animator anim;
    WaitForFixedUpdate wait;
    Collider2D coll;
    
    void Awake()
    {
        wait = new WaitForFixedUpdate();
    }
    void Start()
    {
        if (PlayerFind == null)
        {
            /** PlayerCharacter태그를 가진 플레이어를 찾는다. */
            PlayerFind = GameObject.FindGameObjectWithTag("PlayerCharacter");

            if (PlayerFind != null)
            {
                /** 찾은 Player의 Rigidbody2D컴포넌트를 가져온다. */
                Target = PlayerFind.GetComponent<Rigidbody2D>();
            }
        }

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        bIsLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);

        /** TODO ## 몬스터 타입 정하기 */
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
        if (!bIsLive && PlayerFind == null)
        {
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        /** TODO ## 몬스터 이동 구현 */
        /** 몬스터와 플레이어의 위치 차이(방향값이 나옴) */
        Vector2 DirVec = Target.position - rigid.position;

        /**
        앞으로 가야할 다음 위치 계산 normalized를 사용하여 DirVec를 1로 정규화 해준다.
        피타고라스 정의에 의해 대각선 이동시 크기가 일정하지 핞기 때문에 일정하게 이동할수 있게 해준다.
        */
        Vector2 NextVec = DirVec.normalized * GameManager.GMInstance.MonsterSpeed * Time.deltaTime;

        /** 이 클래스의 물리적 이동 구현 */
        rigid.MovePosition(rigid.position + NextVec);

        /** 물리적 속력 0으로 */
        rigid.velocity = Vector2.zero;

        /** 몬스터 방향 전환 */
        sprite.flipX = Target.position.x < rigid.position.x;
    }

    /*void OnEnable()
    {
        // 재활용하기 위해
        bIsLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        /** 태그가 Bullet이 아니라면 return */
        if (!collision.CompareTag("Bullet"))
            return;

        /** 몬스터의 체력은 Bullet 태그를 가진 오브젝트에 닿으면 m_Damage만큼 빼준다. */
        health -= collision.GetComponent<Bullet>().m_Damage;
        StartCoroutine(KnockBack());

        /** health 0보다 크면 몬스터가 살아있다면 */ 
        if (health > 0)
        {
            //피격 부분에 애니메이터를 호출하여 상태 변경
            anim.SetTrigger("Hit");
        }
        /** 몬스터가 죽으면 */
        else
        {
            // 몬스터가 죽었으므로 비활성화
            bIsLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            sprite.sortingOrder = 1;
            //죽는 애니메이션
            anim.SetBool("Dead", true);
            /** 함수 호출 */
            //Dead();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerpos = GameManager.GMInstance.Player.transform.position;
        Vector3 dirVec = transform.position - playerpos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        
    }

    void Dead()
    {
        /** 게임 오브젝트 비활성화 */
        gameObject.SetActive(false);
    }
}