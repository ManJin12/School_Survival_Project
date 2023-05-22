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
    /** 메테오 스킬 관련 */
    [Header("---Mateo---")]
    public float MateoDamage;
    public bool bIsMateo;
    public float MateoSkillTime;
    public float MateoSkillCoolTime;

    /** 아이스 에이지 스킬 관련 */
    [Header("---IceAge---")]
    public GameObject IceAge;
    public float IceAgeDamage;
    /** 아이스에이지 스킬 적용중인지 */
    public bool bIsIceAgeSkill;
    /** 아이스에이지 스킬 선택됬을때 */
    public bool bIsIceAge;
    /** 줄어드는 시간 */
    public float IceAgeOnTime;
    /** 스킬 유지 시간 */
    public float MaxIceAgeOnTime;
    /** 적용되는 스킬 시간 */
    public float IceAgeSkillTime;
    /** 초기화해주기 위한 스킬 시간 */
    public float IceAgeSkillCoolTime = 3;

    /** 낙뢰 스킬 관련 */
    [Header("---Lightning---")]
    /** 낙뢰 데미지 */
    public float LightningDamage;
    /** 낙뢰 활성화 여부 */
    public bool bIsLightning;
    /** 감소되는 낙뢰 스킬 재사용 시간 */
    public float LightningSkillTime;
    /** 낙뢰를 재발동 시키기 위한 시간  */
    public float LightningSkillCoolTime;

    /** 토네이도 스킬 관련 */
    [Header("---Tornado---")]
    /** 토네이도 데미지 */
    public float TornadoDamage;
    /** 토네이도 활성화 여부 */
    public bool bIsTornado;
    /** 토네이도 생성 여부 */
    public bool bOnTornado;
    public float CharDir;
    /** 감소되는 토네이도 스킬 재사용 시간 */
    public float TornadoSkillTime;
    /** 낙뢰를 재발동 시키기 위한 시간 */
    public float TornadoSkillCoolTime;
    GameObject Tornado;

    [Header("---IceArrow---")]
    public GameObject IceArrow;

    /**------------------------------Acher--------------------------------*/
    [Header("---Vortex---")]
    /** 토네이도 데미지 */
    public float VortexDamage;
    /** 토네이도 활성화 여부 */
    public bool bIsVortex;
    /** 토네이도 생성 여부 */
    public bool bOnVortex;
    public float AcherCharDir;
    /** 감소되는 토네이도 스킬 재사용 시간 */
    public float VortexSkillTime;
    /** 낙뢰를 재발동 시키기 위한 시간 */
    public float VortexSkillCoolTime;
    GameObject Vortex;

    [Header("---Huricane---")]
    public GameObject Huricane;
    public float HuricaneDamage;
    /** 아이스에이지 스킬 적용중인지 */
    public bool bIsHuricaneSkill;
    /** 아이스에이지 스킬 선택됬을때 */
    public bool bIsHuricane;
    /** 줄어드는 시간 */
    public float HuricaneOnTime;
    /** 스킬 유지 시간 */
    public float MaxHuricaneOnTime;
    /** 적용되는 스킬 시간 */
    public float HuricaneSkillTime;
    /** 초기화해주기 위한 스킬 시간 */
    public float HuricaneSkillCoolTime = 3;

    [Header("---WindSpirit---")]
    public GameObject WindSpirit;

    [Header("---Trap---")]
    public GameObject Trap;
    public float TrapDamage;
    public bool bIsTrap;
    public float TrapSkillTime;
    public float TrapCoolTime;

    /** 낙뢰 스킬 관련 */
    [Header("---ArrowRain---")]
    /** 낙뢰 데미지 */
    public float ArrowRainDamage;
    /** 낙뢰 활성화 여부 */
    public bool bIsArrowRain;
    /** 감소되는 낙뢰 스킬 재사용 시간 */
    public float ArrowRainSkillTime;
    /** 낙뢰를 재발동 시키기 위한 시간  */
    public float ArrowRainSkillCoolTime;

    /** 낙뢰 스킬 관련 */
    [Header("---BombArrow---")]
    /** 낙뢰 데미지 */
    public float BombArrowDamage;

    private void Awake()
    {
        Player = GameManager.GMInstance.Player;
    }

    // Start is called before the first frame update
    void Start()
    {
        /** PlayScene이라면 */
        if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene)
        {
            /** GameManager에 클래스 넘겨줌 */
            GameManager.GMInstance.SkillManagerRef = this;
        }
        else
        {
            /** PlayScene이 아니라면 스크립트 비활성화 */
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /** 만약 현재 캐릭터가 마법사일 때 */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** 플레이 씬일때만 메테오가 true일 때 */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsMateo)
            {
                /** 스킬 쿨타임 감소 */
                MateoSkillTime -= Time.deltaTime;

                /** 스킬쿨타임이 0보다 작거나 같으면 */
                if (MateoSkillTime <= 0)
                {
                    /** 다시 쿨타임 최대치로 바꿈 */
                    MateoSkillTime = MateoSkillCoolTime;
                    // Debug.Log(MateoSkillTime);
                    /** 메테오 생성 로직 */
                    MakeMateo();
                }

            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsMateo)

            /** 플레이 씬이고 낙뢰가 적용 되있을때 */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsLightning)
            {
                /** 낙뢰 스킬 쿨타임 */
                LightningSkillTime -= Time.deltaTime;

                /** 스킬 시간이 다 됬으면 */
                if (LightningSkillTime <= 0)
                {
                    /** 다시 스킬시간 초기화 */
                    LightningSkillTime = LightningSkillCoolTime;
                    /** 낙뢰 생성 */
                    MakeLighining();
                }
            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsLightning)


            /** 플레이 씬이고 아이스에이지가 적용되있을 때 */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsIceAge)
            {
                /** 스킬이 적용중이면 */
                if (bIsIceAgeSkill == true)
                {
                    /** 유지시간을 감소시킨다. */
                    IceAgeOnTime -= Time.deltaTime;

                    /** 유지시간이 종료되면 */
                    if (IceAgeOnTime <= 0.0f)
                    {
                        /** 비활성화 함수 호출 */
                        DisabledIceAge();
                    }
                }
                /** 스킬 적용중이 아니면 */
                else if (bIsIceAgeSkill == false)
                {
                    /** 스킬 쿨타임 감소 */
                    IceAgeSkillTime -= Time.deltaTime;

                    /** 스킬 쿨타임 시간이 0이되면 */
                    if (IceAgeSkillTime <= 0)
                    {
                        /** 스킬 쿨타임 초기화 */
                        IceAgeSkillTime = IceAgeSkillCoolTime;

                        /** 아이스에이지 활성화 함수 호출 */
                        EnabledIceAge();
                    }
                }
            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsIceAge)


            /** 토네이도 활성화가 되었다면? */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsTornado)
            {
                /** 생성중이 아닐 때  */
                if (bOnTornado == false)
                {
                    /** 스킬 쿨타임 감소 */
                    TornadoSkillTime -= Time.deltaTime;
                }

                /** 스킬 발동하는 시간 */
                if (TornadoSkillTime <= 0)
                {
                    /** 재발동을 위한 초기화 */
                    TornadoSkillTime = TornadoSkillCoolTime;
                    /** 토네이도 함수 호출 */
                    MakeTorando();
                }

            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsTornado)

        } // if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)

        /**--------------------------------------------------------------------------------------------*/

        /** 만약 현재 캐릭터가 궁수라면 */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsVortex)
            {
                /** 생성중이 아닐 때  */
                if (bOnVortex == false)
                {
                    /** 스킬 쿨타임 감소 */
                    VortexSkillTime -= Time.deltaTime;
                }

                /** 스킬 발동하는 시간 */
                if (VortexSkillTime <= 0)
                {
                    /** 재발동을 위한 초기화 */
                    VortexSkillTime = VortexSkillCoolTime;
                    /** 토네이도 함수 호출 */
                    MakeVortex();
                }
            } // if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar && bIsVortex)

            /** 플레이 씬이고 허리케인이 적용되있을 때 */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsHuricane)
            {
                /** 스킬이 적용중이면 */
                if (bIsHuricaneSkill == true)
                {
                    /** 유지시간을 감소시킨다. */
                    HuricaneOnTime -= Time.deltaTime;

                    /** 유지시간이 종료되면 */
                    if (HuricaneOnTime <= 0.0f)
                    {
                        /** 비활성화 함수 호출 */
                        DisabledHuricane();
                    }
                }
                /** 스킬 적용중이 아니면 */
                else if (bIsHuricaneSkill == false)
                {
                    /** 스킬 쿨타임 감소 */
                    HuricaneSkillTime -= Time.deltaTime;

                    /** 스킬 쿨타임 시간이 0이되면 */
                    if (HuricaneSkillTime <= 0)
                    {
                        /** 스킬 쿨타임 초기화 */
                        HuricaneSkillTime = HuricaneSkillCoolTime;

                        /** 허리케인이 활성화 함수 호출 */
                        EnabledHuricane();
                    }
                }

            } // if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsHuricane)

            /** 플레이 씬일때만 Trap이 true일 때 */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsTrap)
            {
                /** 스킬 쿨타임 감소 */
                TrapSkillTime -= Time.deltaTime;

                /** 스킬쿨타임이 0보다 작거나 같으면 */
                if (TrapSkillTime <= 0)
                {
                    /** 다시 쿨타임 최대치로 바꿈 */
                    TrapSkillTime = TrapCoolTime;
                    // Debug.Log(MateoSkillTime);
                    /** Trap 생성 로직 */
                    MakeTrap();
                }
            } //  if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)

            /**플레이 씬이고 화살 비가 적용 되있을때 */
            if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsArrowRain)
            {
                /** 화살 비 스킬 쿨타임 */
                ArrowRainSkillTime -= Time.deltaTime;

                /** 스킬 시간이 다 됬으면 */
                if (ArrowRainSkillTime <= 0)
                {
                    /** 다시 스킬시간 초기화 */
                    ArrowRainSkillTime = ArrowRainSkillCoolTime;
                    /** 낙뢰 생성 */
                    MakeArrowRain();
                }
            } // iif (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsArrowRain)

        }
    }

    /** 메테오 생성 함수 정의 */
    void MakeMateo()
    {
        /** 메테오 생성 */
        GameObject Mateo = Instantiate(Skills[0]);
        /** 효과음 생성 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Mateo);
        /** 생성된 메테오의 위치 */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
    }

    void MakeTrap()
    {
        /** 트랩 생성 */
        GameObject Trap = Instantiate(Skills[2]);

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Trap);

        Vector3 PlayerPos = GameManager.GMInstance.Player.transform.position;

        /** 생성된 트랩의 위치 */
        Trap.transform.position = PlayerPos;
    }

    void MakeLighining()
    {
        /** 가까운적이 없다면 */
        if (!GameManager.GMInstance.ScannerRef.NearestTarget)
        {
            return;
        }

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Lightning);

        /** 스킬 배열 2번에 저장된 오브젝트를 생성 */
        GameObject Lightning = Instantiate(Skills[2]);
        /** 생성될 위치는 가장 가까운 적의 위치에 생성되게한다. */
        Lightning.transform.position = GameManager.GMInstance.ScannerRef.NearestTarget.position;

        Destroy(Lightning, 0.5f);
    }

    void MakeArrowRain()
    {
        /** 가까운적이 없다면 */
        if (!GameManager.GMInstance.ScannerRef.NearestTarget)
        {
            return;
        }

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.ArrowRain);

        /** 스킬 배열 3번에 저장된 오브젝트를 생성 */
        GameObject ArrowRain = Instantiate(Skills[3]);
        /** 생성될 위치는 가장 가까운 적의 위치에 생성되게한다. */
        ArrowRain.transform.position = GameManager.GMInstance.ScannerRef.NearestTarget.position;
        Destroy(ArrowRain, 1.0f);
    }


    /** 토네이도 활성화 함수 */
    void MakeTorando()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Tornado);

        /** 토네이도 생성 */
        Tornado = Instantiate(Skills[3]);

        Tornado.transform.position = GameManager.GMInstance.playerCtrl.transform.position;
        Tornado.transform.localScale = new Vector2(5.0f, 5.0f);
        
        /** 토네이도 생성 중 */
        bOnTornado = true;

        /** 코루틴 호출 */
        StartCoroutine(TornadoDisabled());
    }

    /** 토네이도 오브젝트 삭제 코루틴 */
    IEnumerator TornadoDisabled()
    {
        yield return new WaitForSeconds(2.0f);

        /** 토네이도 생성 x */
        bOnTornado = false;
        /** 토네이도 오브젝트 삭제 */
        Destroy(Tornado);

    }

    /** 볼텍스 활성화 함수 */
    void MakeVortex()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Vortex);

        /** 볼텍스 생성 */
        Vortex = Instantiate(Skills[0]);

        Vortex.transform.position = GameManager.GMInstance.playerCtrl.transform.position;
        Vortex.transform.localScale = new Vector2(2.0f, 2.0f);

        /** 볼텍스 생성 중 */
        bOnTornado = true;

        /** 코루틴 호출 */
        StartCoroutine(VortexDisabled());
    }

    /** 볼텍스 오브젝트 삭제 코루틴 */
    IEnumerator VortexDisabled()
    {
        yield return new WaitForSeconds(2.0f);

        /** 볼텍스 생성 x */
        bOnVortex = false;
        /** 볼텍스 오브젝트 삭제 */
        Destroy(Vortex);

    }

    /** 허리케인 활성화 함수 */
    public void EnabledHuricane()
    {
        /** 효과음 생성 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Huricane);

        /** 허리케인 엑티브 활성화 */
        Huricane.SetActive(true);
        /** 스킬 실행중 */
        bIsHuricaneSkill = true;
    }

    /** 허리케인 비활성화 함수 */
    void DisabledHuricane()
    {
        /** 만약 스킬 유지시간이 0이 됬다면 */
        if (HuricaneOnTime <= 0)
        {
            /** 허리케인 비활성화 */
            Huricane.SetActive(false);
            /** 스킬 비활성화 */
            bIsHuricaneSkill = false;
            /** 유지시간 초기화 */
            HuricaneOnTime = MaxHuricaneOnTime;
        }
    }

    /** 아이스에이지 활성화 함수 */
    public void EnabledIceAge()
    {
        /** 아이스에이지 엑티브 활성화 */
        IceAge.SetActive(true);
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.IceAge);

        /** 스킬 실행중 */
        bIsIceAgeSkill = true;
    }

    /** 아이스에이지 비활성화 함수 */
    void DisabledIceAge()
    {  
       /** 만약 스킬 유지시간이 0이 됬다면 */
       if (IceAgeOnTime <= 0)
       {
           /** 아이스에이지 비활성화 */
           IceAge.SetActive(false);
           /** 스킬 비활성화 */
           bIsIceAgeSkill = false;
           /** 유지시간 초기화 */
           IceAgeOnTime = MaxIceAgeOnTime;
       }
    }
}
