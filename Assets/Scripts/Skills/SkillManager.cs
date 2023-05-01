using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using My;

public class SkillManager : MonoBehaviour
{
    public GameObject[] Skills;
    GameObject Player;

    /** ���׿� ��ų ���� */
    [Header("---Mateo---")]
    public float MateoDamage;
    public bool bIsMateo;
    public float MateoSkillTime;
    public float MateoSkillCoolTime;

    /** ���̽� ������ ��ų ���� */
    [Header("---IceAge---")]
    public GameObject IceAge;
    public float IceAgeDamage;
    /** ���̽������� ��ų ���������� */
    public bool bIsIceAgeSkill;
    /** ���̽������� ��ų ���É����� */
    public bool bIsIceAge;
    /** �پ��� �ð� */
    public float IceAgeOnTime;
    /** ��ų ���� �ð� */
    public float MaxIceAgeOnTime;
    /** ����Ǵ� ��ų �ð� */
    public float IceAgeSkillTime;
    /** �ʱ�ȭ���ֱ� ���� ��ų �ð� */
    public float IceAgeSkillCoolTime = 10;

    /** ���� ��ų ���� */
    [Header("---Lightning---")]
    /** ���� ������ */
    public float LightningDamage;
    /** ���� Ȱ��ȭ ���� */
    public bool bIsLightning;
    /** ���ҵǴ� ���� ��ų ���� �ð� */
    public float LightningSkillTime;
    /** ���ڸ� ��ߵ� ��Ű�� ���� �ð�  */
    public float LightningSkillCoolTime;

    /** ����̵� ��ų ���� */
    [Header("---Tornado---")]
    GameObject Tornado;
    /** ����̵� ������ */
    public float TornadoDamage;
    /** ����̵� Ȱ��ȭ ���� */
    public bool bIsTornado;
    /** ����̵� ���� ���� */
    public bool bOnTornado;
    /** ���ҵǴ� ����̵� ��ų ���� �ð� */
    public float TornadoSkillTime;
    /** ���ڸ� ��ߵ� ��Ű�� ���� �ð� */
    public float TornadoSkillCoolTime;

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
        /** ���� ���� ĳ���Ͱ� �������� �� */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** �÷��� ���϶��� ���׿��� true�� �� */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsMateo)
            {
                /** ��ų ��Ÿ�� ���� */
                MateoSkillTime -= Time.deltaTime;

                /** ��ų��Ÿ���� 0���� �۰ų� ������ */
                if (MateoSkillTime <= 0)
                {
                    /** �ٽ� ��Ÿ�� �ִ�ġ�� �ٲ� */
                    MateoSkillTime = MateoSkillCoolTime;
                    // Debug.Log(MateoSkillTime);
                    /** ���׿� ���� ���� */
                    MakeMateo();
                }

            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsMateo)

            /** �÷��� ���̰� ���ڰ� ���� �������� */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsLightning)
            {
                /** ���� ��ų ��Ÿ�� */
                LightningSkillTime -= Time.deltaTime;

                /** ��ų �ð��� �� ������ */
                if (LightningSkillTime <= 0)
                {
                    /** �ٽ� ��ų�ð� �ʱ�ȭ */
                    LightningSkillTime = LightningSkillCoolTime;
                    /** ���� ���� */
                    MakeLighining();
                }
            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsLightning)


            /** �÷��� ���̰� ���̽��������� ��������� �� */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsIceAge)
            {
                /** ��ų�� �������̸� */
                if (bIsIceAgeSkill == true)
                {
                    /** �����ð��� ���ҽ�Ų��. */
                    IceAgeOnTime -= Time.deltaTime;

                    /** �����ð��� ����Ǹ� */
                    if (IceAgeOnTime <= 0.0f)
                    {
                        /** ��Ȱ��ȭ �Լ� ȣ�� */
                        DisabledIceAge();
                    }
                }
                /** ��ų �������� �ƴϸ� */
                else if (bIsIceAgeSkill == false)
                {
                    /** ��ų ��Ÿ�� ���� */
                    IceAgeSkillTime -= Time.deltaTime;

                    /** ��ų ��Ÿ�� �ð��� 0�̵Ǹ� */
                    if (IceAgeSkillTime <= 0)
                    {
                        /** ��ų ��Ÿ�� �ʱ�ȭ */
                        IceAgeSkillTime = IceAgeSkillCoolTime;

                        /** ���̽������� Ȱ��ȭ �Լ� ȣ�� */
                        EnabledIceAge();
                    }
                }
            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsIceAge)


            /**  */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsTornado)
            {
                /** �������� �ƴ� ��  */
                if (bOnTornado == false)
                {
                    /** ��ų ��Ÿ�� ���� */
                    TornadoSkillTime -= Time.deltaTime;
                }

                /** ��ų �ߵ��ϴ� �ð� */
                if (TornadoSkillTime <= 0)
                {
                    /** ��ߵ��� ���� �ʱ�ȭ */
                    TornadoSkillTime = TornadoSkillCoolTime;
                    /** ����̵� �Լ� ȣ�� */
                    MakeTorando();
                }

            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsTornado)

        } // if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)

    }

    /** ���׿� ���� �Լ� ���� */
    void MakeMateo()
    {
        /** ���׿� ���� */
        GameObject Mateo = Instantiate(Skills[0]);
        /** ������ ���׿��� ��ġ */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
    }

    void MakeLighining()
    {
        /** ��ų �迭 2���� ����� ������Ʈ�� ���� */
        GameObject Lightning = Instantiate(Skills[2]);
        /** ������ ��ġ�� ���� ����� ���� ��ġ�� �����ǰ��Ѵ�. */
        Lightning.transform.position = GameManager.GMInstance.ScannerRef.NearestTarget.position;

        Destroy(Lightning, 0.5f);
    }

    /** ����̵� Ȱ��ȭ �Լ� */
    void MakeTorando()
    {
        /** ����̵� ���� */
        Tornado = Instantiate(Skills[3]);
        Tornado.transform.position = GameManager.GMInstance.playerCtrl.transform.position;

        /** ����̵� ���� �� */
        bOnTornado = true;

        /** �ڷ�ƾ ȣ�� */
        StartCoroutine(TornadoDisabled());
    }

    /** ����̵� �Լ� ��Ȱ��ȭ �ڷ�ƾ */
    IEnumerator TornadoDisabled()
    {
        yield return new WaitForSeconds(2.0f);

        /** ����̵� ���� x */
        bOnTornado = false;
        /** ����̵� ������Ʈ ���� */
        Destroy(Tornado);

    }

    /** ���̽������� Ȱ��ȭ �Լ� */
    public void EnabledIceAge()
    {
        /** ���̽������� ��Ƽ�� Ȱ��ȭ */
        IceAge.SetActive(true);
        /** ��ų ������ */
        bIsIceAgeSkill = true;
    }

    /** ���̽������� ��Ȱ��ȭ �Լ� */
    void DisabledIceAge()
    {  
       /** ���� ��ų �����ð��� 0�� ��ٸ� */
       if (IceAgeOnTime <= 0)
       {
           /** ���̽������� ��Ȱ��ȭ */
           IceAge.SetActive(false);
           /** ��ų ��Ȱ��ȭ */
           bIsIceAgeSkill = false;
           /** �����ð� �ʱ�ȭ */
           IceAgeOnTime = MaxIceAgeOnTime;
       }
    }
}
