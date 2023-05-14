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


    /** TODO ## LobbyManager.cs ĳ���� �ɷ�ġ ���� ���� */
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
        /** ȭ�� ��ȯ */
        SceneManager.LoadScene("PlayScene");
    }

    /** ���� ĳ���� ��ư ������ �� */
    public void OnClickNextCharacter()
    {
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

        /** ����� Off */
        GameManager.GMInstance.SoundManagerRef.BGMPlayer.mute = false;

        BGM_On_Check.gameObject.SetActive(true);
        BGM_Off_Check.gameObject.SetActive(false);
    }

    /** ����� off �Լ� */
    public void OnClickBGMOff()
    {
        GameManager.GMInstance.SoundManagerRef.bIsBGMOn = false;

        /** ȿ���� ��� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        /** ����� On */
        GameManager.GMInstance.SoundManagerRef.BGMPlayer.mute = true;

        BGM_On_Check.gameObject.SetActive(false);
        BGM_Off_Check.gameObject.SetActive(true);
    }
}
