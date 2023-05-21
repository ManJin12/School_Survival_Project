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

    public float DiffDistance; 

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        /** 생성된 오브젝트 이름이 아이스 에로우라면 */
        if (gameObject.name == "IceArrow(Clone)")
        {
            /** 아이스 에로우 크기 조절 */
            this.transform.localScale = new Vector2(0.06f, 0.06f);
        }
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

    private void Update()
    {
        /** 캐릭터와 불릿사이의 거리 */
        DiffDistance = Vector2.Distance(GameManager.GMInstance.Player.transform.position, transform.position);

        if (DiffDistance >= 20.0f)
        {
            /** 속도를 0으로 해준다. */
            rigid.velocity = Vector2.zero;
            /** 총앟 비활성화 */
            gameObject.SetActive(false);
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

            /** 만약 이 오브젝트 이름이 IceArrow(Clone)이라면 */
            //if (this.gameObject.name == "IceArrow(Clone)")
            //{
            //    this.transform.localScale = new Vector2(0.04f, 0.04f);
            //}

            /** 총앟 비활성화 */
            gameObject.SetActive(false);
        }
    }
}
