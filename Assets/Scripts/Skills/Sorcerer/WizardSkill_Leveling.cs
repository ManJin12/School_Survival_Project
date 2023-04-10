using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;

/** TODO ## WizardSkill_Leveling */
public class WizardSkill_Leveling : MonoBehaviour
{
    public SkillData data;
    public int level = 0;
    public Weapon weapon;
    public WizardPassiveSkill passiveSkill;

    float CurrentDamage;
    float nextDamage;
    int nextCount = 0;

    Image icon;
    Text textLevel;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];

        /** icon의 이미지는 data의 Skill_Icon의 이미지로 설정한다. */
        icon.sprite = data.Skill_Icon;

        Text[] texts = GetComponentsInChildren<Text>();

        /** textLevel은 texts의 0번째 인덱스 */
        textLevel = texts[0];
        
    }

    /** 업데이트 함수가 다 호출된 뒤에 호출된다. */
    void LateUpdate()
    {
        /** textLevel의 text를 변경 */
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        /** 클릭한 data.skillType */
        switch (data.skillType)
        {
            /** Skill_00, Skill_01 눌렀을 때 */
            case SkillData.SkillType.Skill_00:
            case SkillData.SkillType.Skill_01:

                /** level이 0일 때 */
                if (level == 0)
                {
                    /** 빈게임 오브젝트 생성 */
                    GameObject newWeapon = new GameObject();
                    /** weapon은 newWeapon에 생성해준 weapon스크립트 */
                    weapon = newWeapon.AddComponent<Weapon>();
                    /** weapon의 Init함수에 data를 넣어서 초기화한다. */
                    weapon.Init(data);
                }
                /** level이 0이 아니라면 */
                else
                {
                    /** data.skillType이 Skill_00이라면 */
                    if (data.skillType == SkillData.SkillType.Skill_00)
                    {
                        /** 다음 번개구슬 */
                        nextCount = data.countRates[level];
                    }

                    /** 만약 현재 데미지가 0이라면 */
                    if (CurrentDamage == 0)
                    {
                        /** 다음 데미지는 기본 데미지에서 증가비율을 곱해준다. */
                        nextDamage += data.baseDamage * data.damageRates[level];
                    }
                    /** 만약 현재 데미지가 0이 아니라면 */
                    else
                    {
                        /** 다음 데미지는 현재 데미지 * 증가비율을 곱해준다. */
                        nextDamage = CurrentDamage * data.damageRates[level];
                    }

                    /** 현재 데미지를 증가된 데미지로 초기화 시켜준다 */
                    CurrentDamage = nextDamage;

                    
                    Debug.Log(nextDamage);

                    /** weapon의 Levelup함수에 nextDamage, nextCount를 매개변수로 넘겨준다. */
                    weapon.Levelup(nextDamage, nextCount);

                }
                /** 레벨을 증가 시켜 준다. */
                level++;
                break;

            /** SkillType.Skill_02, SkillType.Skill_03 눌렀을 때 */
            case SkillData.SkillType.Skill_02:
            case SkillData.SkillType.Skill_03:
                /** 스킬레벨이 0이라면 */
                if(level == 0)
                {
                    /** 빈 게임 오브젝트 생성 */
                    GameObject newPassvieSkill = new GameObject();
                    /** 빈 게임 오브젝트에 WizardPassiveSkill 스크립트 추가 */
                    passiveSkill = newPassvieSkill.AddComponent<WizardPassiveSkill>();
                    /** WizardPassiveSkill의 Init함수 호출 */
                    passiveSkill.Init(data);
                }
                /** 스킬레벨이 0이 아니라면 */
                else
                {
                    /** 다음레벨 증가비율은 data에 저장된 값으로 증가 */
                    float nextRate = data.damageRates[level];
                    /** WizardPassiveSkill의 LevelUp함수에 nextRate매개 변수로 넘겨준다 */
                    passiveSkill.LevelUp(nextRate);
                }

                /** 레벨을 증가시켜 준다 */
                level++;
                break;

            /** Skill_04을 눌렀을 때 */
            case SkillData.SkillType.Skill_04:
                /** TODO ## WizardSkill_Leveling 현재체력을 100% 회복시킨다. */
                GameManager.GMInstance.Health = GameManager.GMInstance.MaxHealth;
                break;
        }

        /** 만약 레벨이 damageRates의 크기와 같으면 */
        if (level == data.damageRates.Length)
        {
            /** 버튼을 누를 수 없게 해준다. */
            GetComponent<Button>().interactable = false;
        }
    }
}
