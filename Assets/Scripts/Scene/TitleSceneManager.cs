using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;

public class TitleSceneManager : MonoBehaviour
{
    private void Awake()
    {
        /** ���� �� �κ� */
        GameManager.GMInstance.CurrentScene = Define.ESceneType.TitleScene;
    }

    public void OnClickGameStart()
    {
        /** ȿ���� ���� */
        GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.Select);

        SceneManager.LoadScene("Lobby");
    }
}
