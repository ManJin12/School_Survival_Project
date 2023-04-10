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
        /** 플레이 씬일때만 */
        if (GameManager.GMInstance.CurrentScene == ESceneType.PlayScene)
        {
            /** 스킬 쿨타임 감소 */
            SkillTime -= Time.deltaTime;

            /** 메테오 스킬 활성화 시 */
            if (bIsMateo)
            {
                /** 스킬쿨타임이 0보다 작거나 같으면 */
                if (SkillTime <= 0)
                {
                    /** 다시 쿨타임 최대치로 바꿈 */
                    SkillTime = 2.0f;
                    /** 메테오 데미지 */
                    MateoDamage = 10.0f;
                    /** 메테오 생성 로직 */
                    MakeMateo();
                }
            }
        }
    }

    void MakeMateo()
    {
        /** 메테오 생성 */
        GameObject Mateo = Instantiate(Skills[0]);
        /** 생성된 메테오의 위치 */
        Mateo.transform.position = new Vector3(Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f), transform.position.y, 0);
        /** 메테오 크기 조절 */
        Mateo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
