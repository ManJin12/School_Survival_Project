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

        CharInfoInit();
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

        /** ũ��Ƽ�� Ȯ�� ��� */
        GameManager.GMInstance.SetCriticalPercent(GameManager.GMInstance.GetCriticalPercent() + GameManager.GMInstance.CharCriticalPerUpRate);

        /** ũ��Ƽ�� ����Ȯ�� ���� 1+ */
        GameManager.GMInstance.CharCriticalPerLevel++;

        CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalPerLevel + " ũ��Ƽ�� Ȯ�� ����";

        /** �ɷ�ġ â ũ��Ƽ�� Ȯ�� �ʱ�ȭ */
        CharCriticalPer.text =(100 * GameManager.GMInstance.GetCriticalPercent()).ToString("F2") + "%";
    }

    /** ũ��Ƽ�� ������ ���� */
    public void OnClickCrticalDamageUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ũ��Ƽ�� ������ ��� */
        GameManager.GMInstance.SetCriticaDamage(GameManager.GMInstance.GetCriticalDamage() + GameManager.GMInstance.CharCriticalDamageUpRate);

        /** ũ��Ƽ�� ������ ���� ���� 1+ */
        GameManager.GMInstance.CharCriticalDamageLevel++;

        /** ũ��Ƽ�� ������ ���� ���� �ؽ�Ʈ �ʱ�ȭ */
        CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalDamageLevel + " ũ��Ƽ�� ������ ����";

        /** �ɷ�ġ â ũ��Ƽ�� ������ ��ġ �ʱ�ȭ */
        CharCriticalDamage.text = (100 * GameManager.GMInstance.GetCriticalDamage()).ToString("F1") + "%";
    }

    /** ü�� ���� */
    public void OnClickMaxHpUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ���� �ִ�ü���� 10% ���� ��� */
        //GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHealth * 0.1f;

        /** �ִ�ü���� 2�� ���� ��� */
        GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHpLevelUpRate;


        /** ĳ���� �ִ�ü�� �������� 1+ */
        GameManager.GMInstance.MaxHpLevel++;

        /** �ִ�ü������ ���� �ؽ�Ʈ �ʱ�ȭ */
        MaxHpLevelText.text = "Lv" + GameManager.GMInstance.MaxHpLevel + " �ִ� ü�� ����";

        /** �ɷ�ġ â �̵��ӵ� ǥ�� �ʱ�ȭ */
        MaxHPText.text = GameManager.GMInstance.MaxHealth.ToString("F0") + " HP";

    }


    /** �̵��ӵ� ���� */
    public void OnClickSpeedUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** �̵��ӵ� ���� ��� */
        GameManager.GMInstance.PlayerSpeed += GameManager.GMInstance.CharSpeedLevelUpRate;

        /** �̵��ӵ� ���� ���� 1+ */
        GameManager.GMInstance.CharSpeedLevel++;

        /** �̵��ӵ� ���� �ؽ�Ʈ �ʱ�ȭ */
        CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.CharSpeedLevel + " �̵� �ӵ� ����";

        /** �ɷ�ġ â �̵��ӵ� ǥ�� �ʱ�ȭ */
        CharSpeedText.text = (100 * GameManager.GMInstance.PlayerSpeed).ToString("F1") + "%";

    }

    /** ������ ���� */
    public void OnClickDamageUp()
    {
        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ��ų ������ ���� ���� 1+ */
        GameManager.GMInstance.SkillDamageLevel++;

        /** ��ų ������ ���� ���� */
        GameManager.GMInstance.SetSkillDamageUp(GameManager.GMInstance.GetSkillDamageUp() + GameManager.GMInstance.SkillDamageUpRate);

        /** �ɷ�ġ â ��ų ������ ǥ�� �ʱ�ȭ */
        AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageLevel + " ��ų ������ ����";

        /** �ɷ�ġ â ��ų ������ ǥ�� �ʱ�ȭ */
        AttackText.text = (100 * GameManager.GMInstance.GetSkillDamageUp()).ToString("F0") + "%";
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



