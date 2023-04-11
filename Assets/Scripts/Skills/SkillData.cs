using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Scriptble Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType { Skill_00, Skill_01, Skill_02, Skill_03, Skill_04, Meteo, }

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
