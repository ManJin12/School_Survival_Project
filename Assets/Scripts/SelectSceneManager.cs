using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** ĳ���� Ÿ�� */
public enum CharType
{
    BoyChar,
    GirlChar,
    OldBoyChar,
    YoungChar,
}

public class SelectSceneManager : MonoBehaviour
{

    public void OnClickStart_Btn()
    {
        /** ȭ�� ��ȯ */
        SceneManager.LoadScene("PlayScene");
    }
}
