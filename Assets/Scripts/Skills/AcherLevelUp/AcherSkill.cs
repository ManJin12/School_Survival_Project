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
        /** ���� ��ų������ �����ش�. */
        textLevel.text = "Lv." + (level + 1);

        /** ��ų�� ���� */
        switch (data.skillType)
        {
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

