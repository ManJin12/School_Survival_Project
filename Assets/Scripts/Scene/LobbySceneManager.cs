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

    
    public GameObject[] SelectCharacterPrefabs;


    /** TODO ## LobbyManager.cs 캐릭터 능력치 증가 변수 */
    public int Up_Hp;
    public int Up_Damage;
    public int Up_Defense;

    [Header("---Check---")]
    public Image BGM_Off_Check;
    public Image BGM_On_Check;
    public Image EffectSound_On_Check;
    public Image EffectSound_Off_Check;

    // Start is called before the first frame update
    private void Start()
    {
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
        /** 화면 전환 */
        SceneManager.LoadScene("PlayScene");
    }

    /** 다음 캐릭터 버튼 눌렀을 때 */
    public void OnClickNextCharacter()
    {
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

        /** 배경음 Off */
        GameManager.GMInstance.SoundManagerRef.BGMPlayer.mute = false;

        BGM_On_Check.gameObject.SetActive(true);
        BGM_Off_Check.gameObject.SetActive(false);
    }

    /** 배경음 off 함수 */
    public void OnClickBGMOff()
    {
        GameManager.GMInstance.SoundManagerRef.bIsBGMOn = false;

        /** 효과음 재생 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** 배경음 On */
        GameManager.GMInstance.SoundManagerRef.BGMPlayer.mute = true;

        BGM_On_Check.gameObject.SetActive(false);
        BGM_Off_Check.gameObject.SetActive(true);
    }
}
