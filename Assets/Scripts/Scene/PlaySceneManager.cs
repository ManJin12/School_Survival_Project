using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;
using static Define;

public class PlaySceneManager : MonoBehaviour
{
    public float PlayTime;
    public Text SkillSelectPaneltext;
    public Image GameOverImage;
    // float fadeImage = 1;
    public bool bIsFirstStart;
    float BasePlayerSpeed;

    public float HpUpTime;
    public float MonsterHpUpRate = 0.1f;
    public bool bIsMonsterHpUp;
    public float NextMonsterHp;
    public float BaseMonsterHp;
    int HpLevel;

    public GameObject GameClearPanel;
    public GameObject GameOverPanel;
    public GameObject ConfigPanel;
    public LevelUp WizardLevelUp;

    [Header("---DungeonTile0---")]
    public GameObject GrassLandTile;
    public GameObject RockLandTile;
    public GameObject DeathLandTile;

    [Header("---Check---")]
    public Image PlayScene_BGM_Off_Check;
    public Image PlayScene_BGM_On_Check;
    public Image PlayScene_EffectSound_On_Check;
    public Image PlayScene_EffectSound_Off_Check;



    void Start()
    {
        /** 만약 선택된 모드가 초원지대라면 */
        if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.GrassLand)
        {
            /** 초원지대 배경음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Dungeon_Grassland);

            GrassLandTile.SetActive(true);
            RockLandTile.SetActive(false);
            DeathLandTile.SetActive(false);
        }
        /** 만약 선택된 모드가 암석지대라면 */
        else if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.RockLand)
        {
            /** 암석지대 배경음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Dungeon_Rockland);

            GrassLandTile.SetActive(false);
            RockLandTile.SetActive(true);
            DeathLandTile.SetActive(false);
        }
        /** 만약 선택된 모드가 망자의 숲이라면 */
        else if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.DeathLand)
        {
            /** 망자의 숲 배경음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Dungeon_Deathland);

            GrassLandTile.SetActive(false);
            RockLandTile.SetActive(false);
            DeathLandTile.SetActive(true);
        }

        /** 스킬 선택 텍스트UI 함수 호출 */
        TextInit();
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
        GameManager.GMInstance.PlaySceneManagerRef = this;

        /** 게임 플레이 변수 초기화 */
        GameManager.GMInstance.PlaySceneInit(false, 0, GameManager.GMInstance.MaxHealth);

        /** 게임 생명 활성화 */
        GameManager.GMInstance.bIsLive = true;

        GameManager.GMInstance.InfoInit();

        /** 입장 시 플레이어 이동속도 저장 */
        BasePlayerSpeed = GameManager.GMInstance.PlayerSpeed;

        /** 처음 시작하는지 확인하기 위한 변수 */
        bIsFirstStart = true;
    }

    void Update()
    {
        if (GameManager.GMInstance.bIsLive == false)
        {
            return;
        }

        PlayTime += Time.deltaTime;
        HpUpTime += Time.deltaTime;
        GameManager.GMInstance.PlayTime = PlayTime;

        MonsterHpUp();


        GameClear();
    }


    void MonsterHpUp()
    {
        if (NextMonsterHp == 0)
        {
            NextMonsterHp = BaseMonsterHp;
        }

        /** 20초마다 재생 */
        if (HpUpTime / 20 >= 1)
        {
            bIsMonsterHpUp = true;

            /** 시간 초기화 */
            HpUpTime = 0.0f;

            HpLevel++;
            MonsterHpUpRate += 0.1f;

            NextMonsterHp = BaseMonsterHp + BaseMonsterHp * MonsterHpUpRate;

            Debug.Log("BaseMonsterHp * MonsterHpUpRate " + BaseMonsterHp * MonsterHpUpRate);
            Debug.Log("NextMonsterHp " + NextMonsterHp);

            /** 몬스터 체력 증가 */
            // GameManager.GMInstance.MoveRef.MaxHp += GameManager.GMInstance.MoveRef.MaxHp * HpUpRate;

            if (HpLevel == 9)
            {
                /** 다음 몬스터를 위해 초기화 */
                MonsterHpUpRate = 0;
                /** 다음 몬스터를 위해 초기화 */
                HpLevel = 0;
            }       
        }
    }

    public void OnClckReSkill()
    {
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
    }


    /** 환경설정 클릭 */
    public void OnClickConfig()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        ConfigPanel.SetActive(true);

        /** 화면이 멈췄기 때문에 다음 입장을 위해 false로 해준다. */
        GameManager.GMInstance.bIsLive = false;

