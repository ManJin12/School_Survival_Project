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
        /** ���� ��ų������ �����ش�. */
        textLevel.text = "Lv." + (level + 1);

        /** ��ų�� ���� */
        switch(data.skillType)
        {
            /** ���̾�̳� ���ⱸü�� ���������� */
            case SkillData.SkillType.Skill_ElectricBall:
            case SkillData.SkillType.Skill_FireBall:
                /** ��ų ������ �ذ� ���� ������ / ���ⱸü Count : ��ü�� ���� */
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                break;
            /** ��ų �ӵ� ������ �̵� �ӵ� ������ ���������� */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** ��ų ������ �ذ� ���� ������ */
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100);
                break;
            /** ���׿� ��ų�� �������� �� */
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
            /** ���ⱸü�� ���̾� ���� ���������� */
            case SkillData.SkillType.Skill_ElectricBall:
            case SkillData.SkillType.Skill_FireBall:
                /** ���� ��ų������ 0�̶�� */
                if (level == 0)
                {
                    /** �� ���� ������Ʈ ���� */
                    GameObject newWeapon = new GameObject();
                    /** weapon�� ������ newWeapon�� �߰����� Weapon Comoponent�� ����Ѵ�. */
                    weapon = newWeapon.AddComponent<Weapon>();
                    /** weapon�� �ִ� Init�Լ��� data�� �Ű������� ȣ���Ѵ�. */
                    weapon.Init(data);
                }
                /** ��ų������ 0�� �ƴ϶�� */
                else
                {
                    /** ���ⱸü�� �����ٸ� */
                    if (data.skillType == SkillData.SkillType.Skill_ElectricBall)
                    {
                        /** ���� ���ⱸü�� ������ Skilldata�� count�迭�� ����� ������ ũ���� count�� �ҷ�������. */
                        nextCount = data.counts[level];
                    }

                    /** ���� ���� �������� 0�̸� */
                    if (CurrentDamage == 0)
                    {
                        /** ���� �������� SkillData�� ����� ���������� ����� ������������ŭ ���� ������ ����ȴ�. */
                        nextDamage += data.baseDamage * data.damages[level];
                    }
                    /** ���� �������� 0�� �ƴ϶�� */
                    else
                    {
                        /** ���� �������� ���� ���������� ������ ����ġ��ŭ ���� ������ ����ȴ�. */
                        nextDamage = CurrentDamage * data.damages[level];
                    }

                    /** ���� �������� ���� ���� nextDamage�� ���� */
                    CurrentDamage = nextDamage;
                    Debug.Log(nextDamage);

                    /** Weapon.cs�� Levelup�Լ��� nextDamage�� nextCount�� �Ű������� ȣ�� */
                    weapon.Levelup(nextDamage, nextCount);
                }

                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;

                /** ��ų���� ���� */
                level++;

                break;

            /** ��ų �ӵ� ������ �̵� �ӵ� ������ ���������� */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** ��ų������ 0�̶�� */
                if (level == 0)
                {
                    /** newPassvieSkill�̶�� ����� ������Ʈ ���� */
                    GameObject newPassvieSkill = new GameObject();
                    /** ������ newPassvieSkill ����� ������Ʈ�� �߰��� PassiveSkill�� passvieSkill�� �����Ѵ�. */
                    passiveSkill = newPassvieSkill.AddComponent<PassiveSkill>();
                    /** PassiveSkill�� Init�Լ��� data�� �Ű������� ȣ�� */
                    passiveSkill.Init(data);
                }
                /** ������ 0�� �ƴ϶�� */
                else
                {
                    /** ���� ���� ���������� data�� ����� ���� �ҷ��´�. */
                    float nextRate = data.damages[level];
                    /** PassiveSkill�� LevelUp�Լ��� nextRate�� �Ű������� ȣ���Ѵ�. */
                    passiveSkill.LevelUp(nextRate);
                }
                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;

                level++;
                break;

            /** Hill�� ������ */
            case SkillData.SkillType.Skill_Hill:
                /** ���� ĳ������ ü���� 100%�� ȸ�� */
                GameManager.GMInstance.Health = GameManager.GMInstance.MaxHealth;
                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                break;

                /** ���׿��� �����ٸ� */
                /** TODO ## Skill.cs ���׿� ���� ���� */
            case SkillData.SkillType.Skill_Meteo:
                /** ��ų������ 0�̶�� */
                if(level == 0)
                {
                    /** ���׿��� ��ų Ÿ�� */
                    GameManager.GMInstance.SkillManagerRef.MateoSkillCoolTime = data.counts[level];
                    /** SkillManager�� ���׿����� �Լ��� true�� �ٲ��ش�. */
                    GameManager.GMInstance.SkillManagerRef.bIsMateo = true;
                    /** �ʱ� ���׿� �������� ������ �ش�. */
                    GameManager.GMInstance.SkillManagerRef.MateoDamage = 20.0f;
                    /** ���� �������� ���׿� SkillManager�� ���׿� ������ */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.MateoDamage;
                }
                else
                {
                    /** ���׿��� ��ų Ÿ�� */
                    GameManager.GMInstance.SkillManagerRef.MateoSkillCoolTime = data.counts[level];
                    /** ���� ���� �������� ���׿� ���������� SkillData�� ������ ���� �迭�� �ִ� ���� ���� �� */
                    nextDamage += GameManager.GMInstance.SkillManagerRef.MateoDamage * data.damages[level];
                    /** ���緹���� ���� nextDamage */
                    CurrentDamage = nextDamage;
                    Debug.Log(CurrentDamage);
                }

                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų���� ���� */
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
