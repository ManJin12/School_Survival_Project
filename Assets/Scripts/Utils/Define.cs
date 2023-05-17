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
        MonsterTypeA,
        MonsterTypeB,
        MonsterTypeC,
        MonsterTypeD,
        Monster_Slime,
        Monster_Mushroom,
        Monster_Frog,
        Boss_Moth,
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
