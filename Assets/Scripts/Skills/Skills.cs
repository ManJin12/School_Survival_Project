using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using My;

public class Skills : MonoBehaviour
{
    /** ��ų����Ʈ �迭 */
    public GameObject[] SkillEffect;
    /** ��ų ������� �Ÿ� */
    public float DestroySkillLength;
    /** �÷��̾� */
    public GameObject Player;
    /** ��ų id */
    public int Skills_ID;
    /** rigid */
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        if (GameManager.GMInstance.CurrentScene != ESceneType.PlayScene)
        {
            enabled = false;
            return;
        }
        /** �÷��̾� ã�� */
        Player = GameManager.GMInstance.Player;

        /** �̸��� Mateo(Clone)��� */
        if (gameObject.name == "Mateo(Clone)")
        {
            /** Skills_ID�� ��ų�� ����� �ε��� ��ȣ�� �ȴ� */
            Skills_ID = (int)ESkillType.Skill_Mateo;
            /** �߷°��� �������� �ش� */
            rigid.gravityScale = Random.Range(1, 3);
            /** ����� ������ y���� �������� �ش�. */
            DestroySkillLength = Random.Range(Player.transform.position.y - 4.5f, Player.transform.position.y + 4.5f);
            /** ũ�⸦ 0.5��ŭ�Ѵ� */
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    void Update()
    {
        /** �̸��� Mateo(Clone)�϶� */
        if (gameObject.name == "Mateo(Clone)")
        {
            /** ����Ʈ ���� */
            MakeMateoEffect();
        }
    }

    void MakeMateoEffect()
    {
        /** ������Ʈ�� y���� DestroySkillLength���� ������ */
        if (gameObject.transform.position.y < DestroySkillLength)
        {
            /** SkillEffect 0���� ����� ���ӿ�����Ʈ ���� */
            GameObject MateoEffect = Instantiate(SkillEffect[0]);
            /** ��ġ�� ������Ʈ�� ��ġ */
            MateoEffect.transform.position = gameObject.transform.position;
            /** ���׿� ����Ʈ ũ�� ���� */
            MateoEffect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            /** ��ų�� ����Ʈ ����� */
            Destroy(gameObject);
            Destroy(MateoEffect, 0.5f);
        }
    }

}

