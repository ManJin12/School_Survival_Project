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

    /**------------------------------Wizard--------------------------------*/
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
    public float IceAgeSkillCoolTime = 3;

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
    /** ����̵� ������ */
    public float TornadoDamage;
    /** ����̵� Ȱ��ȭ ���� */
    public bool bIsTornado;
    /** ����̵� ���� ���� */
    public bool bOnTornado;
    public float CharDir;
    /** ���ҵǴ� ����̵� ��ų ���� �ð� */
    public float TornadoSkillTime;
    /** ���ڸ� ��ߵ� ��Ű�� ���� �ð� */
    public float TornadoSkillCoolTime;
    GameObject Tornado;

    [Header("---IceArrow---")]
    public GameObject IceArrow;

    /**------------------------------Acher--------------------------------*/
    [Header("---Vortex---")]
    /** ����̵� ������ */
    public float VortexDamage;
    /** ����̵� Ȱ��ȭ ���� */
    public bool bIsVortex;
    /** ����̵� ���� ���� */
    public bool bOnVortex;
    public float AcherCharDir;
    /** ���ҵǴ� ����̵� ��ų ���� �ð� */
    public float VortexSkillTime;
    /** ���ڸ� ��ߵ� ��Ű�� ���� �ð� */
    public float VortexSkillCoolTime;
    GameObject Vortex;

    [Header("---Huricane---")]
    public GameObject Huricane;
    public float HuricaneDamage;
    /** ���̽������� ��ų ���������� */
    public bool bIsHuricaneSkill;
    /** ���̽������� ��ų ���É����� */
    public bool bIsHuricane;
    /** �پ��� �ð� */
    public float HuricaneOnTime;
    /** ��ų ���� �ð� */
    public float MaxHuricaneOnTime;
    /** ����Ǵ� ��ų �ð� */
    public float HuricaneSkillTime;
    /** �ʱ�ȭ���ֱ� ���� ��ų �ð� */
    public float HuricaneSkillCoolTime = 3;

    [Header("---WindSpirit---")]
    public GameObject WindSpirit;

    [Header("---Trap---")]
    public GameObject Trap;
    public float TrapDamage;
    public bool bIsTrap;
    public float TrapSkillTime;
    public float TrapCoolTime;

    /** ���� ��ų ���� */
    [Header("---ArrowRain---")]
    /** ���� ������ */
    public float ArrowRainDamage;
    /** ���� Ȱ��ȭ ���� */
    public bool bIsArrowRain;
    /** ���ҵǴ� ���� ��ų ���� �ð� */
    public float ArrowRainSkillTime;
    /** ���ڸ� ��ߵ� ��Ű�� ���� �ð�  */
    public float ArrowRainSkillCoolTime;

    /** ���� ��ų ���� */
    [Header("---BombArrow---")]
    /** ���� ������ */
    public float BombArrowDamage;

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


            /** ����̵� Ȱ��ȭ�� �Ǿ��ٸ�? */
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

        /**--------------------------------------------------------------------------------------------*/

        /** ���� ���� ĳ���Ͱ� �ü���� */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsVortex)
            {
                /** �������� �ƴ� ��  */
                if (bOnVortex == false)
                {
                    /** ��ų ��Ÿ�� ���� */
                    VortexSkillTime -= Time.deltaTime;
                }

                /** ��ų �ߵ��ϴ� �ð� */
                if (VortexSkillTime <= 0)
                {
                    /** ��ߵ��� ���� �ʱ�ȭ */
                    VortexSkillTime = VortexSkillCoolTime;
                    /** ����̵� �Լ� ȣ�� */
                    MakeVortex();
                }
            } // if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar && bIsVortex)

            /** �÷��� ���̰� �㸮������ ��������� �� */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsHuricane)
            {
                /** ��ų�� �������̸� */
                if (bIsHuricaneSkill == true)
                {
                    /** �����ð��� ���ҽ�Ų��. */
                    HuricaneOnTime -= Time.deltaTime;

                    /** �����ð��� ����Ǹ� */
                    if (HuricaneOnTime <= 0.0f)
                    {
                        /** ��Ȱ��ȭ �Լ� ȣ�� */
                        DisabledHuricane();
                    }
                }
                /** ��ų �������� �ƴϸ� */
                else if (bIsHuricaneSkill == false)
                {
                    /** ��ų ��Ÿ�� ���� */
                    HuricaneSkillTime -= Time.deltaTime;

                    /** ��ų ��Ÿ�� �ð��� 0�̵Ǹ� */
                    if (HuricaneSkillTime <= 0)
                    {
                        /** ��ų ��Ÿ�� �ʱ�ȭ */
                        HuricaneSkillTime = HuricaneSkillCoolTime;

                        /** �㸮������ Ȱ��ȭ �Լ� ȣ�� */
                        EnabledHuricane();
                    }
                }

            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsHuricane)

            /** �÷��� ���϶��� Trap�� true�� �� */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsTrap)
            {
                /** ��ų ��Ÿ�� ���� */
                TrapSkillTime -= Time.deltaTime;

                /** ��ų��Ÿ���� 0���� �۰ų� ������ */
                if (TrapSkillTime <= 0)
                {
                    /** �ٽ� ��Ÿ�� �ִ�ġ�� �ٲ� */
                    TrapSkillTime = TrapCoolTime;
                    // Debug.Log(MateoSkillTime);
                    /** Trap ���� ���� */
                    MakeTrap();
                }
            } //  if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)

            /**�÷��� ���̰� ȭ�� �� ���� �������� */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsArrowRain)
            {
                /** ȭ�� �� ��ų ��Ÿ�� */
                ArrowRainSkillTime -= Time.deltaTime;

                /** ��ų �ð��� �� ������ */
                if (ArrowRainSkillTime <= 0)
                {
                    /** �ٽ� ��ų�ð� �ʱ�ȭ */
                    ArrowRainSkillTime = ArrowRainSkillCoolTime;
                    /** ���� ���� */
                    MakeArrowRain();
                }
            } // iif (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsArrowRain)

        }
    }

    /** ���׿� ���� �Լ� ���� */
    void MakeMateo()
    {
        /** ���׿� ���� */
        GameObject Mateo = Instantiate(Skills[0]);
        /** ȿ���� ���� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Mateo);
        /** ������ ���׿��� ��ġ */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
    }

    void MakeTrap()
    {
        /** Ʈ�� ���� */
        GameObject Trap = Instantiate(Skills[2]);

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Trap);

        Vector3 PlayerPos = GameManager.GMInstance.Player.transform.position;

        /** ������ Ʈ���� ��ġ */
        Trap.transform.position = PlayerPos;
    }

    void MakeLighining()
    {
        /** ��������� ���ٸ� */
        if (!GameManager.GMInstance.ScannerRef.NearestTarget)
        {
            return;
        }

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Lightning);

        /** ��ų �迭 2���� ����� ������Ʈ�� ���� */
        GameObject Lightning = Instantiate(Skills[2]);
        /** ������ ��ġ�� ���� ����� ���� ��ġ�� �����ǰ��Ѵ�. */
        Lightning.transform.position = GameManager.GMInstance.ScannerRef.NearestTarget.position;

        Destroy(Lightning, 0.5f);
    }

    void MakeArrowRain()
    {
        /** ��������� ���ٸ� */
        if (!GameManager.GMInstance.ScannerRef.NearestTarget)
        {
            return;
        }

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowRain);

        /** ��ų �迭 3���� ����� ������Ʈ�� ���� */
        GameObject ArrowRain = Instantiate(Skills[3]);
        /** ������ ��ġ�� ���� ����� ���� ��ġ�� �����ǰ��Ѵ�. */
        ArrowRain.transform.position = GameManager.GMInstance.ScannerRef.NearestTarget.position;
        Destroy(ArrowRain, 1.0f);
    }


    /** ����̵� Ȱ��ȭ �Լ� */
    void MakeTorando()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Tornado);

        /** ����̵� ���� */
        Tornado = Instantiate(Skills[3]);

        Tornado.transform.position = GameManager.GMInstance.playerCtrl.transform.position;
        Tornado.transform.localScale = new Vector2(5.0f, 5.0f);
        
        /** ����̵� ���� �� */
        bOnTornado = true;

        /** �ڷ�ƾ ȣ�� */
        StartCoroutine(TornadoDisabled());
    }

    /** ����̵� ������Ʈ ���� �ڷ�ƾ */
    IEnumerator TornadoDisabled()
    {
        yield return new WaitForSeconds(2.0f);

        /** ����̵� ���� x */
        bOnTornado = false;
        /** ����̵� ������Ʈ ���� */
        Destroy(Tornado);

    }

    /** ���ؽ� Ȱ��ȭ �Լ� */
    void MakeVortex()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Vortex);

        /** ���ؽ� ���� */
        Vortex = Instantiate(Skills[0]);

        Vortex.transform.position = GameManager.GMInstance.playerCtrl.transform.position;
        Vortex.transform.localScale = new Vector2(2.0f, 2.0f);

        /** ���ؽ� ���� �� */
        bOnTornado = true;

        /** �ڷ�ƾ ȣ�� */
        StartCoroutine(VortexDisabled());
    }

    /** ���ؽ� ������Ʈ ���� �ڷ�ƾ */
    IEnumerator VortexDisabled()
    {
        yield return new WaitForSeconds(2.0f);

        /** ���ؽ� ���� x */
        bOnVortex = false;
        /** ���ؽ� ������Ʈ ���� */
        Destroy(Vortex);

    }

    /** �㸮���� Ȱ��ȭ �Լ� */
    public void EnabledHuricane()
    {
        /** ȿ���� ���� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Huricane);

        /** �㸮���� ��Ƽ�� Ȱ��ȭ */
        Huricane.SetActive(true);
        /** ��ų ������ */
        bIsHuricaneSkill = true;
    }

    /** �㸮���� ��Ȱ��ȭ �Լ� */
    void DisabledHuricane()
    {
        /** ���� ��ų �����ð��� 0�� ��ٸ� */
        if (HuricaneOnTime <= 0)
        {
            /** �㸮���� ��Ȱ��ȭ */
            Huricane.SetActive(false);
            /** ��ų ��Ȱ��ȭ */
            bIsHuricaneSkill = false;
            /** �����ð� �ʱ�ȭ */
            HuricaneOnTime = MaxHuricaneOnTime;
        }
    }

    /** ���̽������� Ȱ��ȭ �Լ� */
    public void EnabledIceAge()
    {
        /** ���̽������� ��Ƽ�� Ȱ��ȭ */
        IceAge.SetActive(true);
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.IceAge);

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
