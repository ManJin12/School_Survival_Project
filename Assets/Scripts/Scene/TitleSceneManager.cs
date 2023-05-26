using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;
using System;


public class TitleSceneManager : MonoBehaviour
{
    public Text StartText;
    WaitForSeconds wait;

    private void Awake()
    {
        /** ÇöÀç ¾À ·Îºñ */
        GameManager.GMInstance.CurrentScene = Define.ESceneType.TitleScene;
    }

    void Start()
    {
        //wait = new WaitForSeconds(1.0f);

        //StartCoroutine(BlinkText());
    }

    /** ÅØ½ºÆ® ±ôºýÀÓ ÇÔ¼ö ³Ê¹« µüµüÇÔ */
    IEnumerator BlinkText()
    {
        while (true)
        {
            StartText.text = "Touch To Start";
            yield return wait;
            StartText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        
    }

    public void OnClickGameStart()
    {
        /** È¿°úÀ½ ½ÇÇà */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        SceneManager.LoadScene("Lobby");
    }
}
