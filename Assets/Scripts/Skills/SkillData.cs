using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Scriptble Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType
    { 
        /** ������ ��ų */
        Skill_ElectricBall,
        Skill_FireBall,
        Skill_SkillSpeedUp, 
        Skill_CharSpeedUp,
        Skill_Hill,
        Skill_Meteo,
        Skill_IceAge,
        Skill_Lightning,
        Skill_Tornado,
        Skill_IceArrow,

        /** �ü� ��ų */
        Skill_Arrow,
        Skill_Vortex,
        Skill_Huricane,
        Skill_WindSpirit,
        Skill_Trap,
        Skill_ArrowRain,
        Skill_BombArrow,
    }

    [Header("# Main Info")]
    public SkillType skillType;
    public int Skill_Id;
    public string Skill_Name;
    [TextArea]
    public string Skill_Desc;
    public Sprite Skill_Icon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;
    public float SkillSpeed;

    [Header("# Weapon")]
    public GameObject projectile;
}
