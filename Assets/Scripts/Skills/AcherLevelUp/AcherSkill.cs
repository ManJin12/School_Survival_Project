using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;
using UnityEngine.UI;

public class AcherSkill : MonoBehaviour
{
    public SkillData data;
    public int level = 0;
    Weapon weapon;
    Weapon FireBallWeapon;

    public PassiveSkill passiveSkill;

    public GameObject FireBallObj;
    public SkillData FireBallData;
    bool bCanfirstAttack;


    float CurrentDamage;
    float nextDamage;
    int nextCount = 0;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.Skill_Icon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.Skill_Name;

    }

    void Start()
    {

    }

    void OnEnable()
    {
        /** 다음 스킬레벨을 보여준다. */
        textLevel.text = "Lv." + (level + 1);

        /** 스킬별 설명 */
        switch (data.skillType)
        {
            /** 스킬 속도 증가나 이동 속도 증가를 선택했을때 */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** 스킬 설명을 밑과 같이 적용함 */
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100);
                break;

            default:
                textDesc.text = string.Format(data.Skill_Desc);
                break;
        }
    }

    public void OnClick()
    {
        switch (data.skillType)
        {
            /** 스킬 속도 증가나 이동 속도 증가를 선택했을때 */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** 스킬레벨이 0이라면 */
                if (level == 0)
                {
                    /** newPassvieSkill이라는 빈게임 오브젝트 생성 */
                    GameObject newPassvieSkill = new GameObject();
                    /** 생성된 newPassvieSkill 빈게임 오브젝트에 추가된 PassiveSkill을 passvieSkill에 적용한다. */
                    passiveSkill = newPassvieSkill.AddComponent<PassiveSkill>();
                    /** PassiveSkill의 Init함수에 data를 매개변수로 호출 */
                    passiveSkill.Init(data);
                }
                /** 레벨이 0이 아니라면 */
                else
                {
                    /** 다음 다음 증가비율은 data에 저장된 값을 불러온다. */
                    float nextRate = data.damages[level];
                    /** PassiveSkill의 LevelUp함수에 nextRate를 매개변수로 호출한다. */
                    passiveSkill.LevelUp(nextRate);
                }
                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;

                level++;
                break;
            default:
                break;

        }

        /** 최대 레벨 도달 시 */
        if (level == data.damages.Length)
        {
            /** 버튼 누를 수 없게 비활성화 */
            GetComponent<Button>().interactable = false;
        }
    }
}

