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
    [Header("Mateo")]
    public float MateoDamage;
    public bool bIsMateo;
    public float MateoSkillTime;
    public float MateoSkillCoolTime;
    
    /** 아이스 에이지 스킬 관련 */
    [Header("IceAge")]
    public float IceAgeDamage;
    /** 아이스에이지 스킬 적용중인지 */
    public bool bIsIceAgeSkill;
    /** 아이스에이지 스킬 온 */
    public bool bIsIceAge;

    public float IceAgeSkillTime;
    public float IceAgeSkillCoolTime;

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
        /** 플레이 씬일때만 메테오가 true일 때 */
        if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsMateo)
        {
            /** 스킬 쿨타임 감소 */
            MateoSkillTime -= Time.deltaTime;

            /** 메테오 스킬 활성화 시 */
            // if (bIsMateo)
            // {
                /** 스킬쿨타임이 0보다 작거나 같으면 */
                if (MateoSkillTime <= 0)
                {
                    /** 다시 쿨타임 최대치로 바꿈 */
                    MateoSkillTime = MateoSkillCoolTime;
                    Debug.Log(MateoSkillTime);
                    /** 메테오 생성 로직 */
                    MakeMateo();
                }
           //  }
        }

        if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene && bIsIceAge)
        {
            
            IceAgeSkillTime -= Time.deltaTime;


        }
    }

    /** 메테오 생성 함수 정의 */
    void MakeMateo()
    {
        /** 메테오 생성 */
        GameObject Mateo = Instantiate(Skills[0]);
        /** 생성된 메테오의 위치 */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
    }
}
