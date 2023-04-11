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
    public Image GameOverImage;
    float fadeImage = 1;

    void Start()
    {
        /** 스킬 선택 텍스트UI 함수 호출 */
        TextInit();
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
        GameManager.GMInstance.PlaySceneManagerRef = this;

        /** 게임 플레이 변수 초기화 */
        GameManager.GMInstance.PlaySceneInit(false, 0, GameManager.GMInstance.MaxHealth);
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
            SkillSelectPaneltext.text = "스킬을 선택해 주세요!";
        }
        else if (GameManager.GMInstance.level != 1)
        {
            SkillSelectPaneltext.text = "축하합니다!\n 레벨 업!";
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        GameManager.GMInstance.bIsLive = false;

        GameOverImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        GameManager.GMInstance.PlayStop();
    }

}
