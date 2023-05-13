using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using My;
using static Define;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject[] SelectCharacterPrefabs;
    public GameObject MyCharacter;

    /** TODO ## LobbyManager.cs 캐릭터 능력치 증가 변수 */
    public int Up_Hp;
    public int Up_Damage;
    public int Up_Defense;

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

    public void OnClickSelectCharacterBtn()
    {
        /** 화면 전환 */
        SceneManager.LoadScene("DungeonSelect");
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

}
