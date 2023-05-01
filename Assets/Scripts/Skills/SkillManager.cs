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
    public float IceAgeSkillCoolTime = 10;

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
    GameObject Tornado;
    /** 토네이도 데미지 */
    public float TornadoDamage;
    /** 토네이도 활성화 여부 */
    public bool bIsTornado;
    /** 토네이도 생성 여부 */
    public bool bOnTornado;
    /** 감소되는 토네이도 스킬 재사용 시간 */
    public float TornadoSkillTime;
    /** 낙뢰를 재발동 시키기 위한 시간 */
    public float TornadoSkillCoolTime;

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


            /**  */
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

    }

    /** 메테오 생성 함수 정의 */
    void MakeMateo()
    {
        /** 메테오 생성 */
        GameObject Mateo = Instantiate(Skills[0]);
        /** 생성된 메테오의 위치 */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
    }

    void MakeLighining()
    {
        /** 스킬 배열 2번에 저장된 오브젝트를 생성 */
        GameObject Lightning = Instantiate(Skills[2]);
        /** 생성될 위치는 가장 가까운 적의 위치에 생성되게한다. */
        Lightning.transform.position = GameManager.GMInstance.ScannerRef.NearestTarget.position;

        Destroy(Lightning, 0.5f);
    }

    /** 토네이도 활성화 함수 */
    void MakeTorando()
    {
        /** 토네이도 생성 */
        Tornado = Instantiate(Skills[3]);
        Tornado.transform.position = GameManager.GMInstance.playerCtrl.transform.position;

        /** 토네이도 생성 중 */
        bOnTornado = true;

        /** 코루틴 호출 */
        StartCoroutine(TornadoDisabled());
    }

    /** 토네이도 함수 비활성화 코루틴 */
    IEnumerator TornadoDisabled()
    {
        yield return new WaitForSeconds(2.0f);

        /** 토네이도 생성 x */
        bOnTornado = false;
        /** 토네이도 오브젝트 삭제 */
        Destroy(Tornado);

    }

    /** 아이스에이지 활성화 함수 */
    public void EnabledIceAge()
    {
        /** 아이스에이지 엑티브 활성화 */
        IceAge.SetActive(true);
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
