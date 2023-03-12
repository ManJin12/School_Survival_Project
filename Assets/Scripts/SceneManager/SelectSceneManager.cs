using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** 캐릭터 타입 */
public enum CharType
{
    BoyChar,
    GirlChar,
    OldBoyChar,
    YoungChar,
}

public class SelectSceneManager : MonoBehaviour
{

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