        Time.timeScale = 0.0f;
    }

    /** 환경설정 닫기 버튼 */
    public void OnClickConfig_Resume()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        Time.timeScale = 1.0f;

        /** 화면이 멈췄기 때문에 다음 입장을 위해 false로 해준다. */
        GameManager.GMInstance.bIsLive = true;

        ConfigPanel.SetActive(false);
    }

    /** Lobby Scene으로 이동 */
    public void OnClickConfig_GoLobby()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 입장해서 증가하는 이동속도 반영 x */
        GameManager.GMInstance.PlayerSpeed = BasePlayerSpeed;

        /** 시간흐름 정상화 */
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("Lobby");
    }

    public void TextInit()
    {
        if (GameManager.GMInstance.level == 1)
        {
            SkillSelectPaneltext.text = "스킬을 선택해 주세요!";
        }
        else if (GameManager.GMInstance.level != 1)
        {
            SkillSelectPaneltext.text = "축하합니다!\n 레벨 업!";
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        /** 플레이어 생존 함수 off */
        GameManager.GMInstance.bIsLive = false;

        /** GameOverImage 적용 */
        GameOverImage.gameObject.SetActive(true);

        /** 1초 후 */
        yield return new WaitForSeconds(1.0f);

        /** GameOverPanel On */
        GameOverPanel.SetActive(true);

        StartCoroutine(GameOverTimeRoutine());
        
    }

    IEnumerator GameOverTimeRoutine()
    {
        /** 0.5초 후 */
        yield return new WaitForSeconds(1.0f);

        /** PlayStop함수 호출 */
        GameManager.GMInstance.PlayStop();
    }

    /** 게임 클리어 함수 */
    public void GameClear()
    {
        /** 시간이 다 되었다면 */
        if (GameManager.GMInstance.gameTime == GameManager.GMInstance.maxGameTime || GameManager.GMInstance.SpawnerRef.GetbIsBossClear() == true) 
        {
            /** 게임 클리어 판넬 Active On */
            GameClearPanel.SetActive(true);
            /** 게임 시간 멈춤 */
            Time.timeScale = 0;
        }
    }

    /** 로비화면 이동 함수 */
    public void OnClickLobby()
    {
        /** 입장해서 증가하는 이동속도 반영 x */
        GameManager.GMInstance.PlayerSpeed = BasePlayerSpeed;

        /** 게임 시간을 원상복귀 */
        Time.timeScale = 1.0f;
        /** 로비 씬으로 전환되기 때문에 다음 입장을 위해 false로 해준다. */
        GameManager.GMInstance.bIsLive = false;
        /** 로비화면 이동 */
        SceneManager.LoadScene("Lobby");
    }

    /** 로비화면 이동 함수 */
    public void OnClickReStart()
    {
        /** 게임 시간을 원상복귀 */
        Time.timeScale = 1.0f;

        /** 플레이 시간 초기화 */
        GameManager.GMInstance.PlayTime = 0.0f;

        /** 플레이어 스피드는 기존의 베이스 이동속도를 가져온다. */
        GameManager.GMInstance.PlayerSpeed = GameManager.GMInstance.GetPlayerBaseSpeed();

        /** 로비 씬으로 전환되기 때문에 다음 입장을 위해 false로 해준다. */
        GameManager.GMInstance.bIsLive = false;

        /** 입장해서 증가하는 이동속도 반영 x */
        GameManager.GMInstance.PlayerSpeed = BasePlayerSpeed;

        /** 로비화면 이동 */
        SceneManager.LoadScene("PlayScene");
    }

    /** 효과음 off 함수 */
    public void OnClickSoundOff()
    {
        GameManager.GMInstance.SoundManagerRef.bIsSFXOn = false;

        /** 저장된 효과음 개수만큼 반복 */
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            /** 효과음 소거 */
            GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].mute = true;
        }

        PlayScene_EffectSound_On_Check.gameObject.SetActive(false);
        PlayScene_EffectSound_Off_Check.gameObject.SetActive(true);
    }

    /** 효과음 on 함수 */
    public void OnClickSoundOn()
    {
        GameManager.GMInstance.SoundManagerRef.bIsSFXOn = true;

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 저장된 효과음 개수만큼 반복 */
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            /** 효과음 소거 */
            GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].mute = false;
        }

        PlayScene_EffectSound_On_Check.gameObject.SetActive(true);
        PlayScene_EffectSound_Off_Check.gameObject.SetActive(false);
    }

    /** 배경음 on 함수 */
    public void OnClickBGMOn()
    {
        GameManager.GMInstance.SoundManagerRef.bIsBGMOn = true;

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            /** 배경음 On */
            GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].mute = false;
        }

        PlayScene_BGM_On_Check.gameObject.SetActive(true);
        PlayScene_BGM_Off_Check.gameObject.SetActive(false);
    }

    /** 배경음 off 함수 */
    public void OnClickBGMOff()
    {
        GameManager.GMInstance.SoundManagerRef.bIsBGMOn = false;

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            /** 배경음 off */
            GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].mute = true;
        }

        PlayScene_BGM_On_Check.gameObject.SetActive(false);
        PlayScene_BGM_Off_Check.gameObject.SetActive(true);
    }
}
