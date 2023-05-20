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
        /** 현재 씬 로비 */
        GameManager.GMInstance.CurrentScene = Define.ESceneType.TitleScene;
    }

    void Start()
    {
       // Debug.Log(DateTime.Now.ToString());
    }

    public void OnClickGameStart()
    {
        /** 효과음 실행 */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        SceneManager.LoadScene("Lobby");
    }
}
