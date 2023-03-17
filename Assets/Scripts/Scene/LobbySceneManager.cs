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

    /** TODO ## ĳ���� �ɷ�ġ ���� ���� */
    public int Up_Hp;
    public int Up_Damage;
    public int Up_Defense;

    // Start is called before the first frame update
    private void Start()
    {
        /** ���� ȭ�� �κ��*/
        GameManager.GMInstance.CurrentScene = Define.ESceneType.LobbyScene;

        /** MyCharacter�� GameManager�� �����ϰ� �ִ� ĳ���͸� ��ȯ */
        // MyCharacter = Instantiate(SelectCharacterPrefabs[(int)GameManager.GMInstance.CurrentChar]);
        /** MyCharacterũ�⸦ 3x3x3���� �ٲ��ش�. */
        //MyCharacter.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
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

    public void OnClickSelectCharacterBtn()
    {
        /** ȭ�� ��ȯ */
        SceneManager.LoadScene("DungeonSelect");
    }

    public void OnClickGameplayBtn()
    {
        /** ȭ�� ��ȯ */
        SceneManager.LoadScene("PlayScene");
    }


}
