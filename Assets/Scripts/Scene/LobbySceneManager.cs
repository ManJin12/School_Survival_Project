using System.Collections;
using System.Collections.Generic;
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

    public GameObject[] SelectCharacterPrefabs;


    /** TODO ## LobbyManager.cs 캐릭터 능력치 증가 변수 */
    public int Up_Hp;
    public int Up_Damage;
    public int Up_Defense;

    /** 전사 시작 방지용 버튼 */
    public Button ModeSelectBtn;


    /** 캐릭터 능력치 창 Text변수 */
    [Header("---CharInfoText---")]
    public Text AttackText;
    public Text MaxHPText;
    public Text CharSpeedText;
    public Text CharCriticalPer;
    public Text CharCriticalDamage;

    /** 캐릭터 능력치 레벨 텍스트 */
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

        /** 현재 화면 로비씬*/
        GameManager.GMInstance.CurrentScene = Define.ESceneType.LobbyScene;

        /** MyCharacter에 GameManager가 저장하고 있는 캐릭터를 소환 */
        // MyCharacter = Instantiate(SelectCharacterPrefabs[(int)GameManager.GMInstance.CurrentChar]);
        /** MyCharacter크기를 3x3x3으로 바꿔준다. */
        // MyCharacter.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        /** 만약 배경음이 꺼져있다면 */
        if (GameManager.GMInstance.SoundManagerRef.bIsBGMOn == false)
        {
            /** 배경음 off 체크박스 활성화 */
            BGM_Off_Check.gameObject.SetActive(true);
            /** 배경음 on 체크박스 비활성화 */
            BGM_On_Check.gameObject.SetActive(false);
        }

        /** 만약 효과음이 꺼져있다면 */
        if (GameManager.GMInstance.SoundManagerRef.bIsSFXOn == false)
        {
            /** 효과음 off 체크박스 활성화 */
            EffectSound_Off_Check.gameObject.SetActive(true);
            /** 효과음 on 체크박스 활성화 */
            EffectSound_On_Check.gameObject.SetActive(false);
        }

        CharInfoInit();
    }

    void Update()
    {
        /** TODO ## LobbySceneManager.cs 전사 미구현으로 인한 플레이 차단*/
        /** 현재 캐릭터가 전사라면 */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WorriorChar)
        {
            /** 시작버튼 비활성화 */
            ModeSelectBtn.interactable = false;
        }
        /** 전사가 아니라면 */
        else
        {
            /** 시작버튼 활성화 */
            ModeSelectBtn.interactable = true;
        }

    }

    public void CharInfoInit()
    {
        // AttackText.text

        MaxHPText.text = GameManager.GMInstance.MaxHealth.ToString("F0") + " HP";
        CharSpeedText.text = (100 * GameManager.GMInstance.PlayerSpeed).ToString("F1") + "%";
        CharCriticalPer.text = (100 * GameManager.GMInstance.GetCriticalPercent()).ToString("F2") + "%";
        CharCriticalDamage.text = (100 * GameManager.GMInstance.GetCriticalDamage()).ToString("F1") + "%";
        AttackText.text = (100 * GameManager.GMInstance.SkillDamageUpRate).ToString("F0") + "%";

        CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalPerLevel + " 크리티컬 확률 증가";
        CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalDamageLevel + " 크리티컬 데미지 증가";
        MaxHpLevelText.text = "Lv" + GameManager.GMInstance.MaxHpLevel + " 최대 체력 증가";
        CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.CharSpeedLevel + " 이동 속도 증가";
        AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageLevel + " 스킬 데미지 증가";
    }


    public void OnClickMenuBtn()
    {
        /** MenuPanel 활성화 */
        MenuPanel.SetActive(true);
    }

    public void OnClickMenuCloseBtn()
    {
        /** MenuPanel 비활성화 */
        MenuPanel.SetActive(false);
    }

    public void OnClickGameplayBtn()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        DungeonSelectPanel.SetActive(true);
    }

    /** 다음 캐릭터 버튼 눌렀을 때 */
    public void OnClickNextCharacter()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);


        /** 현재 캐릭터 타입이 마법사라면? */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** 다음 캐릭터 타입은 궁수가 된다. */
            GameManager.GMInstance.CurrentChar = ECharacterType.AcherChar;
        }
        /** 현재 캐릭터 타입이 궁수라면? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            /** 다음 캐릭터 타입은 전사가 된다. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WorriorChar;
        }
        /** 현재 캐릭터 타입이 전사라면? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.WorriorChar)
        {
            /** 다음 캐릭터 타입은 마법사가 된다. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WizardChar;
        }
    }

    /** 이전 캐릭터 버튼 눌렀을 때 */
    public void OnClickPreviousCharacter()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);


        /** 현재 캐릭터 타입이 마법사라면? */
        if (GameManager.GMInstance.CurrentChar == ECharacterType.WizardChar)
        {
            /** 다음 캐릭터 타입은 전사가 된다. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WorriorChar;
        }
        /** 현재 캐릭터 타입이 궁수라면? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.AcherChar)
        {
            /** 다음 캐릭터 타입은 마법사가 된다. */
            GameManager.GMInstance.CurrentChar = ECharacterType.WizardChar;
        }
        /** 현재 캐릭터 타입이 전사라면? */
        else if (GameManager.GMInstance.CurrentChar == ECharacterType.WorriorChar)
        {
            /** 다음 캐릭터 타입은 궁수가 된다. */
            GameManager.GMInstance.CurrentChar = ECharacterType.AcherChar;
        }
    }

    public void OnClickConfigBtn()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
        /** 환경설정 창 off */
        ConfigPanel.SetActive(true);
    }

    public void OnClickCloseBtn()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 환경설정 창 off */
        ConfigPanel.SetActive(false);
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

        EffectSound_On_Check.gameObject.SetActive(false);
        EffectSound_Off_Check.gameObject.SetActive(true);
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

        EffectSound_On_Check.gameObject.SetActive(true);
        EffectSound_Off_Check.gameObject.SetActive(false);
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

        BGM_On_Check.gameObject.SetActive(true);
        BGM_Off_Check.gameObject.SetActive(false);
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

        BGM_On_Check.gameObject.SetActive(false);
        BGM_Off_Check.gameObject.SetActive(true);
    }

    public void OnClickCloseSelectDungeonPanel()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 던전 판넬 끄기 */
        DungeonSelectPanel.gameObject.SetActive(false);
    }

    public void OnClickEnterGrassLand()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 게임매니저 모드를 초원지대로 바꾸고 */
        GameManager.GMInstance.SelectDungeonMode = ESelectDungeon.GrassLand;

        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickEnterRockLand()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 게임매니저 모드를 초원지대로 바꾸고 */
        GameManager.GMInstance.SelectDungeonMode = ESelectDungeon.RockLand;

        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickEnterDeathLand()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 게임매니저 모드를 초원지대로 바꾸고 */
        GameManager.GMInstance.SelectDungeonMode = ESelectDungeon.DeathLand;

        SceneManager.LoadScene("PlayScene");
    }


    /** 크리티컬확률 증가 */
    public void OnClickCrticalUp()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 크리티컬 확률 계산 */
        GameManager.GMInstance.SetCriticalPercent(GameManager.GMInstance.GetCriticalPercent() + GameManager.GMInstance.CharCriticalPerUpRate);

        /** 크리티컬 증가확률 레벨 1+ */
        GameManager.GMInstance.CharCriticalPerLevel++;

        CharCriticalPerLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalPerLevel + " 크리티컬 확률 증가";

        /** 능력치 창 크리티컬 확률 초기화 */
        CharCriticalPer.text =(100 * GameManager.GMInstance.GetCriticalPercent()).ToString("F2") + "%";
    }

    /** 크리티컬 데미지 증가 */
    public void OnClickCrticalDamageUp()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 크리티컬 데미지 계산 */
        GameManager.GMInstance.SetCriticaDamage(GameManager.GMInstance.GetCriticalDamage() + GameManager.GMInstance.CharCriticalDamageUpRate);

        /** 크리티컬 데미지 증가 레벨 1+ */
        GameManager.GMInstance.CharCriticalDamageLevel++;

        /** 크리티컬 데미지 레벨 증가 텍스트 초기화 */
        CharCriticalDamageLevelText.text = "Lv" + GameManager.GMInstance.CharCriticalDamageLevel + " 크리티컬 데미지 증가";

        /** 능력치 창 크리티컬 데미지 수치 초기화 */
        CharCriticalDamage.text = (100 * GameManager.GMInstance.GetCriticalDamage()).ToString("F1") + "%";
    }

    /** 체력 증가 */
    public void OnClickMaxHpUp()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 현재 최대체력의 10% 증가 계산 */
        //GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHealth * 0.1f;

        /** 최대체력의 2씩 증가 계산 */
        GameManager.GMInstance.MaxHealth += GameManager.GMInstance.MaxHpLevelUpRate;


        /** 캐릭터 최대체력 증가레벨 1+ */
        GameManager.GMInstance.MaxHpLevel++;

        /** 최대체력증가 레벨 텍스트 초기화 */
        MaxHpLevelText.text = "Lv" + GameManager.GMInstance.MaxHpLevel + " 최대 체력 증가";

        /** 능력치 창 이동속도 표시 초기화 */
        MaxHPText.text = GameManager.GMInstance.MaxHealth.ToString("F0") + " HP";

    }


    /** 이동속도 증가 */
    public void OnClickSpeedUp()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 이동속도 증가 계산 */
        GameManager.GMInstance.PlayerSpeed += GameManager.GMInstance.CharSpeedLevelUpRate;

        /** 이동속도 증가 레벨 1+ */
        GameManager.GMInstance.CharSpeedLevel++;

        /** 이동속도 레벨 텍스트 초기화 */
        CharSpeedLevelText.text = "Lv" + GameManager.GMInstance.CharSpeedLevel + " 이동 속도 증가";

        /** 능력치 창 이동속도 표시 초기화 */
        CharSpeedText.text = (100 * GameManager.GMInstance.PlayerSpeed).ToString("F1") + "%";

    }

    /** 데미지 증가 */
    public void OnClickDamageUp()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 스킬 데미지 증가 레벨 1+ */
        GameManager.GMInstance.SkillDamageLevel++;

        /** 스킬 데미지 증가 계산식 */
        GameManager.GMInstance.SetSkillDamageUp(GameManager.GMInstance.GetSkillDamageUp() + GameManager.GMInstance.SkillDamageUpRate);

        /** 능력치 창 스킬 데미지 표시 초기화 */
        AttackLevelText.text = "Lv" + GameManager.GMInstance.SkillDamageLevel + " 스킬 데미지 증가";

        /** 능력치 창 스킬 데미지 표시 초기화 */
        AttackText.text = (100 * GameManager.GMInstance.GetSkillDamageUp()).ToString("F0") + "%";
    }

    public void OnClickAbilityCheckOpen()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        AbilityCheckPanel.gameObject.SetActive(true);
    }

    public void OnClickAbilityCheckClose()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);
        AbilityCheckPanel.gameObject.SetActive(false);
    }

    public void OnClickOpenInGameStore()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        InGameMoneyPanel.gameObject.SetActive(true);
        EquipmentGachaPanel.gameObject.SetActive(false);
    }

    public void OnClickOpenEquipmentGacha()
    {
        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        InGameMoneyPanel.gameObject.SetActive(false);
        EquipmentGachaPanel.gameObject.SetActive(true);

    }
}

