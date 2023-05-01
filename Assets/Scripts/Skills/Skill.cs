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
            /** 파이어볼이나 전기구체 data를 보여줌 */
            case SkillData.SkillType.Skill_ElectricBall:
            case SkillData.SkillType.Skill_FireBall:
                /** 스킬레벨 0일떄  */
                if (level == 0)
                {
                    textDesc.text = "스킬\n활성화";
                }
                else
                {
                    /** 스킬 설명을 밑과 같이 적용함 / 전기구체 Count : 구체의 개수 */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;
            /** 스킬 속도 증가나 이동 속도 증가를 선택했을때 */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** 스킬 설명을 밑과 같이 적용함 */
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100);
                break;

            /** 메테오 스킬을 보여줌 */
            case SkillData.SkillType.Skill_Meteo:
                /** 스킬레벨 0일떄  */
                if (level == 0)
                {
                    textDesc.text = "스킬\n활성화";
                }
                else
                {
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            /** 아이스 에이지를 data를 보여줌 */
            case SkillData.SkillType.Skill_IceAge:
                if (level == 0)
                {
                    textDesc.text = "스킬\n활성화";
                }
                else
                {
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level], data.counts[level]);
                }
                // textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);

                break;

            /** 낙뢰 data를 보여줌 */
            case SkillData.SkillType.Skill_Lightning:
                /** 라이트닝 스킬이 0일 때 */
                if (level == 0)
                {
                    textDesc.text = "스킬\n활성화";
                }
                else
                {
                    /** 낙뢰 증가 값과 재사용 시간을 보여준다. */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            /** 토네이도 data를 보여준다. */
            case SkillData.SkillType.Skill_Tornado:
                /** 토네이도 스킬이 0일 때 */
                if (level == 0)
                {
                    textDesc.text = "스킬\n활성화";
                }
                else
                {
                    /** 토네이도 데미지 증가 값과 재사용 시간을 보여준다. */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
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
                    Debug.Log(data.Skill_Name + " " + nextDamage);

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
                    /** TODO ## Skill.cs 메테오 초기 데미지 설정 */
                    GameManager.GMInstance.SkillManagerRef.MateoDamage = data.baseDamage;
                    /** 현재 데미지는 메테오 SkillManager의 메테오 데미지 */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.MateoDamage;
                }
                else
                {
                    /** 메테오의 스킬 타임 */
                    GameManager.GMInstance.SkillManagerRef.MateoSkillCoolTime = data.counts[level];
                    /** 다음 레벨 데미지는 메테오 데미지에서 SkillData의 데미지 증가 배열에 있는 값을 곱한 값 */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** 현재레벨은 계산된 nextDamage */
                    CurrentDamage = nextDamage;
                    Debug.Log("Mateo : " + CurrentDamage);
                }

                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;
                /** 스킬레벨 증가 */
                level++;
                break;

            /** 아이스에이지를 눌렸다면? */
            case SkillData.SkillType.Skill_IceAge:
                /** 스킬레벨이 0일때 */
                if (level == 0)
                {
                    /** 아이스 에이지 스킬 활성화를 알려준다. */
                    GameManager.GMInstance.SkillManagerRef.bIsIceAge = true;
                    /** 아이스에이지 쿨타임 시간 설정 */
                    GameManager.GMInstance.SkillManagerRef.IceAgeSkillTime = GameManager.GMInstance.SkillManagerRef.IceAgeSkillCoolTime;
                    /** 아이스 에이지 유지시간 설정 */
                    GameManager.GMInstance.SkillManagerRef.MaxIceAgeOnTime = data.counts[level];
                    /** 초기 아이스에이지 유지시간 설정 */
                    GameManager.GMInstance.SkillManagerRef.IceAgeOnTime = GameManager.GMInstance.SkillManagerRef.MaxIceAgeOnTime;
                    /** 아이스 에이지 활성화 시 데미지 */
                    GameManager.GMInstance.SkillManagerRef.IceAgeDamage = data.baseDamage;
                    /** 현재 데미지 */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.IceAgeDamage;
                    /** 아이스 에이지 활성화 */
                    GameManager.GMInstance.SkillManagerRef.EnabledIceAge();
                    
                }
                else
                {
                    /** 아이스에이지 유지시간 */
                    GameManager.GMInstance.SkillManagerRef.MaxIceAgeOnTime = data.counts[level];
                    /** 레벨업한 아이스에이지 데미지 */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** 현재 데미지값은 레벨업하여 증가된 데미지 */
                    CurrentDamage = nextDamage;
                    /** 아이스에이지 데미지 적용 */
                    GameManager.GMInstance.SkillManagerRef.IceAgeDamage = CurrentDamage;
                    
                }
            
                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;
                /** 스킬레벨 증가 */
                level++;
                break;

            /** 낙뢰를 선택했을 때 */
            case SkillData.SkillType.Skill_Lightning:
                /** 낙뢰 스킬레벨이 0일 떄 */
                if (level == 0)
                {
                    /** 낙뢰 스킬 활성화 */
                    GameManager.GMInstance.SkillManagerRef.bIsLightning = true;
                    /** 낙뢰 스킬의 기본 데미지 */
                    GameManager.GMInstance.SkillManagerRef.LightningDamage = data.baseDamage;
                    /** 현재 낙뢰데미지 */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.LightningDamage;
                    /** 스킬 쿨타임 설정 */
                    GameManager.GMInstance.SkillManagerRef.LightningSkillCoolTime = data.counts[level];
                    /** 스킬 시간 감소 적용될 변수 초기화 */
                    GameManager.GMInstance.SkillManagerRef.LightningSkillTime = GameManager.GMInstance.SkillManagerRef.LightningSkillCoolTime;
                }
                /** 낙뢰스킬레벨이 0이 아닐 때 */
                else
                {
                    /** 스킬 쿨타임 설정 */
                    GameManager.GMInstance.SkillManagerRef.LightningSkillCoolTime = data.counts[level];

                    /** 레벨업하여 증가된 데미지 */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** 레벨업하여 증가된 현재 데미지 */
                    CurrentDamage = nextDamage;
                    /** 낙뢰의 데미지 값으로 준다 */
                    GameManager.GMInstance.SkillManagerRef.LightningDamage = CurrentDamage;
                }

                /** 게임을 실행시킨다. */
                GameManager.GMInstance.bIsLive = true;
                /** 스킬레벨 증가 */
                level++;
                break;

            /** 토네이도를 선택했을때 */
            case SkillData.SkillType.Skill_Tornado:

                /** 토네이도 스킬레벨이 0일 때 */
                if (level == 0)
                {
                    /** 토네이도 스킬 활성화 */
                    GameManager.GMInstance.SkillManagerRef.bIsTornado = true;
                    /** 토네이도 데미지는 baseDamage에 설정된 값 */
                    GameManager.GMInstance.SkillManagerRef.TornadoDamage = data.baseDamage;
                    /** 스킬 쿨타임 설정 */
                    GameManager.GMInstance.SkillManagerRef.TornadoSkillCoolTime = data.counts[level];
                    /** 감소될 시간 값 초기화 */
                    GameManager.GMInstance.SkillManagerRef.TornadoSkillTime = GameManager.GMInstance.SkillManagerRef.TornadoSkillCoolTime;
                    /** 현재 데미지 값 */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.TornadoDamage;

                }
                else
                {
                    /** 스킬 쿨타임 설정 */
                    GameManager.GMInstance.SkillManagerRef.TornadoSkillCoolTime = data.counts[level];
                    /** 증가된 데미지 계산 */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** 현재 데미지는 증가된 데미지로 초기화 */
                    CurrentDamage = nextDamage;
                    /** 토네이도 데미지 값 초기화 */
                    GameManager.GMInstance.SkillManagerRef.TornadoDamage = CurrentDamage;
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
