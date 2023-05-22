using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    float m_EffTime = 0.0f; //���� �ð� ���� ����
    public Text m_DamageText;

    float MvVelocity = 1.1f / 1.05f; // 1.05�� ���ȿ� 1.1m ���ٸ�.. �ӵ�
    float ApVelocity = 1.0f / (1.0f - 0.4f); // Alpha 0.4(0.0f)�ʺ��� ���� 1.0��(1.0f)����

    Vector3 m_CurPos; // ��ġ���� ����
    Color m_Color; // �÷� ���� ����

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        m_EffTime += Time.deltaTime;

        if (m_EffTime < 1.05f)
        {
            m_CurPos = m_DamageText.transform.position;
            m_CurPos.y += Time.deltaTime * MvVelocity;

            m_DamageText.transform.position = m_CurPos;
        }

        if (0.4 < m_EffTime)
        {
            m_Color = m_DamageText.color;
            m_Color.a -= (Time.deltaTime * ApVelocity);

            if (m_Color.a < 0.0f)
                m_Color.a = 0.0f;
            m_DamageText.color = m_Color;
        }

        if (1.05f <= m_EffTime)
        {
            Destroy(gameObject);
        }
    }

    public void initDamge(float a_Damage, Color a_Color)
    {
        if (m_DamageText != null)
            m_DamageText = GetComponentInChildren<Text>();

        if (a_Damage <= 0.0f)
        {
            int a_Dmg = (int)Mathf.Abs(a_Damage); //Abs = ���밪 �Լ�
            m_DamageText.text = "" + a_Dmg;
        }
        else
            m_DamageText.text = "" + (int)a_Damage;

        a_Color.a = 1.0f;
        m_DamageText.color = a_Color;
    }
}
