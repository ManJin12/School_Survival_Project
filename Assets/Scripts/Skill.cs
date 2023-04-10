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

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.Skill_Icon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        
    }

  
    void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }

    public void OnClick()
    {
        switch(data.skillType)
        {
            case SkillData.SkillType.Skill_00:
            case SkillData.SkillType.Skill_01:
             

                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    if (data.skillType == SkillData.SkillType.Skill_00)
                    {
                        nextCount = data.counts[level];
                    }

                    if (CurrentDamage == 0)
                    {
                        nextDamage += data.baseDamage * data.damages[level];
                    }
                    else
                    {
                        nextDamage = CurrentDamage * data.damages[level];
                    }

                    CurrentDamage = nextDamage;

                    Debug.Log(nextDamage);

                    weapon.Levelup(nextDamage, nextCount);

                }
                level++;
                break;

            case SkillData.SkillType.Skill_02:
            case SkillData.SkillType.Skill_03:
                if(level == 0)
                {
                    GameObject newPassvieSkill = new GameObject();
                    passiveSkill = newPassvieSkill.AddComponent<PassiveSkill>();
                    passiveSkill.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    passiveSkill.LevelUp(nextRate);
                }
                level++;
                break;

            case SkillData.SkillType.Skill_04:
                GameManager.GMInstance.Health = GameManager.GMInstance.MaxHealth;
                break;
        }

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
