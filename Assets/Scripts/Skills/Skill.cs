using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;

public class Skill : MonoBehaviour
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
        /** �� ��ũ��Ʈ ���� ���ӿ�����Ʈ�� FireBall_Btn�� �ƴ϶�� */
        if (this.gameObject.name != "FireBall_Btn")
        {
            /** ���̾�� ������ �����´� */
            FireBallData = FireBallObj.GetComponent<Skill>().data;
        }
        else
        {
            FireBallData = this.data;
        }
    }

    void OnEnable()
    {
        /** ���� ��ų������ �����ش�. */
        textLevel.text = "Lv." + (level + 1);

        /** ��ų�� ���� */
        switch(data.skillType)
        {
            /** ���ⱸü data�� ������ */
            case SkillData.SkillType.Skill_ElectricBall:
                /** ��ų���� 0�ϋ�  */
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    /** ��ų ������ �ذ� ���� ������ / ���ⱸü Count : ��ü�� ���� */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            /** ���̾ data�� ������ */
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

            /** ���׿� ��ų�� ������ */
            case SkillData.SkillType.Skill_Meteo:
                /** ��ų���� 0�ϋ�  */
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            /** ���̽� �������� data�� ������ */
            case SkillData.SkillType.Skill_IceAge:
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level], data.counts[level]);
                }
                // textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);

                break;

            /** ���� data�� ������ */
            case SkillData.SkillType.Skill_Lightning:
                /** ����Ʈ�� ��ų�� 0�� �� */
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    /** ���� ���� ���� ���� �ð��� �����ش�. */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            /** ����̵� data�� �����ش�. */
            case SkillData.SkillType.Skill_Tornado:
                /** ����̵� ��ų�� 0�� �� */
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    /** ����̵� ������ ���� ���� ���� �ð��� �����ش�. */
                    textDesc.text = string.Format(data.Skill_Desc, data.damages[level] * 100, data.counts[level]);
                }
                break;

            case SkillData.SkillType.Skill_IceArrow:
                /** ��ų���� 0�ϋ�  */
                if (level == 0)
                {
                    textDesc.text = "��ų\nȰ��ȭ";
                }
                else
                {
                    /** ��ų ������ �ذ� ���� ������ */
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
            /** ���ⱸü�� ���̽� ���ο��� ���������� */
            case SkillData.SkillType.Skill_ElectricBall:
            case SkillData.SkillType.Skill_IceArrow:
                /** ���� ��ų������ 0�̰� ���̺��� �ƴҶ� */
                if (level == 0)
                {
                    /** ���� ��ų Ÿ���� ���̽����ο���? */
                    if (data.skillType == SkillData.SkillType.Skill_IceArrow)
                    {
                        GameManager.GMInstance.SkillManagerRef.IceArrow.SetActive(true);
                    }

                    /** �� ���� ������Ʈ ���� */
                    GameObject newWeapon = new GameObject();
                    /** weapon�� ������ newWeapon�� �߰����� Weapon Comoponent�� ����Ѵ�. */
                    weapon = newWeapon.AddComponent<Weapon>();
                    /** weapon�� �ִ� Init�Լ��� data�� �Ű������� ȣ���Ѵ�. */
                    weapon.Init(data);

                    /** ���̾ ������ 0�̶�� */
                    if (FireBallObj.GetComponent<Skill>().level == 0)
                    {
                        /** ����� ������Ʈ ���� */
                        GameObject FireBallWeaponObj = new GameObject();
                        /** ������ ������Ʈ�� Weapon ��ũ��Ʈ �߰� */
                        FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                        /** ���̾�� ������ �ø��� */
                        FireBallObj.GetComponent<Skill>().level++;
                        /** ���̾�� �⺻ ������ */
                        FireBallObj.GetComponent<Skill>().CurrentDamage = FireBallData.baseDamage;
                        /** ���̾ ��ư ������Ʈ�� ��ũ��Ʈ�� ������ �ִ� FireBallWeapon�� �� ��ũ��Ʈ�� ��� �ִ� FireBallWeapon  */
                        FireBallObj.GetComponent<Skill>().FireBallWeapon = this.FireBallWeapon;
                        FireBallWeapon.Init(FireBallData);
                    }
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
                    Debug.Log(data.Skill_Name + " " + nextDamage);

                    /** Weapon.cs�� Levelup�Լ��� nextDamage�� nextCount�� �Ű������� ȣ�� */
                    weapon.Levelup(nextDamage, nextCount);
                }

                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų���� ���� */
                level++;
                break;

            /** ���̾�� �������� �� */
            case SkillData.SkillType.Skill_FireBall:
                if (level == 0)
                {
                    /** �� ���� ������Ʈ ���� */
                    GameObject FireBallWeaponObj = new GameObject();
                    /** weapon�� ������ newWeapon�� �߰����� Weapon Comoponent�� ����Ѵ�. */
                    FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                    /** �⺻���� ������ */
                    CurrentDamage = data.baseDamage;
                    nextCount = data.baseCount;
                    /** weapon�� �ִ� Init�Լ��� data�� �Ű������� ȣ���Ѵ�. */
                    FireBallWeapon.Init(FireBallData);
                }
                else
                {
                    /** ���� �������� ���� ���������� ������ ����ġ��ŭ ���� ������ ����ȴ�. */
                    nextDamage = CurrentDamage * FireBallData.damages[level];

                    /** ���� �������� ���� ���� nextDamage�� ���� */
                    CurrentDamage = nextDamage;

                    Debug.Log(data.Skill_Name + " " + nextDamage);

                    /** Weapon.cs�� Levelup�Լ��� nextDamage�� nextCount�� �Ű������� ȣ�� */
                    // FireBallWeapon.Levelup(nextDamage, nextCount);
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

                    /** ���̾ ������ 0�̶�� */
                    if (FireBallObj.GetComponent<Skill>().level == 0)
                    {
                        /** ����� ������Ʈ ���� */
                        GameObject FireBallWeaponObj = new GameObject();
                        /** ������ ������Ʈ�� Weapon ��ũ��Ʈ �߰� */
                        FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                        /** ���̾�� ������ �ø��� */
                        FireBallObj.GetComponent<Skill>().level++;
                        /** ���̾�� �⺻ ������ */
                        FireBallObj.GetComponent<Skill>().CurrentDamage = FireBallData.baseDamage;
                        /** ���̾ ��ư ������Ʈ�� ��ũ��Ʈ�� ������ �ִ� FireBallWeapon�� �� ��ũ��Ʈ�� ��� �ִ� FireBallWeapon  */
                        FireBallObj.GetComponent<Skill>().FireBallWeapon = this.FireBallWeapon;
                        FireBallWeapon.Init(FireBallData);
                    }
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
                    /** TODO ## Skill.cs ���׿� �ʱ� ������ ���� */
                    GameManager.GMInstance.SkillManagerRef.MateoDamage = data.baseDamage;
                    /** ���� �������� ���׿� SkillManager�� ���׿� ������ */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.MateoDamage;


                    /** ���̾ ������ 0�̶�� */
                    if (FireBallObj.GetComponent<Skill>().level == 0)
                    {
                        /** ����� ������Ʈ ���� */
                        GameObject FireBallWeaponObj = new GameObject();
                        /** ������ ������Ʈ�� Weapon ��ũ��Ʈ �߰� */
                        FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                        /** ���̾�� ������ �ø��� */
                        FireBallObj.GetComponent<Skill>().level++;
                        /** ���̾�� �⺻ ������ */
                        FireBallObj.GetComponent<Skill>().CurrentDamage = FireBallData.baseDamage;
                        /** ���̾ ��ư ������Ʈ�� ��ũ��Ʈ�� ������ �ִ� FireBallWeapon�� �� ��ũ��Ʈ�� ��� �ִ� FireBallWeapon  */
                        FireBallObj.GetComponent<Skill>().FireBallWeapon = this.FireBallWeapon;
                        FireBallWeapon.Init(FireBallData);
                    }
                }
                else
                {
                    /** ���׿��� ��ų Ÿ�� */
                    GameManager.GMInstance.SkillManagerRef.MateoSkillCoolTime = data.counts[level];
                    /** ���� ���� �������� ���׿� ���������� SkillData�� ������ ���� �迭�� �ִ� ���� ���� �� */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** ���緹���� ���� nextDamage */
                    CurrentDamage = nextDamage;
                    Debug.Log("Mateo : " + CurrentDamage);
                }

                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų���� ���� */
                level++;
                break;

            /** ���̽��������� ���ȴٸ�? */
            case SkillData.SkillType.Skill_IceAge:
                /** ��ų������ 0�϶� */
                if (level == 0)
                {
                    /** ���̽� ������ ��ų Ȱ��ȭ�� �˷��ش�. */
                    GameManager.GMInstance.SkillManagerRef.bIsIceAge = true;
                    /** ���̽������� ��Ÿ�� �ð� ���� */
                    GameManager.GMInstance.SkillManagerRef.IceAgeSkillTime = GameManager.GMInstance.SkillManagerRef.IceAgeSkillCoolTime;
                    /** ���̽� ������ �����ð� ���� */
                    GameManager.GMInstance.SkillManagerRef.MaxIceAgeOnTime = data.counts[level];
                    /** �ʱ� ���̽������� �����ð� ���� */
                    GameManager.GMInstance.SkillManagerRef.IceAgeOnTime = GameManager.GMInstance.SkillManagerRef.MaxIceAgeOnTime;
                    /** ���̽� ������ Ȱ��ȭ �� ������ */
                    GameManager.GMInstance.SkillManagerRef.IceAgeDamage = data.baseDamage;
                    /** ���� ������ */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.IceAgeDamage;
                    /** ���̽� ������ Ȱ��ȭ */
                    GameManager.GMInstance.SkillManagerRef.EnabledIceAge();


                    /** ���̾ ������ 0�̶�� */
                    if (FireBallObj.GetComponent<Skill>().level == 0)
                    {
                        /** ����� ������Ʈ ���� */
                        GameObject FireBallWeaponObj = new GameObject();
                        /** ������ ������Ʈ�� Weapon ��ũ��Ʈ �߰� */
                        FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                        /** ���̾�� ������ �ø��� */
                        FireBallObj.GetComponent<Skill>().level++;
                        /** ���̾�� �⺻ ������ */
                        FireBallObj.GetComponent<Skill>().CurrentDamage = FireBallData.baseDamage;
                        /** ���̾ ��ư ������Ʈ�� ��ũ��Ʈ�� ������ �ִ� FireBallWeapon�� �� ��ũ��Ʈ�� ��� �ִ� FireBallWeapon  */
                        FireBallObj.GetComponent<Skill>().FireBallWeapon = this.FireBallWeapon;
                        FireBallWeapon.Init(FireBallData);
                    }

                }
                else
                {
                    /** ���̽������� �����ð� */
                    GameManager.GMInstance.SkillManagerRef.MaxIceAgeOnTime = data.counts[level];
                    /** �������� ���̽������� ������ */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** ���� ���������� �������Ͽ� ������ ������ */
                    CurrentDamage = nextDamage;
                    /** ���̽������� ������ ���� */
                    GameManager.GMInstance.SkillManagerRef.IceAgeDamage = CurrentDamage;
                    
                }
            
                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų���� ���� */
                level++;
                break;

            /** ���ڸ� �������� �� */
            case SkillData.SkillType.Skill_Lightning:
                /** ���� ��ų������ 0�� �� */
                if (level == 0)
                {
                    /** ���� ��ų Ȱ��ȭ */
                    GameManager.GMInstance.SkillManagerRef.bIsLightning = true;
                    /** ���� ��ų�� �⺻ ������ */
                    GameManager.GMInstance.SkillManagerRef.LightningDamage = data.baseDamage;
                    /** ���� ���ڵ����� */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.LightningDamage;
                    /** ��ų ��Ÿ�� ���� */
                    GameManager.GMInstance.SkillManagerRef.LightningSkillCoolTime = data.counts[level];
                    /** ��ų �ð� ���� ����� ���� �ʱ�ȭ */
                    GameManager.GMInstance.SkillManagerRef.LightningSkillTime = GameManager.GMInstance.SkillManagerRef.LightningSkillCoolTime;


                    /** ���̾ ������ 0�̶�� */
                    if (FireBallObj.GetComponent<Skill>().level == 0)
                    {
                        /** ����� ������Ʈ ���� */
                        GameObject FireBallWeaponObj = new GameObject();
                        /** ������ ������Ʈ�� Weapon ��ũ��Ʈ �߰� */
                        FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                        /** ���̾�� ������ �ø��� */
                        FireBallObj.GetComponent<Skill>().level++;
                        /** ���̾�� �⺻ ������ */
                        FireBallObj.GetComponent<Skill>().CurrentDamage = FireBallData.baseDamage;
                        /** ���̾ ��ư ������Ʈ�� ��ũ��Ʈ�� ������ �ִ� FireBallWeapon�� �� ��ũ��Ʈ�� ��� �ִ� FireBallWeapon  */
                        FireBallObj.GetComponent<Skill>().FireBallWeapon = this.FireBallWeapon;
                        FireBallWeapon.Init(FireBallData);
                    }
                }
                /** ���ڽ�ų������ 0�� �ƴ� �� */
                else
                {
                    /** ��ų ��Ÿ�� ���� */
                    GameManager.GMInstance.SkillManagerRef.LightningSkillCoolTime = data.counts[level];

                    /** �������Ͽ� ������ ������ */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** �������Ͽ� ������ ���� ������ */
                    CurrentDamage = nextDamage;
                    /** ������ ������ ������ �ش� */
                    GameManager.GMInstance.SkillManagerRef.LightningDamage = CurrentDamage;
                }

                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų���� ���� */
                level++;
                break;

            /** ����̵��� ���������� */
            case SkillData.SkillType.Skill_Tornado:

                /** ����̵� ��ų������ 0�� �� */
                if (level == 0)
                {
                    /** ����̵� ��ų Ȱ��ȭ */
                    GameManager.GMInstance.SkillManagerRef.bIsTornado = true;
                    /** ����̵� �������� baseDamage�� ������ �� */
                    GameManager.GMInstance.SkillManagerRef.TornadoDamage = data.baseDamage;
                    /** ��ų ��Ÿ�� ���� */
                    GameManager.GMInstance.SkillManagerRef.TornadoSkillCoolTime = data.counts[level];
                    /** ���ҵ� �ð� �� �ʱ�ȭ */
                    GameManager.GMInstance.SkillManagerRef.TornadoSkillTime = GameManager.GMInstance.SkillManagerRef.TornadoSkillCoolTime;
                    /** ���� ������ �� */
                    CurrentDamage = GameManager.GMInstance.SkillManagerRef.TornadoDamage;


                    /** ���̾ ������ 0�̶�� */
                    if (FireBallObj.GetComponent<Skill>().level == 0)
                    {
                        /** ����� ������Ʈ ���� */
                        GameObject FireBallWeaponObj = new GameObject();
                        /** ������ ������Ʈ�� Weapon ��ũ��Ʈ �߰� */
                        FireBallWeapon = FireBallWeaponObj.AddComponent<Weapon>();
                        /** ���̾�� ������ �ø��� */
                        FireBallObj.GetComponent<Skill>().level++;
                        /** ���̾�� �⺻ ������ */
                        FireBallObj.GetComponent<Skill>().CurrentDamage = FireBallData.baseDamage;
                        FireBallWeapon.Init(FireBallData);
                    }

                }
                else
                {
                    /** ��ų ��Ÿ�� ���� */
                    GameManager.GMInstance.SkillManagerRef.TornadoSkillCoolTime = data.counts[level];
                    /** ������ ������ ��� */
                    nextDamage = CurrentDamage * data.damages[level];
                    /** ���� �������� ������ �������� �ʱ�ȭ */
                    CurrentDamage = nextDamage;
                    /** ����̵� ������ �� �ʱ�ȭ */
                    GameManager.GMInstance.SkillManagerRef.TornadoDamage = CurrentDamage;
                }

                /** ������ �����Ų��. */
                GameManager.GMInstance.bIsLive = true;
                /** ��ų���� ���� */
                level++;
                break;

            /** TODO ## Skill.cs ���̽� ���ο� �ּ� */
            //case SkillData.SkillType.Skill_IceArrow:
            //     /** ���� ��ų������ 0�̶�� */
            //    if (level == 0)
            //    {
            //        /** ���̽����ο� Ȱ��ȭ */
            //        GameManager.GMInstance.SkillManagerRef.IceArrow.SetActive(true);
            //    }
            //    /** ��ų������ 0�� �ƴ϶�� */
            //    else
            //    {
            //        /** ���� �������� ���� ���������� ������ ����ġ��ŭ ���� ������ ����ȴ�. */
            //        nextDamage = CurrentDamage * data.damages[level];
            //        /** ���� �������� ���� ���� nextDamage�� ���� */
            //        CurrentDamage = nextDamage;
            //        Debug.Log(data.Skill_Name + " " + nextDamage);
            //        /** Weapon.cs�� Levelup�Լ��� nextDamage�� nextCount�� �Ű������� ȣ�� */
            //        weapon.Levelup(nextDamage, nextCount);
            //    }

            //    /** ������ �����Ų��. */
            //    GameManager.GMInstance.bIsLive = true;
            //    /** ��ų���� ���� */
            //    level++;
            //    break;

            default:
                break;
        }

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
