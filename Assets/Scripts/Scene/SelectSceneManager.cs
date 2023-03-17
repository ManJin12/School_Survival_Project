using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;

public class SelectSceneManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.GMInstance.CurrentScene = Define.ESceneType.DungeonSelectScene;
    }


    public void OnClickLobby_Btn()
    {
        /** 화면 전환 */
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickDungeon_Btn()
    {
        /** 화면 전환 */
        SceneManager.LoadScene("PlayScene");
    }
}
