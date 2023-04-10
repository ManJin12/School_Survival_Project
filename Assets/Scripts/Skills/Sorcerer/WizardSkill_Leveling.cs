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

        /** icon�� �̹����� data�� Skill_Icon�� �̹����� �����Ѵ�. */
        icon.sprite = data.Skill_Icon;

        Text[] texts = GetComponentsInChildren<Text>();

        /** textLevel�� texts�� 0��° �ε��� */
        textLevel = texts[0];
        
    }

    /** ������Ʈ �Լ��� �� ȣ��� �ڿ� ȣ��ȴ�. */
    void LateUpdate()
    {
        /** textLevel�� text�� ���� */
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        /** Ŭ���� data.skillType */
        switch (data.skillType)
        {
            /** Skill_00, Skill_01 ������ �� */
            case SkillData.SkillType.Skill_00:
            case SkillData.SkillType.Skill_01:

                /** level�� 0�� �� */
                if (level == 0)
                {
                    /** ����� ������Ʈ ���� */
                    GameObject newWeapon = new GameObject();
                    /** weapon�� newWeapon�� �������� weapon��ũ��Ʈ */
                    weapon = newWeapon.AddComponent<Weapon>();
                    /** weapon�� Init�Լ��� data�� �־ �ʱ�ȭ�Ѵ�. */
                    weapon.Init(data);
                }
                /** level�� 0�� �ƴ϶�� */
                else
                {
                    /** data.skillType�� Skill_00�̶�� */
                    if (data.skillType == SkillData.SkillType.Skill_00)
                    {
                        /** ���� �������� */
                        nextCount = data.countRates[level];
                    }

                    /** ���� ���� �������� 0�̶�� */
                    if (CurrentDamage == 0)
                    {
                        /** ���� �������� �⺻ ���������� ���������� �����ش�. */
                        nextDamage += data.baseDamage * data.damageRates[level];
                    }
                    /** ���� ���� �������� 0�� �ƴ϶�� */
                    else
                    {
                        /** ���� �������� ���� ������ * ���������� �����ش�. */
                        nextDamage = CurrentDamage * data.damageRates[level];
                    }

                    /** ���� �������� ������ �������� �ʱ�ȭ �����ش� */
                    CurrentDamage = nextDamage;

                    
                    Debug.Log(nextDamage);

                    /** weapon�� Levelup�Լ��� nextDamage, nextCount�� �Ű������� �Ѱ��ش�. */
                    weapon.Levelup(nextDamage, nextCount);

                }
                /** ������ ���� ���� �ش�. */
                level++;
                break;

            /** SkillType.Skill_02, SkillType.Skill_03 ������ �� */
            case SkillData.SkillType.Skill_02:
            case SkillData.SkillType.Skill_03:
                /** ��ų������ 0�̶�� */
                if(level == 0)
                {
                    /** �� ���� ������Ʈ ���� */
                    GameObject newPassvieSkill = new GameObject();
                    /** �� ���� ������Ʈ�� WizardPassiveSkill ��ũ��Ʈ �߰� */
                    passiveSkill = newPassvieSkill.AddComponent<WizardPassiveSkill>();
                    /** WizardPassiveSkill�� Init�Լ� ȣ�� */
                    passiveSkill.Init(data);
                }
                /** ��ų������ 0�� �ƴ϶�� */
                else
                {
                    /** �������� ���������� data�� ����� ������ ���� */
                    float nextRate = data.damageRates[level];
                    /** WizardPassiveSkill�� LevelUp�Լ��� nextRate�Ű� ������ �Ѱ��ش� */
                    passiveSkill.LevelUp(nextRate);
                }

                /** ������ �������� �ش� */
                level++;
                break;

            /** Skill_04�� ������ �� */
            case SkillData.SkillType.Skill_04:
                /** TODO ## WizardSkill_Leveling ����ü���� 100% ȸ����Ų��. */
                GameManager.GMInstance.Health = GameManager.GMInstance.MaxHealth;
                break;
        }

        /** ���� ������ damageRates�� ũ��� ������ */
        if (level == data.damageRates.Length)
        {
            /** ��ư�� ���� �� ���� ���ش�. */
            GetComponent<Button>().interactable = false;
        }
    }
}
