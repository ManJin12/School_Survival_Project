using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
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

    public GameObject PlayerFind;


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
    }

    void Update()
    {
        if (!bIsLive && PlayerFind == null)
        {
            return;
        }

        sprite.flipX = Target.position.x < rigid.position.x;
    }

    void FixedUpdate()
    {
        if (!bIsLive && PlayerFind == null)
        {
            return;
        }

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
    }
}