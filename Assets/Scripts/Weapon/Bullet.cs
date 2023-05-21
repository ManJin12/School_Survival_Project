using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class Bullet : MonoBehaviour
{
    /** ������ */
    public float m_Damage;
    /** ����� */
    public int m_Per;

    Rigidbody2D rigid;

    public float DiffDistance; 

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        /** ������ ������Ʈ �̸��� ���̽� ���ο��� */
        if (gameObject.name == "IceArrow(Clone)")
        {
            /** ���̽� ���ο� ũ�� ���� */
            this.transform.localScale = new Vector2(0.06f, 0.06f);
        }
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        /** m_Damage�� �Ű������� ���� _damage */
        m_Damage = damage;
        /** m_Per�� �Ű������� ���� _per */
        m_Per = per; 

        /** ������ -1(����)���� ũ�� */
        if (per > -1)
        {
            /** �Ҹ��� �ӵ��� _dir�� �Ѵ�. */
            rigid.velocity = dir * 10.0f;
        }
    }

    private void Update()
    {
        /** ĳ���Ϳ� �Ҹ������� �Ÿ� */
        DiffDistance = Vector2.Distance(GameManager.GMInstance.Player.transform.position, transform.position);

        if (DiffDistance >= 20.0f)
        {
            /** �ӵ��� 0���� ���ش�. */
            rigid.velocity = Vector2.zero;
            /** �ѝ� ��Ȱ��ȭ */
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || m_Per == -1)
        {
            return;
        }

        /** ���� -1 ���� */
        m_Per--;

        /** ������� -1�� �Ǹ� ������ �� �Ǹ� */
        if (m_Per == -1)
        {
            /** �ӵ��� 0���� ���ش�. */
            rigid.velocity = Vector2.zero;

            /** ���� �� ������Ʈ �̸��� IceArrow(Clone)�̶�� */
            //if (this.gameObject.name == "IceArrow(Clone)")
            //{
            //    this.transform.localScale = new Vector2(0.04f, 0.04f);
            //}

            /** �ѝ� ��Ȱ��ȭ */
            gameObject.SetActive(false);
        }
    }
}
