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
    bool bIsFirstStart;
    float BasePlayerSpeed;

    public float HpUpTime;
    public float MonsterHpUpRate = 0.1f;
    public bool bIsMonsterHpUp;
    public float NextMonsterHp;
    public float BaseMonsterHp;
    int HpLevel;
    int GainMagicStone;

    // public GameObject GameClearPanel;
    // public GameObject GameOverPanel;
    public GameObject ConfigPanel;
    public LevelUp WizardLevelUp;

    [Header("---GainMagicStoneText---")]
    public Text GameClearMagicStoneTxt;
    public Text GameOverMagicStoneTxt;

    [Header("---Damage---")]
    public Transform Damage_Canvas;
    public GameObject m_DamageRoot;
    GameObject a_DmgClone;
    DamageText DmgTxt;
    Vector3 StartCacPos;

    [Header("---Volum---")]
    public Slider BGMVolum;
    public Slider SFXVolum;

    [Header("---DungeonTile---")]
    public GameObject GrassLandTile;
    public GameObject RockLandTile;
    public GameObject DeathLandTile;

    [Header("---Check---")]
    public Image PlayScene_BGM_Off_Check;
    public Image PlayScene_BGM_On_Check;
    public Image PlayScene_EffectSound_On_Check;
    public Image PlayScene_EffectSound_Off_Check;

    public int GetGameEndMagicStone()
    {
        return GainMagicStone;
    }
    public void SetGameEndMagicStone(int magicStone)
    {
        GainMagicStone = magicStone;
    }

    void Start()
    {
        /** ���� ���õ� ��尡 �ʿ������� */
        if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.GrassLand)
        {
            /** �ʿ����� ����� ��� */
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Dungeon_Grassland);

            GrassLandTile.SetActive(true);
            RockLandTile.SetActive(false);
            DeathLandTile.SetActive(false);
        }
        /** ���� ���õ� ��尡 �ϼ������� */
        else if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.RockLand)
        {
            /** �ϼ����� ����� ��� */
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Dungeon_Rockland);

            GrassLandTile.SetActive(false);
            RockLandTile.SetActive(true);
            DeathLandTile.SetActive(false);
        }
        /** ���� ���õ� ��尡 ������ ���̶�� */
        else if (GameManager.GMInstance.SelectDungeonMode == ESelectDungeon.DeathLand)
        {
            /** ������ �� ����� ��� */
            GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Dungeon_Deathland);

            GrassLandTile.SetActive(false);
            RockLandTile.SetActive(false);
            DeathLandTile.SetActive(true);
        }

        /** �ʱ� ����� ���� �� �ʱ�ȭ */
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            BGMVolum.value = GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume;
        }
        /** �ʱ� ȿ���� ���� �� �ʱ�ȭ */
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            SFXVolum.value = GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume;
        }


        /** ��ų ���� �ؽ�ƮUI �Լ� ȣ�� */
        TextInit();
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
        GameManager.GMInstance.PlaySceneManagerRef = this;

        /** ���� �÷��� ���� �ʱ�ȭ */
        GameManager.GMInstance.PlaySceneInit(false, 0, GameManager.GMInstance.MaxHealth);

        /** ���� ���� Ȱ��ȭ */
        GameManager.GMInstance.bIsLive = true;

        GameManager.GMInstance.InfoInit();

        /** ���� �� �÷��̾� �̵��ӵ� ���� */
        BasePlayerSpeed = GameManager.GMInstance.PlayerSpeed;

        /** ó�� �����ϴ��� Ȯ���ϱ� ���� ���� */
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

        /** 20�ʸ��� ��� */
        if (HpUpTime / 20 >= 1)
        {
            bIsMonsterHpUp = true;

            /** �ð� �ʱ�ȭ */
            HpUpTime = 0.0f;

            HpLevel++;
            MonsterHpUpRate += 0.1f;

            NextMonsterHp = BaseMonsterHp + BaseMonsterHp * MonsterHpUpRate;

            Debug.Log("BaseMonsterHp * MonsterHpUpRate " + BaseMonsterHp * MonsterHpUpRate);
            Debug.Log("NextMonsterHp " + NextMonsterHp);

            /** ���� ü�� ���� */
            // GameManager.GMInstance.MoveRef.MaxHp += GameManager.GMInstance.MoveRef.MaxHp * HpUpRate;

            if (HpLevel == 9)
            {
                /** ���� ���͸� ���� �ʱ�ȭ */
                MonsterHpUpRate = 0;
                /** ���� ���͸� ���� �ʱ�ȭ */
                HpLevel = 0;
            }       
        }
    }

    public void OnClickReSkill()
    {
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
    }

    public void OnClickTicketSkillRe()
    {
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ���� ���� ������ ĳ���Ͷ�� */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** ������ ĳ���� ��ų �缳�� */
            GameManager.GMInstance.UiLevelUp.Next();
        }
        /** ���� ���� �ü� ĳ���Ͷ�� */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            /** �ü� ��ų �缳�� */
            GameManager.GMInstance.AcherLevelUpRef.Next();
        }
    }

    /** ȯ�漳�� Ŭ�� */
    public void OnClickConfig()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        ConfigPanel.SetActive(true);

        /** ȭ���� ����� ������ ���� ������ ���� false�� ���ش�. */
        GameManager.GMInstance.bIsLive = false;

        Time.timeScale = 0.0f;
    }

    /** ȯ�漳�� �ݱ� ��ư */
    public void OnClickConfig_Resume()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        Time.timeScale = 1.0f;

        /** ȭ���� ����� ������ ���� ������ ���� false�� ���ش�. */
        GameManager.GMInstance.bIsLive = true;

        ConfigPanel.SetActive(false);
    }

    /** Lobby Scene���� �̵� */
    public void OnClickConfig_GoLobby()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** �����ؼ� �����ϴ� �̵��ӵ� �ݿ� x */
        GameManager.GMInstance.PlayerSpeed = BasePlayerSpeed;

        /** �ð��帧 ����ȭ */
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("Lobby");
    }

    public void TextInit()
    {
        if (GameManager.GMInstance.level == 1)
        {
            SkillSelectPaneltext.text = "��ų�� ������ �ּ���!";
        }
        else if (GameManager.GMInstance.level != 1)
        {
            SkillSelectPaneltext.text = "�����մϴ�!\n ���� ��!";
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        /** �÷��̾� ���� �Լ� off */
        GameManager.GMInstance.bIsLive = false;

        /** GameOverImage ���� */
        GameOverImage.gameObject.SetActive(true);

        /** óġ�� ������ 10% ��ŭ ������ ȹ�� */
        GainMagicStone = Mathf.RoundToInt(GameManager.GMInstance.killcount * 0.1f);

        /** ȹ���� �������� �����ش�. */
        GameManager.GMInstance.MagicStone += GainMagicStone;

        GameOverMagicStoneTxt.text = GainMagicStone + " ������";

        /** ������ ���� */
        GameManager.GMInstance.CoinManagerRef.JsonSave();

        /** 1�� �� */
        yield return new WaitForSeconds(1.0f);

        /** GameOverPanel On */
        GameManager.GMInstance.GameFailedAdsPanelRef.EndGamePanelrect.localScale = Vector3.one;

        StartCoroutine(GameOverTimeRoutine());
        
    }

    IEnumerator GameOverTimeRoutine()
    {
        /** 0.5�� �� */
        yield return new WaitForSeconds(1.0f);

        /** PlayStop�Լ� ȣ�� */
        GameManager.GMInstance.PlayStop();
    }

    /** ���� Ŭ���� �Լ� */
    public void GameClear()
    {
        /** �ð��� �� �Ǿ��ٸ� */
        if (GameManager.GMInstance.gameTime == GameManager.GMInstance.maxGameTime || GameManager.GMInstance.SpawnerRef.GetbIsBossClear() == true) 
        {
            GameManager.GMInstance.EndGameAdsPanelRef.EndGamePanelrect.localScale = Vector3.one;

            /** óġ�� ������ 10% ��ŭ ������ ȹ�� */
            GainMagicStone = Mathf.RoundToInt(GameManager.GMInstance.killcount * 0.1f);

            /** ȹ���� �������� �����ش�. */
            GameManager.GMInstance.MagicStone += GainMagicStone;

            GameClearMagicStoneTxt.text = GainMagicStone + " ������";

            /** ������ ���� */
            GameManager.GMInstance.CoinManagerRef.JsonSave();

            /** ���� �ð� ���� */
            Time.timeScale = 0;
        }
    }

    /** �κ�ȭ�� �̵� �Լ� */
    public void OnClickLobby()
    {
        /** �����ؼ� �����ϴ� �̵��ӵ� �ݿ� x */
        GameManager.GMInstance.PlayerSpeed = BasePlayerSpeed;

        /** ���� �ð��� ���󺹱� */
        Time.timeScale = 1.0f;
        /** �κ� ������ ��ȯ�Ǳ� ������ ���� ������ ���� false�� ���ش�. */
        GameManager.GMInstance.bIsLive = false;
        /** �κ�ȭ�� �̵� */
        SceneManager.LoadScene("Lobby");
    }

    /** �κ�ȭ�� �̵� �Լ� */
    public void OnClickReStart()
    {
        /** ���� �ð��� ���󺹱� */
        Time.timeScale = 1.0f;

        /** �÷��� �ð� �ʱ�ȭ */
        GameManager.GMInstance.PlayTime = 0.0f;

        /** �÷��̾� ���ǵ�� ������ ���̽� �̵��ӵ��� �����´�. */
        GameManager.GMInstance.PlayerSpeed = GameManager.GMInstance.GetPlayerBaseSpeed();

        /** �κ� ������ ��ȯ�Ǳ� ������ ���� ������ ���� false�� ���ش�. */
        GameManager.GMInstance.bIsLive = false;

        /** �����ؼ� �����ϴ� �̵��ӵ� �ݿ� x */
        GameManager.GMInstance.PlayerSpeed = BasePlayerSpeed;

        /** �κ�ȭ�� �̵� */
        SceneManager.LoadScene("PlayScene");
    }

    /** ȿ���� off �Լ� */
    public void OnClickSoundOff()
    {
        GameManager.GMInstance.SoundManagerRef.bIsSFXOn = false;

        /** ����� ȿ���� ������ŭ �ݺ� */
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            /** ȿ���� �Ұ� */
            GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].mute = true;
        }

        PlayScene_EffectSound_On_Check.gameObject.SetActive(false);
        PlayScene_EffectSound_Off_Check.gameObject.SetActive(true);
    }

    /** ȿ���� on �Լ� */
    public void OnClickSoundOn()
    {
        GameManager.GMInstance.SoundManagerRef.bIsSFXOn = true;

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ����� ȿ���� ������ŭ �ݺ� */
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            /** ȿ���� �Ұ� */
            GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].mute = false;
        }

        PlayScene_EffectSound_On_Check.gameObject.SetActive(true);
        PlayScene_EffectSound_Off_Check.gameObject.SetActive(false);
    }

    /** ����� on �Լ� */
    public void OnClickBGMOn()
    {
        GameManager.GMInstance.SoundManagerRef.bIsBGMOn = true;

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            /** ����� On */
            GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].mute = false;
        }

        PlayScene_BGM_On_Check.gameObject.SetActive(true);
        PlayScene_BGM_Off_Check.gameObject.SetActive(false);
    }

    /** ����� off �Լ� */
    public void OnClickBGMOff()
    {
        GameManager.GMInstance.SoundManagerRef.bIsBGMOn = false;

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            /** ����� off */
            GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].mute = true;
        }

        PlayScene_BGM_On_Check.gameObject.SetActive(false);
        PlayScene_BGM_Off_Check.gameObject.SetActive(true);
    }

    /** TODO ## PlaySceneManager.cs ����� ���� ���� */
    public void SetBGMVolum(float volum)
    {
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = volum;
        }
    }

    /** TODO ## PlaySceneManager.cs ȿ���� ���� ���� */
    public void SetSFXVolum(float volum)
    {
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = volum;
        }
    }

    /** TODO ## PlaySceneManager ������ Test */
    public void DamageTxt(float a_Value, Vector3 a_Pos, Color a_Color)
    {
        if (m_DamageRoot == null || Damage_Canvas == null)
            return;

        a_DmgClone = (GameObject)Instantiate(m_DamageRoot);
        a_DmgClone.transform.SetParent(Damage_Canvas);
        DmgTxt = a_DmgClone.GetComponent<DamageText>();

        if (DmgTxt != null)
            DmgTxt.initDamge(a_Value, a_Color);
        StartCacPos = new Vector3(a_Pos.x, a_Pos.y, 0.0f);
        a_DmgClone.transform.position = StartCacPos;
    }
}
