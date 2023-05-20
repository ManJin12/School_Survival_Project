using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    /** 아이템 등급 */
    public enum ItemGrade
    {
        Epic,
        Rare,
        Normal,
    }

    public enum ECharacterType
    {
        WizardChar,
        AcherChar,
        WorriorChar,
        YoungChar,
    }

    public enum EGameState
    {
        GameReady,
        GamePlaying,
        GameOver,
    }

    public enum ESceneType
    {
        TitleScene,
        LobbyScene,
        PlayScene,
    }

    public enum EWeaponType
    {
        NearWeapon,
        FarWeapon,
    }

    public enum EMonsterType
    {
        None,
        Monster_Slime,
        Monster_Mushroom,
        Monster_Frog,
        Monster_Scorpion = 7,
        Monster_Rock,
        Monster_Golem,
        Monster_EyeBall,
        Monster_Head,
        Monster_Mummy,
        MonsterTypeA,
        MonsterTypeB,
        MonsterTypeC,
        MonsterTypeD,
    }

    public enum EBossType
    {    
        Boss_Grass,     
        Boss_Moai,
    }

    public enum ESkillType
    {
        Skill_Mateo,
    }

    public enum ESelectDungeon
    {
        GrassLand,
        RockLand,
        DeathLand,
    }
}
