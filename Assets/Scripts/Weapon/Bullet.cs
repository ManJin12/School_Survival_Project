using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class Bullet : MonoBehaviour
{
    /** 데미지 */
    public float m_Damage;
    /** 관통력 */
    public int m_Per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        /** m_Damage는 매개변수로 받은 _damage */
        m_Damage = damage;
        /** m_Per는 매개변수로 받은 _per */
        m_Per = per; 

        /** 관통이 -1(무한)보다 크면 */
        if (per > -1)
        {
            /** 불릿의 속도를 _dir로 한다. */
            rigid.velocity = dir * 10.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || m_Per == -1)
        {
            return;
        }
        /** 관통 -1 감소 */
        m_Per--;

        /** 관통력이 -1이 되면 관통이 다 되면 */
        if (m_Per == -1)
        {
            /** 속도를 0으로 해준다. */
            rigid.velocity = Vector2.zero;
            /** 총앟 비활성화 */
            gameObject.SetActive(false);

        }
    }
}
