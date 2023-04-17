using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{ 
    public enum ECharacterType
    {
        WizardChar,
        GirlChar,
        OldBoyChar,
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
        DungeonSelectScene,
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
    }

    public enum ESkillType
    {
        Skill_Mateo,

    }
}
