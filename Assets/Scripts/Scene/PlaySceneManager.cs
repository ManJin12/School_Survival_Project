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
    // float fadeImage = 1;
    public bool bIsFirstStart;
    public GameObject GameClearPanel;
    public GameObject GameOverPanel;
    public LevelUp WizardLevelUp;

    void Start()
    {
        /** 스킬 선택 텍스트UI 함수 호출 */
        TextInit();
        GameManager.GMInstance.CurrentScene = Define.ESceneType.PlayScene;
        GameManager.GMInstance.PlaySceneManagerRef = this;

        /** 게임 플레이 변수 초기화 */
        GameManager.GMInstance.PlaySceneInit(false, 0, GameManager.GMInstance.MaxHealth);

        /** 게임 생명 활성화 */
        GameManager.GMInstance.bIsLive = true;

        /** 처음 시작하는지 확인하기 위한 변수 */
        bIsFirstStart = true;
    }

    void Update()
    {
        if (GameManager.GMInstance.bIsLive == false)
        {
            return;
        }

        PlayTime += Time.deltaTime;
        GameManager.GMInstance.PlayTime = PlayTime;

        GameClear();
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
        /** 플레이어 생존 함수 off */
        GameManager.GMInstance.bIsLive = false;

        /** GameOverImage 적용 */
        GameOverImage.gameObject.SetActive(true);

        /** 1초 후 */
        yield return new WaitForSeconds(1.0f);

        /** GameOverPanel On */
        GameOverPanel.SetActive(true);

        StartCoroutine(GameOverTimeRoutine());
        
    }

    IEnumerator GameOverTimeRoutine()
    {
        /** 0.5초 후 */
        yield return new WaitForSeconds(1.0f);

        /** PlayStop함수 호출 */
        GameManager.GMInstance.PlayStop();
    }

    /** 게임 클리어 함수 */
    void GameClear()
    {
        /** 시간이 다 되었다면 */
        if (GameManager.GMInstance.gameTime == GameManager.GMInstance.maxGameTime)
        {
            /** 게임 클리어 판넬 Active On */
            GameClearPanel.SetActive(true);
            /** 게임 시간 멈춤 */
            Time.timeScale = 0;
        }
    }

    /** 로비화면 이동 함수 */
    public void OnClickLobby()
    {
        /** 게임 시간을 원상복귀 */
        Time.timeScale = 1.0f;
        /** 로비 씬으로 전환되기 때문에 다음 입장을 위해 false로 해준다. */
        GameManager.GMInstance.bIsLive = false;

        /** 로비화면 이동 */
        SceneManager.LoadScene("Lobby");
    }

    /** 로비화면 이동 함수 */
    public void OnClickReStart()
    {
        /** 게임 시간을 원상복귀 */
        Time.timeScale = 1.0f;

        /** 플레이 시간 초기화 */
        GameManager.GMInstance.PlayTime = 0.0f;

        /** 플레이어 스피드는 기존의 베이스 이동속도를 가져온다. */
        GameManager.GMInstance.PlayerSpeed = GameManager.GMInstance.GetPlayerBaseSpeed();

        /** 로비 씬으로 전환되기 때문에 다음 입장을 위해 false로 해준다. */
        GameManager.GMInstance.bIsLive = false;

        /** 로비화면 이동 */
        SceneManager.LoadScene("PlayScene");
    }


}
