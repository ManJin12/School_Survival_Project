using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;

public class PlaySceneManager : MonoBehaviour
{
    public float PlayTime;

    void Start()
    {
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
    }

    void Update()
    {
        PlayTime += Time.deltaTime;
        GameManager.GMInstance.PlayTime = PlayTime;
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("DungeonSelect");
    }
}
