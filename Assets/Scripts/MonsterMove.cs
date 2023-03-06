using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
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
        if (PlayerFind == null)
        {
            /** PlayerCharacter�±׸� ���� �÷��̾ ã�´�. */
            PlayerFind = GameObject.FindGameObjectWithTag("PlayerCharacter");

            if (PlayerFind != null)
            {
                /** ã�� Player�� Rigidbody2D������Ʈ�� �����´�. */
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

        /** TODO ## ���� �̵� ���� */
        /** ���Ϳ� �÷��̾��� ��ġ ����(���Ⱚ�� ����) */
        Vector2 DirVec = Target.position - rigid.position;

        /** ������ ������ ���� ��ġ ��� (���밪)*/
        Vector2 NextVec = DirVec.normalized * Speed * Time.deltaTime;

        /** �� Ŭ������ ������ �̵� ���� */
        rigid.MovePosition(rigid.position + NextVec);

        /** ������ �ӷ� 0���� */
        rigid.velocity = Vector2.zero;
    }
}
