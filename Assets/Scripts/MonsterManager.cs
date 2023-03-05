using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    /** ���� �̵� �ӵ� */
    public float Speed;

    /** ���������� ���� Ÿ�� */
    public Rigidbody2D Target;

    /** ���� �������� */
    bool bIsLive = true;

    /** �� Ŭ������ ���� ������Ʈ�� Rigid�� ������ ���� ���� */
    Rigidbody2D rigid;
    /** �� Ŭ������ ���� ������Ʈ SpritrRenderer������ ���� ���� */
    SpriteRenderer sprite;

    public GameObject PlayerFind;


    void Start()
    {
        GameManager.GMInstance.MonsterManagerRef = this;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        /** PlayerCharacter�±׸� ���� �÷��̾ ã�´�. */
        PlayerFind = GameObject.FindGameObjectWithTag("PlayerCharacter");

        /** ã�� Player�� Rigidbody2D������Ʈ�� �����´�. */
        Target = PlayerFind.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!bIsLive)
        {
            return;
        }

        /** TODO ## ���� �̵� ���� */
        /** ���Ϳ� �÷��̾��� ��ġ ����(���Ⱚ�� ����) */
        Vector2 DirVec = Target.position - rigid.position;

        /** ������ ������ ���� ��ġ ��� (���밪)*/
        Vector2 NextVec = DirVec.normalized * Speed * Time.fixedDeltaTime;

        /** �� Ŭ������ ������ �̵� ���� */
        rigid.MovePosition(rigid.position + NextVec);

        /** ������ �ӷ� 0���� */
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!bIsLive)
        {
            return;
        }

        sprite.flipX = Target.position.x < rigid.position.x;
    }

}
