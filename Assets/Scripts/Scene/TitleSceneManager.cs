using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;
using System;


public class TitleSceneManager : MonoBehaviour
{
    private void Awake()
    {
        /** ���� �� �κ� */
        GameManager.GMInstance.CurrentScene = Define.ESceneType.TitleScene;
    }

    void Start()
    {
       // Debug.Log(DateTime.Now.ToString());
    }

    public void OnClickGameStart()
    {
        /** ȿ���� ���� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        SceneManager.LoadScene("Lobby");
    }
}
