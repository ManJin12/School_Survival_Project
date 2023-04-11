using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;

public class PlaySceneManager : MonoBehaviour
{
    public float PlayTime;
    public Text SkillSelectPaneltext;

    void Start()
    {
        TextInit();
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
        GameManager.GMInstance.PlaySceneManagerRef = this;
    }

    void Update()
    {
        if (GameManager.GMInstance.bIsLive == false)
        {
            return;
        }

        PlayTime += Time.deltaTime;
        GameManager.GMInstance.PlayTime = PlayTime;
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("DungeonSelect");
    }
    public void TextInit()
    {
        if (GameManager.GMInstance.level == 1)
        {
            SkillSelectPaneltext.text = "��ų�� ������ �ּ���!";
        }
        else if (GameManager.GMInstance.level != 1)
        {
            SkillSelectPaneltext.text = "�����մϴ�!\n ���� ��!";
        }
    }


}
