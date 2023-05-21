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
    Weapon ArrowWeapon;

    public GameObject ArrowObj;
    public SkillData ArrowData;

    public PassiveSkill passiveSkill;
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
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = data.Skill_Icon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.Skill_Name;
    }

    void Start()
    {
        /** �� ��ũ��Ʈ ���� ���ӿ�����Ʈ�� Arrow_Btn�� �ƴ϶�� */
        if (this.gameObject.name != "Arrow_Btn")
        {
            /** ���̾�� ������ �����´� */
            ArrowData = ArrowObj.GetComponent<AcherSkill>().data;
        }
        else
        {
            ArrowData = this.data;
        }
    }

    void OnEnable()
    {
        /** ���� ��ų������ �����ش�. */
        textLevel.text = "Lv." + (level + 1);

        /** ��ų�� ���� */
        switch (data.skillType)
        {
            case SkillData.SkillType.Skill_Arrow:
                /** ��ų ������ �ذ� ���� ������ / ���ⱸü Count*/
                textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100);
                break;

            case SkillData.SkillType.Skill_Vortex:
                /** ���ؽ� ��ų�� 0�� �� */
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    /** ���ؽ� ������ ���� ���� ���� �ð��� �����ش�. */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            /** ���̽� �������� data�� ������ */
            case SkillData.SkillType.Skill_Huricane:
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level]);
                }
                // textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);

                break;

            /** ��ų �ӵ� ������ �̵� �ӵ� ������ ���������� */
            case SkillData.SkillType.Skill_SkillSpeedUp:
            case SkillData.SkillType.Skill_CharSpeedUp:
                /** ��ų ������ �ذ� ���� ������ */
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
            /** ȭ����� Ŭ�� �� */
            case SkillData.SkillType.Skill_Arrow:
                 if (level == 0)
                {
                    /** �� ���� ������Ʈ ���� */
                    GameObject ArrowWeaponObj = new GameObject();
                    /** weapon�� ������ newWeapon�� �߰����� Weapon Comoponent�� ����Ѵ�. */
                    ArrowWeapon = ArrowWeaponObj.AddComponent<Weapon>();
                    /** ȭ�� ������ �⺻ �������� GameManager�� ���� */
                    GameManager.GMInstance.SetArrowBaseDamage(data.baseDamage);
                    /** �⺻���� ������ */
                    CurrentDamage = data.baseDamage;
                    nextCount = data.baseCount;
                    /** weapon�� �ִ� Init�Լ��� data�� �Ű������� ȣ���Ѵ�. */
                    ArrowWeapon.Init(ArrowData);
                }
                else
                {
                    /** ���� �������� ���� ���������� ������ ����ġ��ŭ ���� ������ ����ȴ�. */
                    nextDamage = CurrentDamage * ArrowData.damages[level];
                    /** ���� �������� ���� ���� nextDamage�� ���� */
                    CurrentDamage = nextDamage;
                    ArrowWeapon.Levelup(CurrentDamage, 0);

                    Debug.Log(data.Skill_Name + " " + nextDamage);

                    /** Weapon.cs�� Levelup�Լ��� nextDamage�� nextCount�� �Ű������� ȣ�� */
                }
                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų ���� ���� */
                level++;
                break;

            /** ���ؽ��� Ŭ������ �� */
            case SkillData.SkillType.Skill_Vortex:
                /** ����̵� ��ų������ 0�� �� */
                if (level == 0)
                {
                    /** ����̵� ��ų Ȱ��ȭ */
                    GameManager.GMInstance.SkillManagerRef.bIsVortex = true;
                    /** ����̵� �������� baseDamage�� ������ �� */
                    GameManager.GMInstance.SkillManagerRef.VortexDamage = data.baseDamage;
                    /** ��������� �⺻ �������� GameManager�� ���� */
                    GameManager.GMInstance.SetVortexBaseDamage(data.baseDamage);
                    /** ��ų ��Ÿ�� ���� */
                    GameManager.GMInstance.SkillManagerRef.VortexSkillCoolTime = data.counts[level];
                    /** ���ҵ� �ð� �� �ʱ�ȭ */
                    GameManager.GMInstance.SkillManagerRef.VortexSkillTime = GameManager.GMInstance.SkillManagerRef.VortexSkillCoolTime;
                    /** ���� ������ �� */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.VortexDamage;

                }
                else
                {
                    /** ��ų ��Ÿ�� ���� */
                    GameManager.GMInstance.SkillManagerRef.VortexSkillCoolTime = data.counts[level];
                    /** ������ ������ ��� */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** ���� �������� ������ �������� �ʱ�ȭ */
                    CurrentDamage = nextDamage;
                    /** ����̵� ������ �� �ʱ�ȭ */
                    GameManager.GMInstance.SkillManagerRef.VortexDamage = CurrentDamage;
                }
                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų ���� ���� */
                level++;
                break;

            /** �㸮���� ���� */
            case SkillData.SkillType.Skill_Huricane:
                /** ��ų������ 0�϶� */
                if (level == 0)
                {
                    /** �㸮���� ��ų Ȱ��ȭ�� �˷��ش�. */
                    GameManager.GMInstance.SkillManagerRef.bIsHuricane = true;
                    /** �㸮���� ��Ÿ�� �ð� ���� */
                    GameManager.GMInstance.SkillManagerRef.HuricaneSkillTime = GameManager.GMInstance.SkillManagerRef.HuricaneSkillCoolTime;
                    /** �㸮���� �����ð� ���� */
                    GameManager.GMInstance.SkillManagerRef.MaxHuricaneOnTime = data.counts[level];

                    /** �㸮������ �⺻ �������� GameManager�� ���� */
                    GameManager.GMInstance.SetHuricaneBaseDamage(data.baseDamage);

                    /** �ʱ� �㸮���� �����ð� ���� */
                    GameManager.GMInstance.SkillManagerRef.HuricaneOnTime = GameManager.GMInstance.SkillManagerRef.MaxHuricaneOnTime;
                    /** �㸮���� Ȱ��ȭ �� ������ */
                    GameManager.GMInstance.SkillManagerRef.HuricaneDamage = data.baseDamage;
                    /** ���� ������ */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.HuricaneDamage;
                    /** �㸮���� Ȱ��ȭ */
                    GameManager.GMInstance.SkillManagerRef.EnabledHuricane();
                }
                else
                {
                    /** ���̽������� �����ð� */
                    GameManager.GMInstance.SkillManagerRef.MaxHuricaneOnTime = data.counts[level];
                    /** �������� ���̽������� ������ */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** ���� ���������� �������Ͽ� ������ ������ */
                    CurrentDamage = nextDamage;
                    /** ���̽������� ������ ���� */
                    GameManager.GMInstance.SkillManagerRef.HuricaneDamage = CurrentDamage;
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
            default:
                break;

        }

        /** �ִ� ���� ���� �� */
        if (level == data.damages.Length)
        {
            /** ��ư ���� �� ���� ��Ȱ��ȭ */
            GetComponent<Button>().interactable = false;
        }
    }
}

