using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using My;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject[] SelectCharacterPrefabs;
    public GameObject MyCharacter;

    /** TODO ## 캐릭터 능력치 증가 변수 */
    public int Up_Hp;
    public int Up_Damage;
    public int Up_Defense;

    // Start is called before the first frame update
    private void Start()
    {
        /** 현재 화면 로비씬*/
        GameManager.GMInstance.CurrentScene = Define.ESceneType.LobbyScene;

        /** MyCharacter에 GameManager가 저장하고 있는 캐릭터를 소환 */
        // MyCharacter = Instantiate(SelectCharacterPrefabs[(int)GameManager.GMInstance.CurrentChar]);
        /** MyCharacter크기를 3x3x3으로 바꿔준다. */
        //MyCharacter.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
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


}
