using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using My;
using static Define;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject ConfigPanel;
    public GameObject MyCharacter;
    
    public GameObject DungeonSelectPanel;
    public GameObject AbilityCheckPanel;
    public GameObject InGameMoneyPanel;
    public GameObject EquipmentGachaPanel;
    public GameObject TicketPanel;

    public GameObject DayQuestPanel;
    public GameObject WeekQuestPanel;

    public GameObject[] SelectCharacterPrefabs;


    /** TODO ## LobbyManager.cs ĳ���� �ɷ�ġ ���� ���� */
    public int Up_Hp;
    public int Up_Damage;
    public int Up_Defense;

    [Header("---EconomyText---")]
    public Text MagicStonText;
    public Text DiamondText;

    //[Header("---Button---")]
    //public Button SkillDamageBtn;
    //public Button MaxHpUpBtn;
    //public Button SpeedUpBtn;
    //public Button CriticalUpBtn;
    //public Button CriticalDamageUpBtn;

    [Header("---Volum---")]
    public Slider BGMVolum;
    public Slider SFXVolum;

    /** ���� ���� ������ ��ư */
    public Button ModeSelectBtn;

    [Header("---QuestTime---")]
    public Text DayRemainTime;
    public Text WeekRemainTime;

    /** ĳ���� �ɷ�ġ â Text���� */
    [Header("---CharInfoText---")]
    public Text AttackText;
    public Text MaxHPText;
    public Text CharSpeedText;
    public Text CharCriticalPer;
    public Text CharCriticalDamage;

    /** ĳ���� �ɷ�ġ ���� �ؽ�Ʈ */
    [Header("---CharAbilityLevelText---")]
    public Text AttackLevelText;
    public Text MaxHpLevelText;
    public Text CharSpeedLevelText;
    public Text CharCriticalPerLevelText;
    public Text CharCriticalDamageLevelText;

    [Header("---Check---")]
    public Image BGM_Off_Check;
    public Image BGM_On_Check;
    public Image EffectSound_On_Check;
    public Image EffectSound_Off_Check;

    [Header("---PriceText---")]
    public Text DamageUpPriceText;
    public Text MaxHpUpPricelText;
    public Text SpeedUpPriceText;
    public Text CriticalUpPriceText;
    public Text CriticalDamageUpPriceText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GMInstance.SoundManagerRef.PlayBGM(SoundManager.BGM.Lobby);

        // Time.timeScale = 1;

        /** ���� ȭ�� �κ��*/
        GameManager.GMInstance.CurrentScene = Define.ESceneType.LobbyScene;

        /** MyCharacter�� GameManager�� �����ϰ� �ִ� ĳ���͸� ��ȯ */
        // MyCharacter = Instantiate(SelectCharacterPrefabs[(int)GameManager.GMInstance.CurrentChar]);
        /** MyCharacterũ�⸦ 3x3x3���� �ٲ��ش�. */
        // MyCharacter.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        /** ���� ������� �����ִٸ� */
        if (GameManager.GMInstance.SoundManagerRef.bIsBGMOn == false)
        {
            /** ����� off üũ�ڽ� Ȱ��ȭ */
            BGM_Off_Check.gameObject.SetActive(true);
            /** ����� on üũ�ڽ� ��Ȱ��ȭ */
            BGM_On_Check.gameObject.SetActive(false);
        }

        /** ���� ȿ������ �����ִٸ� */
        if (GameManager.GMInstance.SoundManagerRef.bIsSFXOn == false)
        {
            /** ȿ���� off üũ�ڽ� Ȱ��ȭ */
            EffectSound_Off_Check.gameObject.SetActive(true);
            /** ȿ���� on üũ�ڽ� Ȱ��ȭ */
            EffectSound_On_Check.gameObject.SetActive(false);
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

        /** ���� �� �ְ� ����Ʈ ũ�� �Ⱥ��̱� */
        WeekQuestPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        InitText();
        // CharInfoInit();
    }

    void Update()
    {
        /** TODO ## LobbySceneManager.cs ���� �̱������� ���� �÷��� ����*/
        /** ���� ĳ���Ͱ� ������ */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WorriorChar)
        {
            /** ���۹�ư ��Ȱ��ȭ */
            ModeSelectBtn.interactable = false;
        }
        /** ���簡 �ƴ϶�� */
        else
        {
            /** ���۹�ư Ȱ��ȭ */
            ModeSelectBtn.interactable = true;
        }

        /** ���Ϲ̼� ���� �ð� ��� */
        DayRemainTime.text = (23 - GetCurrentHour()).ToString() + ":" + (60 - GetCurrentMinute()).ToString() + ":" + (60 - GetCurrentSecond()).ToString();

        /** �ְ��̼� ���� �ð� ��� */
        WeekRemainTime.text = (7 - GetCurrentDay()).ToString() + ":" + (23 - GetCurrentHour()).ToString() + ":" + (60 - GetCurrentMinute()).ToString();
    }

    int GetCurrentDay()
    {
        return (int)(DateTime.Now).DayOfWeek;
    }

    int GetCurrentSecond()
    {
        return (DateTime.Now).Second;
    }

    int GetCurrentMinute()
    {
        return (DateTime.Now).Minute;
    }

    int GetCurrentHour()
    {
        return (DateTime.Now).Hour;
    }

    public void CharInfoInit()
    {
        // AttackText.text

        MaxHPText.text = GameManager.GMInstance.MaxHealth.ToString("F0") + " HP";
        CharSpeedText.text = (100 * GameManager.GMInstance.PlayerSpeed).ToString("F1") + "%";
        CharCriticalPer.text = (100 * GameManager.GMInstance.GetCriticalPercent()).ToString("F2") + "%";
        CharCriticalDamage.text = (100 * GameManager.GMInstance.GetCriticalDamage()).ToString("F1") + "%";
        AttackText.text = (100 * GameManager.GMInstance.SkillDamageUpRate).ToString("F0") + "%";

        CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalPerLevel + " ũ��Ƽ�� Ȯ�� ����";
        CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalDamageLevel + " ũ��Ƽ�� ������ ����";
        MaxHpLevelText.text = "Lv" + GameManager.GMInstance.MaxHpLevel + " �ִ� ü�� ����";
        CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.CharSpeedLevel + " �̵� �ӵ� ����";
        AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageLevel + " ��ų ������ ����";
    }


    public void OnClickMenuBtn()
    {
        /** MenuPanel Ȱ��ȭ */
        MenuPanel.SetActive(true);
    }

    public void OnClickMenuCloseBtn()
    {
        /** MenuPanel ��Ȱ��ȭ */
        MenuPanel.SetActive(false);
    }

    public void OnClickGameplayBtn()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        DungeonSelectPanel.SetActive(true);
    }

    /** ���� ĳ���� ��ư ������ �� */
    public void OnClickNextCharacter()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);


        /** ���� ĳ���� Ÿ���� ��������? */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** ���� ĳ���� Ÿ���� �ü��� �ȴ�. */
            GameManager.GMInstance.CurrentChar = ECharacterType.AcherChar;
        }
        /** ���� ĳ���� Ÿ���� �ü����? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            /** ���� ĳ���� Ÿ���� ���簡 �ȴ�. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WorriorChar;
        }
        /** ���� ĳ���� Ÿ���� ������? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.WorriorChar)
        {
            /** ���� ĳ���� Ÿ���� �����簡 �ȴ�. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WizardChar;
        }
    }

    /** ���� ĳ���� ��ư ������ �� */
    public void OnClickPreviousCharacter()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);


        /** ���� ĳ���� Ÿ���� ��������? */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** ���� ĳ���� Ÿ���� ���簡 �ȴ�. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WorriorChar;
        }
        /** ���� ĳ���� Ÿ���� �ü����? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            /** ���� ĳ���� Ÿ���� �����簡 �ȴ�. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WizardChar;
        }
        /** ���� ĳ���� Ÿ���� ������? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.WorriorChar)
        {
            /** ���� ĳ���� Ÿ���� �ü��� �ȴ�. */
            GameManager.GMInstance.CurrentChar = ECharacterType.AcherChar;
        }
    }

    public void OnClickConfigBtn()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
        /** ȯ�漳�� â off */
        ConfigPanel.SetActive(true);
    }

    public void OnClickCloseBtn()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ȯ�漳�� â off */
        ConfigPanel.SetActive(false);
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

        EffectSound_On_Check.gameObject.SetActive(false);
        EffectSound_Off_Check.gameObject.SetActive(true);
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

        EffectSound_On_Check.gameObject.SetActive(true);
        EffectSound_Off_Check.gameObject.SetActive(false);
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

        BGM_On_Check.gameObject.SetActive(true);
        BGM_Off_Check.gameObject.SetActive(false);
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

        BGM_On_Check.gameObject.SetActive(false);
        BGM_Off_Check.gameObject.SetActive(true);
    }

    /** TODO ## LobbySceneManager.cs ����� ���� ���� */
    public void SetBGMVolum(float volum)
    {
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.BGMPlayers.Length; i++)
        {
            GameManager.GMInstance.SoundManagerRef.BGMPlayers[i].volume = volum;
        }
    }

    /** TODO ## LobbySceneManager.cs ȿ���� ���� ���� */
    public void SetSFXVolum(float volum)
    {
        for (int i = 0; i < GameManager.GMInstance.SoundManagerRef.SFXPlayers.Length; i++)
        {
            GameManager.GMInstance.SoundManagerRef.SFXPlayers[i].volume = volum;
        }
    }

    public void OnClickCloseSelectDungeonPanel()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ���� �ǳ� ���� */
        DungeonSelectPanel.gameObject.SetActive(false);
    }

    public void OnClickEnterGrassLand()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ���ӸŴ��� ��带 �ʿ������ �ٲٰ� */
        GameManager.GMInstance.SelectDungeonMode = ESelectDungeon.GrassLand;

        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickEnterRockLand()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ���ӸŴ��� ��带 �ʿ������ �ٲٰ� */
        GameManager.GMInstance.SelectDungeonMode = ESelectDungeon.RockLand;

        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickEnterDeathLand()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ���ӸŴ��� ��带 �ʿ������ �ٲٰ� */
        GameManager.GMInstance.SelectDungeonMode = ESelectDungeon.DeathLand;

        SceneManager.LoadScene("PlayScene");
    }


    /** ũ��Ƽ��Ȯ�� ���� */
    public void OnClickCrticalUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ������ ���������� ���׷��̵� ������ ���ٸ�? */
        if (GameManager.GMInstance.MagicStone - GameManager.GMInstance.CriticalUpPrice < 0)
        {
            return;
        }


        // -------------------���� �߰�-----------------------
        GameManager.GMInstance.MagicStone -= GameManager.GMInstance.CriticalUpPrice;
        /** ��ų������ ������ ������ UpPriceRate��ŭ ���� */
        GameManager.GMInstance.CriticalUpPrice += GameManager.GMInstance.UpPriceRate;
        /** ������ �ؽ�Ʈ �ʱ�ȭ */
        MagicStonText.text = GameManager.GMInstance.MagicStone.ToString();
        // -------------------���� �߰�-----------------------

        /** ũ��Ƽ�� Ȯ�� ��� */
        // GameManager.GMInstance.SetCriticalPercent(GameManager.GMInstance.GetCriticalPercent() + GameManager.GMInstance.CharCriticalPerUpRate);
        GameManager.GMInstance.CriticalUpSum += GameManager.GMInstance.CharCriticalPerUpRate;
        GameManager.GMInstance.CharacterCriticalPercent += GameManager.GMInstance.CharCriticalPerUpRate;

        /** ũ��Ƽ�� ����Ȯ�� ���� 1+ */
        // GameManager.GMInstance.CharCriticalPerLevel++;
        GameManager.GMInstance.CriticalUpLevel++;

        /** ũ��Ƽ�� ���� ���� �ؽ�Ʈ �ʱ�ȭ */
        // CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalPerLevel + " ũ��Ƽ�� Ȯ�� ����";
        CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CriticalUpLevel + " ũ��Ƽ�� Ȯ�� ����";
        /** �ɷ�ġ â ũ��Ƽ�� Ȯ�� �ʱ�ȭ */
        CharCriticalPer.text = (100 * GameManager.GMInstance.GetCriticalPercent()).ToString("F2") + "%";
        /** ������ ���� �ؽ�Ʈ �ʱ�ȭ */
        CriticalUpPriceText.text = GameManager.GMInstance.CriticalUpPrice + " ������";

        GameManager.GMInstance.CoinManagerRef.JsonSave();
        GameManager.GMInstance.UpGradeManagerRef.JsonSave();
    }

    /** ũ��Ƽ�� ������ ���� */
    public void OnClickCrticalDamageUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ������ ���������� ���׷��̵� ������ ���ٸ�? */
        if (GameManager.GMInstance.MagicStone - GameManager.GMInstance.CriticalDamageUpPrice < 0)
        {
            return;
        }

        // -------------------���� �߰�-----------------------
        GameManager.GMInstance.MagicStone -= GameManager.GMInstance.CriticalDamageUpPrice;
        /** ��ų������ ������ ������ UpPriceRate��ŭ ���� */
        GameManager.GMInstance.CriticalDamageUpPrice += GameManager.GMInstance.UpPriceRate;
        /** ������ �ؽ�Ʈ �ʱ�ȭ */
        MagicStonText.text = GameManager.GMInstance.MagicStone.ToString();
        // -------------------���� �߰�-----------------------

        /** ũ��Ƽ�� ������ ��� */
        // GameManager.GMInstance.SetCriticaDamage(GameManager.GMInstance.GetCriticalDamage() + GameManager.GMInstance.CharCriticalDamageUpRate);
        GameManager.GMInstance.CriticalDamageUpSum += GameManager.GMInstance.CharCriticalDamageUpRate;
        GameManager.GMInstance.CharacterCriticalDamage += GameManager.GMInstance.CharCriticalDamageUpRate;

        /** ũ��Ƽ�� ������ ���� ���� 1+ */
        // GameManager.GMInstance.CharCriticalDamageLevel++;
        GameManager.GMInstance.CriticalDamageUpLevel++;
        /** ũ��Ƽ�� ������ ���� ���� �ؽ�Ʈ �ʱ�ȭ */
        // CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalDamageLevel + " ũ��Ƽ�� ������ ����";
        CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CriticalDamageUpLevel + " ũ��Ƽ�� ������ ����";
        /** �ɷ�ġ â ũ��Ƽ�� ������ ��ġ �ʱ�ȭ */
        CharCriticalDamage.text = (100 * GameManager.GMInstance.GetCriticalDamage()).ToString("F1") + "%";
        /** ������ ���� �ؽ�Ʈ �ʱ�ȭ */
        CriticalDamageUpPriceText.text = GameManager.GMInstance.CriticalDamageUpPrice + " ������";

        GameManager.GMInstance.CoinManagerRef.JsonSave();
        GameManager.GMInstance.UpGradeManagerRef.JsonSave();
    }

    /** ü�� ���� */
    public void OnClickMaxHpUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ������ ���������� ���׷��̵� ������ ���ٸ�? */
        if (GameManager.GMInstance.MagicStone - GameManager.GMInstance.MaxHpUpPrice < 0)
        {
            return;
        }

        // -------------------���� �߰�-----------------------
        GameManager.GMInstance.MagicStone -= GameManager.GMInstance.MaxHpUpPrice;
        /** ��ų������ ������ ������ UpPriceRate��ŭ ���� */
        GameManager.GMInstance.MaxHpUpPrice += GameManager.GMInstance.UpPriceRate;
        /** ������ �ؽ�Ʈ �ʱ�ȭ */
        MagicStonText.text = GameManager.GMInstance.MagicStone.ToString();
        // -------------------���� �߰�-----------------------


        /** ���� �ִ�ü���� 10% ���� ��� */
        //GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHealth * 0.1f;

        /** �ִ�ü���� 2�� ���� ��� */
        // GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHpLevelUpRate;
        GameManager.GMInstance.MaxHpUpSum += GameManager.GMInstance.MaxHpLevelUpRate;
        GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHpLevelUpRate;

        /** ĳ���� �ִ�ü�� �������� 1+ */
        GameManager.GMInstance.MaxHpLevel++;

        /** �ִ�ü������ ���� �ؽ�Ʈ �ʱ�ȭ */
        MaxHpLevelText.text = "Lv" + GameManager.GMInstance.MaxHpLevel + " �ִ� ü�� ����";

        /** �ɷ�ġ â �̵��ӵ� ǥ�� �ʱ�ȭ */
        MaxHPText.text = GameManager.GMInstance.MaxHealth.ToString("F0") + " HP";
        /** ������ ���� �ؽ�Ʈ �ʱ�ȭ */
        MaxHpUpPricelText.text = GameManager.GMInstance.MaxHpUpPrice + " ������";

        GameManager.GMInstance.CoinManagerRef.JsonSave();
        GameManager.GMInstance.UpGradeManagerRef.JsonSave();
    }


    /** �̵��ӵ� ���� */
    public void OnClickSpeedUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ������ ���������� ���׷��̵� ������ ���ٸ�? */
        if (GameManager.GMInstance.MagicStone - GameManager.GMInstance.SpeedUpPrice < 0)
        {
            return;
        }

        // -------------------���� �߰�-----------------------
        GameManager.GMInstance.MagicStone -= GameManager.GMInstance.SpeedUpPrice;
        /** ��ų������ ������ ������ UpPriceRate��ŭ ���� */
        GameManager.GMInstance.SpeedUpPrice += GameManager.GMInstance.UpPriceRate;
        /** ������ �ؽ�Ʈ �ʱ�ȭ */
        MagicStonText.text = GameManager.GMInstance.MagicStone.ToString();
        // -------------------���� �߰�-----------------------

        /** �̵��ӵ� ���� ��� */
        // GameManager.GMInstance.PlayerSpeed += GameManager.GMInstance.CharSpeedLevelUpRate;
        GameManager.GMInstance.SpeedUpSum += GameManager.GMInstance.CharSpeedLevelUpRate;
        GameManager.GMInstance.PlayerSpeed += GameManager.GMInstance.CharSpeedLevelUpRate;

        /** �̵��ӵ� ���� ���� 1+ */
        // GameManager.GMInstance.CharSpeedLevel++;
        GameManager.GMInstance.SpeedUpLevel++;

        /** �̵��ӵ� ���� �ؽ�Ʈ �ʱ�ȭ */
        // CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.CharSpeedLevel + " �̵� �ӵ� ����";
        CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.SpeedUpLevel + " �̵� �ӵ� ����";

        /** �ɷ�ġ â �̵��ӵ� ǥ�� �ʱ�ȭ */
        CharSpeedText.text = (100 * GameManager.GMInstance.PlayerSpeed).ToString("F1") + "%";
        /** ������ ���� �ؽ�Ʈ �ʱ�ȭ */
        SpeedUpPriceText.text = GameManager.GMInstance.SpeedUpPrice + " ������";

        GameManager.GMInstance.CoinManagerRef.JsonSave();
        GameManager.GMInstance.UpGradeManagerRef.JsonSave();
    }

    /** ������ ���� */
    public void OnClickDamageUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ������ ���������� ���׷��̵� ������ ���ٸ�? */
        if (GameManager.GMInstance.MagicStone - GameManager.GMInstance.SkillDamageUpPrice <= 0)
        {
            return;
        }

        // -------------------���� �߰�-----------------------
        GameManager.GMInstance.MagicStone -= GameManager.GMInstance.SkillDamageUpPrice;
        /** ��ų������ ������ ������ UpPriceRate��ŭ ���� */
        GameManager.GMInstance.SkillDamageUpPrice += GameManager.GMInstance.UpPriceRate;
        /** ������ �ؽ�Ʈ �ʱ�ȭ */
        MagicStonText.text = GameManager.GMInstance.MagicStone.ToString();
        // -------------------���� �߰�-----------------------

        /** ��ų ������ ���� ���� 1+ */
        //GameManager.GMInstance.SkillDamageLevel++;
        GameManager.GMInstance.SkillDamageUpLevel++;

        /** ��ų ������ ���� ���� */
        // GameManager.GMInstance.SetSkillDamageUp(GameManager.GMInstance.GetSkillDamageUp() + GameManager.GMInstance.SkillDamageUpRate);
        GameManager.GMInstance.SkillDamageUpSum += GameManager.GMInstance.SkillDamageUpRate;

        /** �ɷ�ġ â ��ų ������ ǥ�� �ʱ�ȭ */
        // AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageLevel + " ��ų ������ ����";
        AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageUpLevel + " ��ų ������ ����";

        /** �ɷ�ġ â ��ų ������ ǥ�� �ʱ�ȭ */
        // AttackText.text = (100 * GameManager.GMInstance.GetSkillDamageUp()).ToString("F0") + "%";
        AttackText.text = (100 * GameManager.GMInstance.SkillDamageUpSum).ToString("F0") + "%";
        /** ������ ���� �ؽ�Ʈ �ʱ�ȭ */
        DamageUpPriceText.text = GameManager.GMInstance.SkillDamageUpPrice + " ������";

        GameManager.GMInstance.CoinManagerRef.JsonSave();
        GameManager.GMInstance.UpGradeManagerRef.JsonSave();
    }

    public void OnClickAbilityCheckOpen()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        AbilityCheckPanel.gameObject.SetActive(true);
    }

    public void OnClickAbilityCheckClose()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
        AbilityCheckPanel.gameObject.SetActive(false);
    }

    public void OnClickOpenInGameStore()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
       
        EquipmentGachaPanel.gameObject.SetActive(false);
        TicketPanel.SetActive(false);

        InGameMoneyPanel.gameObject.SetActive(true);
    }

    public void OnClickOpenEquipmentGacha()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        InGameMoneyPanel.gameObject.SetActive(false);
        TicketPanel.SetActive(false);

        EquipmentGachaPanel.gameObject.SetActive(true);
        

    }

    //[Header("---PriceText---")]
    //public Text DamageUpPriceText;
    //public Text MaxHpUpPricelText;
    //public Text SpeedUpPriceText;
    //public Text CriticalUpPriceText;
    //public Text CriticalDamageUpPriceText;

    void InitText()
    {
        /** ������ �ؽ�Ʈ ���� �ʱ�ȭ */
        MagicStonText.text = GameManager.GMInstance.MagicStone.ToString();
        /** ���̾� �ؽ�Ʈ ���� �ʱ�ȭ */
        DiamondText.text = GameManager.GMInstance.Diamond.ToString();

        /** ---- ��ų ������ ���� Text ---- */
        AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageUpLevel + " ��ų ������ ����";
        AttackText.text = (100 * GameManager.GMInstance.SkillDamageUpSum).ToString("F0") + "%";
        DamageUpPriceText.text = GameManager.GMInstance.SkillDamageUpPrice + " ������";

        /** ---- �ִ� ü�� ���� Text ---- */
        MaxHpLevelText.text = "Lv" + GameManager.GMInstance.MaxHpLevel + " �ִ� ü�� ����";
        MaxHPText.text = (GameManager.GMInstance.MaxHealth + GameManager.GMInstance.MaxHpUpSum).ToString("F0") + " HP";
        MaxHpUpPricelText.text = GameManager.GMInstance.MaxHpUpPrice + " ������";

        /** ---- �̵� �ӵ� ���� Text ---- */
        CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.SpeedUpLevel + " �̵� �ӵ� ����";
        CharSpeedText.text = (100 * GameManager.GMInstance.PlayerSpeed).ToString("F1") + "%";
        SpeedUpPriceText.text = GameManager.GMInstance.SpeedUpPrice + " ������";

        /** ---- ũ��Ƽ�� Ȯ�� ���� Text ---- */
        CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CriticalUpLevel + " ũ��Ƽ�� Ȯ�� ����";
        CharCriticalPer.text = (100 * GameManager.GMInstance.GetCriticalPercent()).ToString("F2") + "%";
        CriticalUpPriceText.text = GameManager.GMInstance.CriticalUpPrice + " ������";

        /** ---- ũ��Ƽ�� ������ ���� Text ---- */
        CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CriticalDamageUpLevel + " ũ��Ƽ�� ������ ����";
        CharCriticalDamage.text = (100 * GameManager.GMInstance.GetCriticalDamage()).ToString("F1") + "%";
        CriticalDamageUpPriceText.text = GameManager.GMInstance.CriticalDamageUpPrice + " ������";

    }

    public void OnClickDayQuestPanel()
    {
        DayQuestPanel.GetComponent<RectTransform>().localScale = Vector3.one;
        WeekQuestPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    public void OnClickWeekQuestPanel()
    {
        DayQuestPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        WeekQuestPanel.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void OnClickOpenTicket()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        InGameMoneyPanel.gameObject.SetActive(false);
        EquipmentGachaPanel.gameObject.SetActive(false);

        TicketPanel.SetActive(true);
    }
}



