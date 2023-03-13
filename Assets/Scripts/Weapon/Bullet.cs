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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
            /** �ѝ� ��Ȱ��ȭ */
            gameObject.SetActive(false);

        }
    }
}
