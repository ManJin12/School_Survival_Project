using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using My;

public class SkillManager : MonoBehaviour
{
    public GameObject[] Skills;
    public float SkillTime = 2.0f;
    GameObject Player;

    public float MateoDamage;
    public bool bIsMateo = true;
  
    private void Awake()
    {
        Player = GameManager.GMInstance.Player;
    }

    // Start is called before the first frame update
    void Start()
    {
        /** PlayScene�̶�� */
        if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene)
        {
            /** GameManager�� Ŭ���� �Ѱ��� */
            GameManager.GMInstance.SkillManagerRef = this;
        }
        else
        {
            /** PlayScene�� �ƴ϶�� ��ũ��Ʈ ��Ȱ��ȭ */
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /** �÷��� ���϶��� */
        if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene)
        {
            /** ��ų ��Ÿ�� ���� */
            SkillTime -= Time.deltaTime;

            /** ���׿� ��ų Ȱ��ȭ �� */
            if (bIsMateo)
            {
                /** ��ų��Ÿ���� 0���� �۰ų� ������ */
                if (SkillTime <= 0)
                {
                    /** �ٽ� ��Ÿ�� �ִ�ġ�� �ٲ� */
                    SkillTime = 2.0f;
                    /** ���׿� ������ */
                    MateoDamage = 10.0f;
                    /** ���׿� ���� ���� */
                    MakeMateo();
                }
            }
        }
    }

    void MakeMateo()
    {
        /** ���׿� ���� */
        GameObject Mateo = Instantiate(Skills[0]);
        /** ������ ���׿��� ��ġ */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
        /** ���׿� ũ�� ���� */
        Mateo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
