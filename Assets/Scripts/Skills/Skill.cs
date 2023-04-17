using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;

public class Skill : MonoBehaviour
{
    public SkillData data;
    public int level = 0;
    public Weapon weapon;
    public PassiveSkill passiveSkill;

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
        GameManager.GMInstance.SkillRef = this;
    }

    void OnEnable()
    {
        /** 다음 스킬레벨을 보여준다. */
        textLevel.text = "Lv." + (level + 1);

        /** 스킬별 설명 */
        switch(data.skillType)
        {
            /** 파이어볼이나 전기구체를 선택했을때 */
            case SkillData.SkillType.Skill_ElectricBall:
            case SkillData.SkillType.Skill_FireBall:
                /** 스킬 설명을 밑과 같이 적용함 / 전기구체 Count : 구체의 개수 */
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                break;
            /** 스킬 속도 증가나 이동 속도 증가를 선택했을때 */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** 스킬 설명을 밑과 같이 적용함 */
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100);
                break;
            /** 메테오 스킬을 선택했을 때 */
            case SkillData.SkillType.Skill_Meteo:
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                break;
            default:
                textDesc.text = string.Format(data.Skill_Desc);
                break;
        } 
    }
    
    public void OnClick()
    {
        switch(data.skillType)
        {
            /** 전기구체나 파이어 볼을 선택했을때 */
            case SkillData.SkillType.Skill_ElectricBall:
            case SkillData.SkillType.Skill_FireBall:
                /** 만약 스킬레벨이 0이라면 */
                if (level == 0)
                {
                    /** 빈 게임 오브젝트 생성 */
                    GameObject newWeapon = new GameObject();
                    /** weapon은 생성된 newWeapon에 추가해준 Weapon Comoponent를 사용한다. */
                    weapon = newWeapon.AddComponent<Weapon>();
                    /** weapon에 있는 Init함수를 data를 매개변수로 호출한다. */
                    weapon.Init(data);
                }
                /** 스킬레벨이 0이 아니라면 */
                else
                {
                    /** 전기구체를 눌렀다면 */
                    if (data.skillType == SkillData.SkillType.Skill_ElectricBall)
                    {
                        /** 다음 전기구체의 개수는 Skilldata의 count배열의 적용된 레벨의 크기의 count가 불러와진다. */
                        nextCount = data.counts[level];
                    }

                    /** 만약 현재 데미지가 0이면 */
                    if (CurrentDamage == 0)
                    {
                        /** 다음 데미지는 SkillData에 저장된 데미지에서 저장된 데미지증가만큼 곱한 값으로 적용된다. */
                        nextDamage += data.baseDamage * data.damages[level];
                    }
                    /** 현재 데미지가 0이 아니라면 */
                    else
                    {
                        /** 다음 데미지는 현재 데미지에서 데미지 증가치만큼 곱한 값으로 적용된다. */
                        nextDamage = CurrentDamage * data.damages[level];
                    }

                    /** 현재 데미지는 값이 계산된 nextDamage로 변경 */
                    CurrentDamage = nextDamage;
                    Debug.Log(nextDamage);

                    /** Weapon.cs의 Levelup함수에 nextDamage와 nextCount를 매개변수로 호출 */
                    weapon.Levelup(nextDamage, nextCount);
                }

                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;

                /** 스킬레벨 증가 */
                level++;

                break;

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

            /** Hill을 누르면 */
            case SkillData.SkillType.Skill_Hill:
                /** 현재 캐릭터의 체력을 100%로 회복 */
                GameManager.GMInstance.Health = GameManager.GMInstance.MaxHealth;
                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;
                break;

                /** 메테오를 눌렀다면 */
                /** TODO ## Skill.cs 메테오 설정 로직 */
            case SkillData.SkillType.Skill_Meteo:
                /** 스킬레벨이 0이라면 */
                if(level == 0)
                {
                    /** 메테오의 스킬 타임 */
                    GameManager.GMInstance.SkillManagerRef.MateoSkillCoolTime = data.counts[level];
                    /** SkillManager의 메테오출현 함수를 true로 바꿔준다. */
                    GameManager.GMInstance.SkillManagerRef.bIsMateo = true;
                    /** 초기 메테오 데미지를 설정해 준다. */
                    GameManager.GMInstance.SkillManagerRef.MateoDamage = 20.0f;
                    /** 현재 데미지는 메테오 SkillManager의 메테오 데미지 */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.MateoDamage;
                }
                else
                {
                    /** 메테오의 스킬 타임 */
                    GameManager.GMInstance.SkillManagerRef.MateoSkillCoolTime = data.counts[level];
                    /** 다음 레벨 데미지는 메테오 데미지에서 SkillData의 데미지 증가 배열에 있는 값을 곱한 값 */
                    nextDamage += GameManager.GMInstance.SkillManagerRef.MateoDamage * data.damages[level];
                    /** 현재레벨은 계산된 nextDamage */
                    CurrentDamage = nextDamage;
                    Debug.Log(CurrentDamage);
                }

                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;
                /** 스킬레벨 증가 */
                level++;
                break;

            default:
                break;
        }

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
